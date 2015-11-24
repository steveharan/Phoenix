using Phoenix.Data.Configurations;
using Phoenix.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Phoenix.Data
{
    public class PhoenixContext : DbContext
    {
        public PhoenixContext()
            : base("Phoenix")
        {
            Database.SetInitializer<PhoenixContext>(null);
        }

        #region Entity Sets
        public IDbSet<User> UserSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<UserRole> UserRoleSet { get; set; }
        public IDbSet<Family> FamilySet { get; set; }
        public IDbSet<Person> PersonSet { get; set; }
        public IDbSet<PersonRelationship> PersonRelationshipSet { get; set; }
        public IDbSet<Diagnosis> DiagnosisSet { get; set; }
        public IDbSet<DiagnosisSubType> DiagnosisSubTypeSet { get; set; }
        public IDbSet<Ethnicity> EthnicitySet { get; set; }
        public IDbSet<RelationshipType> RelationshipTypeSet { get; set; }
        public IDbSet<Error> ErrorSet { get; set; }
        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new FamilyConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new DiagnosisConfiguration());
            modelBuilder.Configurations.Add(new DiagnosisSubTypeConfiguration());
        }
    }
}
