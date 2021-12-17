using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RocketElevatorsCustomerPortal.Models
{
    public partial class jacobgomezContext : DbContext
    {
        public jacobgomezContext()
        {
        }

        public jacobgomezContext(DbContextOptions<jacobgomezContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ArInternalMetadatum> ArInternalMetadata { get; set; }
        public virtual DbSet<Battery> Batteries { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<BuildingDetail> BuildingDetails { get; set; }
        public virtual DbSet<Column> Columns { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Elevator> Elevators { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<SchemaMigration> SchemaMigrations { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=codeboxx.cq6zrczewpu2.us-east-1.rds.amazonaws.com;database=jacobgomez;user=codeboxx;password=Codeboxx1!;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressType)
                    .HasMaxLength(255)
                    .HasColumnName("address_type");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Entity)
                    .HasMaxLength(255)
                    .HasColumnName("entity");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .HasColumnName("notes");

                entity.Property(e => e.NumberAndStreet)
                    .HasMaxLength(255)
                    .HasColumnName("number_and_street");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(255)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.SuiteAndApartment)
                    .HasMaxLength(255)
                    .HasColumnName("suite_and_apartment");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ArInternalMetadatum>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PRIMARY");

                entity.ToTable("ar_internal_metadata");

                entity.Property(e => e.Key).HasColumnName("key");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Battery>(entity =>
            {
                entity.ToTable("batteries");

                entity.HasIndex(e => e.BuildingId, "index_batteries_on_building_id");

                entity.HasIndex(e => e.EmployeeId, "index_batteries_on_employee_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BatteryType)
                    .HasMaxLength(255)
                    .HasColumnName("battery_type");

                entity.Property(e => e.BuildingId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("building_id");

                entity.Property(e => e.CertificateOfOperations)
                    .HasMaxLength(255)
                    .HasColumnName("certificate_of_operations");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DateOfCommissioning)
                    .HasColumnType("date")
                    .HasColumnName("date_of_commissioning");

                entity.Property(e => e.DateOfLastInspection)
                    .HasColumnType("date")
                    .HasColumnName("date_of_last_inspection");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("employee_id");

                entity.Property(e => e.Information)
                    .HasMaxLength(255)
                    .HasColumnName("information");

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasColumnName("notes");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Batteries)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_fc40470545");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Batteries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_ceeeaf55f7");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("buildings");

                entity.HasIndex(e => e.AddressId, "index_buildings_on_address_id");

                entity.HasIndex(e => e.CustomerId, "index_buildings_on_customer_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("address_id");

                entity.Property(e => e.AdminEmail)
                    .HasMaxLength(255)
                    .HasColumnName("admin_email");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(255)
                    .HasColumnName("admin_name");

                entity.Property(e => e.AdminPhoneNumer)
                    .HasMaxLength(255)
                    .HasColumnName("admin_phone_numer");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("customer_id");

                entity.Property(e => e.TechEmail)
                    .HasMaxLength(255)
                    .HasColumnName("tech_email");

                entity.Property(e => e.TechFullName)
                    .HasMaxLength(255)
                    .HasColumnName("tech_full_name");

                entity.Property(e => e.TechPhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("tech_phone_number");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_6dc7a885ab");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_c29cbe7fb8");
            });

            modelBuilder.Entity<BuildingDetail>(entity =>
            {
                entity.ToTable("building_details");

                entity.HasIndex(e => e.BuildingId, "index_building_details_on_building_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BuildingId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("building_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.InformationKey)
                    .HasMaxLength(255)
                    .HasColumnName("information_key");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.BuildingDetails)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_51749f8eac");
            });

            modelBuilder.Entity<Column>(entity =>
            {
                entity.ToTable("columns");

                entity.HasIndex(e => e.BatteryId, "index_columns_on_battery_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BatteryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("battery_id");

                entity.Property(e => e.ColumnType)
                    .HasMaxLength(255)
                    .HasColumnName("column_type");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Information)
                    .HasMaxLength(255)
                    .HasColumnName("information");

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasColumnName("notes");

                entity.Property(e => e.NumberOfFloor)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_floor");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Battery)
                    .WithMany(p => p.Columns)
                    .HasForeignKey(d => d.BatteryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_021eb14ac4");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.AddressId, "index_customers_on_address_id");

                entity.HasIndex(e => e.UserId, "index_customers_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("address_id");

                entity.Property(e => e.CompanyContactName)
                    .HasMaxLength(255)
                    .HasColumnName("company_contact_name");

                entity.Property(e => e.CompanyDescription)
                    .HasMaxLength(255)
                    .HasColumnName("company_description");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("company_name");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(255)
                    .HasColumnName("contact_email");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .HasColumnName("contact_phone");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ServiceTechEmail)
                    .HasMaxLength(255)
                    .HasColumnName("service_tech_email");

                entity.Property(e => e.ServiceTechName)
                    .HasMaxLength(255)
                    .HasColumnName("service_tech_name");

                entity.Property(e => e.ServiceTechPhone)
                    .HasMaxLength(255)
                    .HasColumnName("service_tech_phone");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_3f9404ba26");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_9917eeaf5d");
            });

            modelBuilder.Entity<Elevator>(entity =>
            {
                entity.ToTable("elevators");

                entity.HasIndex(e => e.ColumnId, "index_elevators_on_column_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CertificateOfInspection)
                    .HasMaxLength(255)
                    .HasColumnName("certificate_of_inspection");

                entity.Property(e => e.ColumnId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("column_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DateOfCommissioning)
                    .HasColumnType("date")
                    .HasColumnName("date_of_commissioning");

                entity.Property(e => e.DateOfLastInspection)
                    .HasColumnType("date")
                    .HasColumnName("date_of_last_inspection");

                entity.Property(e => e.ElevatorType)
                    .HasMaxLength(255)
                    .HasColumnName("elevator_type");

                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .HasColumnName("model");

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasColumnName("notes");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .HasColumnName("serial_number");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Column)
                    .WithMany(p => p.Elevators)
                    .HasForeignKey(d => d.ColumnId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_rails_69442d7bc2");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.UserId, "index_employees_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_dcfd3d4fc3");
            });

            modelBuilder.Entity<Intervention>(entity =>
            {
                entity.ToTable("interventions");

                entity.HasIndex(e => e.BatteryId, "index_interventions_on_battery_id");

                entity.HasIndex(e => e.BuildingId, "index_interventions_on_building_id");

                entity.HasIndex(e => e.ColumnId, "index_interventions_on_column_id");

                entity.HasIndex(e => e.CustomerId, "index_interventions_on_customer_id");

                entity.HasIndex(e => e.ElevatorId, "index_interventions_on_elevator_id");

                entity.HasIndex(e => e.EmployeeId, "index_interventions_on_employee_id");

                entity.HasIndex(e => e.UserId, "index_interventions_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BatteryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("battery_id");

                entity.Property(e => e.BuildingId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("building_id");

                entity.Property(e => e.ColumnId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("column_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("customer_id");

                entity.Property(e => e.ElevatorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("elevator_id");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("employee_id");

                entity.Property(e => e.InterventionEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("intervention_end");

                entity.Property(e => e.InterventionStart)
                    .HasColumnType("datetime")
                    .HasColumnName("intervention_start");

                entity.Property(e => e.Report)
                    .HasColumnType("text")
                    .HasColumnName("report");

                entity.Property(e => e.Result)
                    .HasMaxLength(255)
                    .HasColumnName("result");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Battery)
                    .WithMany(p => p.Interventions)
                    .HasForeignKey(d => d.BatteryId)
                    .HasConstraintName("fk_rails_268aede6d6");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Interventions)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("fk_rails_911b4ef939");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Interventions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_rails_4242c0f569");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interventions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_99380975b7");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("leads");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Attachment).HasColumnName("attachment");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("company_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasColumnName("created_on");

                entity.Property(e => e.Department)
                    .HasMaxLength(255)
                    .HasColumnName("department");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Filename)
                    .HasMaxLength(255)
                    .HasColumnName("filename");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.Message)
                    .HasColumnType("mediumtext")
                    .HasColumnName("message");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ProjectDescription)
                    .HasColumnType("text")
                    .HasColumnName("project_description");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(255)
                    .HasColumnName("project_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.ToTable("quotes");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AmountOfElevator)
                    .HasMaxLength(255)
                    .HasColumnName("amount_of_elevator");

                entity.Property(e => e.Apartments)
                    .HasMaxLength(255)
                    .HasColumnName("apartments");

                entity.Property(e => e.Basements)
                    .HasMaxLength(255)
                    .HasColumnName("basements");

                entity.Property(e => e.BuildingType)
                    .HasMaxLength(255)
                    .HasColumnName("building_type");

                entity.Property(e => e.BusinessHours)
                    .HasMaxLength(255)
                    .HasColumnName("business_hours");

                entity.Property(e => e.Companies)
                    .HasMaxLength(255)
                    .HasColumnName("companies");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("company_name");

                entity.Property(e => e.Corporations)
                    .HasMaxLength(255)
                    .HasColumnName("corporations");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasColumnName("created_on");

                entity.Property(e => e.Elevators)
                    .HasMaxLength(255)
                    .HasColumnName("elevators");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Floors)
                    .HasMaxLength(255)
                    .HasColumnName("floors");

                entity.Property(e => e.Installation)
                    .HasMaxLength(255)
                    .HasColumnName("installation");

                entity.Property(e => e.MaximumOccupancy)
                    .HasMaxLength(255)
                    .HasColumnName("maximum_occupancy");

                entity.Property(e => e.ParkingSpots)
                    .HasMaxLength(255)
                    .HasColumnName("parking_spots");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PriceElevatorTotal)
                    .HasMaxLength(255)
                    .HasColumnName("price_elevator_total");

                entity.Property(e => e.PricePerElevator)
                    .HasMaxLength(255)
                    .HasColumnName("price_per_elevator");

                entity.Property(e => e.ServicesType)
                    .HasMaxLength(255)
                    .HasColumnName("services_type");

                entity.Property(e => e.TotalPrice)
                    .HasMaxLength(255)
                    .HasColumnName("total_price");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<SchemaMigration>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PRIMARY");

                entity.ToTable("schema_migrations");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "index_users_on_email")
                    .IsUnique();

                entity.HasIndex(e => e.ResetPasswordToken, "index_users_on_reset_password_token")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.EncryptedPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("encrypted_password")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.RememberCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("remember_created_at");

                entity.Property(e => e.ResetPasswordSentAt)
                    .HasColumnType("datetime")
                    .HasColumnName("reset_password_sent_at");

                entity.Property(e => e.ResetPasswordToken).HasColumnName("reset_password_token");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
