using System.Collections.Generic;
using System.Linq;

namespace ThailandAdministrativeDivision {
    internal class CsvParser {
        private static RawInfo ParseLine(string line) {
            var data = line.Split(',').Select(x => x.Trim()).ToArray();
            if (data.Length == 12) {
                return new RawInfo
                {
                    AdLevel = data[0],
                    TaId = data[1],
                    TambonT = data[2],
                    TambonE = data[3],
                    AmpId = data[4],
                    AmphoeT = data[5],
                    AmphoeE = data[6],
                    ChId = data[7],
                    ChangwatT = data[8],
                    ChangwatE = data[9],
                    LAT = data[10],
                    LONG = data[11]
                };
            }
            return null;
        }

        public static IEnumerable<RawInfo> ParseText(string text) =>
            text.Split('\n').Skip(1).Select(ParseLine).Where(x => x != null);

    }
}
