namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        AppUserId = c.Long(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 100),
                        Middlename = c.String(maxLength: 100),
                        Lastname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 100),
                        Password = c.String(),
                        AppUserImage = c.String(),
                        FingerPrint = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                        EmployeeId = c.Long(),
                        InstitutionId = c.Long(),
                        RoleId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppUserId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.InstitutionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Long(nullable: false, identity: true),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.EmployeeBankDatas",
                c => new
                    {
                        EmployeeBankDataId = c.Long(nullable: false, identity: true),
                        BankId = c.Long(nullable: false),
                        AccountName = c.String(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        AccountType = c.String(nullable: false),
                        FakeId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeBankDataId)
                .ForeignKey("dbo.Banks", t => t.BankId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.BankId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.EmployeeEducationalQualifications",
                c => new
                    {
                        EmployeeEducationalQualificationId = c.Long(nullable: false, identity: true),
                        InstitutionName = c.String(),
                        Location = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DegreeAttained = c.String(),
                        ClassOfDegree = c.String(),
                        FileUpload = c.String(),
                        FakeId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeEducationalQualificationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeFamilyDatas",
                c => new
                    {
                        EmployeeFamilyDataId = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        NextOfKin = c.Boolean(nullable: false),
                        Relationship = c.String(nullable: false),
                        FakeId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeFamilyDataId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeMedicalDatas",
                c => new
                    {
                        EmployeeMedicalDataId = c.Long(nullable: false, identity: true),
                        Genotype = c.String(nullable: false),
                        BloodGroup = c.String(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeMedicalDataId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeePastWorkExperiences",
                c => new
                    {
                        EmployeePastWorkExperienceId = c.Long(nullable: false, identity: true),
                        EmployerName = c.String(),
                        EmployerLocation = c.String(),
                        EmployerContact = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ReasonForLeaving = c.String(),
                        PositionHeld = c.String(),
                        FakeId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeePastWorkExperienceId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeePersonalDatas",
                c => new
                    {
                        EmployeePersonalDataId = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Middlename = c.String(),
                        Lastname = c.String(nullable: false),
                        PlaceOfBirth = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PrimaryAddress = c.String(nullable: false),
                        SecondaryAddress = c.String(),
                        Gender = c.String(nullable: false),
                        StateId = c.Int(nullable: false),
                        LgaId = c.Int(nullable: false),
                        PostalCode = c.String(nullable: false),
                        HomePhone = c.String(nullable: false),
                        MobilePhone = c.String(),
                        WorkPhone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MaritalStatus = c.String(nullable: false),
                        EmployeeImage = c.String(),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeePersonalDataId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Lgas", t => t.LgaId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId)
                .Index(t => t.LgaId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeWorkDatas",
                c => new
                    {
                        EmployeeWorkDataId = c.Long(nullable: false, identity: true),
                        EmploymentType = c.String(),
                        EmploymentDate = c.DateTime(nullable: false),
                        EmploymentStatus = c.String(),
                        PositionHeld = c.String(),
                        Category = c.String(),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeWorkDataId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Motto = c.String(),
                        Logo = c.String(),
                        Location = c.String(),
                        SubscriprionStartDate = c.DateTime(nullable: false),
                        SubscriptonEndDate = c.DateTime(nullable: false),
                        SubscriptionDuration = c.String(),
                        AccessCode = c.String(),
                        ContactNumber = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        PackageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Double(nullable: false),
                        Type = c.String(),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PackageId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ManagePackages = c.Boolean(nullable: false),
                        ManageInstitutions = c.Boolean(nullable: false),
                        ManageFaculties = c.Boolean(nullable: false),
                        ManageDepartments = c.Boolean(nullable: false),
                        ManageUnits = c.Boolean(nullable: false),
                        ManageEmployees = c.Boolean(nullable: false),
                        ManageAppUsers = c.Boolean(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        FacultyId = c.Long(nullable: false),
                        InstitutionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        InstitutionId = c.Long(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.FacultyId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DepartmentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UnitId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Units", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Faculties", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AppUsers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.EmployeeWorkDatas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeePersonalDatas", "StateId", "dbo.States");
            DropForeignKey("dbo.EmployeePersonalDatas", "LgaId", "dbo.Lgas");
            DropForeignKey("dbo.Lgas", "StateId", "dbo.States");
            DropForeignKey("dbo.EmployeePersonalDatas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeePastWorkExperiences", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeMedicalDatas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeFamilyDatas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeEducationalQualifications", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeBankDatas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeBankDatas", "BankId", "dbo.Banks");
            DropIndex("dbo.Units", new[] { "DepartmentId" });
            DropIndex("dbo.Faculties", new[] { "InstitutionId" });
            DropIndex("dbo.Departments", new[] { "InstitutionId" });
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropIndex("dbo.Roles", new[] { "InstitutionId" });
            DropIndex("dbo.Institutions", new[] { "PackageId" });
            DropIndex("dbo.EmployeeWorkDatas", new[] { "EmployeeId" });
            DropIndex("dbo.Lgas", new[] { "StateId" });
            DropIndex("dbo.EmployeePersonalDatas", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeePersonalDatas", new[] { "LgaId" });
            DropIndex("dbo.EmployeePersonalDatas", new[] { "StateId" });
            DropIndex("dbo.EmployeePastWorkExperiences", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeMedicalDatas", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeFamilyDatas", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeEducationalQualifications", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeBankDatas", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeBankDatas", new[] { "BankId" });
            DropIndex("dbo.Employees", new[] { "InstitutionId" });
            DropIndex("dbo.AppUsers", new[] { "RoleId" });
            DropIndex("dbo.AppUsers", new[] { "InstitutionId" });
            DropIndex("dbo.AppUsers", new[] { "EmployeeId" });
            DropTable("dbo.Units");
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
            DropTable("dbo.Roles");
            DropTable("dbo.Packages");
            DropTable("dbo.Institutions");
            DropTable("dbo.EmployeeWorkDatas");
            DropTable("dbo.States");
            DropTable("dbo.Lgas");
            DropTable("dbo.EmployeePersonalDatas");
            DropTable("dbo.EmployeePastWorkExperiences");
            DropTable("dbo.EmployeeMedicalDatas");
            DropTable("dbo.EmployeeFamilyDatas");
            DropTable("dbo.EmployeeEducationalQualifications");
            DropTable("dbo.Banks");
            DropTable("dbo.EmployeeBankDatas");
            DropTable("dbo.Employees");
            DropTable("dbo.AppUsers");
        }
    }
}
