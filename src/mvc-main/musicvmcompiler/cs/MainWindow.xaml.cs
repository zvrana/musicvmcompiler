using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using musicvmcompiler.Engine;
using musicvmcompiler.Engine.Instructions;
using musicvmcompiler.Model;

namespace musicvmcompiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        MainWindowModel Model { get; set; }

        private CompilerSettings compilerSettings = CompilerSettings.Default;

        public MainWindow()
        {
            Model = new MainWindowModel();

            Model.Input = @"
    makesin(buf1, 38, 0.0f,1.);
    makesin(buf2, 38, 1.5f,1.);
    minbuf(buf3, buf1, buf2);
    makesin(buf3, 50, 0.0f,.5);
    pow3(buf3);
    
    repeat_envelope(buf4,1., T_8TH* 0,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 1,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 2,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 3,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 4,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 5,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 6,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH* 7,100,T_4TH*8,.25,.75,100,T_8TH);
    
    repeat_envelope(buf4,.75,T_8TH* 8,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH*10,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH*12,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH*13,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH*14,100,T_4TH*8,.25,.75,100,T_8TH);
    repeat_envelope(buf4,.5, T_8TH*15,100,T_4TH*8,.25,.75,100,T_8TH);

    multiply(buf5,buf3,buf4);

    makesin(buf6, 41, 0.0f);
    makesin(buf7, 41, 1.5f);
    minbuf(buf8, buf6, buf7);
    makesin(buf8, 53, 0.0f,.5);
    pow3(buf8);

    repeat_envelope(buf9, .5, T_8TH*9,100,T_4TH*8,.25,.75,100,T_8TH);

    multiply(buf10,buf8,buf9);

    makesin(buf11, 43, 0.0f);
    makesin(buf12, 43, 1.5f);
    minbuf(buf13, buf11, buf12);
    makesin(buf13, 55, 0.0f,.5);
    pow3(buf13);

    repeat_envelope(buf14, .5, T_8TH*11,100,T_4TH*8,.25,.75,100,T_8TH);

    multiply(buf14,buf13,buf14);

    add(buf15,buf5);
    add(buf15,buf10);
    add(buf15,buf14);

    distort(buf15,5,.5);
";

            InitializeComponent();

            DataContext = Model;
        }

        private void CompileOnClick(object sender, RoutedEventArgs e)
        {
            var start = Environment.TickCount;
            var compiler = new Compiler();

            compiler.Compile(Model.Input.Split(new[] {Environment.NewLine}, StringSplitOptions.None));

            var optimizer = new Optimizer();
            optimizer.Optimize(compiler.Instructions);

            Model.ParameterSlots = string.Join(Environment.NewLine,
                compilerSettings.ParameterSlots.Select(
                    entry => string.Format("{0}{1}", entry.Key.PadRight(30, '.'), entry.Value)));
         
            Model.Opcodes = string.Join(Environment.NewLine,
                compilerSettings.Opcodes.Select(
                    entry => string.Format("{0}{1}", entry.Key.PadRight(30, '.'), entry.Value)));
         
            Model.Output = compiler.Instructions.Select(WrapInstruction).ToList();
            
            Model.FloatConsts = string.Join(Environment.NewLine,
                compiler.FloatConsts.Values.Select(v => v.ToString(CultureInfo.InvariantCulture)));
            
            Model.IntConsts = string.Join(Environment.NewLine,
                compiler.IntConsts.Values.Select(v => v.ToString(CultureInfo.InvariantCulture)));
            
            Model.TickConsts = string.Join(Environment.NewLine,
                compiler.TickConsts.Values.Select(v => v.ToString(CultureInfo.InvariantCulture)));
            
            Model.UnoptimizedBytes = compiler.Instructions.Sum(i => i.ToBytes().Length);

            Model.OptimizedBytes = compiler.Instructions.Where(i => i.Enabled).Sum(i => i.ToBytes().Length);

            var statistics = new Statistics(compiler);

            var optimizedByteFrequencies = statistics.OptimizedByteFrequencies.ToFrequencies();
            var unoptimizedByteFrequencies = statistics.UnoptimizedByteFrequencies.ToFrequencies();

            Model.Statistics =
                statistics
                .AllBytes
                .Select(b => new StatisticsModel(b, optimizedByteFrequencies[b], unoptimizedByteFrequencies[b]))
                .ToList();

            var end = Environment.TickCount;
            Model.CompileTime = end - start;
        }

        private InstructionModel WrapInstruction(Instruction instruction)
        {
            var foreground = Brushes.Black;
            if (!instruction.Enabled)
            {
                foreground = Brushes.LightGray;
            }
            if (instruction is Comment)
            {
                foreground = Brushes.Green;
            }
            return new InstructionModel()
            {
                Foreground = foreground,
                Text = instruction.ToListing()
            };            
        }
    }
}
