using ApplicationRegistry.Database;
using ApplicationRegistry.Database.Entities;
using ApplicationRegistry.Infrastructure.Properties;
using ApplicationRegistry.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.UnitTests.TestInfrastructure
{
    public class TestDatabaseContext : ApplicationRegistryDatabaseContext
    {
        public TestDatabaseContext(DbContextOptions<ApplicationRegistryDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedSampleData(modelBuilder);
        }
        #region sample data
        private static void SeedSampleData(ModelBuilder modelBuilder)
        {
            var projectGuids = new[] {
                Guid.Parse("{375D4E9F-921C-4659-9EE2-D8E86EB75F4A}"),
                Guid.Parse("{9082CE90-A689-48FC-8070-B67FA3B2B446}"),
                Guid.Parse("{0E6A3923-6FB2-4274-8076-0D8B9A3C8B29}") };

            modelBuilder.Entity<SystemEntity>().HasData(
                new SystemEntity(projectGuids[0], "Shared Services") { Description = "Common / infrastructural services shared by other service" },
                new SystemEntity(projectGuids[1], "Accounting system") { Description = "System used by accountants" },
                new SystemEntity(projectGuids[2], "HR system") { Description = "System that is used for HR related topics" }
                );

            modelBuilder.Entity<EnvironmentEntity>().HasData(
                new EnvironmentEntity("INT") { Description = "INT env" }
                , new EnvironmentEntity("UAT") { Description = "UAT env" }
                , new EnvironmentEntity("QUA") { Description = "QUA env" }
                , new EnvironmentEntity("PRD") { Description = "PRD env" }
                , new EnvironmentEntity("Analytics") { Description = "Unused env. To be removed" }
                );
            var applications = new Guid[]{
                Guid.Parse("{EA1F66CB-FBC0-42E6-8021-FB424020F15F}"),
                Guid.Parse("{63F4F9C6-CAA5-44DC-8C78-5F6E4C01DF3C}"),
                Guid.Parse("{6CC102A4-8D86-4922-8402-D205426A4E63}"),
                Guid.Parse("{E09C25DF-0BB9-400C-BB53-2FE134F97630}"),
                Guid.Parse("{939984AB-4122-4572-A760-EE13EE991C64}"),
                Guid.Parse("{5F2DF207-8BFC-402B-A9A9-35D75AAF9EB2}"),
                Guid.Parse("{1FCD899A-CC9B-4A99-AF35-B3A772878ED8}"),
                Guid.Parse("{B9AF71B5-FA2D-48A0-8324-13403F0778CE}"),
                Guid.Parse("{31695E16-B600-4D3A-AB49-36BC2C11E94F}"),
                Guid.Parse("{3309A157-E24F-4286-A368-DF84719AD065}"),

            };
            modelBuilder.Entity<ApplicationEntity>().HasData(
                  new ApplicationEntity(applications[0], "Employees", "Employees", projectGuids[0]) { Description = "Application that stores list of all employees", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/Employees" }
                , new ApplicationEntity(applications[1], "EmailService", "EmailService", projectGuids[0]) { Description = "Abstraction over our SMTP server that introduces security and accounting", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/EmailService" }
                , new ApplicationEntity(applications[2], "BankConnector", "BankConnector", projectGuids[0]) { Description = "Connector to bank that is responsible for account services", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/BankConnector" }
                , new ApplicationEntity(applications[3], "Invoices", "Invoices", projectGuids[1]) { Description = "Handling incoming and outgoing invoices. Hear of accounting system", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/Invoices" }
                , new ApplicationEntity(applications[4], "BankTransfers", "BankTransfers", projectGuids[0]) { Description = "Fetch transfers from the Bank", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/BankTransfers" }
                , new ApplicationEntity(applications[5], "Salaries", "Salaries", projectGuids[2]) { Description = "Stores list of salaries. Have possibility to order transfer in Bank", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/Salaries" }
                , new ApplicationEntity(applications[6], "Holidays", "Holidays", projectGuids[2]) { Description = "Stores evidention of holidays", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/Holidays" }
                , new ApplicationEntity(applications[7], "SickLeave", "SickLeave", projectGuids[2]) { Description = "Stores evidention of sick leaves", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/SickLeave" }
                , new ApplicationEntity(applications[8], "AccountingSystem.UI", "AccountingSystem.UI", projectGuids[1]) { Description = "Stores evidention of sick leaves", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/AccountingSystem.UI" }
                , new ApplicationEntity(applications[9], "HRSystem.UI", "HRSystem.UI", projectGuids[2]) { Description = "Stores evidention of sick leaves", Owner = "MiniMe", RepositoryUrl = "http://myrepo.com/HRSystem.UI" }
                );

            var varsionids = new Guid[]
            {
                Guid.Parse("{90CA2C95-8EF4-44F0-9AD7-D22B776B17C4}"),//0
                Guid.Parse("{0F40D8FF-2A04-404F-8BCA-2DB216792A6C}"),//1
                Guid.Parse("{9D934147-04FD-4673-ABAD-1AB4A4DCB093}"),//2
                Guid.Parse("{7AF4354B-111E-4B43-9260-3A831E1E5B02}"),//3
                Guid.Parse("{EEF41169-5CBE-43C5-BD54-DAE3DE598EF0}"),//4
                Guid.Parse("{845D57BC-141F-400B-B58F-CE0DBC134F24}"),//5
                Guid.Parse("{1DA76784-DFB3-4BAF-A720-FCA345F3F192}"),//6
                Guid.Parse("{CBF23271-490F-4035-ACE0-1C6F8FBCC00C}"),//7
                Guid.Parse("{8D621A2C-E8BB-462E-9A18-F82B37290F47}"),//8
                Guid.Parse("{1915A3ED-0711-4E6A-B9A6-3ACC1FE26D6B}"),//9
                Guid.Parse("{E921B760-86CD-4486-9A30-7EF93B249DAC}"),//10
                Guid.Parse("{CB11D6C5-6BBE-4B52-A08B-C80465216ECA}"),//11
                Guid.Parse("{D214213E-48DD-4129-BE36-03D7EEE08D40}"),//12
                Guid.Parse("{00D8EDA4-B8DF-4AA3-8225-392A6D78F98E}"),//13
                Guid.Parse("{D72DBCE5-81C7-4B00-8196-A99733F579A2}"),//14
                Guid.Parse("{E7105AA9-B2F2-4DA5-8FC5-EA4B41440C3D}"),//15
                Guid.Parse("{8D02C6A4-9569-4525-9B55-5B58DC01C0A5}"),//16
                Guid.Parse("{6F1E4D7D-111B-42E2-8AFF-8774583B5448}"),//17
                Guid.Parse("{46C4FD13-9348-462A-AF74-AB9E8CC789B5}"),//18
                Guid.Parse("{630EA8E9-7FFC-431F-9DF7-37DF56A6C35F}"),//19
                Guid.Parse("{A9E6F0BA-7188-4866-9ACF-7CEDD48BBF76}") //20
            };

            modelBuilder.Entity<ApplicationVersionEntity>().HasData(
                new ApplicationVersionEntity { Id = varsionids[0], IdApplication = applications[0], IdEnvironment = "INT", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[1], IdApplication = applications[0], IdEnvironment = "INT", Version = "1.0_12345", CreateDate = DateTime.UtcNow, IsArchived = true },
                new ApplicationVersionEntity { Id = varsionids[2], IdApplication = applications[0], IdEnvironment = "UAT", Version = "1.0_12345", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[3], IdApplication = applications[0], IdEnvironment = "QUA", Version = "1.0_12345", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[4], IdApplication = applications[0], IdEnvironment = "PRD", Version = "1.0_12345", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[5], IdApplication = applications[1], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[6], IdApplication = applications[3], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[7], IdApplication = applications[4], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[8], IdApplication = applications[5], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[9], IdApplication = applications[2], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[10], IdApplication = applications[6], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[11], IdApplication = applications[7], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[12], IdApplication = applications[8], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[13], IdApplication = applications[9], IdEnvironment = "PRD", Version = "1.1_12332", CreateDate = DateTime.UtcNow, IsArchived = false },
                new ApplicationVersionEntity { Id = varsionids[14], IdApplication = applications[0], IdEnvironment = "Analytics", Version = "RODO", CreateDate = DateTime.UtcNow, IsArchived = false }
                );

            //var simpleDependencies = new[] {
            //    /* 0*/ DependencyEntity.CreateDependencyEntity("System", "NUGET"),
            //    /* 1*/ DependencyEntity.CreateDependencyEntity("Microsoft.Extensions.Logging", "NUGET")
            //};
            //modelBuilder.Entity<DependencyEntity>().HasData(simpleDependencies);
            //var dependencyVersion = new string[] {
            //    Guid.Parse("{19E6AD75-997D-4EFF-8E9F-8A0C8E73A047}").ToString(),
            //    Guid.Parse("{D413B105-80FC-49A1-997E-94503E465494}").ToString(),
            //    Guid.Parse("{21A9505B-035A-485B-A686-DF91B0F24EEE}").ToString(),
            //    Guid.Parse("{81FEC17E-E0FA-495A-9B7B-E72993BBD110}").ToString()
            //};

            //modelBuilder.Entity<DependencyVersionEntity>().HasData(
            //    new DependencyVersionEntity { Id = dependencyVersion[0], IdDependency = string.Concat("NUGET", ":", "System"), Version = "2.1.0", CreateDate = DateTime.UtcNow, },
            //    new DependencyVersionEntity { Id = dependencyVersion[1], IdDependency = string.Concat("NUGET", ":", "Microsoft.Extensions.Logging"), Version = "1.1.4", CreateDate = DateTime.UtcNow }
            //);


            //var applicationDependencies = new[]
            //{
            //    /* 2*/ DependencyEntity.CreateApplicationDependency("Employees", applications[0]),
            //    /* 3*/ DependencyEntity.CreateApplicationDependency("EmailService", applications[1]),
            //    /* 4*/ DependencyEntity.CreateApplicationDependency("BankConnector", applications[2]),
            //    /* 5*/ DependencyEntity.CreateApplicationDependency("Invoices", applications[3]),
            //    /* 6*/ DependencyEntity.CreateApplicationDependency("BankTransfers", applications[4]),
            //    /* 7*/ DependencyEntity.CreateApplicationDependency("Salaries", applications[5]),
            //    /* 8*/ DependencyEntity.CreateApplicationDependency("Holidays", applications[6]),
            //    /* 9*/ DependencyEntity.CreateApplicationDependency("SickLeave", applications[7]),
            //    /*10*/ DependencyEntity.CreateApplicationDependency("AccountingSystem.UI", applications[8]),
            //    /*11*/ DependencyEntity.CreateApplicationDependency("HRSystem.UI", applications[9])
            //};


            //modelBuilder.Entity<ApplicationDependencyEntity>().HasData(applicationDependencies);

            //modelBuilder.Entity<ApplicationVersionDependencyEntity>().HasData(
            //    ApplicationVersionDependencyEntity.Create(varsionids[07], applicationDependencies[4 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[08], applicationDependencies[2 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[10], applicationDependencies[2 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[11], applicationDependencies[2 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[12], applicationDependencies[2 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[12], applicationDependencies[3 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[12], applicationDependencies[4 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[12], applicationDependencies[5 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[12], applicationDependencies[6 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[2 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[3 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[4 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[6 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[7 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[9 - 2].Id),
            //    ApplicationVersionDependencyEntity.Create(varsionids[13], applicationDependencies[8 - 2].Id)
            //    );

            var specificationId = Guid.Parse("{FFE22DAE-443D-4AFD-8CAE-DCA4F1F7883B}");

            modelBuilder.Entity<SwaggerApplicationVersionSpecificationEntity>().HasData(
                new SwaggerApplicationVersionSpecificationEntity { Id = specificationId, CreateDate = DateTime.UtcNow, IdApplicationVersion = varsionids[0], ContentType = "application/yaml", SpecificationType = "Swagger", Code = Guid.NewGuid().ToString(), SpecificationTextHash = Resources.SwaggerDemo.CalculateSHA256() });

            //modelBuilder.Entity<MapAutorestClientTaskEntity>().HasData(
            //    new MapAutorestClientTaskEntity
            //    {
            //        Id = Guid.NewGuid(),
            //        Title = "Task1",
            //        Description = "Desc1",
            //        IdApplicationVersionDependency = Guid.NewGuid(),
            //        Status = TaskStatus.New,
            //        TaskType = TaskType.AUTORESTCLIENTMAP
            //    });
        }
        #endregion
    }

    public class DbContextFactory
    {
        public ApplicationRegistryDatabaseContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationRegistryDatabaseContext>()
                                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ApplicationRegistry21;Trusted_Connection=True;MultipleActiveResultSets=true",
                                b => b.MigrationsAssembly("ApplicationRegistry.Web").UseHierarchyId());

            var options = builder.Options;


            var context = new TestDatabaseContext(options);
            //var deleted = context.Database.EnsureDeleted();
            var created = context.Database.EnsureCreated();
            //context.Database.Migrate();
            return context;
        }
    }
}
