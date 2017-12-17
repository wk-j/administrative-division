using System.Collections.Generic;
using System.Linq;

namespace ThailandAdministrativeDivision {
    public class Division {

        private Division() { }

        static Division division = null;

        public IEnumerable<Province> Subdistricts { set; get; }
        public IEnumerable<District> Districts { set; get; }
        public IEnumerable<Subdistrict> Provinces { set; get; }

        public static Division Load() {
            if (division == null) {
                var text = Library.LoadCsvDocument();
                var raws = CsvParser.ParseText(text).ToArray();
                var provinces = raws.GroupBy(x => x.ChId).Select(x => x.First()).Select(x => Library.CreateProvince(raws, x));
                var districts = raws.GroupBy(x => x.AmpId).Select(x => x.First()).Select(x => Library.CreateDistrict(raws, x));
                var subdistricts = raws.GroupBy(x => x.TaId).Select(x => x.First()).Select(x => Library.CreateSubdistrict(raws, x));

                division = new Division
                {
                    Districts = districts,
                    Subdistricts = subdistricts,
                    Provinces = provinces
                };
            }
            return division;
        }
    }
}
