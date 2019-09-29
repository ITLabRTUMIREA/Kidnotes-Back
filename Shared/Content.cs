using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Content
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime SendingDate { get; set; }
        public bool Approved { get; set; }

        public Work Work { get; set; }
        public int WorkId { get; set; }
    }
}
