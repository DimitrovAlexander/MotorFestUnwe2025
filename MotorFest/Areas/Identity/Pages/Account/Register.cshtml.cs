// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MotorFest.Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotorFest.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MFUser> _signInManager;
        private readonly UserManager<MFUser> _userManager;



        public RegisterModel(
            UserManager<MFUser> userManager,
            IUserStore<MFUser> userStore,
            SignInManager<MFUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;

            _signInManager = signInManager;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }



        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Имейл адресът е задължителен")]
            [EmailAddress(ErrorMessage = "Имейл адресът не е валиден")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Паролата е задължителна")]
            [StringLength(100, ErrorMessage = "Паролата трябва да бъде миниму 6 символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат!")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "Името е задължително")]
            [RegularExpression(@"^[a-zA-Z\u0410-\u044F]+$", ErrorMessage = "Името трябва да съдържа само букви (латиница или кирилица).")]
            [StringLength(30, ErrorMessage = "Името трябва да бъде между 3 и 30 символа.", MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Потребителското име е задължително")]
            [StringLength(30, ErrorMessage = "Потребителското име трябва да бъде между 3 и 30 символа", MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            public string Usernmae { get; set; }
            [Required(ErrorMessage = "Фамилията е задължителна")]
            [StringLength(100, ErrorMessage = "Фамилията трябва да бъде между 3 и 30 символа", MinimumLength = 3)]
            [RegularExpression(@"^[a-zA-Z\u0410-\u044F]+$", ErrorMessage = "Фамилията трябва да съдържа само букви (латиница или кирилица).")]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Ролята е задължителна")]
            [StringLength(100, ErrorMessage = "Ролята трбява да бъде минимум 3 символа", MinimumLength = 3)]
            [DataType(DataType.Text)]
            public string Role { get; set; }
            [Required(ErrorMessage = "ЕГН/БУЛСТАТе задължителен")]
            [StringLength(10, ErrorMessage = "ЕГН/БУЛСТАТ трябва да бъде 10 символа", MinimumLength = 10)]
            [DataType(DataType.Text)]
            [Display(Name = "Identifier")]
            public string Identifier { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new MFUser
                {
                    UserName = Input.Usernmae,
                    Email = Input.Email,
                    Firstname = Input.FirstName,
                    Lastname = Input.LastName,
                    Identifier = Input.Identifier,
                };
                if (Input.Role.Length > 4)
                {
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    var roleAssignmentResult = await _userManager.AddToRoleAsync(user, Input.Role);
                    if (result.Succeeded)
                    {


                        var userId = await _userManager.GetUserIdAsync(user);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        user.EmailConfirmed = true;

                        return Redirect("/");

                    }
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "InvalidUserName")
                        {
                            error.Description = "Потребителското име трябва да съдържа само букви и цифри, без празни места";

                        }
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                //ModelState.AddModelError(string.Empty, "Ролята е заължителна");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private MFUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<MFUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(MFUser)}'. " +
                    $"Ensure that '{nameof(MFUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


    }
}
