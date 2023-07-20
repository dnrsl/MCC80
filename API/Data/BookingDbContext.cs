using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Data;

public class BookingDbContext : DbContext
{
    //Konstruktor yang dibutuhkan untuk tahap awal (harus ada)
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

    //Untuk mendaftarkan table ke database
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set;}


    //Fluent API (Lakukan dulu data annotations seperti di BaseEntity)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
                    .HasIndex(e => new
                    {
                        e.Nik,
                        e.Email,
                        e.PhoneNumber
                    }).IsUnique();

        //One University with Many Education (1:N)
        modelBuilder.Entity<University>()
                    .HasMany(u => u.Educations)
                    .WithOne(e => e.University)
                    .HasForeignKey(e => e.UniversityGuid);

        //Many Education with One University (N:1) (bisa dibolak balik)
        /*  
         *  modelBuilder.Entity<Education>()
                      .HasOne(e => e.University)
                      .WithMany(u => u.Educations)
                      .HasForeignKey(u => u.UniversityGuid); 
        */


        modelBuilder.Entity<Employee>()
                    .HasMany(e => e.Bookings)
                    .WithOne(b => b.Employee)
                    .HasForeignKey(b => b.EmployeeGuid);

        modelBuilder.Entity<Education>()
                    .HasOne(d => d.Employee)
                    .WithOne(e => e.Education)
                    .HasForeignKey<Education>(e => e.Guid); //Karena PK dan FK diambil dari Guid milik Education

        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Account)
                    .WithOne(a => a.Employee)
                    .HasForeignKey<Employee>(a => a.Guid);

        modelBuilder.Entity<AccountRole>()
                    .HasOne(ar => ar.Account)
                    .WithMany(a => a.AccountRoles)
                    .HasForeignKey(a => a.AccountGuid);
        modelBuilder.Entity<AccountRole>()
                    .HasOne(ar => ar.Role)
                    .WithMany(r => r.AccountRoles)
                    .HasForeignKey(r => r.RoleGuid);
        modelBuilder.Entity<University>()
                    .HasMany(u => u.Educations)
                    .WithOne(ed => ed.University)
                    .HasForeignKey(ed => ed.UniversityGuid);
        modelBuilder.Entity<Room>()
                    .HasMany(r => r.Bookings)
                    .WithOne(b => b.Room)
                    .HasForeignKey(b => b.RoomGuid);
    }
}
