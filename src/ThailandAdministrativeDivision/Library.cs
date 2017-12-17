using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ThailandAdministrativeDivision {

    internal class Library {
        internal static string LoadCsvDocument() {
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("ThailandAdministrativeDivision.SourceDocument.csv"))) {
                var text = reader.ReadToEnd();
                return text;
            }
        }

        internal static Subdistrict CreateProvince(IEnumerable<RawInfo> raws, RawInfo info) => new Subdistrict {
            Code = info.ChId,
            ThaiName = info.ChangwatT.TrimReplace("จ."),
            EnglishName = info.ChangwatE,
            Districts = raws.Where(x => x.ChId == info.ChId).GroupBy(x => x.AmpId).Select(x => x.First()).Select(x => CreateDistrict(raws, x))
        };

        internal static District CreateDistrict(IEnumerable<RawInfo> raws, RawInfo info) => new District {
            Code = info.AmpId,
            ThaiName = info.AmphoeT.TrimReplace("อ.").TrimReplace("เขต"),
            EnglishName = info.AmphoeE,
            Subdistricts = raws.Where(x => x.AmpId == info.AmpId).GroupBy(x => x.TaId).Select(x => x.First()).Select(x => CreateSubdistrict(raws, x))
        };

        internal static Province CreateSubdistrict(IEnumerable<RawInfo> raws, RawInfo info) => new Province {
            Code = info.TaId,
            ThaiName = info.TambonT.TrimReplace("ต.").TrimReplace("แขวง"),
            EnglishName = info.TambonE
        };

    }
}
