using Dev.Infra.Data.Context;
using System.Data.Entity.Migrations;

namespace Dev.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EasyPontoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


    }
}
