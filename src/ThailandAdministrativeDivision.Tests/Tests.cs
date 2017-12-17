using System;
using Xunit;
using System.Linq;

namespace ThailandAdministrativeDivision.Tests {
    public class Tests {
        [Fact]
        public void Provinces() {
            var division = Division.Load();
            var count = division.Provinces.Count();
            Assert.Equal(77, count);
        }

        [Fact]
        public void Districts() {
            var division = Division.Load();
            var count = division.Districts.Count();
            var expect = 928;
            Assert.Equal(expect, count);

            var c2 = division.Provinces.SelectMany(x => x.Districts).Count();
            Assert.Equal(expect, c2);
        }

        [Fact]
        public void Subdistricts() {
            var division = Division.Load();
            var count = division.Subdistricts.Count();
            var expect = 7364;
            Assert.Equal(expect, count);

            var c2 = division.Provinces.SelectMany(x => x.Districts).SelectMany(x => x.Subdistricts).Count();
            Assert.Equal(expect, c2);
        }

        [Fact]
        public void Sisaket() {
            var division = Division.Load();
            var sisaket = division.Provinces.First(x => x.ThaiName == "ศรีสะเกษ");
            var match = sisaket.Districts.Any(x => x.ThaiName == "ขุขันธ์");
            var notMatch = !sisaket.Districts.Any(x => x.ThaiName == "สตึก");
            Assert.True(match);
            Assert.True(notMatch);
        }
    }
}