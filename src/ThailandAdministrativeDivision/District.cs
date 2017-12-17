using System.Collections.Generic;

namespace ThailandAdministrativeDivision {
    public class District {
        internal District() { }
        public string Code { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public IEnumerable<Province> Subdistricts { set; get; }
    }
}
