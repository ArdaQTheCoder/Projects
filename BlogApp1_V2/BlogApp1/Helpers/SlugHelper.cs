namespace BlogApp1.Helpers
{
    public static class SlugHelper
    {
        public static string ToSlug(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Küçük harfe dönüştürme
            text = text.ToLower();

            // Türkçe karakterleri engelleme (isteğe bağlı)
            text = text.Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u")
                        .Replace("ş", "s").Replace("ç", "c").Replace("ö", "o").Replace("ü", "u");

            // Boşlukları tire ile değiştiriyoruz
            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "-");

            // Sadece alfanümerik karakterleri bırakıyoruz
            text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-z0-9\-]", "");

            // Başında ve sonunda tire varsa, onları kaldırıyoruz
            text = text.Trim('-');

            return text;
        }
    }
}
