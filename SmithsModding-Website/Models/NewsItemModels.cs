using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmithsModding_Website.Models
{
    public class NewsViewModel
    {
        public List<NewsItem> Items { get; set; }

        public NewsItem newNewsItem { get; set; }
    }
}
