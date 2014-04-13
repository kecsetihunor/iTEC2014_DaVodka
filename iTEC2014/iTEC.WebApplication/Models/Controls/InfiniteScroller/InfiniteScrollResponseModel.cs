using System;

namespace iTEC.WebApplication.Models.Controls.InfiniteScroller
{
    public class InfiniteScrollResponseModel<TEntity> : InfiniteScrollModel<InfiniteScrollResponse>
    {
        public TEntity[] Result { get; set; }
        public Boolean Done { get; private set; }

        private Int32 requestSize;

        public InfiniteScrollResponseModel(InfiniteScrollRequestModel model)
        {
            Infinite.Entity = model.Infinite.Entity;
            requestSize = model.Infinite.Size;
        }

        public void Finish()
        {
            Infinite.Entity += Result.Length;
            Done = Result.Length < requestSize;
        }
    }
}