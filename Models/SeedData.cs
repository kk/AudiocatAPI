using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audiocat.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AudiocatContext(
                serviceProvider.GetRequiredService<DbContextOptions<AudiocatContext>>()))
            {
                // Look for any movies.
                if (context.AudioItem.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.AudioItem.AddRange(
                    new AudioItem
                    {
                        Title = "Cool sounds",
                        Tag = "Cool",
                        Timestamp = "07-10-18 4:20T18:25:43.511Z",
                    },
                    new AudioItem
                    {
                        Title = "Good music",
                        Tag = "Nice",
                        Timestamp = "07-11-18 4:20T18:25:43.511Z",
                    },
                    new AudioItem
                    {
                        Title = "Coolest hits 2018",
                        Tag = "Kool",
                        Timestamp = "08-11-18 4:20T18:25:43.511Z",
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
