using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BATCH336A.DataModel
{
    public partial class BATCH336AContext : DbContext
    {
        public BATCH336AContext()
        {
        }

        public BATCH336AContext(DbContextOptions<BATCH336AContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MAdmin> MAdmins { get; set; } = null!;
        public virtual DbSet<MBiodataAddress> MBiodataAddresses { get; set; } = null!;
        public virtual DbSet<MBiodatum> MBiodata { get; set; } = null!;
        public virtual DbSet<MCourier> MCouriers { get; set; } = null!;
        public virtual DbSet<MCourierType> MCourierTypes { get; set; } = null!;
        public virtual DbSet<MCustomer> MCustomers { get; set; } = null!;
        public virtual DbSet<MCustomerMember> MCustomerMembers { get; set; } = null!;
        public virtual DbSet<MCustomerRelation> MCustomerRelations { get; set; } = null!;
        public virtual DbSet<MDoctor> MDoctors { get; set; } = null!;
        public virtual DbSet<MDoctorEducation> MDoctorEducations { get; set; } = null!;
        public virtual DbSet<MEducationLevel> MEducationLevels { get; set; } = null!;
        public virtual DbSet<MLocation> MLocations { get; set; } = null!;
        public virtual DbSet<MLocationLevel> MLocationLevels { get; set; } = null!;
        public virtual DbSet<MMedicalFacility> MMedicalFacilities { get; set; } = null!;
        public virtual DbSet<MMedicalFacilityCategory> MMedicalFacilityCategories { get; set; } = null!;
        public virtual DbSet<MMedicalFacilitySchedule> MMedicalFacilitySchedules { get; set; } = null!;
        public virtual DbSet<MMedicalItem> MMedicalItems { get; set; } = null!;
        public virtual DbSet<MMedicalItemCategory> MMedicalItemCategories { get; set; } = null!;
        public virtual DbSet<MMedicalItemSegmentation> MMedicalItemSegmentations { get; set; } = null!;
        public virtual DbSet<MMenu> MMenus { get; set; } = null!;
        public virtual DbSet<MMenuRole> MMenuRoles { get; set; } = null!;
        public virtual DbSet<MPaymentMethod> MPaymentMethods { get; set; } = null!;
        public virtual DbSet<MRole> MRoles { get; set; } = null!;
        public virtual DbSet<MServiceUnit> MServiceUnits { get; set; } = null!;
        public virtual DbSet<MSpecialization> MSpecializations { get; set; } = null!;
        public virtual DbSet<MUser> MUsers { get; set; } = null!;
        public virtual DbSet<MWalletDefaultNominal> MWalletDefaultNominals { get; set; } = null!;
        public virtual DbSet<TAppointment> TAppointments { get; set; } = null!;
        public virtual DbSet<TAppointmentDone> TAppointmentDones { get; set; } = null!;
        public virtual DbSet<TCourierDiscount> TCourierDiscounts { get; set; } = null!;
        public virtual DbSet<TCurrentDoctorSpecialization> TCurrentDoctorSpecializations { get; set; } = null!;
        public virtual DbSet<TCustomerChat> TCustomerChats { get; set; } = null!;
        public virtual DbSet<TCustomerChatHistory> TCustomerChatHistories { get; set; } = null!;
        public virtual DbSet<TCustomerCustomNominal> TCustomerCustomNominals { get; set; } = null!;
        public virtual DbSet<TCustomerWallet> TCustomerWallets { get; set; } = null!;
        public virtual DbSet<TCustomerWalletWithdraw> TCustomerWalletWithdraws { get; set; } = null!;
        public virtual DbSet<TDoctorOffice> TDoctorOffices { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeSchedule> TDoctorOfficeSchedules { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatment> TDoctorOfficeTreatments { get; set; } = null!;
        public virtual DbSet<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPrices { get; set; } = null!;
        public virtual DbSet<TDoctorTreatment> TDoctorTreatments { get; set; } = null!;
        public virtual DbSet<TPrescription> TPrescriptions { get; set; } = null!;
        public virtual DbSet<TResetPassword> TResetPasswords { get; set; } = null!;
        public virtual DbSet<TToken> TTokens { get; set; } = null!;
        public virtual DbSet<VwMedicalFacility> VwMedicalFacilities { get; set; } = null!;
        public virtual DbSet<VwProfileDoctor2> VwProfileDoctor2s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=BATCH336A;user id=sa;Password=P@ssw0rd; connection timeout=600;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MAdmin>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MCustomerMember>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MCustomerRelation>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MDoctorEducation>(entity =>
            {
                entity.Property(e => e.IsLastEducation).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MLocationLevel>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MLocationLevelCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__m_locatio__creat__45BE5BA9");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MLocationLevelDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__m_locatio__delet__47A6A41B");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MLocationLevelModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__m_locatio__modif__46B27FE2");
            });

            modelBuilder.Entity<MMedicalFacility>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__m_medical__creat__51300E55");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalFacilityDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__m_medical__delet__531856C7");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalFacilityModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__m_medical__modif__5224328E");
            });

            modelBuilder.Entity<MMedicalFacilityCategory>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__m_medical__creat__3493CFA7");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCategoryDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__m_medical__delet__367C1819");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalFacilityCategoryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__m_medical__modif__3587F3E0");
            });

            modelBuilder.Entity<MMedicalFacilitySchedule>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MMedicalFacilityScheduleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__m_medical__creat__5CA1C101");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MMedicalFacilityScheduleDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__m_medical__delet__5E8A0973");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MMedicalFacilityScheduleModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__m_medical__modif__5D95E53A");
            });

            modelBuilder.Entity<MMedicalItem>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MMedicalItemCategory>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MMedicalItemSegmentation>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MMenuRole>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MServiceUnit>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MSpecialization>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MSpecializationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__m_special__creat__40058253");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MSpecializationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__m_special__delet__41EDCAC5");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.MSpecializationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__m_special__modif__40F9A68C");
            });

            modelBuilder.Entity<TAppointment>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TAppointmentDone>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TCurrentDoctorSpecialization>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TCurrentDoctorSpecializationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__t_current__creat__4B7734FF");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TCurrentDoctorSpecializationDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__t_current__delet__4D5F7D71");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TCurrentDoctorSpecializationModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__t_current__modif__4C6B5938");
            });

            modelBuilder.Entity<TCustomerWallet>(entity =>
            {
                entity.Property(e => e.WdAttempt).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TCustomerWalletWithdraw>(entity =>
            {
                entity.Property(e => e.WdAttempt).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TDoctorOffice>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__t_doctor___creat__56E8E7AB");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__t_doctor___delet__58D1301D");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__t_doctor___modif__57DD0BE4");
            });

            modelBuilder.Entity<TDoctorOfficeSchedule>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TDoctorOfficeTreatment>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__t_doctor___creat__625A9A57");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__t_doctor___delet__6442E2C9");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__t_doctor___modif__634EBE90");
            });

            modelBuilder.Entity<TDoctorOfficeTreatmentPrice>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentPriceCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__t_doctor___creat__681373AD");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentPriceDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__t_doctor___delet__69FBBC1F");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorOfficeTreatmentPriceModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__t_doctor___modif__690797E6");
            });

            modelBuilder.Entity<TDoctorTreatment>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TDoctorTreatmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__t_doctor___creat__3A4CA8FD");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TDoctorTreatmentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK__t_doctor___delet__3C34F16F");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TDoctorTreatmentModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK__t_doctor___modif__3B40CD36");
            });

            modelBuilder.Entity<TPrescription>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrintAttempt).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TToken>(entity =>
            {
                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VwMedicalFacility>(entity =>
            {
                entity.ToView("vw_MedicalFacility");
            });

            modelBuilder.Entity<VwProfileDoctor2>(entity =>
            {
                entity.ToView("vw_Profile_Doctor2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
