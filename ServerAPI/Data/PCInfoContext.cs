using Microsoft.EntityFrameworkCore;
using ServerAPI.Models;

namespace ServerAPI.Data
{
    public class PCInfoContext : DbContext
    {
        public PCInfoContext(DbContextOptions<PCInfoContext> options) : base(options)
        {
        }

        public DbSet<PCInfo> PCInfos { get; set; }
    }
}
