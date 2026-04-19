using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Nutrilife.LogicLayer.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IEmailSender _EmailSender;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileService _fileService;

        public AuthenticationService(UserManager<ApplicationUser> UserManager,
            IEmailSender emailSender,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor, 
            ApplicationDbContext dbContext, IFileService fileService) 
        {
            _UserManager = UserManager;
            _EmailSender = emailSender;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _fileService = fileService;
        }  


         public async Task<RegisterResponse> RegisterAsync(ClientRequest request)
        {
            var user = request.Adapt<Client>();
            var result = await _UserManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResponse() { Success = false, Message = errors };
            }

            await _UserManager.AddToRoleAsync(user, "Client");


            var token = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);

            var EmailUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Account/ConfirmEmail?token={token}&userid={user.Id}";
            // للتاكد انه الي دخل على الصفحة وصلته رسالة عالايميل, مش حدت عشوائي استخدم الرابط

            await _EmailSender.SendEmailAsync(user.Email, "welcom",
                " <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"max-width:600px;margin:auto;background:#ffffff;" +
                "font-family:Arial,sans-serif;border-radius:8px;overflow:hidden;\">\n  \n  <tr>\n   " +
                " <td style=\"background:#4CAF50;color:#ffffff;text-align:center;padding:20px;font-size:22px;font-weight:bold;\">\n   " +
                "   {{NutriLife}}\n    </td>\n  </tr>\n\n  <tr>\n   " +
                " <td style=\"padding:30px;color:#333;font-size:16px;line-height:1.6;\">\n    " +
                $"  \n      <p>Hi {user.FullName},</p>\n\n      <p>Thanks for registering! Please confirm your email" +
                " address to activate your account.</p>\n\n   " +
                $"   <div style=\"text-align:center;margin:30px 0;\">\n        <a href=\"{EmailUrl}\" \n   " +
                "        style=\"background:#4CAF50;color:#ffffff;text-decoration:none;padding:12px 24px;" +
                "border-radius:5px;display:inline-block;font-size:16px;\">\n    " +
                "      Confirm Account\n        </a>\n      </div>\n\n      <p>If you didn’t create this account," +
                " you can ignore this message.</p>\n\n      <p>— The {{NutriLife}} Team</p>\n\n    </td>\n  </tr>\n\n  <tr>\n " +
                "   <td style=\"background:#f4f4f4;text-align:center;padding:15px;font-size:12px;color:#777;\">\n   " +
                "   © {{2026}} {{NutriLife}}\n    </td>\n  </tr>\n\n</table> "
                );

            return new RegisterResponse() { Success = true, Message = "Success" };

        }

        public async Task<RegisterResponse> RegisterNutritionistAsync(NutritionistRequest request)
        {
            var user = request.Adapt<Nutritionist>();

            var result = await _UserManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResponse { Success = false, Message = errors };
            }

            await _UserManager.AddToRoleAsync(user, "Nutritionist");

            var token = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);
            var EmailUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Account/ConfirmEmail?token={token}&userid={user.Id}";
            // للتاكد انه الي دخل على الصفحة وصلته رسالة عالايميل, مش حدت عشوائي استخدم الرابط

            await _EmailSender.SendEmailAsync(user.Email, "welcom",
               " <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"max-width:600px;margin:auto;background:#ffffff;" +
               "font-family:Arial,sans-serif;border-radius:8px;overflow:hidden;\">\n  \n  <tr>\n   " +
               " <td style=\"background:#4CAF50;color:#ffffff;text-align:center;padding:20px;font-size:22px;font-weight:bold;\">\n   " +
               "   {{NutriLife}}\n    </td>\n  </tr>\n\n  <tr>\n   " +
               " <td style=\"padding:30px;color:#333;font-size:16px;line-height:1.6;\">\n    " +
               $"  \n      <p>Hi {user.FullName},</p>\n\n      <p>Thanks for registering! Please confirm your email" +
               " address to activate your account.</p>\n\n   " +
               $"   <div style=\"text-align:center;margin:30px 0;\">\n        <a href=\"{EmailUrl}\" \n   " +
               "        style=\"background:#4CAF50;color:#ffffff;text-decoration:none;padding:12px 24px;" +
               "border-radius:5px;display:inline-block;font-size:16px;\">\n    " +
               "      Confirm Account\n        </a>\n      </div>\n\n      <p>If you didn’t create this account," +
               " you can ignore this message.</p>\n\n      <p>— The {NutriLife}} Team</p>\n\n    </td>\n  </tr>\n\n  <tr>\n " +
               "   <td style=\"background:#f4f4f4;text-align:center;padding:15px;font-size:12px;color:#777;\">\n   " +
               "   © {{2026}} {{NutriLife}}\n    </td>\n  </tr>\n\n</table> "
               );

            return new RegisterResponse() { Success = true, Message = "Success" };

        }


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponse() { Success = false, Message = "Invalid Email" };
            }

            if (! await _UserManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse() { Success = false, Message = "Email is not confirmed" };
            }

            var result = await _UserManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new LoginResponse() { Success = false, Message = "Invalid Passwoed" };
            }

            return new LoginResponse() { Success = true, Message = "succcess", AccessToken = await GenerateAccessToken(user)};

        }

        public async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var roles = await _UserManager.GetRolesAsync(user);
            var UserClaims = new List<Claim>()
            { // المعلومات الي باخذها لما افك تشفير التوكن
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
       issuer: _configuration["Jwt:Issuer"],
       audience: _configuration["Jwt:Audience"],
       claims: UserClaims,
       expires: DateTime.Now.AddDays(5),
       signingCredentials: credentials
       );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ConfirmEmailAsync(string token, string UserId)
        {
            var user = await _UserManager.FindByIdAsync(UserId);
            if (user == null) {return false;}

            var result = await _UserManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            return true;

        }


        // if err happend in email, user could 
        public async Task<bool> ResendConfirmationEmailAsync(string email)
        {
            var user = await _UserManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found");

            if (user.EmailConfirmed)
                throw new Exception("Email is already confirmed");

            var token = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);


            var EmailUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Account/ConfirmEmail?token={token}&userid={user.Id}";

            await _EmailSender.SendEmailAsync(user.Email, "welcom", $"<h1> welcom {user.UserName} </h1>" + "  "
                 + $"<a href='{EmailUrl}'> confirm </a> ");

            return true;
        }

        
        // send 4 numbers code (available for 15 min) that allow user to reset his password
        public async Task<ResetPasswordResponse> resetPasswordAsync(ResendConfirmationEmailDTO request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);
            if(user == null) // نتأكد انه اليوزر موجود
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message="email not found"
                };
            }

            var random = new Random();
            var code = random.Next(1000, 9999).ToString(); // كود عشوائي من 4 ارقام

            user.codeResetPassword = code;
            user.passwordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15); //  الكود صالح لمدة 15 دقيقة

            await _UserManager.UpdateAsync(user);

            await _EmailSender.SendEmailAsync(request.Email, "reset Password", $"<p> code is {code} </p>");

            return new ResetPasswordResponse()
            {
                Success = true,
                Message = "Code Sent To Your Email"
            };
        }

        public async Task<ResetPasswordResponse> NewPasswordAsync(NewPasswordRequest request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);
            if (user == null) // نتأكد انه اليوزر موجود
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "email not found"
                };
            }
            else if(user.codeResetPassword != request.Code)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "Invalid Code"
                };
            }
            else if(user.passwordResetCodeExpiry < DateTime.UtcNow)
            {
                    return new ResetPasswordResponse()
                    {
                        Success = false,
                        Message = "Code Expired"
                    };
            }

            var isSmaePass = await _UserManager.CheckPasswordAsync(user, request.NewPassword);
            if (isSmaePass)
            {
                    return new ResetPasswordResponse()
                    {
                        Success = false,
                        Message = "This is your old Pass! The new must be different."
                    };
            }

            var token = await _UserManager.GeneratePasswordResetTokenAsync(user); //ما الها فائدة هنا, لانه الطريقة ما تطلب توكن, الاستخدام حتى يروح الايرور في ميثود الابديت
           var result=  await _UserManager.ResetPasswordAsync(user,token, request.NewPassword);
            if (!result.Succeeded)
            {
                    return new ResetPasswordResponse()
                    {
                        Success = false,
                        Message = "Password reset Faild!"
                    };
            }

            await _EmailSender.SendEmailAsync(request.Email, "Change Password", "$<p> Your Password Changed Successfully.. </p>");


             return new ResetPasswordResponse()
                {
                    Success = true,
                    Message = "Changed Successfully.."
                };
        }

        public async Task<List<ClientResponse>> GetAllClientsInNutrilife()
        {
            var clients = await _dbContext.Users.OfType<Client>().ToListAsync();

            return clients.Adapt<List<ClientResponse>>();
        }
        public async Task<List<NutritionistResponse>> GetAllNutritionistInNutrilife()
        {
            var Nutri = await _dbContext.Users.OfType<Nutritionist>().ToListAsync();

            return Nutri.Adapt<List<NutritionistResponse>>();
        }


        public async Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            //  Find the user
            var user = await _UserManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return new DeleteAccountResponse { Success = false, Message = "User not found." };
            }
            var isPasswordCorrect = await _UserManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordCorrect)
                return new DeleteAccountResponse { Success = false, Message = "Incorrect password." };


            // Block if active subscription exists
            var hasActiveSubscription = await _dbContext.Subscriptions
                .AnyAsync(s => (s.ClientId == request.Id || s.NutritionistId == request.Id)
                            && s.Status == SubscriptionStatus.Active);

            if (hasActiveSubscription)
                return new DeleteAccountResponse
                {
                    Success = false,
                    Message = "Cannot delete account while you have an active subscription. Please cancel it first."
                };

            // 3. Soft delete 
            await DeleteProfileImgAsync(); // delete from db and server 
            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
            user.Email = $"deleted_{request.Id}@deleted.com";      // free up the email
            user.UserName = $"deleted_{request.Id}";               // free up the username
            user.NormalizedEmail = $"DELETED_{request.Id}@DELETED.COM";
            user.NormalizedUserName = $"DELETED_{request.Id}";

            var result = await _UserManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new DeleteAccountResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };

            return new DeleteAccountResponse { Success = true, Message = "Account deleted successfully." };
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<ProfileImageResponse> AddProfileImgAsync(UploadProfileImageRequest request)
        {
            // check file
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentException("No file uploaded.");

            // check extention
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(request.File.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("Invalid file type. Only jpg, jpeg, png, and webp are allowed.");


            var user = await _dbContext.Users.FindAsync(GetCurrentUserId());
            //حذف الصورة القديمة
            if (!string.IsNullOrEmpty(user.ProfileImage))
            { await _fileService.DeleteAsync(user.ProfileImage); }

            var fileName = await _fileService.UploadAsync(request.File);

            user.ProfileImage = fileName;
            await _UserManager.UpdateAsync(user);

            return new ProfileImageResponse() 
            {
                ProfileImage = fileName,
                ProfileImageUrl = BuildImageUrl(fileName)
            };
        }

        public async Task<ProfileImageResponse> GetProfileImgAsync()
        {
            var userId = GetCurrentUserId();
            var user = await _dbContext.Users.FindAsync(userId)
                ?? throw new Exception("User not found.");

            if (string.IsNullOrEmpty(user.ProfileImage))
                throw new Exception("No profile image found.");

            return new ProfileImageResponse
            {
                ProfileImage = user.ProfileImage,
                ProfileImageUrl = BuildImageUrl(user.ProfileImage)
            };
        }

        public async Task DeleteProfileImgAsync()
        {
            var userId = GetCurrentUserId();
            var user = await _dbContext.Users.FindAsync(userId)
                ?? throw new Exception("User not found.");

            if (string.IsNullOrEmpty(user.ProfileImage))
                throw new Exception("No profile image to delete.");

            // 1. Delete from wwwroot
           await _fileService.DeleteAsync(user.ProfileImage); // my method in fileservice

            // 2. Clear from DB
            user.ProfileImage = null;
            await _UserManager.UpdateAsync(user);
        }

        private string BuildImageUrl(string fileName)
        {
            // Returns: https://nutrilife.runasp.net/images/profiles/filename.jpg
            var baseUrl = _configuration["AppSettings:BaseUrl"]
                ?? $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            return $"{baseUrl}/images/profiles/{fileName}";
        }
    }
}
