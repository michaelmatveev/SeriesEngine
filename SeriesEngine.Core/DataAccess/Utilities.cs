using System.Data.Entity;

namespace SeriesEngine.Core.DataAccess
{
    public static class Utilities
    {
        public static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added: return EntityState.Added;
                case ObjectState.Deleted: return EntityState.Deleted;
                case ObjectState.Modified: return EntityState.Modified;
                default: return EntityState.Unchanged;
            }
        }

        public static void FixState(this DbContext context)
        {
            foreach(var entity in context.ChangeTracker.Entries<IStateObject>())
            {
                IStateObject stateInfo = entity.Entity;
                entity.State = ConvertState(stateInfo.State);
            }
        }

    }
}