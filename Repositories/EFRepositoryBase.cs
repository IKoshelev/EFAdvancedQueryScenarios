using EFDataContext;

namespace Repositories
{
    public abstract class EFRepositoryBase
    {
        public EFRepositoryBase(IAdventureWorksDataContext context)
        {
            DataContext = context;
        }

        protected IAdventureWorksDataContext DataContext
        {
            get;
            private set;
        }
    }

}
