using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PedalProAPI.Models;

namespace PedalProAPI.Context
{
    public class PedalProDbContext : IdentityDbContext<PedalProUser>
    {
        public PedalProDbContext(DbContextOptions<PedalProDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Administrator> Administrators { get; set; }

        public virtual DbSet<Bicycle> Bicycles { get; set; }

        public virtual DbSet<PackageRevenue> PackageRevenues { get; set; }

        public virtual DbSet<BookingRevenue> BookingRevenues { get; set; }

        public virtual DbSet<BicycleBrand> BicycleBrands { get; set; }

        public virtual DbSet<BicycleCategory> BicycleCategories { get; set; }

        public virtual DbSet<BicyclePart> BicycleParts { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

        public virtual DbSet<BookingType> BookingTypes { get; set; }

        public virtual DbSet<BrandImage> BrandImages { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<CartStatus> CartStatuses { get; set; }

        public virtual DbSet<Checkout> Checkouts { get; set; }

        public virtual DbSet<CheckoutStatus> CheckoutStatuses { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<ClientPackage> ClientPackages { get; set; }

        public virtual DbSet<ClientType> ClientTypes { get; set; }

        public virtual DbSet<Date> Dates { get; set; }

        public virtual DbSet<DateSlot> DateSlots { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }

        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<FeedbackCategory> FeedbackCategories { get; set; }

        public virtual DbSet<Help> Helps { get; set; }

        public virtual DbSet<HelpCategory> Helpcategories { get; set; }

        public virtual DbSet<ImageType> ImageTypes { get; set; }

        public virtual DbSet<Package> Packages { get; set; }

        public virtual DbSet<PackagePrice> PackagePrices { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        //public virtual DbSet<PedalProRole> PedalProRoles { get; set; }

        //public virtual DbSet<PedalProUser> PedalProUsers { get; set; }

        public virtual DbSet<PedalProUser> PedalProUsers { get; set; }

        public virtual DbSet<Price> Prices { get; set; }

        public virtual DbSet<Refund> Refunds { get; set; }

        public virtual DbSet<RefundReason> RefundReasons { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        //public virtual DbSet<ServicePrice> ServicePrices { get; set; }

        public virtual DbSet<Setup> Setups { get; set; }

        //public virtual DbSet<SetupPrice> SetupPrices { get; set; }

        public virtual DbSet<Timeslot> Timeslots { get; set; }

        public virtual DbSet<TrainingMaterial> TrainingMaterials { get; set; }

        public virtual DbSet<TrainingModule> TrainingModules { get; set; }

        public virtual DbSet<TrainingModuleStatus> TrainingModuleStatuses { get; set; }

        public virtual DbSet<TrainingSession> TrainingSessions { get; set; }

        public virtual DbSet<VAT> Vats { get; set; }

        public virtual DbSet<VideoLink> VideoLinks { get; set; }

        public virtual DbSet<VideoType> VideoTypes { get; set; }

        public virtual DbSet<Workout> Workouts { get; set; }

        public virtual DbSet<WorkoutType> WorkoutTypes { get; set; }

        public virtual DbSet<IndemnityForm> IndemnityForms { get; set; }
        public DbSet<ClientIndemnityForm> ClientIndemnityForms { get; set; }

        public DbSet<TimeslotStatus> TimeslotStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //EmployeeStatus
            modelBuilder.Entity<EmployeeStatus>().HasData(new { EmpStatusId = 1, EmpStatusName = "Available" });


            //VideoTypes
            modelBuilder.Entity<VideoType>().HasData(new { VideoTypeId = 1, VideoTypeName = "MP4" });
            modelBuilder.Entity<VideoType>().HasData(new { VideoTypeId = 2, VideoTypeName = "Mov" });
            modelBuilder.Entity<VideoType>().HasData(new { VideoTypeId = 3, VideoTypeName = "AVI" });


            //Image type
            modelBuilder.Entity<ImageType>().HasData(new { ImageTypeId = 1, ImageTypeName = "JPG" });
            modelBuilder.Entity<ImageType>().HasData(new { ImageTypeId = 2, ImageTypeName = "JPEG" });
            modelBuilder.Entity<ImageType>().HasData(new { ImageTypeId = 3, ImageTypeName = "PNG" });
            modelBuilder.Entity<ImageType>().HasData(new { ImageTypeId = 4, ImageTypeName = "WEBJ" });


            //Training Module Status
            modelBuilder.Entity<TrainingModuleStatus>().HasData(new { TrainingModuleStatusId = 1, TrainingModuleStatusName = "Unstarted" });

            //TestPackage
            //modelBuilder.Entity<Package>().HasData(new { PackageId = 1, PackageName = "Platinum", PackageDescription = "The base package" });

            //TestClient
            //modelBuilder.Entity<Client>().HasData(new { ClientId = 1, ClientTitle = "Mr", ClientName = "Nathan", ClientSurname = "Lombard", ClientEmailAddress = "lompies1234@gmail.com", ClientPhysicalAddress = "Hatfield", ClientPhoneNum = "0768582883", ClientDateOfBirth = DateTime.Now, ClientProfilePicture = "URL" });

            //Booking Status
            modelBuilder.Entity<BookingStatus>().HasData(new { BookingStatusId = 1, BookingStatusName = "Progress" });
            modelBuilder.Entity<BookingStatus>().HasData(new { BookingStatusId = 2, BookingStatusName = "Booked" });
            modelBuilder.Entity<BookingStatus>().HasData(new { BookingStatusId = 3, BookingStatusName = " payment" });


            //Booking Type
            modelBuilder.Entity<BookingType>().HasData(new { BookingTypeId = 1, BookingTypeName = "Training", BookingTypePrice=100.00 });
            modelBuilder.Entity<BookingType>().HasData(new { BookingTypeId = 2, BookingTypeName = "Repair", BookingTypePrice = 500.00 });
            modelBuilder.Entity<BookingType>().HasData(new { BookingTypeId = 3, BookingTypeName = " Setup", BookingTypePrice=250.00 });


            //ClientTypes
            modelBuilder.Entity<ClientType>().HasData(new { ClientTypeId = 1, ClientTypeName = "Basic" });
            modelBuilder.Entity<ClientType>().HasData(new { ClientTypeId = 2, ClientTypeName = "Remote" });
            modelBuilder.Entity<ClientType>().HasData(new { ClientTypeId = 3, ClientTypeName = "Paid" });


            //Help Catergory
            modelBuilder.Entity<HelpCategory>().HasData(new { HelpCategoryId = 1, HelpCategoryName = "Bookings" });
            modelBuilder.Entity<HelpCategory>().HasData(new { HelpCategoryId = 2, HelpCategoryName = "Account" });
            modelBuilder.Entity<HelpCategory>().HasData(new { HelpCategoryId = 3, HelpCategoryName = "TrainingM" });
            modelBuilder.Entity<HelpCategory>().HasData(new { HelpCategoryId = 4, HelpCategoryName = "Workout" });

            //Feedback Catergory
            modelBuilder.Entity<FeedbackCategory>().HasData(new { FeedbackCategoryId = 1, FeedbackCategoryName = "Usability" });
            modelBuilder.Entity<FeedbackCategory>().HasData(new { FeedbackCategoryId = 2, FeedbackCategoryName = "Support" });
            modelBuilder.Entity<FeedbackCategory>().HasData(new { FeedbackCategoryId = 3, FeedbackCategoryName = "Service" });
            


            //Timeslot status
            modelBuilder.Entity<TimeslotStatus>().HasData(new { TimeslotStatusId = 1, TimeslotStatusName = "Available" });
            modelBuilder.Entity<TimeslotStatus>().HasData(new { TimeslotStatusId = 2, TimeslotStatusName = "Booked" });




            //Cart status
            modelBuilder.Entity<CartStatus>().HasData(new { CartStatusId = 1, CartStatusName = "Empty" });
            modelBuilder.Entity<CartStatus>().HasData(new { CartStatusId = 2, CartStatusName = "Full" });


        }
    }
}
