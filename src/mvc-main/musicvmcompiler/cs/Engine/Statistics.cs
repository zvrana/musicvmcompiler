using System;
using System.Collections.Generic;
using System.Linq;

namespace musicvmcompiler.Engine
{
    public class Statistics
    {
        private readonly Frequencies unoptimizedByteFrequencies;
        private readonly Frequencies optimizedByteFrequencies;
        private byte[] allBytes; 

        public Statistics(Compiler fromCompiler)
        {
            unoptimizedByteFrequencies =
                new Frequencies(fromCompiler.Instructions.SelectMany(i => i.ToBytes()).ToArray());
            optimizedByteFrequencies =
                new Frequencies(fromCompiler.Instructions.Where(i => i.Enabled).SelectMany(i => i.ToBytes()).ToArray());

            allBytes = unoptimizedByteFrequencies.Counts.Keys.ToArray();
            Array.Sort(allBytes, CompareOptimizedFrequencies);
        }

        private int CompareOptimizedFrequencies(byte a, byte b)
        {
            var ia = optimizedByteFrequencies.Counts.ContainsKey(a) ? -optimizedByteFrequencies.Counts[a] : a + 1;
            var ib = optimizedByteFrequencies.Counts.ContainsKey(b) ? -optimizedByteFrequencies.Counts[b] : b + 1;

            return Comparer<int>.Default.Compare(ia, ib);
        }

        public Frequencies UnoptimizedByteFrequencies
        {
            get { return unoptimizedByteFrequencies; }
        }

        public Frequencies OptimizedByteFrequencies
        {
            get { return optimizedByteFrequencies; }
        }

        public byte[] AllBytes
        {
            get { return allBytes; }
        }
    }

    public class Frequencies
    {
        private readonly Dictionary<byte, int> counts = new Dictionary<byte, int>();
        private int max;

        public Frequencies(byte[] bytes)
        {
            foreach (var b in bytes)
            {
                counts[b]=0;
            }
            foreach (var b in bytes)
            {
                counts[b]++;
            }

            max = counts.Values.Max();
        }

        public int Max
        {
            get { return max; }
        }

        public Dictionary<byte, int> Counts
        {
            get { return counts; }
        }

        public Dictionary<byte, double> ToFrequencies()
        {
            var d = (double)max;

            return counts.ToDictionary(entry => entry.Key, entry => entry.Value/d);
        }

        public byte[] GetMostFrequentBytes()
        {
            return counts.Keys.OrderBy(b => -(counts[b] * 256 + b)).ToArray();
        }
    }
}
