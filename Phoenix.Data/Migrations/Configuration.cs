namespace Phoenix.Data.Migrations
{
    using Phoenix.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoenixContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhoenixContext context)
        {

            // create Phoenix stuff
            //context.DiagnosisSet.AddOrUpdate(GenerateDiagnosis());
            //context.DiagnosisSubTypeSet.AddOrUpdate(GenerateDiagnosisSubType());
            //context.EthnicitySet.AddOrUpdate(GenerateEthnicity());

            //context.FamilySet.AddOrUpdate(GenerateFamilies());

            //context.PersonSet.AddOrUpdate(GeneratePersons());


            //context.RelationshipTypeSet.AddOrUpdate(GenerateRelationTypes());

            context.RoleSet.AddOrUpdate(r => r.Name, GenerateRoles());
            //context.UserSet.AddOrUpdate(u => u.Email, new User[]{
            //    new User()
            //    {
            //        Email="chsakells.blog@gmail.com",
            //        Username="chsakell",
            //        HashedPassword ="XwAQoiq84p1RUzhAyPfaMDKVgSwnn80NCtsE8dNv3XI=",
            //        Salt = "mNKLRbEFCH8y1xIyTXP4qA==",
            //        IsLocked = false,
            //        DateCreated = DateTime.Now
            //    }
            //});

            //context.UserRoleSet.AddOrUpdate(new UserRole[] {
            //        new UserRole() {
            //            RoleId = 1, // admin
            //            UserId = 1  // chsakell
            //        }
            //    });
        }

        private Ethnicity[] GenerateEthnicity()
        {
            Ethnicity[] ethnicity = new Ethnicity[]
            {
                new Ethnicity()
                {
                    EthnicityName = "White"
                },
                new Ethnicity()
                {
                    EthnicityName = "Black or Afro-Carribbean"
                },
                new Ethnicity()
                {
                    EthnicityName = "Asian"
                },
                new Ethnicity()
                {
                    EthnicityName = "Mixed Race"
                }
            };
            return ethnicity;
        }

        private RelationshipType[] GenerateRelationTypes()
        {
            RelationshipType[] types = new RelationshipType[]
            {
                new RelationshipType()
                {
                    RelationshipTypeName = "Father"
                },
                new RelationshipType()
                {
                    RelationshipTypeName = "Mother"
                },
                new RelationshipType()
                {
                    RelationshipTypeName = "Spouse"
                }
            };
            return types;
        }

        private Diagnosis[] GenerateDiagnosis()
        {
            Diagnosis[] diagnoses = new Diagnosis[]
            {
                new Diagnosis()
                {
                    Name = "Diagnosis 1"
                },
                new Diagnosis()
                {
                    Name = "Diagnosis 2"
                }
            };

            return diagnoses;
        }

        private DiagnosisSubType[] GenerateDiagnosisSubType()
        {
            DiagnosisSubType[] diagnosisSybTypes = new DiagnosisSubType[]
            {
                new DiagnosisSubType()
                {
                    Name = "Diagnosis Sub Type 1.1",
                    DiagnosisId = 1
                },
                new DiagnosisSubType()
                {
                    Name = "Diagnosis Sub Type 1.2",
                    DiagnosisId = 1
                },
                new DiagnosisSubType()
                {
                    Name = "Diagnosis Sub Type 2.1",
                    DiagnosisId = 2
                },
                new DiagnosisSubType()
                {
                    Name = "Diagnosis Sub Type 2.2",
                    DiagnosisId = 2
                }
            };

            return diagnosisSybTypes;
        }

        private Family[] GenerateFamilies()
        {
            Family[] families = new Family[]
            {
                new Family()
                {
                    FamilyName = "Haran",
                    FirstRegisteredDate = DateTime.Now,
                    Notes = "A nice family",
                    EthnicityID = 1,
                    Deleted = false,
                    FamilyIdentifier = "F123456"
                },
                new Family()
                {
                    FamilyName = "Alen",
                    FirstRegisteredDate = DateTime.Now,
                    Notes = "A great family",
                    EthnicityID = 2,
                    Deleted = false,
                    FamilyIdentifier = "F123546"
                }
            };

            return families;
        }

        private Person[] GeneratePersons()
        {
            Person[] persons = new Person[]
            {
                new Person()
                {
                    NhsNumber = "ABC123ZXC",
                    FirstName = "Steve",
                    SurName = "Haran",
                    DateOfBirth = DateTime.Now,
                    Twin = false,
                    Adopted = false,
                    HeightCM = 200,
                    WeightKG = 76,
                    Deceased = false,
                    DateDeceased = null,
                    Gender = "M",
                    FirstRegisteredDate = DateTime.Now,
                    Notes = "Notes",
                    EthnicityId = 1,
                    FamilyId = 2,
                    Deleted = false
                },
                new Person()
                {
                    NhsNumber = "BBC133ZXC",
                    FirstName = "Pablo",
                    SurName = "Haran",
                    DateOfBirth = DateTime.Now,
                    Twin = false,
                    Adopted = false,
                    HeightCM = 100,
                    WeightKG = 30,
                    Deceased = false,
                    DateDeceased = null,
                    Gender = "M",
                    FirstRegisteredDate = DateTime.Now,
                    Notes = "Notes",
                    EthnicityId = 1, 
                    FamilyId = 2,
                    Deleted = false
                }
            };

            return persons;
        }

        private Role[] GenerateRoles()
        {
            Role[] _roles = new Role[]{
                new Role()
                {
                    Name="Admin"
                }
            };

            return _roles;
        }
        /*private Rental[] GenerateRentals()
        {
            for (int i = 1; i <= 45; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Random r = new Random();
                    int customerId = r.Next(1, 99);
                    Rental _rental = new Rental()
                    {
                        CustomerId = 1,
                        StockId = 1,
                        Status = "Returned",
                        RentalDate = DateTime.Now.AddDays(j),
                        ReturnedDate = DateTime.Now.AddDays(j + 1)
                    };

                    _rentals.Add(_rental);
                }
            }

            //return _rentals.ToArray();
        }*/
    }
}