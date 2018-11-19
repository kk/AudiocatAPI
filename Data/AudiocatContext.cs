using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Audiocat.Models
{
    public class AudiocatContext : DbContext
    {
        public AudiocatContext (DbContextOptions<AudiocatContext> options)
            : base(options)
        {
        }

        public DbSet<Audiocat.Models.AudioItem> AudioItem { get; set; }
    }
}
