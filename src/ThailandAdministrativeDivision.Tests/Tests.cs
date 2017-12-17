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
            Assert.Equal(928, count);
        }

        [Fact]
        public void Tambons() {
            var division = Division.Load();
            var count = division.Tambons.Count();
            Assert.Equal(7364, count);
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