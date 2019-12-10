using AdventureWorks.Web.Models.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Models
{
    public class SearchViewModel
    {
        public string Subtitle { get { return "Zoeken"; } }

        public SearchQuery Query { get; set; }
    }
}
