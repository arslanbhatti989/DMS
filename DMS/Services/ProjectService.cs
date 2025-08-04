using DMS.Data;
using DMS.Models;
using DMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DMS.Services
{
    public class ProjectService : IProjectRepository
    {
        private readonly ApplicationDbContext db;
        public ProjectService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Project>> List()
        {
            var list = await db.Projects.Include(a=>a.City).Include(a=>a.Country).OrderByDescending(a=>a.Created_At).ToListAsync();
            return list;
        }
        public async Task<Project> GetDetails(int id)
        {
            var model = await db.Projects.Include(a=>a.Country).Include(a=>a.City).Where(a=>a.Project_Id == id).FirstOrDefaultAsync() ?? new Project();
            return model;
        }
        public async Task<bool> Delete(int id)
        {
            var Get =  db.Projects.Where(a => a.Project_Id == id).FirstOrDefault();
            if(Get != null)
            {
                var GetPaymentPlan = await db.Payment_Plans.Where(a => a.Project_Id == id).FirstOrDefaultAsync();
                if (GetPaymentPlan != null)
                {
                    var Installments = await db.Installments.Where(a => a.Payment_Plan_Id == GetPaymentPlan.Payment_Plan_Id).ToListAsync();
                    db.Installments.RemoveRange(Installments);
                    await db.SaveChangesAsync();
                    db.Payment_Plans.Remove(GetPaymentPlan);
                    await db.SaveChangesAsync();
                }

                db.Projects.Remove(Get);
                await db.SaveChangesAsync();
            }
            
            return true;
        }
        public async Task<Project> AddUpdate(Project model)
        {
            var project = await db.Projects.Where(a=>a.Project_Id == model.Project_Id).FirstOrDefaultAsync();
            var add = false;
            if (project == null)
            {
                project = new Project();
                add = true;
                project.Created_By = model.Created_By;
                project.Created_At = DateTime.Now;
            }
            project.Updated_At = DateTime.Now;
            project.Updated_By = model.Updated_By;
            project.City_Id = model.City_Id;
            project.Constructed_Area = model.Constructed_Area;
            project.Construction_Status = model.Construction_Status;
            project.Country_Id = model.Country_Id;
            project.Plot_Number = model.Plot_Number;
            project.Project_Address = model.Project_Address;
            project.Project_Land_Area = model.Project_Land_Area;
            project.Project_Name = model.Project_Name;
            project.Project_Seller_Id = model.Project_Seller_Id;
            project.Project_Used = model.Project_Used;
            project.Saleable_Area = model.Saleable_Area;
            project.Total_Floors = model.Total_Floors;
            if (add)
            {
                db.Projects.Add(project);
            }
             db.SaveChanges();
            return model;
        }
    }
}
