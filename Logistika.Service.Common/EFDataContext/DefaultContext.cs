
namespace Logistika.Service.Common.EFDataContext
{    
#pragma warning disable 1573

    using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

        public partial class KFISBaseContext : DbContext
        {
            static KFISBaseContext()
            {
                Database.SetInitializer<KFISBaseContext>(null);
            }

            public KFISBaseContext()
                : base("name=KFISEntities")
            {
            }

            public KFISBaseContext(string nameOrConnectionString)
                : base(nameOrConnectionString)
            {
            }

            public KFISBaseContext(string nameOrConnectionString, DbCompiledModel model)
                : base(nameOrConnectionString, model)
            {
            }

            public KFISBaseContext(DbConnection existingConnection, bool contextOwnsConnection)
                : base(existingConnection, contextOwnsConnection)
            {
            }

            public KFISBaseContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
                : base(existingConnection, model, contextOwnsConnection)
            {
            }
             
        }
}
