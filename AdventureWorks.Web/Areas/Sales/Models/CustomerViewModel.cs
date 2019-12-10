using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Areas.Sales.Models
{
    public class CustomerViewModel
    {
        public string Subtitle
        {
            get { return $"Details van {FirstName} {LastName}"; }
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

}
