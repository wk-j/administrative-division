namespace ThailandAdministrativeDivision {
    internal static class StringExtension {
        public static string TrimReplace(this string input, string replace) => input.Replace(replace, "").Trim();
    }
}
