using Azure;
using DMS.Data;
using DMS.Models;
using DMS.Models.ViewModels;
using DMS.Repositories;
using DMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DMS.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IUsers _usersRepository;
        public AccountsController(IUsers usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _usersRepository.GetAllUsers(User);
                if (!result.HasAccess)
                {
                    return RedirectToAction("AccessDenied", "Accounts");
                }

                return View(result.Items);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                // Log the error message or handle it accordingly
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddEditUser(string? id)
        {

            try
            {
                UserViewModel userViewModel = new UserViewModel();
                if (id != null)
                {
                    var response = await _usersRepository.GetUserById(User, id);
                    if (response != null)
                    {
                        if (response.HasAccess)
                        {
                            var rolesResponse = await _usersRepository.GetRoles();
                            ViewBag.Role = new SelectList(rolesResponse.Items, "Id", "Name", response?.Item?.RoleId);


                            return View(response?.Item); // Return an empty user if no ID is provided
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied", "Accounts");
                        }

                    }
                }
                else
                {
                    var response = await _usersRepository.GetAllUsers(User);
                    if (response != null)
                    {
                        if (response.HasAccess)

                        {
                            var rolesResponse = await _usersRepository.GetRoles();
                            ViewBag.Role = new SelectList(rolesResponse.Items, "Id", "Name", rolesResponse);
                            return View(new UserViewModel());
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied", "Accounts");
                        }
                    }

                }
                return View(new UserViewModel());
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View(new Users());
        }
        [HttpPost]
        public async Task<IActionResult> AddEditUser(UserViewModel model)
        {

            if (model.Password != model.ConfirmPassword )
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                var rolesResponse = await _usersRepository.GetRoles();
                ViewBag.Role = new SelectList(rolesResponse.Items, "Id", "Name", model.RoleId);

                return View(model);
            }
            if (!model.IsEdit)
            {
                if (model.Password == null || model.ConfirmPassword == null)
                {
                    ModelState.AddModelError("Password and Confirm Passwrod ", "Please Check Your Password and Confirm Password.");

                    var rolesResponse = await _usersRepository.GetRoles();
                    ViewBag.Role = new SelectList(rolesResponse.Items, "Id", "Name", model.RoleId);

                    return View(model);
                }
            }
           
            if (model.RoleId == null)
            {
                ModelState.AddModelError("Password and Confirm Passwrod ", "Please Check Your Password and Confirm Password.");

                var rolesResponse = await _usersRepository.GetRoles();
                ViewBag.Role = new SelectList(rolesResponse.Items, "Id", "Name", model.RoleId);

                return View(model);
            }
            var response = await _usersRepository.AddEditUser(model, User);

            if (!string.IsNullOrEmpty(response.Message))
            {
                
                //TempData["AlertMessage"] = response.Message;
                if (response.Message == "Invalid file type." || response.Message == "File size exceeds 800 KB.")
                {
                    string errorMessage = string.Join(", ", response.Message);
                    TempData["AlertMessage"] = SweetAlertDanger.ShowSweetAlert(errorMessage);
                    var rolesResponse = await _usersRepository.GetRoles();
                    ViewBag.Role = new SelectList(rolesResponse.Items, "Id", "Name",model.RoleId);
                    
                    return View(model);
                }
            }
            
            TempData["AlertMessage"] = SweetAlert.ShowSweetAlert(response.Message);
            return RedirectToAction("Index"); // or redirect based on response
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var reponse = await _usersRepository.Delete(id);
                // Check if the user exists
                if (reponse == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                

                if (reponse.Message == "User deleted successfully.")
                {
                    return Json(new { success = true, message = "User has been deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete User." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)

                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

       // [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
