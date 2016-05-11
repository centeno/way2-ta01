using System;

namespace way2_ta01.model
{
    public class FindResult
    {
        public bool Success { get; set; }
        public long Tentatives { get; set; }
        public long? Index { get; set; }
        public String Word { get; set; }
    }
}
