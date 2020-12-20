using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErsSapAPI.Models
{
    public class HotPurchaseOrder
    {
        public long ID { get; set; }
        public string PO_NUMBER { get; set; }
        public int PO_ITEM { get; set; }
        public string MATERIAL { get; set; }
        public string ItemBarcode { get; set; }
        public decimal? Store_Quantity { get; set; }
        public DateTime? Posting_Date { get; set; }
        public string? DeliveryNote { get; set; }
        public int? REG_FL { get; set; }
        public string? EMaterialdocument { get; set; }
        public int Try { get; set; }
        public string? Unit { get; set; }
        public string Vendor { get; set; }

    }
}
