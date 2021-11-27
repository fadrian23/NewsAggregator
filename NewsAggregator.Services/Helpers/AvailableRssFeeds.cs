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
        };
    }
}
