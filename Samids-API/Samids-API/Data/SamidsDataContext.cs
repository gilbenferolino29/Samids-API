using Microsoft.EntityFrameworkCore;
using Samids_API.Models;

namespace Samids_API.Data
{
    public class SamidsDataContext : DbContext
    {
        public SamidsDataContext(DbContextOptions<SamidsDataContext> options) : base(options)
        {
                
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Config> Configs { get; set; }

        public DbSet<SubjectSchedule> SubjectSchedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }

    }
}
