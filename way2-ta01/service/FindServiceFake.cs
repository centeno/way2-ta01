using System;

namespace way2_ta01.service
{
    public class FindServiceFake : FindService
    {

        public FindServiceFake(String url)
            : base(url)
        {

        }

        public FindServiceFake()
            : base()
        {

        }

        public long? CompareFake(long idx)
        {
            return base.Compare(idx);
        }
    }
}
