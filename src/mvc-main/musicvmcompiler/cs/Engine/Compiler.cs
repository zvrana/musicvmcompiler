using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using musicvmcompiler.Engine.Instructions;

namespace musicvmcompiler.Engine
{
    public class Compiler
    {
        private readonly List<Instruction> instructions = new List<Instruction>();

        private readonly ConstantTable<float, ConstantReference<float>> floatConsts =
            new ConstantTable<float, ConstantReference<float>>((table, value) => new ConstantReference<float>(table, value));

        private readonly ConstantTable<int, ConstantReference<int>> intConsts =
            new ConstantTable<int, ConstantReference<int>>((table, value) => new ConstantReference<int>(table, value));

        private readonly ConstantTable<string, TickConstantReference> tickConsts =
            new ConstantTable<string, TickConstantReference>((table, value) => new TickConstantReference(table, value), new TickConstantComparer());

        public void Compile(IEnumerable<string> lines)
        {
            var emitters = new Dictionary<string, Action<IList<string>>>
            {
                {"add", EmitAdd},
                {"bessel_filter", EmitBesselFilter},
                {"clear", EmitClear},
                {"distort", EmitDistort},
                {"lfo", EmitLfo},
                {"makesin", EmitMakesin},
                {"minbuf", EmitMinbuf},
                {"multiply", EmitMultiply},
                {"pow3", EmitPow3},
                {"repeat_envelope", EmitRepeatEnvelope},
                {"resonant_filter", EmitResonantFilter},
                {"reverb", EmitReverb}
            };

            foreach (var inputLine in lines.Select(l => (l ?? "").Trim()))
            {
                var line = inputLine.Split(new[] {"//"}, StringSplitOptions.None).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(line)) continue;

                instructions.Add(new Comment(line));

                var match = Regex.Match(line, @"(?<fname>\w+)\((?<args>[^\)]*)\);");
                var fname = match.Groups["fname"].Value;
                var args = match.Groups["args"].Value.Split(',').Select(v => v.Trim()).ToList();

                emitters[fname](args);
            }
        }

        public List<Instruction> Instructions
        {
            get { return instructions; }
        }

        public ConstantTable<float, ConstantReference<float>> FloatConsts
        {
            get { return floatConsts; }
        }

        public ConstantTable<int, ConstantReference<int>> IntConsts
        {
            get { return intConsts; }
        }

        public ConstantTable<string, TickConstantReference> TickConsts
        {
            get { return tickConsts; }
        }

        private int ParseBufName(string name)
        {
            if (!name.StartsWith("buf"))
            {
                throw new CompileException("not a buffer name");
            }
            var idx = name.Remove(0, 3);
            return int.Parse(idx);
        }

        private ConstantReference<float> ParseFloatConst(string s)
        {
            s = s.TrimEnd('f');
            var f = float.Parse(s, CultureInfo.InvariantCulture);

            return floatConsts.Introduce(f);
        }

        private int ParseNote(string s)
        {
            return int.Parse(s);
        }

        private Either<ConstantReference<int>, TickConstantReference> ParseIntConst(string s)
        {
            int i;
            return int.TryParse(s, out i)
                ? new Either<ConstantReference<int>, TickConstantReference>(intConsts.Introduce(i))
                : new Either<ConstantReference<int>, TickConstantReference>(tickConsts.Introduce(s));
        }

        private static Instruction CreateSetIntegerConstInstruction(int target,
            Either<ConstantReference<int>, TickConstantReference> idx)
        {
            return idx.IsFirst ? (Instruction) new OpSetIConst(target, idx.First) : new OpSetTConst(target, idx.Second);
        }

        private void EmitAdd(IList<string> obj)
        {
            var bufa = ParseBufName(obj[0]);
            var bufb = ParseBufName(obj[1]);

            instructions.Add(new OpSetBuf(0, bufa));
            instructions.Add(new OpSetBuf(1, bufb));
            instructions.Add(new OpAdd());
        }

        private void EmitBesselFilter(IList<string> obj)
        {
            var outbuf = ParseBufName(obj[0]);
            var inbuf = ParseBufName(obj[1]);

            var gain = ParseFloatConst(obj[0]);
            var g1 = ParseFloatConst(obj[1]);
            var g2 = ParseFloatConst(obj[2]);
            var g3 = ParseFloatConst(obj[3]);

            instructions.Add(new OpSetBuf(0, outbuf));
            instructions.Add(new OpSetBuf(1, inbuf));
            instructions.Add(new OpSetFConst(0, gain));
            instructions.Add(new OpSetFConst(0, g1));
            instructions.Add(new OpSetFConst(1, g2));
            instructions.Add(new OpSetFConst(2, g3));
            instructions.Add(new OpBesselFilter());
        }

        private void EmitClear(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(new OpClear());
        }

        private void EmitDistort(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);
            var amount = ParseFloatConst(obj[1]);
            var outgain = ParseFloatConst(obj[2]);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(new OpSetFConst(0, amount));
            instructions.Add(new OpSetFConst(1, outgain));
            instructions.Add(new OpDistort());
        }

        private void EmitLfo(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);
            var amin = ParseFloatConst(obj[1]);
            var amax = ParseFloatConst(obj[2]);
            var period = ParseFloatConst(obj[3]);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(new OpSetFConst(0, amin));
            instructions.Add(new OpSetFConst(1, amax));
            instructions.Add(new OpSetFConst(2, period));
            instructions.Add(new OpLfo());
        }

        private void EmitMakesin(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);
            var note = ParseNote(obj[1]);
            var detune = obj.Count > 2 ? ParseFloatConst(obj[2]) : floatConsts.Introduce(0);
            var amax = obj.Count > 3 ? ParseFloatConst(obj[3]) : floatConsts.Introduce(1);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(new OpSetNote(note));
            instructions.Add(new OpSetFConst(1, detune));
            instructions.Add(new OpSetFConst(2, amax));
            instructions.Add(new OpMakesin());

        }

        private void EmitMinbuf(IList<string> obj)
        {
            var bufa = ParseBufName(obj[0]);
            var bufb = ParseBufName(obj[1]);
            var bufc = ParseBufName(obj[2]);

            instructions.Add(new OpSetBuf(0, bufa));
            instructions.Add(new OpSetBuf(1, bufb));
            instructions.Add(new OpSetBuf(2, bufc));
            instructions.Add(new OpMinbuf());
        }

        private void EmitMultiply(IList<string> obj)
        {
            var bufa = ParseBufName(obj[0]);
            var bufb = ParseBufName(obj[1]);
            var bufc = ParseBufName(obj[2]);

            instructions.Add(new OpSetBuf(0, bufa));
            instructions.Add(new OpSetBuf(1, bufb));
            instructions.Add(new OpSetBuf(2, bufc));
            instructions.Add(new OpMultiply());
        }

        private void EmitPow3(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(new OpPow3());
        }

        private void EmitRepeatEnvelope(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);
            var amax = ParseFloatConst(obj[1]);
            var startPos = ParseIntConst(obj[2]);
            var attackLength = ParseIntConst(obj[3]);
            var decayLength = ParseIntConst(obj[4]);
            var attackAlpha = ParseFloatConst(obj[5]);
            var decayAlpha = ParseFloatConst(obj[6]);
            var count = ParseIntConst(obj[7]);
            var length = ParseIntConst(obj[8]);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(new OpSetFConst(0, amax));

            instructions.Add(CreateSetIntegerConstInstruction(1, attackLength));
            instructions.Add(CreateSetIntegerConstInstruction(2, decayLength));
            instructions.Add(new OpSetFConst(1, attackAlpha));
            instructions.Add(new OpSetFConst(2, decayAlpha));
            instructions.Add(CreateSetIntegerConstInstruction(3, count));
            instructions.Add(CreateSetIntegerConstInstruction(4, length));
            instructions.Add(CreateSetIntegerConstInstruction(0, startPos));
            instructions.Add(new OpRepeatEnvelope());
        }

        private void EmitResonantFilter(IList<string> obj)
        {
            var outbuf = ParseBufName(obj[0]);
            var inbuf = ParseBufName(obj[1]);
            var fbuf = ParseBufName(obj[2]);
            var qbuf = ParseBufName(obj[3]);

            instructions.Add(new OpSetBuf(0, outbuf));
            instructions.Add(new OpSetBuf(1, inbuf));
            instructions.Add(new OpSetBuf(1, fbuf));
            instructions.Add(new OpSetBuf(1, qbuf));
            instructions.Add(new OpResonantFilter());
        }

        private void EmitReverb(IList<string> obj)
        {
            var buf = ParseBufName(obj[0]);
            var length = ParseIntConst(obj[1]);
            var amount = ParseFloatConst(obj[1]);

            instructions.Add(new OpSetBuf(0, buf));
            instructions.Add(CreateSetIntegerConstInstruction(0, length));
            instructions.Add(new OpSetFConst(0, amount));
            instructions.Add(new OpReverb());
        }
    }
}
