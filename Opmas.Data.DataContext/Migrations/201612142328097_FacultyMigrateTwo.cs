namespace Opmas.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacultyMigrateTwo : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: false)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: false)
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

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeWorkDatas",
                c => new
                    {
                        EmployeeWorkDataId = c.Long(nullable: false, identity: true),
                        EmploymentType = c.String(),
                        EmploymentDate = c.DateTime(nullable: false),
                        EmploymentStatus = c.String(),
                        PositionHeld = c.String(),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeWorkDataId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.Lgas",
                c => new
                    {
                        LgaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LgaId);
            
            CreateTable(
                "dbo.EmployeePersonalDatas",
                c => new
                    {
                        EmployeePersonalDataId = c.Long(nullable: false, identity: true),
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
                .PrimaryKey(t => t.EmployeePersonalDataId);
            
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
                .PrimaryKey(t => t.EmployeePastWorkExperienceId);
            
            CreateTable(
                "dbo.EmployeeMedicalDatas",
                c => new
                    {
                        EmployeeMedicalDataId = c.Long(nullable: false, identity: true),
                        Genotype = c.String(nullable: false),
                        BloodGroup = c.String(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeMedicalDataId);
            
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
                        FakeId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeEducationalQualificationId);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
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
                .PrimaryKey(t => t.EmployeeBankDataId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Long(nullable: false, identity: true),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
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
                        Role = c.String(),
                        AppUserImage = c.String(),
                        FingerPrint = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                        EmployeeId = c.Long(),
                        InstitutionId = c.Long(),
                        CreatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppUserId);
            
            CreateIndex("dbo.EmployeeWorkDatas", "EmployeeId");
            CreateIndex("dbo.Lgas", "StateId");
            CreateIndex("dbo.EmployeePersonalDatas", "EmployeeId");
            CreateIndex("dbo.EmployeePersonalDatas", "LgaId");
            CreateIndex("dbo.EmployeePersonalDatas", "StateId");
            CreateIndex("dbo.EmployeePastWorkExperiences", "EmployeeId");
            CreateIndex("dbo.EmployeeMedicalDatas", "EmployeeId");
            CreateIndex("dbo.EmployeeEducationalQualifications", "EmployeeId");
            CreateIndex("dbo.EmployeeBankDatas", "EmployeeId");
            CreateIndex("dbo.EmployeeBankDatas", "BankId");
            CreateIndex("dbo.AppUsers", "InstitutionId");
            CreateIndex("dbo.AppUsers", "EmployeeId");
            AddForeignKey("dbo.AppUsers", "InstitutionId", "dbo.Institutions", "InstitutionId");
            AddForeignKey("dbo.AppUsers", "EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.EmployeeWorkDatas", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeePersonalDatas", "StateId", "dbo.States", "StateId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeePersonalDatas", "LgaId", "dbo.Lgas", "LgaId", cascadeDelete: true);
            AddForeignKey("dbo.Lgas", "StateId", "dbo.States", "StateId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeePersonalDatas", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeePastWorkExperiences", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeMedicalDatas", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeEducationalQualifications", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeBankDatas", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeBankDatas", "BankId", "dbo.Banks", "BankId", cascadeDelete: true);
        }
    }
}
