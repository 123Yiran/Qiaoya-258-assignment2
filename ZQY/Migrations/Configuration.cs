namespace ZQY.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZQY.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ZQY.Data.ZQYContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZQY.Data.ZQYContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            /*
            var seasons = new List<Season>
            {
                new Season{Name="Jan"},
                new Season{Name="Feb"},
                new Season{Name="Mar"},
                new Season{Name="Ari"},
                new Season{Name="May"},
                new Season{Name="Jun"},
                new Season{Name="Jul"}
            };
            seasons.ForEach(c => context.Seasons.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

            var peveries = new List<Every>
            {new Every{ Name=""}

            };
            

        }*/
        }
    }
}