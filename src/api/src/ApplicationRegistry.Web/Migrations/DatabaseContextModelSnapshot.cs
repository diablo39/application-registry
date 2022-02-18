﻿// <auto-generated />
using System;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApplicationRegistry.Web.Migrations
{
    [DbContext(typeof(ApplicationRegistryDatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildProcessUrls")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)");

                    b.Property<string>("Framework")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("IdSystem")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdSystem");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Owner")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("RepositoryUrl")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("IdSystem");

                    b.ToTable("Application", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CollectorBatchStatuses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectorExecutionDuration")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("CollectorExecutionSucceeded")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("FrameworkVersion")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<Guid>("IdApplication")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdCommit")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("IdEnvironment")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("ToolsVersion")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("ValidationStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("(0)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("IdApplication");

                    b.HasIndex("IdEnvironment");

                    b.ToTable("ApplicationVersion", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSwaggerSpecificationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("IdApplicationVersion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OperationsStringified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificationTextHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersion");

                    b.ToTable("ApplicationVersionSpecification", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.CollectorKnowledgeBaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("IdApplicationClient")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationServer")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationVersionDependencyChild")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationVersionDependencyParent")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CollectorKnowledgeBase", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("CanBeAddedManualy")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DependencyType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "APPLICATION",
                            CanBeAddedManualy = true,
                            CreateDate = new DateTimeOffset(new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Name = "Application"
                        });
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.EnvironmentEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Environment", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerSpecificationOperationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("HttpMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdApplicationVersionSpecification")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OperationId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersionSpecification");

                    b.ToTable("SwaggerSpecificationOperation", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SystemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.ToTable("Systems", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            CreateDate = new DateTimeOffset(new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Description = "Project for applications generated by CLI",
                            Name = "Unasigned applications"
                        });
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Applications.ApplicationEndpointEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("EnvironmentId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("EnvironmentId");

                    b.ToTable("ApplicationEndpoint", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Applications.ApplicationVersionNugetPackageDependency", b =>
                {
                    b.Property<Guid>("IdApplicationVersion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdNugetPackage")
                        .HasColumnType("bigint");

                    b.HasKey("IdApplicationVersion", "IdNugetPackage");

                    b.HasIndex("IdNugetPackage");

                    b.ToTable("ApplicationVersionNugetPackageDependency", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.CollectorLogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), -2147483648L, 1);

                    b.Property<string>("Application")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Env")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CollectorLogs");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.EndpointDependencies", b =>
                {
                    b.Property<HierarchyId>("Id")
                        .HasColumnType("hierarchyid");

                    b.Property<string>("ApplicationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("EnvironmentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HierarchyChecksum")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HttpMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HierarchyChecksum")
                        .IsUnique()
                        .HasFilter("[HierarchyChecksum] IS NOT NULL");

                    b.ToTable("EndpointDependency", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Network.LoadBalancerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)");

                    b.Property<string>("Fqdn")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Ip")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Port")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("LoadBalancer", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Network.VlanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Alias")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Cidr")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)");

                    b.Property<string>("IpAsHexString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVirtualDirectory")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<string>("RFC")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.ToTable("Vlan", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.NugetPackageEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("Name", "Version")
                        .IsUnique();

                    b.ToTable("NugetPackage", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.BulkLoadRedisEndpointEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RedisId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RedisEndpoints", "BulkLoad");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.RedisDeploymentTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RedisDeploymentTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "SENTINEL",
                            CreateDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Description = "Sentinel"
                        },
                        new
                        {
                            Id = "MASTER_SLAVE",
                            CreateDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Description = "Master-Slave"
                        },
                        new
                        {
                            Id = "CLUSTER",
                            CreateDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Description = "Cluster"
                        });
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.RedisEndpointEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RedisId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RedisId");

                    b.ToTable("RedisEndpoints", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.RedisEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdEnvironment")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetworkZone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RedisDeploymentTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Requester")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RfcId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SentinelRedisId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdEnvironment");

                    b.HasIndex("RedisDeploymentTypeId");

                    b.ToTable("Redis", (string)null);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.SystemEntity", "System")
                        .WithMany("Applications")
                        .HasForeignKey("IdSystem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationEntity", "Application")
                        .WithMany("Versions")
                        .HasForeignKey("IdApplication")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Database.Entities.EnvironmentEntity", "Environment")
                        .WithMany()
                        .HasForeignKey("IdEnvironment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("Environment");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSwaggerSpecificationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", "ApplicationVersion")
                        .WithMany("SwaggerSpecifications")
                        .HasForeignKey("IdApplicationVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationVersion");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerSpecificationOperationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionSwaggerSpecificationEntity", "Specification")
                        .WithMany()
                        .HasForeignKey("IdApplicationVersionSpecification")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specification");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Applications.ApplicationEndpointEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationEntity", "Application")
                        .WithMany("Endpoints")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Database.Entities.EnvironmentEntity", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("Environment");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Applications.ApplicationVersionNugetPackageDependency", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", "ApplicationVersion")
                        .WithMany("ApplicationVersionNugetPackageDependencies")
                        .HasForeignKey("IdApplicationVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Domain.Entities.NugetPackageEntity", "NugetPackage")
                        .WithMany("ApplicationVersions")
                        .HasForeignKey("IdNugetPackage")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationVersion");

                    b.Navigation("NugetPackage");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.RedisEndpointEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Domain.Entities.Redis.RedisEntity", null)
                        .WithMany("Endpoints")
                        .HasForeignKey("RedisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.RedisEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.EnvironmentEntity", "Environment")
                        .WithMany()
                        .HasForeignKey("IdEnvironment");

                    b.HasOne("ApplicationRegistry.Domain.Entities.Redis.RedisDeploymentTypeEntity", "RedisDeploymentType")
                        .WithMany()
                        .HasForeignKey("RedisDeploymentTypeId");

                    b.Navigation("Environment");

                    b.Navigation("RedisDeploymentType");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.Navigation("Endpoints");

                    b.Navigation("Versions");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", b =>
                {
                    b.Navigation("ApplicationVersionNugetPackageDependencies");

                    b.Navigation("SwaggerSpecifications");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SystemEntity", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.NugetPackageEntity", b =>
                {
                    b.Navigation("ApplicationVersions");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Redis.RedisEntity", b =>
                {
                    b.Navigation("Endpoints");
                });
#pragma warning restore 612, 618
        }
    }
}
