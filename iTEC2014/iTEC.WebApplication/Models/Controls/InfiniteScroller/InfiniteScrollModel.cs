
namespace iTEC.WebApplication.Models.Controls.InfiniteScroller
{
    public abstract class InfiniteScrollModel<TEntity>
        where TEntity : InfiniteScroll, new()
    {
        public TEntity Infinite { get; protected set; }

        public InfiniteScrollModel()
        {
            Infinite = new TEntity();
        }
    }
}