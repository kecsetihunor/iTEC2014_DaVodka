using System;

namespace iTEC.WebApplication.Models.Controls.InfiniteScroller
{
    public class InfiniteScrollRequest : InfiniteScroll
    {
        private Int32 size;

        public static Int32 MinSize = 4;
        public static Int32 MaxSize = 40;
        public Int32 Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value < MinSize)
                {
                    Size = MinSize;
                }
                else if (value > MaxSize)
                {
                    Size = MaxSize;
                }
                else
                {
                    size = value;
                }
            }
        }

        public InfiniteScrollRequest()
        {
            Size = MinSize;
        }
    }
}