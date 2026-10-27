using System.Globalization;
using System.Text.RegularExpressions;

namespace RealEstate_Dapper_UI.Helpers
{
    /// <summary>
    /// Presentation-only helpers used by the Razor views (formatting, safe excerpts,
    /// slugs for links). No business rules live here.
    /// </summary>
    public static class ViewFormat
    {
        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        /// <summary>Formats a price with thousands separators, keeping decimals only when present.</summary>
        public static string Money(decimal value) =>
            value == decimal.Truncate(value) ? value.ToString("#,0", Inv) : value.ToString("#,0.00", Inv);

        /// <summary>Same algorithm as PropertyController.CreateSlug, so links resolve without a redirect.</summary>
        public static string Slug(string? title)
        {
            if (string.IsNullOrWhiteSpace(title)) return "";
            var s = title.ToLowerInvariant().Trim();
            s = Regex.Replace(s, @"[^a-z0-9\s-]", "");
            s = Regex.Replace(s, @"\s+", " ").Trim();
            s = s.Replace(" ", "-");
            s = Regex.Replace(s, "-{2,}", "-");
            return s;
        }

        /// <summary>URL of the public property page: /property/{slug}/{id}.</summary>
        public static string PropertyUrl(string? slugUrl, string? title, int id)
        {
            var slug = string.IsNullOrWhiteSpace(slugUrl) ? Slug(title) : slugUrl;
            if (string.IsNullOrWhiteSpace(slug)) slug = "listing";
            return $"/property/{Uri.EscapeDataString(slug)}/{id}";
        }

        /// <summary>Safe excerpt (the original used Substring(0,100), which throws on short text).</summary>
        public static string Excerpt(string? text, int max = 100)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            text = text.Trim();
            return text.Length <= max ? text : text[..max].TrimEnd() + "…";
        }

        /// <summary>Statistics endpoints return raw JSON (e.g. "\"Istanbul\"" or "12.5"); show the bare value.</summary>
        public static string Stat(object? raw)
        {
            var s = raw?.ToString()?.Trim();
            if (string.IsNullOrEmpty(s) || s == "null") return "—";
            if (s.Length >= 2 && s[0] == '"' && s[^1] == '"') s = s[1..^1];
            return string.IsNullOrWhiteSpace(s) ? "—" : s;
        }

        public static string Initials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "RE";
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var a = char.ToUpperInvariant(parts[0][0]);
            return parts.Length > 1 ? $"{a}{char.ToUpperInvariant(parts[^1][0])}" : a.ToString();
        }

        public static string Date(DateTime value, string format = "dd MMM yyyy") => value.ToString(format, Inv);
    }
}
