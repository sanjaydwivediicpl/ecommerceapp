namespace ProjectManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DapperProject;
    using DapperProject.Models;
    using System.Threading.Tasks;

    /// <summary>  
    /// Account controller class.    
    /// </summary>  
    public class AccountController : Controller
    {
        #region Private Properties    
        /// <summary>  
        /// Database Store property.    
        /// </summary>  
        private ShopingCart databaseManager = new ShopingCart();
        #endregion
        #region Default Constructor    
        /// <summary>  
        /// Initializes a new instance of the <see cref="AccountController" /> class.    
        /// </summary>  
        public AccountController()
        {
        }
        #endregion
        #region Login methods    
        /// <summary>  
        /// GET: /Account/Login    
        /// </summary>  
        /// <param name="returnUrl">Return URL parameter</param>  
        /// <returns>Return login view</returns>  
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                // Verification.    
                if (this.Request.IsAuthenticated)
                {
                    // Info.    
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info.    
            return this.View();
        }
        /// <summary>  
        /// POST: /Account/Login    
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <param name="returnUrl">Return URL parameter</param>  
        /// <returns>Return login view</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                // Verification.    
                if (ModelState.IsValid)
                {
                    // Initialization.    
                    var loginInfo = this.databaseManager.AppUsers.ToList().Where(e => e.UserName == model.Username && e.Password == model.Password).ToList();
                    // Verification.    
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        // Initialization.    
                        var logindetails = loginInfo.First();
                        // Login In.    
                        this.SignInUser(logindetails.UserName, false);
                        Session.Add("UserIdentity", logindetails.UserName);
                        // Info.    
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        // Setting.    
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // If we got this far, something failed, redisplay form    
            return this.View(model);
        }
        #endregion
        #region Log Out method.    
        /// <summary>  
        /// POST: /Account/LogOff    
        /// </summary>  
        /// <returns>Return log off action</returns>  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                // Setting.    
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign Out.    
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Login", "Account");
        }
        #endregion
        #region Helpers    
        #region Sign In method.    
        /// <summary>  
        /// Sign In User method.    
        /// </summary>  
        /// <param name="username">Username parameter.</param>  
        /// <param name="isPersistent">Is persistent parameter.</param>  
        private void SignInUser(string username, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }
        #endregion
        #region Redirect to local method.    
        /// <summary>  
        /// Redirect to local method.    
        /// </summary>  
        /// <param name="returnUrl">Return URL parameter.</param>  
        /// <returns>Return redirection action</returns>  
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Home");
        }
        #endregion
        #endregion

        [Authorize(Roles = "Admin")]
        public ActionResult RoleCreate()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(string roleName)
        {
            using (var context = new ShopingCart())
            {
                AspNetRole role = new AspNetRole();
                role.Name = roleName;
                var result = context.AspNetRoles.Add(role);
                context.SaveChanges();
            }

            ViewBag.ResultMessage = "Role created successfully !";
            return RedirectToAction("RoleIndex", "Account");
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult RoleIndex()
        {
            List<string> roles;
            using (var context = new ShopingCart())
            {
                roles = context.AspNetRoles.ToList().Select(u => u.Name).ToList();
            }
            return View(roles.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RoleDelete(string roleName)
        {
            using (var context = new ShopingCart())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = roleManager.FindByName(roleName);

                roleManager.Delete(role);
                context.SaveChanges();
            }

            ViewBag.ResultMessage = "Role deleted succesfully !";
            return RedirectToAction("RoleIndex", "Account");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RoleAddToUser()
        {
            List<string> roles;
            List<string> users;
            using (var context = new ShopingCart())
            {
                users = context.AppUsers.ToList().Select(u => u.UserName).ToList();
                roles = context.AspNetRoles.ToList().Select(u => u.Name).ToList();
            }

            ViewBag.Roles = new SelectList(roles);
            ViewBag.Users = new SelectList(users);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string roleName, string userName)
        {
            List<string> roles;
            List<string> users;
            using (var context = new ShopingCart())
            {
                var roleManager = context.AspNetRoles.ToList();
                var user = context.AppUsers.ToList().Where(u => u.UserName == userName).FirstOrDefault();
                if (user == null)
                    throw new Exception("User not found!");

                var role = roleManager.Where(u => u.Name == roleName).FirstOrDefault();
                if (role == null)
                    throw new Exception("Role not found!");

                if (User.IsInRole(role.Name))
                {
                    ViewBag.ResultMessage = "This user already has the role specified !";
                }
                else
                {
                    var uRole = new UserRole();
                    uRole.UserId = user.Id;
                    uRole.RoleName = roleName;
                    context.UserRoles.Add(uRole);
                    context.SaveChanges();

                    ViewBag.ResultMessage = "Username added to the role succesfully !";
                }

                roles = context.AspNetRoles.ToList().Select(u => u.Name).ToList();
                users = context.AppUsers.ToList().Select(u => u.UserName).ToList();
            }

            ViewBag.Roles = new SelectList(roles);
            ViewBag.Users = new SelectList(users);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                List<string> userRoles;
                List<string> roles;
                List<string> users;
                using (var context = new ShopingCart())
                {
                    roles = context.AspNetRoles.ToList().Select(u => u.Name).ToList();

                    users = context.AppUsers.ToList().Select(u => u.UserName).ToList();

                    var userId = context.AppUsers.Find(userName).Id;

                    var userRoleIds = context.AspNetUserRoles.ToList().Where(u => u.UserId == userId).FirstOrDefault().RoleId;
                    if (userId == 0)
                        throw new Exception("User not found!");

                    userRoles = context.AspNetRoles.ToList().Where(e => e.Id == userRoleIds).Select(u => u.Name).ToList();

                }

                ViewBag.Roles = new SelectList(roles);
                ViewBag.Users = new SelectList(users);
                ViewBag.RolesForThisUser = userRoles;
            }

            return View("RoleAddToUser");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                AppUser objAppUser = new AppUser { UserName = model.Email, Password = model.Password, CreatedDate = DateTime.Now, CreatedBy ="Admkn",EditStatus =1, Role ="Admin" };
                var result = databaseManager.AppUsers.Add(objAppUser);
                databaseManager.SaveChanges();
                //if (result>0)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //    // Send an email with this link
                //    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                //    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                //    return RedirectToAction("Index", "Home");
                //}
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}