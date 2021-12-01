using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Helpers
{
    public static class AvailableRssFeeds
    {
        public static Dictionary<string, string> RssFeeds = new Dictionary<string, string>()
        {
            {"PolsatNews", "https://www.polsatnews.pl/rss/wszystkie.xml"},
            {"PolsatNews_Polska", "https://www.polsatnews.pl/rss/polska.xml"},
            {"PolsatNews_Swiat", "https://www.polsatnews.pl/rss/swiat.xml"},
            {"PolsatNews_Wideo", "https://www.polsatnews.pl/rss/wideo.xml"},
            {"PolsatNews_Biznes", "https://www.polsatnews.pl/rss/biznes.xml"},
            {"PolsatNews_Technologie", "https://www.polsatnews.pl/rss/technologie.xml"},
            {"PolsatNews_Moto", "https://www.polsatnews.pl/rss/moto.xml"},
            {"PolsatNews_Kultura", "https://www.polsatnews.pl/rss/kultura.xml"},
            {"PolsatNews_Sport", "https://www.polsatnews.pl/rss/sport.xml"},
            {"PolsatNews_CzystaPolska", "https://www.polsatnews.pl/rss/czysta-polska.xml"},
            {"Tvn24", "https://tvn24.pl/najnowsze.xml"},
            {"Onet", "https://wiadomosci.onet.pl/.feed"},
            {"WP", "https://wiadomosci.wp.pl/RSS.XML"},
            {"Interia", "https://wydarzenia.interia.pl/feed"},
            {"Interia_Polska", "https://wydarzenia.interia.pl/polska/feed"},
            {"Interia_Wywiady", "https://wydarzenia.interia.pl/wywiady/feed"},
            {"Interia_Swiat", "https://wydarzenia.interia.pl/swiat/feed"},
            {"Interia_Zagranica", "https://wydarzenia.interia.pl/zagranica/feed"},
            {"Interia_Kultura", "https://wydarzenia.interia.pl/kultura/feed"},
            {"Interia_Historia", "https://wydarzenia.interia.pl/historia/feed"},
            {"Interia_Nauka", "https://wydarzenia.interia.pl/nauka/feed"},
            {"Interia_Religia", "https://wydarzenia.interia.pl/religia/feed"},
            {"Interia_Ciekawostki", "https://wydarzenia.interia.pl/ciekawostki/feed"},
            {"Interia_Autorzy", "https://wydarzenia.interia.pl/autor/feed"},
            {"Interia_Opinie", "https://wydarzenia.interia.pl/opinie/feed"},
            {"Interia_Sport", "https://sport.interia.pl/feed"},
            {"Interia_Kobieta", "https://kobieta.interia.pl/feed"},
            {"Interia_Menway", "https://menway.interia.pl/feed"},
            {"Interia_Gry", "https://gry.interia.pl/feed"},
            {"Interia_NoweTechnologie", "https://nt.interia.pl/feed"},
        };
    }
}
