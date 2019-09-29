using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Prices
{
    public class Price
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public Work Work { get; set; }
        public int WorkId { get; set; }

        public PriceType PriceType { get; set; }
        public int PriceTypeId { get; set; }

    }
}
