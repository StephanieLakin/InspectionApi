using Microsoft.EntityFrameworkCore;
using InspectionAPI;

namespace InspectionAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Inspection> Inspection { get; set; }   
        public DbSet<Inspection> InspectionTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<InspectionAPI.InspectionType> InspectionType { get; set; }

    }
}
