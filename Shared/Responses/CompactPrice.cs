using Shared.Prices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class CompactPrice
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public PriceType PriceType { get; set; }
    }
}
