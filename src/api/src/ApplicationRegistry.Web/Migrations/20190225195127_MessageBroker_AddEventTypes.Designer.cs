﻿// <auto-generated />
using System;
using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationRegistry.Web.Migrations
{
    [DbContext(typeof(ApplicationRegistryDatabaseContext))]
    [Migration("20190225195127_MessageBroker_AddEventTypes")]
    partial class MessageBroker_AddEventTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuildProcessUrls");

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description");

                    b.Property<Guid>("IdProject");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Owner");

                    b.Property<string>("RepositoryUrl");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("IdProject");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionDependencyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("ExtraProperties");

                    b.Property<Guid>("IdApplicationVersion");

                    b.Property<string>("IdDependency")
                        .IsRequired();

                    b.Property<string>("IdDependencyVersion");

                    b.Property<Guid?>("IdParent");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersion");

                    b.HasIndex("IdDependency");

                    b.HasIndex("IdDependencyVersion");

                    b.ToTable("ApplicationVersionDependency");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdApplication");

                    b.Property<string>("IdCommit");

                    b.Property<string>("IdEnvironment")
                        .IsRequired();

                    b.Property<bool>("IsArchived");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("ValidationStatus")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(0)");

                    b.Property<string>("Version")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdApplication");

                    b.HasIndex("IdEnvironment");

                    b.ToTable("ApplicationVersion");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdApplicationVersion");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SpecificationTextHash")
                        .IsRequired();

                    b.Property<string>("SpecificationType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersion");

                    b.HasIndex("SpecificationType");

                    b.ToTable("ApplicationVersionSpecification");

                    b.HasDiscriminator<string>("SpecificationType").HasValue("ApplicationVersionSpecificationEntity");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationTextEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Specification");

                    b.HasKey("Id");

                    b.ToTable("ApplicationVersionSpecificationText");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.CollectorKnowledgeBaseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientType");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("IdApplicationClient");

                    b.Property<Guid>("IdApplicationServer");

                    b.Property<Guid>("IdApplicationVersionDependencyChild");

                    b.Property<Guid>("IdApplicationVersionDependencyParent");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("CollectorKnowledgeBase");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("ExtraProperties");

                    b.Property<string>("IdDependencyType")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("IdDependencyType");

                    b.ToTable("Dependency");

                    b.HasDiscriminator<string>("IdDependencyType").HasValue("NUGET");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanBeAddedManualy");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("DependencyType");

                    b.HasData(
                        new { Id = "NUGET", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 970, DateTimeKind.Local), Name = "Nuget" },
                        new { Id = "APPLICATION", CanBeAddedManualy = true, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "Application" },
                        new { Id = "EXTERNALAPPLICATION", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "External Application" },
                        new { Id = "FRAMEWORK", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "Framework" },
                        new { Id = "NPM", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "npm" },
                        new { Id = "DATABASE", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "Database" },
                        new { Id = "AUTORESTCLIENT", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "AutoRest Client" },
                        new { Id = "MESSAGEBROKERCONSUMER", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "Message Broker Topic Consumer" },
                        new { Id = "MESSAGEBROKERPRODUCER", CanBeAddedManualy = false, CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 976, DateTimeKind.Local), Name = "Message Broker Topic Producer" }
                    );
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyVersionEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("DependencyId");

                    b.Property<string>("IdDependency")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion");

                    b.Property<int>("ValidationStatus");

                    b.Property<string>("Version")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DependencyId");

                    b.ToTable("DependencyVersion");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.EnvironmentEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Environment");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new { Id = new Guid("00000000-0000-0000-0000-000000000001"), CreateDate = new DateTime(2019, 2, 25, 20, 51, 26, 977, DateTimeKind.Local), Description = "Project for applications generated by CLI", Name = "Unasigned applications" }
                    );
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SpecificationTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("SpecificationTypes");

                    b.HasData(
                        new { Id = "Swagger", CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Swagger" }
                    );
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerSpecificationOperationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("HttpMethod");

                    b.Property<Guid>("IdApplicationVersionSpecification");

                    b.Property<string>("OperationId");

                    b.Property<string>("Path");

                    b.Property<byte[]>("RowVersion");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationVersionSpecification");

                    b.ToTable("SwaggerSpecificationOperation");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.TaskEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Status");

                    b.Property<int>("TaskType");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Task");

                    b.HasDiscriminator<int>("TaskType");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerApplicationVersionSpecificationEntity", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity");

                    b.Property<string>("OperationsStringified");

                    b.ToTable("ApplicationVersionSpecification");

                    b.HasDiscriminator().HasValue("Swagger");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationDependencyEntity", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");

                    b.Property<Guid>("IdApplication");

                    b.ToTable("Dependency");

                    b.HasDiscriminator().HasValue("APPLICATION");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.AutorestClientDependency", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");


                    b.ToTable("Dependency");

                    b.HasDiscriminator().HasValue("AUTORESTCLIENT");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MessageBrokerTopicConsumerDependency", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");


                    b.ToTable("Dependency");

                    b.HasDiscriminator().HasValue("MESSAGEBROKERCONSUMER");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MessageBrokerTopicProducentDependency", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.DependencyEntity");


                    b.ToTable("Dependency");

                    b.HasDiscriminator().HasValue("MESSAGEBROKERPRODUCER");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.MapAutorestClientTaskEntity", b =>
                {
                    b.HasBaseType("ApplicationRegistry.Database.Entities.TaskEntity");

                    b.Property<Guid>("IdApplication");

                    b.Property<Guid>("IdApplicationVersionDependency");

                    b.ToTable("Task");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ProjectEntity", "Project")
                        .WithMany("Applications")
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionDependencyEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", "ApplicationVersion")
                        .WithMany("Dependencies")
                        .HasForeignKey("IdApplicationVersion")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyEntity", "Dependency")
                        .WithMany()
                        .HasForeignKey("IdDependency")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyVersionEntity", "DependencyVersion")
                        .WithMany()
                        .HasForeignKey("IdDependencyVersion");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationEntity", "Application")
                        .WithMany("Versions")
                        .HasForeignKey("IdApplication")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationRegistry.Database.Entities.EnvironmentEntity", "Environment")
                        .WithMany()
                        .HasForeignKey("IdEnvironment")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionEntity", "ApplicationVersion")
                        .WithMany("Specifications")
                        .HasForeignKey("IdApplicationVersion")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationRegistry.Database.Entities.SpecificationTypeEntity")
                        .WithMany()
                        .HasForeignKey("SpecificationType")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationTextEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationEntity")
                        .WithOne("SpecificationText")
                        .HasForeignKey("ApplicationRegistry.Database.Entities.ApplicationVersionSpecificationTextEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyTypeEntity", "DependencyType")
                        .WithMany("Dependencies")
                        .HasForeignKey("IdDependencyType")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.DependencyVersionEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.DependencyEntity", "Dependency")
                        .WithMany("Versions")
                        .HasForeignKey("DependencyId");
                });

            modelBuilder.Entity("ApplicationRegistry.Database.Entities.SwaggerSpecificationOperationEntity", b =>
                {
                    b.HasOne("ApplicationRegistry.Database.Entities.SwaggerApplicationVersionSpecificationEntity", "Specification")
                        .WithMany()
                        .HasForeignKey("IdApplicationVersionSpecification")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
