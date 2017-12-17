using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;

namespace ThailandAdministrativeDivision.Benchmark {
    public class Test {
        [Benchmark]
        public void LoadChangwats() => Division.Load().Changwats.ToList();

        [Benchmark]
        public void LoadAmphoes() => Division.Load().Changwats.SelectMany(x => x.Amphoes).ToList();

        [Benchmark]
        public void LoadTambons() => Division.Load().Changwats.SelectMany(x => x.Amphoes).SelectMany(x => x.Tambons).ToList();
    }

    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<Test>();
        }
    }
}
