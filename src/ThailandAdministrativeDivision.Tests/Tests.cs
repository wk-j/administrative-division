using System;
using Xunit;
using System.Linq;

namespace ThailandAdministrativeDivision.Tests {
    public class Tests {
        [Fact]
        public void Changwats() {
            var division = Division.Load();
            var count = division.Changwats.Count();
            Assert.Equal(77, count);
        }

        [Fact]
        public void Amphoes() {
            var division = Division.Load();
            var count = division.Ampoes.Count();
            var expect = 928;
            Assert.Equal(expect, count);

            var c2 = division.Changwats.SelectMany(x => x.Amphoes).Count();
            Assert.Equal(expect, c2);
        }

        [Fact]
        public void Tambons() {
            var division = Division.Load();
            var count = division.Tambons.Count();
            var expect = 7364;
            Assert.Equal(expect, count);

            var c2 = division.Changwats.SelectMany(x => x.Amphoes).SelectMany(x => x.Tambons).Count();
            Assert.Equal(expect, c2);
        }

        [Fact]
        public void Sisaket() {
            var division = Division.Load();
            var sisaket = division.Changwats.First(x => x.ThaiName == "ศรีสะเกษ");
            var match = sisaket.Amphoes.Any(x => x.ThaiName == "ขุขันธ์");
            var notMatch = !sisaket.Amphoes.Any(x => x.ThaiName == "สตึก");
            Assert.True(match);
            Assert.True(notMatch);
        }
    }
}