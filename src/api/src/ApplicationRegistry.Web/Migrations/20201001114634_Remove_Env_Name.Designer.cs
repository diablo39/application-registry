﻿// <auto-generated />
using System;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationRegistry.Web.Migrations
{
    [DbContext(typeof(ApplicationRegistryDatabaseContext))]
    [Migration("20201001114634_Remove_Env_Name")]
    partial class Remove_Env_Name
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildProcessUrls")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdProject")
                        .HasColumnName("IdProject")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("IdProject");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionDependencyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdApplicationVersion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdDependency")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdDependencyVersion")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("IdParent")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersion");

                    b.HasIndex("IdDependency");

                    b.HasIndex("IdDependencyVersion");

                    b.ToTable("ApplicationVersionDependency");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdApplication")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdCommit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdEnvironment")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("ToolsVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValidationStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("(0)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdApplication");

                    b.HasIndex("IdEnvironment");

                    b.ToTable("ApplicationVersion");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdApplicationVersion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SpecificationTextHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersion");

                    b.HasIndex("SpecificationType");

                    b.ToTable("ApplicationVersionSpecification");

                    b.HasDiscriminator<string>("SpecificationType").HasValue("ApplicationVersionSpecificationEntity");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationTextEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Specification")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationVersionSpecificationText");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.CollectorKnowledgeBaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdApplicationClient")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationServer")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationVersionDependencyChild")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationVersionDependencyParent")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CollectorKnowledgeBase");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdDependencyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdDependencyType");

                    b.ToTable("Dependency");

                    b.HasDiscriminator<string>("IdDependencyType").HasValue("NUGET");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("CanBeAddedManualy")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DependencyType");

                    b.HasData(
                        new
                        {
                            Id = "NUGET",
                            CanBeAddedManualy = false,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Nuget"
                        },
                        new
                        {
                            Id = "APPLICATION",
                            CanBeAddedManualy = true,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Application"
                        },
                        new
                        {
                            Id = "EXTERNALAPPLICATION",
                            CanBeAddedManualy = false,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "External Application"
                        },
                        new
                        {
                            Id = "FRAMEWORK",
                            CanBeAddedManualy = false,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Framework"
                        },
                        new
                        {
                            Id = "DATABASE",
                            CanBeAddedManualy = false,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Database"
                        },
                        new
                        {
                            Id = "AUTORESTCLIENT",
                            CanBeAddedManualy = false,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "AutoRest Client"
                        },
                        new
                        {
                            Id = "MESSAGEBROKERCONSUMER",
                            CanBeAddedManualy = true,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Message Broker Topic Consumer"
                        },
                        new
                        {
                            Id = "MESSAGEBROKERPRODUCER",
                            CanBeAddedManualy = false,
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Message Broker Topic Producer"
                        });
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyVersionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("IdDependency")
                        .IsRequired()
                        .HasColumnName("DependencyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ValidationStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("(0)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdDependency");

                    b.ToTable("DependencyVersion");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.EnvironmentEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Environment");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Project for applications generated by CLI",
                            Name = "Unasigned applications"
                        });
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SpecificationTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SpecificationTypes");

                    b.HasData(
                        new
                        {
                            Id = "Swagger",
                            CreateDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Swagger"
                        });
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerSpecificationOperationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

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

                    b.ToTable("SwaggerSpecificationOperation");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.TaskEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TaskType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Task");

                    b.HasDiscriminator<int>("TaskType");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.Applications.ApplicationVersionNugetPackageDependency", b =>
                {
                    b.Property<Guid>("IdApplicationVersion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("IdNugetPackage")
                        .HasColumnType("bigint");

                    b.HasKey("IdApplicationVersion", "IdNugetPackage");

                    b.HasIndex("IdNugetPackage");

                    b.ToTable("ApplicationVersionNugetPackageDependency");
                });

            modelBuilder.Entity("ApplicationRegistry.Domain.Entities.NugetPackageEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("Name", "Version")
                        .IsUnique();

                    b.ToTable("NugetPackage");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerApplicationVersionSpecificationEntity", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity");

                    b.Property<string>("OperationsStringified")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Swagger");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationDependencyEntity", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");

                    b.Property<Guid>("IdApplication")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue("APPLICATION");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.AutorestClientDependency", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");

                    b.HasDiscriminator().HasValue("AUTORESTCLIENT");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MessageBrokerTopicConsumerDependency", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");

                    b.HasDiscriminator().HasValue("MESSAGEBROKERCONSUMER");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MessageBrokerTopicProducentDependency", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");

                    b.HasDiscriminator().HasValue("MESSAGEBROKERPRODUCER");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MapAutorestClientTaskEntity", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.TaskEntity");

                    b.Property<string>("AutorestClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdApplication")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdApplicationVersionDependency")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdDependency")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("IdApplicationVersionDependency");

                    b.HasIndex("IdDependency");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ProjectEntity", "Project")
                        .WithMany("Applications")
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionDependencyEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", "ApplicationVersion")
                        .WithMany("Dependencies")
                        .HasForeignKey("IdApplicationVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyEntity", "Dependency")
                        .WithMany()
                        .HasForeignKey("IdDependency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyVersionEntity", "DependencyVersion")
                        .WithMany()
                        .HasForeignKey("IdDependencyVersion");
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
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", "ApplicationVersion")
                        .WithMany("Specifications")
                        .HasForeignKey("IdApplicationVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Database.Entities.SpecificationTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("SpecificationType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationTextEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity", null)
                        .WithOne("SpecificationText")
                        .HasForeignKey("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationTextEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyTypeEntity", "DependencyType")
                        .WithMany("Dependencies")
                        .HasForeignKey("IdDependencyType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyVersionEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyEntity", "Dependency")
                        .WithMany("Versions")
                        .HasForeignKey("IdDependency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerSpecificationOperationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.SwaggerApplicationVersionSpecificationEntity", "Specification")
                        .WithMany()
                        .HasForeignKey("IdApplicationVersionSpecification")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MapAutorestClientTaskEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionDependencyEntity", "ApplicationVersionDependency")
                        .WithMany()
                        .HasForeignKey("IdApplicationVersionDependency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyEntity", "Dependency")
                        .WithMany()
                        .HasForeignKey("IdDependency");
                });
#pragma warning restore 612, 618
        }
    }
}
