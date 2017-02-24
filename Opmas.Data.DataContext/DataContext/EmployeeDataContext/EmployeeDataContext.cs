using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.LeaveManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Objects.PerformanceManagement;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public partial class EmployeeDataContext : DbContext
    {
        public EmployeeDataContext()
            : base("name=Opmas")
        {
        }


        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePersonalData> EmployeePersonalDatas { get; set; }
        public virtual DbSet<EmployeeBankData> EmployeeBankDatas { get; set; }
        public virtual DbSet<EmployeeEducationalQualification> EmployeeEducationalQualifications { get; set; }
        public virtual DbSet<EmployeeMedicalData> EmployeeMedicalDatas { get; set; }
        public virtual DbSet<EmployeePastWorkExperience> EmployeePastWorkExperiences { get; set; }
        public virtual DbSet<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmployeeFamilyData> EmployeeFamilyDatas { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }
        public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
        public virtual DbSet<TrainingCategory> TrainingCategory { get; set; }
        public virtual DbSet<EmployeeTrainingMapping> EmployeeTrainingMappings { get; set; }
        public virtual DbSet<EmploymentCategory> EmploymentCategories { get; set; }
        public virtual DbSet<EmploymentPosition> EmploymentPositions { get; set; }
        public virtual DbSet<PaymentAllowance> PaymentAllowances { get; set; }
        public virtual DbSet<PaymentDeduction> PaymentDeductions { get; set; }
        public virtual DbSet<PositionAllowanceMapping> PositionAllowanceMappings { get; set; }
        public virtual DbSet<PositionDeductionMapping> PositionDeductionMappings { get; set; }
        public virtual DbSet<InstitutionStructure> InstitutionStructures { get; set; }
        public virtual DbSet<PaymentDeductionRequest> PaymentDeductionRequests { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<ApplicationNotification> ApplicationNotifications { get; set; }
        public virtual DbSet<AppraisalFactor> AppraisalFactors { get; set; }
        public virtual DbSet<AppraisalCategory> AppraisalCategories { get; set; }
        public virtual DbSet<AppraisalPositionMapping> AppraisalPositionMappings { get; set; }
        public virtual DbSet<AppraisalScoreCard> AppraisalScoreCards { get; set; }
        public virtual DbSet<Responsibility> Responsibilities { get; set; }
        public virtual DbSet<EmployeeResponsibilityMapping> EmployeeResponsibilityMappings { get; set; }
        public virtual DbSet<PositionChange> PositionChanges { get; set; }
        public virtual DbSet<ApplicationStatistic> ApplicationStatistics { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
