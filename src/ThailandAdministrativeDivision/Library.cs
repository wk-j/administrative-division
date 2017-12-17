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

    public class Province {
        internal Province() { }
        public string Code { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public string Latitude { set; get; }
        public string Longitude { set; get; }
    }

    public class District {
        internal District() { }
        public string Code { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public IEnumerable<Province> Subdistricts { set; get; }
    }

    public class Subdistrict {
        internal Subdistrict() { }
        public string Code { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public IEnumerable<District> Districts { set; get; }
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

        public IEnumerable<Province> Subdistricts { set; get; }
        public IEnumerable<District> Districts { set; get; }
        public IEnumerable<Subdistrict> Provinces { set; get; }

        public static Division Load() {

            if (!raws.Any()) {
                var text = Library.LoadCsvDocument();
                raws = CsvParser.ParseText(text).ToArray();
            }

            Subdistrict createProvince(RawInfo info) => new Subdistrict {
                Code = info.ChId,
                ThaiName = info.ChangwatT.TrimReplace("จ."),
                EnglishName = info.ChangwatE,
                Districts = raws.Where(x => x.ChId == info.ChId).GroupBy(x => x.AmpId).Select(x => x.First()).Select(createDistrict)
            };

            District createDistrict(RawInfo info) => new District {
                Code = info.AmpId,
                ThaiName = info.AmphoeT.TrimReplace("อ.").TrimReplace("เขต"),
                EnglishName = info.AmphoeE,
                Subdistricts = raws.Where(x => x.AmpId == info.AmpId).GroupBy(x => x.TaId).Select(x => x.First()).Select(createSubdistrict)
            };

            Province createSubdistrict(RawInfo info) => new Province {
                Code = info.TaId,
                ThaiName = info.TambonT.TrimReplace("ต.").TrimReplace("แขวง"),
                EnglishName = info.TambonE
            };

            var provinces = raws.GroupBy(x => x.ChId).Select(x => x.First()).Select(createProvince);
            var districts = raws.GroupBy(x => x.AmpId).Select(x => x.First()).Select(createDistrict);
            var subdistricts = raws.GroupBy(x => x.TaId).Select(x => x.First()).Select(createSubdistrict);

            return new Division {
                Districts = districts,
                Subdistricts = subdistricts,
                Provinces = provinces
            };
        }
    }
}
