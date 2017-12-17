using System.Collections.Generic;

namespace ThailandAdministrativeDivision {
    public class Subdistrict {
        internal Subdistrict() { }
        public string Code { set; get; }
        public string ThaiName { set; get; }
        public string EnglishName { set; get; }
        public IEnumerable<District> Districts { set; get; }
    }
}
