using System.Collections.Generic;

namespace GFOL.AddressFinder
{
    public class AddressListVM
    {
        public string PostCode { get; set; }
        public List<AddressResult> Addresses { get; set; }
        public string PageId { get; set; }
        public string NextPageId { get; set; }
    }
}