using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DMS.Services
{
    public class UnitService : IUnitRepository
    {
        private readonly ApplicationDbContext db;
        public UnitService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Units>> GetAll()
        {
            var list = await db.Units.Include(a=>a.Project).Include(a => a.UnitType).ToListAsync();
            return list;
        }
        public async Task<Units> GetDetails(int id)
        {
            var details = await db.Units.Include(a=>a.Project).Include(a=>a.UnitType).Where(a=>a.Unit_Id == id).FirstOrDefaultAsync() ?? new Units();
            return details;
        }
        public async Task<UnitViewModel> GetPdfDetails(int id)
        {
            // Step 1: Fetch the unit with its related data
            var unit = await db.Units
                .Include(a => a.Project)
                .Include(a => a.UnitType)
                .FirstOrDefaultAsync(a => a.Unit_Id == id);

            // Step 2: If unit is found, map it to UnitViewModel
            if (unit != null)
            {
                var adminFee = await db.ProjectSellerAdminFee
                    .FirstOrDefaultAsync(a => a.Project_Id == unit.Project_Id);
                var paymentPlan = await db.Payment_Plans.Include(a=>a.Project).Where(a => a.Project_Id == unit.Project_Id && a.Plan_Status)
                    .Select(x => new PaymentPlansViewModel 
                    {
                        Project_Id = x.Project_Id,
                        Payment_Plan_Id = x.Payment_Plan_Id,
                        Created_At = x.Created_At,
                        Update_At = x.Update_At,
                        Created_By = x.Created_By,
                        Plan_Name = x.Plan_Name,
                        Plan_Status = x.Plan_Status,
                        Project = unit.Project,
                        Updated_By = unit.Updated_By,
                        TotalInstallments = db.Installments.Where(a => a.Payment_Plan_Id == x.Payment_Plan_Id).Count(),
                        InstallmentsList = db.Installments.OrderBy(a=>a.Sequence_Number).Include(a=>a.Payment_Plans).Where(a=>a.Payment_Plan_Id == x.Payment_Plan_Id)
                        .Select(d=> new InstallmentViewModel 
                        {
                            Installment_Id = d.Installment_Id,
                            Sequence_Number = d.Sequence_Number,
                            Amount = d.Amount,
                            DueDate = d.DueDate,
                            Installment_Name = d.Installment_Name,
                            Payment_Plans = d.Payment_Plans,
                            Payment_Plan_Id= d.Payment_Plan_Id,
                        }).ToList()
                    }).FirstOrDefaultAsync();


                var details = new UnitViewModel
                {
                    UnitType = unit.UnitType,
                    Unit_Id = unit.Unit_Id,
                    Unit_Number = unit.Unit_Number,
                    Unit_Type_Id = unit.Unit_Type_Id,
                    External_Unit_Size_Sqft = unit.External_Unit_Size_Sqft,
                    Unit_View = unit.Unit_View,
                    Updated_By = unit.Updated_By,
                    Created_At = unit.Created_At,
                    Project_Id = unit.Project_Id,
                    Price = unit.Price,
                    Project = unit.Project,
                    Update_At = unit.Update_At,
                    Floor_Number = unit.Floor_Number,
                    Status = unit.Status,
                    Created_By = unit.Created_By,
                    Total_Size_Sqft = unit.Total_Size_Sqft,
                    Interal_Unit_Size_Sqft = unit.Interal_Unit_Size_Sqft,
                    Project_Seller_Admin_Fee_Id = adminFee?.Project_Seller_Admin_Fee_Id ?? 0,
                    Admin_Fee_Description = adminFee?.Admin_Fee_Description ?? "",
                    Admin_Fee_Value = adminFee?.Admin_Fee_Value ?? 0,
                    OQoob_Fee_Value =adminFee?.OQoob_Fee_Value ?? 0,
                    OQood_Fee_Description = adminFee?.OQood_Fee_Description ?? "",
                    Other_Charges = adminFee?.Other_Charges ?? 0,
                    Rera_Fee = adminFee?.Rera_Fee ?? "",
                    Rera_Fee_Description = adminFee?.Rera_Fee_Description ?? "",
                    paymentPlans = paymentPlan
                };

                return details;
            }

            // Step 3: Return empty model if not found
            return new UnitViewModel();

        }
        public async Task<UnitViewModel> GetBookDetails(int id)
        {
            // Step 1: Fetch the unit with its related data
            var unit = await db.Units
                .Include(a => a.Project)
                .Include(a => a.UnitType)
                .FirstOrDefaultAsync(a => a.Unit_Id == id);
            if(unit != null)
            {
                var paymentPlans = await db.Payment_Plans.Include(a => a.Project).Where(a => a.Project_Id == unit.Project_Id && a.Plan_Status)
                    .Select(x => new PaymentPlansViewModel
                    {
                        Project_Id = x.Project_Id,
                        Payment_Plan_Id = x.Payment_Plan_Id,
                        Created_At = x.Created_At,
                        Update_At = x.Update_At,
                        Created_By = x.Created_By,
                        Plan_Name = x.Plan_Name,
                        Plan_Status = x.Plan_Status,
                        Project = unit.Project,
                        Updated_By = unit.Updated_By,
                        TotalInstallments = db.Installments.Where(a => a.Payment_Plan_Id == x.Payment_Plan_Id).Count(),
                        InstallmentsList = db.Installments.OrderBy(a => a.Sequence_Number).Include(a => a.Payment_Plans).Where(a => a.Payment_Plan_Id == x.Payment_Plan_Id)
                        .Select(d => new InstallmentViewModel
                        {
                            Installment_Id = d.Installment_Id,
                            Sequence_Number = d.Sequence_Number,
                            Amount = d.Amount,
                            DueDate = d.DueDate,
                            Installment_Name = d.Installment_Name,
                            Payment_Plans = d.Payment_Plans,
                            Payment_Plan_Id = d.Payment_Plan_Id,
                        }).ToList()
                    }).ToListAsync();

                var persons = await db.Persons.Where(a=>a.Unit_Id == unit.Unit_Id).ToListAsync();
                var companies = await db.Company.Where(a=>a.Unit_Id == unit.Unit_Id).ToListAsync();
                var buyerModel = await db.UnitBuyer.Include(a => a.Company).Include(a => a.Person).ThenInclude(p=>p.User).Where(a => a.Unit_Id == unit.Unit_Id)
                    .Select(x => new NewUnitFormViewModel
                    { 
                 BuyerType = x.BuyerType,
                 IsMainBuyer = x.IsMainBuyer, 
                 Company_Id = x.Company_Id,
                 Company = x.Company,
                 Person = x.Person,
                 Person_Id = x.Person_Id,
                 UnitBuyer_Id = x.UnitBuyer_Id,
                 Unit_Id = x.Unit_Id, 
                }).ToListAsync();
                var UnitBuyerId = await db.UnitBuyer.Where(a => a.Unit_Id == unit.Unit_Id).FirstOrDefaultAsync() ?? new UnitBuyer();
                var details = new UnitViewModel
                {
                    UnitType = unit.UnitType,
                    Unit_Id = unit.Unit_Id,
                    Unit_Number = unit.Unit_Number,
                    Unit_Type_Id = unit.Unit_Type_Id,
                    External_Unit_Size_Sqft = unit.External_Unit_Size_Sqft,
                    Unit_View = unit.Unit_View,
                    Updated_By = unit.Updated_By,
                    Created_At = unit.Created_At,
                    Project_Id = unit.Project_Id,
                    Price = unit.Price,
                    Project = unit.Project,
                    Update_At = unit.Update_At,
                    Floor_Number = unit.Floor_Number,
                    Status = unit.Status,
                    Created_By = unit.Created_By,
                    Total_Size_Sqft = unit.Total_Size_Sqft,
                    Interal_Unit_Size_Sqft = unit.Interal_Unit_Size_Sqft, 
                    PaymentPlansList = paymentPlans,
                    PersonList = persons,
                    CompanyList = companies,
                    UnitBuyers = buyerModel ,
                    UnitBuyers_Id = UnitBuyerId.UnitBuyer_Id,
                    CountryList = await db.Countries.OrderBy(a=>a.Country_Name).ToListAsync(),
                    CityList  = await db.Cities.OrderBy(a=>a.City_Name).ToListAsync()
                };

                return details;
            }
            return new UnitViewModel();
        }
        public async Task<UnitViewModel> SaveUpdateUnitBooking(UnitViewModel unitViewModel)
        {
            //var add = false;
            var exist = await db.UnitBuyer.Where(a => a.UnitBuyer_Id == unitViewModel.UnitBuyers_Id).AnyAsync();
            if(unitViewModel.UnitBuyers!=null && unitViewModel.UnitBuyers.Count() > 0)
            {
                foreach (var item in unitViewModel.UnitBuyers)
                {
                    var addBuyer = false;
                    var GetBuyer = await db.UnitBuyer.Where(a => a.UnitBuyer_Id == item.UnitBuyer_Id && a.Unit_Id == unitViewModel.Unit_Id).FirstOrDefaultAsync();
                    if (GetBuyer == null)
                    {
                        GetBuyer = new UnitBuyer();
                        addBuyer = true;
                        GetBuyer.Unit_Id = unitViewModel.Unit_Id;
                    }
                    if (item.BuyerType == "Individual")
                    {
                        var addPerson = false;
                        var person = await db.Persons.Where(a => a.Id == item.Person_Id).FirstOrDefaultAsync();
                        if (person == null)
                        {
                            person = new Person();
                            person.Created_At = DateTime.Now;
                            person.Created_By = unitViewModel.UserId;
                            addPerson = true;

                        }
                        var user = new UserViewModel
                        {
                            Email = item.PersonEmail,
                            Role = "Person",
                            DOB = item.PersonDOB,
                            Name = item.PersonFirstName + " " + item.PersonLastName,
                        };
                        var GetUserDetails = await CreateUser(user);
                        person.Unit_Id = unitViewModel.Unit_Id;
                        person.Zip_Code = item.PersonZip_Code;
                        person.City_Id = item.PersonCity_Id;
                        person.Address_Country_Id = item.PersonAddress_Country_Id;
                        person.Address_Line_1 = item.PersonAddress_Line_1;
                        person.Updated_By = unitViewModel.UserId;
                        person.Update_At = DateTime.Now;
                        person.Emirates_Id_Number = item.PersonEmirates_Id_Number;
                        person.Emireate_Id_Expiry_Date = item.PersonEmireate_Id_Expiry_Date;
                        person.Employer = item.PersonEmployer;
                        person.ContactPerson = item.PersonContactPerson;
                        person.FirstName = item.PersonFirstName;
                        person.LastName = item.PersonLastName;
                        person.Marital = item.PersonMarital;
                        person.NationalityCountry_Id = item.PersonNationalityCountry_Id;
                        person.Occupation = item.PersonOccupation;
                        person.PassportCountry_Id = item.PersonPassportCountry_Id;
                        person.PassportNationality_Id = item.PersonPassportNationality_Id;
                        person.Alternate_Phone = item.PersonAlternate_Phone;
                        person.Passport_Type = item.PersonPassport_Type;
                        person.Person_Title = item.PersonPerson_Title;
                        person.Person_Code = item.PersonPerson_Code;
                        person.Passport_Number = item.PersonPassport_Number;
                        person.Passport_Expiry_Date = item.PersonPassport_Expiry_Date;
                        person.Ownership = item.PersonOwnership;
                        person.UserId = GetUserDetails.UserId;
                        if (item.PersonFundsFile != null)
                        {
                            string fileName = Path.GetFileName(item.PersonFundsFile.FileName);
                            string fileExtension = Path.GetExtension(item.PersonFundsFile.FileName);
                            //Save
                            string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Funds");
                            if (!Directory.Exists(uploadsPath))
                            {
                                Directory.CreateDirectory(uploadsPath);
                            }
                            string filePath = Path.Combine(uploadsPath, fileName);
                            using (var filestream = new FileStream(filePath, FileMode.Create))
                            {
                                item.PersonFundsFile.CopyTo(filestream);
                            }
                            person.ProofOfFundsPath = "/Funds/" + fileName;
                        }
                        if (addPerson)
                        {
                            await db.Persons.AddAsync(person);
                        }
                        await db.SaveChangesAsync();
                        GetBuyer.Company_Id = null;
                        GetBuyer.Person_Id = person.Id;
                    }
                    else if (item.BuyerType == "Company")
                    {
                        var addComany = false;
                        var company = await db.Company.Where(a => a.Company_Id == item.Company_Id).FirstOrDefaultAsync();
                        if (company == null)
                        {
                            company = new Company();
                            company.Created_By = unitViewModel.UserId;
                            company.Created_At = DateTime.Now;
                            addComany = true;
                        }  
                        company.Company_Name = item.Company_Name;
                        company.Updated_By = unitViewModel.UserId;
                        company.Update_At = DateTime.Now;
                        company.License_Expiry_Date = item.CompanyLicense_Expiry_Date;
                        company.Address_Line_1 = item.CompanyAddress_Line_1;
                        company.City_Id = item.CompanyCity_Id;
                        company.Country_Id = item.CompanyCountry_Id;
                        company.Contact_Person_Email = item.CompanyContact_Person_Email;
                        company.Contact_Person_Designation = item.CompanyContact_Person_Designation;
                        company.Contact_Person_Emirates_Id = item.CompanyContact_Person_Emirates_Id;
                        company.Contact_Person_Passport = item.CompanyContact_Person_Passport;
                        company.Contact_Person_Phone = item.CompanyContact_Person_Phone;
                        company.Contact_Person_Name = item.CompanyContact_Person_Name;
                        company.Ownership = item.CompanyOwnership;
                        company.Zip_Code = item.CompanyZip_Code;
                        company.Trade_License_Number = item.CompanyTrade_License_Number;
                        company.Unit_Id = unitViewModel.Unit_Id;
                        if (item.CompanyProofOfFundsFile != null)
                        {
                            string fileName = Path.GetFileName(item.CompanyProofOfFundsFile.FileName);
                            string fileExtension = Path.GetExtension(item.CompanyProofOfFundsFile.FileName);
                            //Save
                            string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Funds");
                            if (!Directory.Exists(uploadsPath))
                            {
                                Directory.CreateDirectory(uploadsPath);
                            }
                            string filePath = Path.Combine(uploadsPath, fileName);
                            using (var filestream = new FileStream(filePath, FileMode.Create))
                            {
                                item.CompanyProofOfFundsFile.CopyTo(filestream);
                            }
                            company.ProofOfFundsPath = "/Funds/" + fileName;
                        }
                        if (addComany)
                        {
                            await db.Company.AddAsync(company);
                        }
                        await db.SaveChangesAsync();
                        GetBuyer.Person_Id = null;
                        GetBuyer.Company_Id = company.Company_Id;
                    }
                    GetBuyer.IsMainBuyer = item.IsMainBuyer;
                    GetBuyer.BuyerType = item.BuyerType;
                    if (addBuyer)
                    {
                        await db.UnitBuyer.AddAsync(GetBuyer);
                    }
                    await db.SaveChangesAsync();
                }
            }
            
            //if(!exist && unitViewModel.UnitBuyers!=null && unitViewModel.UnitBuyers.Count() > 0)
            //{
            //    foreach(var item in unitViewModel.UnitBuyers)
            //    {
            //        var addBuyer = false;
            //        var GetBuyer = await db.UnitBuyer.Where(a => a.UnitBuyer_Id == item.UnitBuyer_Id && a.Unit_Id == unitViewModel.Unit_Id).FirstOrDefaultAsync();
            //        if (GetBuyer == null)
            //        {
            //            GetBuyer = new UnitBuyer();
            //            addBuyer = true;
            //            GetBuyer.Unit_Id = unitViewModel.Unit_Id;
            //        }
            //        if (item.BuyerType == "Individual")
            //        {
            //            var addPerson = false;
            //            var person = await db.Persons.Where(a => a.Id == item.Person_Id).FirstOrDefaultAsync();
            //            if (person == null)
            //            {
            //                person = new Person();
            //                person.Created_At = DateTime.Now;
            //                person.Created_By = unitViewModel.UserId;
            //                addPerson = true;
                            
            //            }
            //            var user = new UserViewModel 
            //            {
            //                Email = item.PersonEmail,
            //                Role = "Person",
            //                DOB = item.PersonDOB,
            //                Name = item.PersonFirstName + " " + item.PersonLastName, 
            //            };
            //            var GetUserDetails = await CreateUser(user);
            //            person.Unit_Id = unitViewModel.Unit_Id;
            //            person.Zip_Code = item.PersonZip_Code;
            //            person.City_Id = item.PersonCity_Id;
            //            person.Address_Country_Id = item.PersonAddress_Country_Id;
            //            person.Address_Line_1 = item.PersonAddress_Line_1;
            //            person.Updated_By = unitViewModel.UserId;
            //            person.Update_At = DateTime.Now;
            //            person.Emirates_Id_Number = item.PersonEmirates_Id_Number;
            //            person.Emireate_Id_Expiry_Date = item.PersonEmireate_Id_Expiry_Date;
            //            person.Employer = item.PersonEmployer;
            //            person.ContactPerson = item.PersonContactPerson;
            //            person.FirstName = item.PersonFirstName;
            //            person.LastName = item.PersonLastName;
            //            person.Marital = item.PersonMarital;
            //            person.NationalityCountry_Id = item.PersonNationalityCountry_Id;
            //            person.Occupation = item.PersonOccupation;
            //            person.PassportCountry_Id = item.PersonPassportCountry_Id;
            //            person.Alternate_Phone = item.PersonAlternate_Phone;
            //            person.Passport_Type = item.PersonPassport_Type;
            //            person.Person_Title = item.PersonPerson_Title;
            //            person.Person_Code = item.PersonPerson_Code;
            //            person.Passport_Number = item.PersonPassport_Number;
            //            person.Passport_Expiry_Date = item.PersonPassport_Expiry_Date;
            //            person.Ownership = item.PersonOwnership;
            //            person.UserId = GetUserDetails.UserId;
            //            if(item.PersonFundsFile != null)
            //            {
            //                string fileName = Path.GetFileName(item.PersonFundsFile.FileName);
            //                string fileExtension = Path.GetExtension(item.PersonFundsFile.FileName);
            //                //Save
            //                string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Funds");
            //                if (!Directory.Exists(uploadsPath))
            //                {
            //                    Directory.CreateDirectory(uploadsPath);
            //                }
            //                string filePath = Path.Combine(uploadsPath, fileName);
            //                using (var filestream = new FileStream(filePath, FileMode.Create))
            //                {
            //                    item.PersonFundsFile.CopyTo(filestream);
            //                }
            //                person.ProofOfFundsPath = "/Funds/" + fileName; 
            //            }
            //            if (addPerson)
            //            {
            //                await db.Persons.AddAsync(person);
            //            }
            //            await db.SaveChangesAsync();
            //            GetBuyer.Company_Id = null;
            //            GetBuyer.Person_Id = person.Id; 
            //        }
            //        else if (item.BuyerType == "Company")
            //        {
            //            var addComany = false;
            //            var company = await db.Company.Where(a => a.Company_Id == item.Company_Id).FirstOrDefaultAsync();
            //            if(company == null)
            //            {
            //                company = new Company();
            //                company.Created_By = unitViewModel.UserId;
            //                company.Created_At = DateTime.Now;
            //                addComany = true;
            //            } 
            //            company.Company_Name = item.Company_Name;
            //            company.Updated_By = unitViewModel.UserId; 
            //            company.Update_At = DateTime.Now;
            //            company.License_Expiry_Date = item.CompanyLicense_Expiry_Date;
            //            company.Address_Line_1 = item.CompanyAddress_Line_1;
            //            company.City_Id = item.CompanyCity_Id;
            //            company.Country_Id = item.CompanyCountry_Id;
            //            company.Contact_Person_Email = item.CompanyContact_Person_Email;
            //            company.Contact_Person_Designation = item.CompanyContact_Person_Designation;
            //            company.Contact_Person_Emirates_Id = item.CompanyContact_Person_Emirates_Id;
            //            company.Contact_Person_Passport = item.CompanyContact_Person_Passport;
            //            company.Contact_Person_Phone = item.CompanyContact_Person_Phone;
            //            company.Contact_Person_Name = item.CompanyContact_Person_Name;
            //            company.Ownership = item.CompanyOwnership;
            //            company.Zip_Code = item.CompanyZip_Code;
            //            company.Trade_License_Number = item.CompanyTrade_License_Number;
            //            company.Unit_Id = unitViewModel.Unit_Id;
            //            if (item.CompanyProofOfFundsFile != null)
            //            {
            //                string fileName = Path.GetFileName(item.CompanyProofOfFundsFile.FileName);
            //                string fileExtension = Path.GetExtension(item.CompanyProofOfFundsFile.FileName);
            //                //Save
            //                string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Funds");
            //                if (!Directory.Exists(uploadsPath))
            //                {
            //                    Directory.CreateDirectory(uploadsPath);
            //                }
            //                string filePath = Path.Combine(uploadsPath, fileName);
            //                using (var filestream = new FileStream(filePath, FileMode.Create))
            //                {
            //                    item.CompanyProofOfFundsFile.CopyTo(filestream);
            //                }
            //                company.ProofOfFundsPath = "/Funds/" + fileName;
            //            }
            //            if (addComany)
            //            {
            //                await db.Company.AddAsync(company);
            //            }
            //            await db.SaveChangesAsync();
            //            GetBuyer.Person_Id = null;
            //            GetBuyer.Company_Id = company.Company_Id;
            //        }
            //        GetBuyer.IsMainBuyer = true;
            //        GetBuyer.BuyerType = item.BuyerType;
            //        if (addBuyer)
            //        {
            //            await db.UnitBuyer.AddAsync(GetBuyer);
            //        }
            //        await db.SaveChangesAsync();
            //    }
            //   // add = true;
            //}
            return unitViewModel;
        }
        public async Task<bool> DeleteBuyer(int buyerId)
        {
            var getBuyer = await db.UnitBuyer.Where(a=>a.UnitBuyer_Id == buyerId).FirstOrDefaultAsync();
            if (getBuyer != null)
            {
                if(getBuyer.BuyerType == "Individual" && getBuyer.Person_Id!=null)
                {
                    var person = db.Persons.Find(getBuyer.Person_Id);
                    db.Persons.Remove(person);
                    await db.SaveChangesAsync();
                    if (getBuyer.Company_Id != null)
                    {
                        var comp = db.Company.Find(getBuyer.Company_Id);
                        db.Company.Remove(comp);
                        await db.SaveChangesAsync();
                    }
                }
                else if (getBuyer.BuyerType == "Company")
                {
                    var comp = db.Company.Find(getBuyer.Company_Id);
                    db.Company.Remove(comp);
                    await db.SaveChangesAsync();
                    if (getBuyer.Person_Id != null)
                    {
                        var pers = db.Persons.Find(getBuyer.Person_Id);
                        db.Persons.Remove(pers);
                        await db.SaveChangesAsync();
                    }
                }
            }
            return true;
        }
        public async Task<UserViewModel> CreateUser(UserViewModel model)
        {
            var id = "";
            var mid = "";
            var sid = "";
            if (!await db.Users.Where(c => c.UserName == model.Email).AnyAsync())
            {
                var hasher = new PasswordHasher<Users>();
                var user = new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    NormalizedUserName = model.Email.ToUpper(),
                    NormalizedEmail = model.Email.ToUpper(),
                    Email = model.Email,
                    LockoutEnabled = false,
                    CreatedDate = DateTime.Now,
                    Role = model.Role,
                    PhoneNumber = "1122",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, model.Email + "@123"),
                    Name = model.Name,
                };
                await db.Users.AddAsync(user);
                var roleid = await db.Roles.Where(a => a.Name == model.Role).FirstOrDefaultAsync();
                if (roleid != null)
                {
                    id = roleid.Id;
                }
                else
                {
                    var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = model.Role, NormalizedName = model.Role.ToUpper() };
                    id = role.Id;
                    await db.Roles.AddAsync(role);
                    await db.SaveChangesAsync();
                }
                    await db.UserRoles.AddAsync(new IdentityUserRole<string> { RoleId = id, UserId = user.Id });

                await db.SaveChangesAsync();
                model.UserId = user.Id;
            }
            else
            {
                var user = await db.Users.Where(a=>a.UserName == model.Email).FirstOrDefaultAsync();
                //model.Email = user.Email;
                if(user != null)
                {
                    model.UserId = user.Id;
                    user.DOB = model.DOB;
                    user.Name = model.Name;
                    await db.SaveChangesAsync();
                }
               
            }
                return model;
        }
    }
    
}
