using System;

namespace iTEC.WebApplication.Models.Controls.InfiniteScroller
{
    public abstract class InfiniteScroll
    {
        private Int32 entity;

        public static Int32 MinEntity = 0;
        public Int32 Entity
        {
            get
            {
                return entity;
            }
            set
            {
                if (entity < MinEntity)
                {
                    entity = MinEntity;
                }
                else
                {
                    entity = value;
                }
            }
        }

        public InfiniteScroll()
        {
            Entity = MinEntity;
        }
    }
}