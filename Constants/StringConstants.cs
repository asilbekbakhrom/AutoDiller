namespace bot.Constants;

public static class StringConstants
{
    public static Dictionary<string, string> Menu => new()
    {
        { "bm", "🏠 Bosh Menu" },
    };   
    public static Dictionary<string, string> LanguageNames => new()
    {
        { "uz", "O'zbekcha" },
        { "ru", "Русский" },
        { "en", "English" },
    };
    public static Dictionary<string, string> CarNames => new()
    {
        { "ta", "Tesla" },
        { "hi", "Hyundai" },
        { "ka", "KIA" },
        { "ct", "Chevrolet" },
        { "bw", "BMW"},
    };
    public static Dictionary<string, string> FunctionsNames => new()
    {
        { "td", "Test Drive dan o'tish" },
        { "soa", "Shartnoma online ariza" },
        { "at", "Avtomobil turlari" },
        { "nj", "Narxlar jadvali" },
    };
    public static Dictionary<string, string> KIATypes => new()
    {
        { "ks", "KIA Soul" },
        { "k5", "KIA K5"},
        { "kn", "KIA Niro"},
        { "ks", "KIA Sorento"},
    };
    public static Dictionary<string, string> HyundaiTypes => new()
    {
        { "hs", "Hyundai Sonata" },
        { "he", "Hyundai Elantra"},
        { "ho", "Hyundai Sontafe"},
        { "ht", "Hyundai Tucson"},
    };
    public static Dictionary<string, string> ChevroletTypes => new()
    {
        { "cc", "Chevrolet Comaro" },
        { "cm", "Chevrolet Malibu"},
        { "ct", "Chevrolet Trailblazer"},
        { "ca", "Chevrolet Tahoe"},
    };
    public static Dictionary<string, string> BMWTypes => new()
    {
        { "x5", "BMW X5" },
        { "m5", "BMW M5" },
        { "i8", "BMW I8"},
        { "m4", "BMW M4"},
    };
    public static Dictionary<string, string> TeslaTypes => new()
    {
        { "mx", "Tesla Model X" },
        { "m3", "Tesla Model 3"},
        { "my", "Tesla Model Y"},
        { "cr", "Tesla Cyber Truck"},
    };
}