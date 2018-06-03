

namespace Logistika.Service.Common.DataAccess
{
 
#pragma warning disable 1573

    using Logistika.Service.Common.DataAccess.Mapping;
    using Logistika.Service.Common.Entities.ErrorLog;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

        public partial class CommonContext : DbContext
        {
            static CommonContext()
            {
                Database.SetInitializer<CommonContext>(null);
            }

            public CommonContext()
                : base("name=KFISEntities")
            {
            }

            public CommonContext(string nameOrConnectionString)
                : base(nameOrConnectionString)
            {
            }

            public CommonContext(string nameOrConnectionString, DbCompiledModel model)
                : base(nameOrConnectionString, model)
            {
            }

            public CommonContext(DbConnection existingConnection, bool contextOwnsConnection)
                : base(existingConnection, contextOwnsConnection)
            {
            }

            public CommonContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
                : base(existingConnection, model, contextOwnsConnection)
            {
            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
                modelBuilder.Configurations.Add(new SystemErrorLogMapping());
                modelBuilder.Configurations.Add(new WebServiceLogMapping()); 
            }

            public DbSet<SystemErrorLog> SystemErrorLog { get; set; }
            public DbSet<WebServiceLog> WebServiceLog { get; set; } 
        }
} 
