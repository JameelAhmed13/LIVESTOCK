﻿// <auto-generated />
using System;
using LIVESTOCK.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LIVESTOCK.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLEnrollment", b =>
                {
                    b.Property<short>("BLEnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLGeneralID");

                    b.Property<short>("ClassID");

                    b.Property<short>("EnrollBoys");

                    b.Property<short>("EnrollGirls");

                    b.Property<short>("HeadCountBoys");

                    b.Property<short>("HeadCountGirls");

                    b.Property<short>("PresenceBoys");

                    b.Property<short>("PresenceGirls");

                    b.HasKey("BLEnrollmentID");

                    b.HasIndex("BLGeneralID");

                    b.HasIndex("ClassID");

                    b.ToTable("BLEnrollments");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLFacilitiesInfo", b =>
                {
                    b.Property<short>("BLFacilitiesInfoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLGeneralID");

                    b.Property<string>("IfWaterNotAvailble");

                    b.Property<bool>("IsBoundrywall");

                    b.Property<bool>("IsECERooms");

                    b.Property<bool>("IsElectricity");

                    b.Property<bool>("IsGas");

                    b.Property<bool>("IsITRoom");

                    b.Property<bool>("IsLibrary");

                    b.Property<bool>("IsResourceHall");

                    b.Property<bool>("IsSportsEquipment");

                    b.Property<bool>("IsTelephone");

                    b.Property<bool>("IsWaterAvailable");

                    b.Property<short?>("RoomsFunctional");

                    b.Property<short?>("RoomsNeedRepair");

                    b.Property<short?>("RoomsNonFunctional");

                    b.Property<short?>("RoomsNotInUsed");

                    b.Property<short>("RoomsTotal");

                    b.Property<short>("StudentWithFurniture");

                    b.Property<short?>("ToiletsFunctional");

                    b.Property<string>("ToiletsNonFuncReason");

                    b.Property<short?>("ToiletsNonFunctional");

                    b.Property<short>("ToiletsTotal");

                    b.Property<short?>("TotalCupboards");

                    b.Property<string>("WaterSource");

                    b.HasKey("BLFacilitiesInfoID");

                    b.HasIndex("BLGeneralID");

                    b.ToTable("BLFacilitiesInfos");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLFeederDetail", b =>
                {
                    b.Property<short>("BLFeederDetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLFeederSchoolID");

                    b.Property<short>("ClassID");

                    b.Property<short>("TotalBoys");

                    b.Property<short>("TotalGirls");

                    b.HasKey("BLFeederDetailID");

                    b.HasIndex("BLFeederSchoolID");

                    b.HasIndex("ClassID");

                    b.ToTable("BLFeederDetails");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLFeederSchool", b =>
                {
                    b.Property<short>("BLFeederSchoolID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BEMISCode");

                    b.Property<short>("BLGeneralID");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("SchoolLevel");

                    b.Property<string>("SchoolName");

                    b.HasKey("BLFeederSchoolID");

                    b.HasIndex("BLGeneralID");

                    b.ToTable("BLFeederSchools");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLLandAvailable", b =>
                {
                    b.Property<short>("LandAvailableID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLGeneralID");

                    b.Property<bool?>("CommunityDonateAdditionalLand");

                    b.Property<bool?>("CommunityDonateLand");

                    b.Property<bool?>("NeedAdditionalLand");

                    b.Property<bool>("SchoolOwnLand");

                    b.HasKey("LandAvailableID");

                    b.HasIndex("BLGeneralID");

                    b.ToTable("BLLandAvailables");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLPTSMCInfo", b =>
                {
                    b.Property<short>("BLPtsmcID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLGeneralID");

                    b.Property<string>("IfNotFunctional");

                    b.Property<bool>("IsPtsmcFormed");

                    b.Property<bool?>("IsPtsmcFunctional");

                    b.Property<bool?>("IsPtsmcTrained");

                    b.Property<short?>("PtsmcMembers");

                    b.HasKey("BLPtsmcID");

                    b.HasIndex("BLGeneralID");

                    b.ToTable("BLPTSMCInfos");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherDetail", b =>
                {
                    b.Property<int?>("BLTeacherID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLGeneralID");

                    b.Property<string>("Diploma");

                    b.Property<string>("MajorSubjects");

                    b.Property<string>("Post");

                    b.Property<string>("Qualification");

                    b.Property<string>("Status");

                    b.Property<string>("TeacherName");

                    b.Property<bool>("TrainedOnECE");

                    b.HasKey("BLTeacherID");

                    b.HasIndex("BLGeneralID");

                    b.ToTable("BLTeacherDetails");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherPost", b =>
                {
                    b.Property<short>("BLTeacherPostID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PostName")
                        .IsRequired();

                    b.HasKey("BLTeacherPostID");

                    b.ToTable("BLTeacherPosts");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherPresent", b =>
                {
                    b.Property<int>("BLTeacherPresentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BLGeneralID");

                    b.Property<short>("TeachersAbsent");

                    b.Property<short>("TeachersAbsent1_7");

                    b.Property<short>("TeachersAbsent30Plus");

                    b.Property<short>("TeachersAbsent7_30");

                    b.Property<short>("TeachersPresent");

                    b.HasKey("BLTeacherPresentID");

                    b.HasIndex("BLGeneralID");

                    b.ToTable("BLTeacherPresents");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherSection", b =>
                {
                    b.Property<int>("BLTeacherSectionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Attached");

                    b.Property<short>("BLGeneralID");

                    b.Property<short>("BLTeacherPostID");

                    b.Property<short>("Filled");

                    b.Property<short>("Sanctioned");

                    b.HasKey("BLTeacherSectionID");

                    b.HasIndex("BLGeneralID");

                    b.HasIndex("BLTeacherPostID");

                    b.ToTable("BLTeacherSections");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BaselineGeneral", b =>
                {
                    b.Property<short>("BLGeneralID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BEMISCode");

                    b.Property<int?>("ClusterBEMISCode")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("Remarks");

                    b.Property<string>("SName");

                    b.Property<int>("SchoolID");

                    b.Property<int>("SchoolType");

                    b.Property<string>("Type");

                    b.Property<int?>("UCID");

                    b.Property<string>("VarifiedBy");

                    b.Property<string>("VisitorName");

                    b.Property<bool>("varified");

                    b.HasKey("BLGeneralID");

                    b.HasIndex("SchoolID");

                    b.ToTable("BaselineGenerals");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.District", b =>
                {
                    b.Property<int>("DistrictID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DistrictCode");

                    b.Property<string>("DistrictName")
                        .IsRequired();

                    b.Property<int>("RegionID");

                    b.HasKey("DistrictID");

                    b.HasIndex("RegionID");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.Region", b =>
                {
                    b.Property<int>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegionCode");

                    b.Property<string>("RegionName")
                        .IsRequired();

                    b.Property<short>("TotalDistricts");

                    b.HasKey("RegionID");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.School", b =>
                {
                    b.Property<int>("SchoolID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Abandon");

                    b.Property<int?>("BEMIS");

                    b.Property<int?>("Budget");

                    b.Property<string>("Construction");

                    b.Property<DateTime?>("ConstructionStartDate");

                    b.Property<DateTime?>("DateOpen");

                    b.Property<int?>("InitialEnrollment");

                    b.Property<bool?>("LandMutation");

                    b.Property<string>("Latitude");

                    b.Property<short>("Level");

                    b.Property<string>("Longitude");

                    b.Property<DateTime?>("NotificationDate");

                    b.Property<bool?>("Onboard");

                    b.Property<bool?>("PTSMC");

                    b.Property<string>("Password");

                    b.Property<string>("Phase");

                    b.Property<string>("Remarks");

                    b.Property<string>("SCode");

                    b.Property<string>("SName")
                        .IsRequired();

                    b.Property<int>("SchoolOf");

                    b.Property<bool?>("SelectedStatus");

                    b.Property<bool>("Status");

                    b.Property<int>("UCID");

                    b.Property<string>("Upgradation");

                    b.Property<bool?>("Zone");

                    b.Property<string>("type");

                    b.HasKey("SchoolID");

                    b.HasIndex("UCID");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.SchoolClass", b =>
                {
                    b.Property<short>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName");

                    b.HasKey("ClassID");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.Tehsil", b =>
                {
                    b.Property<int>("TehsilID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DistrictID");

                    b.Property<string>("TehsilName")
                        .IsRequired();

                    b.Property<string>("Tehsilcode");

                    b.HasKey("TehsilID");

                    b.HasIndex("DistrictID");

                    b.ToTable("Tehsils");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.UC", b =>
                {
                    b.Property<int>("UCID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TehsilID");

                    b.Property<string>("UCCode");

                    b.Property<string>("UCName")
                        .IsRequired();

                    b.HasKey("UCID");

                    b.HasIndex("TehsilID");

                    b.ToTable("UCs");
                });

            modelBuilder.Entity("LIVESTOCK.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLEnrollment", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGenerals")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LIVESTOCK.Models.Data.SchoolClass", "schoolClass")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLFacilitiesInfo", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGenerals")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLFeederDetail", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BLFeederSchool", "BlFeederSchool")
                        .WithMany()
                        .HasForeignKey("BLFeederSchoolID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LIVESTOCK.Models.Data.SchoolClass", "SchoolClass")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLFeederSchool", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGenerals")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLLandAvailable", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGeneral")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLPTSMCInfo", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGenerals")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherDetail", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "baselinegeneral")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherPresent", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGenerals")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BLTeacherSection", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.BaselineGeneral", "BaselineGenerals")
                        .WithMany()
                        .HasForeignKey("BLGeneralID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LIVESTOCK.Models.Data.BLTeacherPost", "BLTeacherPost")
                        .WithMany()
                        .HasForeignKey("BLTeacherPostID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.BaselineGeneral", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.District", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.School", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.UC", "UC")
                        .WithMany()
                        .HasForeignKey("UCID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.Tehsil", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LIVESTOCK.Models.Data.UC", b =>
                {
                    b.HasOne("LIVESTOCK.Models.Data.Tehsil", "Tehsil")
                        .WithMany()
                        .HasForeignKey("TehsilID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LIVESTOCK.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LIVESTOCK.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LIVESTOCK.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LIVESTOCK.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}