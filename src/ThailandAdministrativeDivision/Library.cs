using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ThailandAdministrativeDivision {
    internal class RawInfo {
        public string AdLevel { set; get; }
        public string TaId { set; get; }
        public string TambonT { set; get; }
        public string TambonE { set; get; }
        public string AmpId { set; get; }
        public string AmphoeT { set; get; }
        public string AmphoeE { set; get; }
        public string ChId { set; get; }
        public string ChangwatT { set; get; }
        public string ChangwatE { set; get; }
        public string LAT { set; get; }
        public string LONG { set; get; }
    }

    public class Tambon {
        internal Tambon() { }
        public string Id { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public string Latitude { set; get; }
        public string Longitude { set; get; }
    }

    public class Amphoe {
        internal Amphoe() { }
        public string Id { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public IEnumerable<Tambon> Tambons { set; get; }
    }

    public class Changwat {
        internal Changwat() { }
        public string Id { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public IEnumerable<Amphoe> Amphoes { set; get; }
    }

    internal class CsvParser {
        private static RawInfo ParseLine(string line) {
            var data = line.Split(',').Select(x => x.Trim()).ToArray();
            if (data.Length == 12) {
                return new RawInfo {
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

    internal class Library {
        internal static string LoadCsvDocument() {
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("ThailandAdministrativeDivision.source-document.csv"))) {
                var text = reader.ReadToEnd();
                return text;
            }
        }
    }

    internal static class StringExtension {
        public static string TrimReplace(this string input, string replace) => input.Replace(replace, "").Trim();
    }


    public class Division {

        private Division() { }

        private static IEnumerable<RawInfo> raws = Enumerable.Empty<RawInfo>();

        public IEnumerable<Tambon> Tambons { set; get; }
        public IEnumerable<Amphoe> Ampoes { set; get; }
        public IEnumerable<Changwat> Changwats { set; get; }

        public static Division Load() {

            if (!raws.Any()) {
                var text = Library.LoadCsvDocument();
                raws = CsvParser.ParseText(text);
            }

            Changwat createChangwat(RawInfo info) => new Changwat {
                Id = info.ChId,
                ThaiName = info.ChangwatT.TrimReplace("จ."),
                EnglishName = info.ChangwatE,
                Amphoes = raws.Where(x => x.ChId == info.ChId).Select(createAmphoe)
            };

            Amphoe createAmphoe(RawInfo info) => new Amphoe {
                Id = info.AmpId,
                ThaiName = info.AmphoeT.TrimReplace("อ."),
                EnglishName = info.AmphoeE,
                Tambons = raws.Where(x => x.AmpId == info.AmpId).Select(createTambon)
            };

            Tambon createTambon(RawInfo info) => new Tambon {
                Id = info.TaId,
                ThaiName = info.TambonT.TrimReplace("ต."),
                EnglishName = info.TambonE
            };

            var changwats = raws.GroupBy(x => x.ChId).Select(x => x.First()).Select(createChangwat);
            var amphos = raws.GroupBy(x => x.AmphoeE).Select(x => x.First()).Select(createAmphoe);
            var tambons = raws.GroupBy(x => x.TaId).Select(x => x.First()).Select(createTambon);

            return new Division {
                Ampoes = amphos,
                Tambons = tambons,
                Changwats = changwats
            };
        }
    }
}
