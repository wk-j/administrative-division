using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;

namespace ThailandAdministrativeDivision.Benchmark {
    public class Test {
        [Benchmark]
        public void LoadProvinces() => Division.Load().Provinces.ToList();

        [Benchmark]
        public void LoadDistricts() => Division.Load().Provinces.SelectMany(x => x.Districts).ToList();

        [Benchmark]
        public void LoadSubdistricts() => Division.Load().Provinces.SelectMany(x => x.Districts).SelectMany(x => x.Subdistricts).ToList();
    }

    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<Test>();
        }
    }
}