using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WWImporters.Models.ViewModel
{
    public class DashboardVM
    {
        public Person Person { get; set; }

        public Customer Customer { get; set; }

        public IEnumerable<Invoice> Invoice { get; set; }

        public IEnumerable<InvoiceLine> InvoiceLine { get; set; }
    }
}