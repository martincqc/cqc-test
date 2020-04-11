using System;
using System.Collections.Generic;
using System.Text;

namespace AddressLookup
{
    public class AddressListVM
    {
        public string PostCode { get; set; }
        public List<AddressResult> Addresses { get; set; }
        public string PageId { get; set; }
        public string NextPageId { get; set; }
    }
}
