using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Nutrilife.LogicLayer.Service;
using Nutrilife.DataAccessLayer.DTO.Request;
using IAuthenticationService = Nutrilife.LogicLayer.Service.IAuthenticationService;
using RegisterRequest = Nutrilife.DataAccessLayer.DTO.Request.RegisterRequest;
using LoginRequest = Nutrilife.DataAccessLayer.DTO.Request.LoginRequest;
using Microsoft.AspNetCore.Authorization;

namespace NutriLife.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IFileService _fileService;

        public AccountController(IAuthenticationService authenticationService, 
            IFileService fileService)
        {
            _authenticationService = authenticationService;
            _fileService = fileService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ClientRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("register/nutritionist")]
        public async Task<IActionResult> RegisterNutritionist(NutritionistRequest request)
        {
            var result = await _authenticationService.RegisterNutritionistAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);


            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail( string token,  string UserId)
        {
            var isConfirmed = await _authenticationService.ConfirmEmailAsync(token, UserId);

            if (!isConfirmed) { return BadRequest(); }
            return Ok();
        }


        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail(ResendConfirmationEmailDTO request)
        {
            var result = await _authenticationService
                .ResendConfirmationEmailAsync(request.Email);
            return Ok(new { message = "Confirmation email sent successfully" });
        }

        [HttpPost("sendCode")]
        public async Task<IActionResult> ResetPasswordRequest(ResendConfirmationEmailDTO request)
        {
           var result = await _authenticationService.resetPasswordAsync(request);

            if (!result.Success) {return BadRequest(result); }

            return Ok(result);
        }


        [HttpPut("ChangePass")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest  request)
        {
            var result = await _authenticationService.ChangePassword(request);
            if (result== null) { return BadRequest(result); }

            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> NewPassword(NewPasswordRequest request)
        {
            var result = await _authenticationService.NewPasswordAsync(request);

            if (!result.Success) { return BadRequest(result); }

            return Ok(result);
        }

        [HttpGet("allClients")]
        public async Task<IActionResult> AllClients()
        {
            var result = await _authenticationService.GetAllClientsInNutrilife();
            if(result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("allNutritionists")]
        public async Task<IActionResult> allNutritionists()
        {
            var result = await _authenticationService.GetAllNutritionistInNutrilife();
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("deleteAccount")]
        public async Task<IActionResult> DeleteAccount(DeleteAccountRequest request)
        {
            var result = await _authenticationService.DeleteAccountAsync(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        
        [HttpGet("myprofileimg")]
        public async Task<IActionResult> GetProfileImage()
        {
            try
            {
                var result = await _authenticationService.GetProfileImgAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPost("addprofileimg")]
        [Authorize]
        public async Task<IActionResult> UploadProfileImage(
        [FromForm] UploadProfileImageRequest request)
        {
            try
            {
                var result = await _authenticationService.AddProfileImgAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("deleteprofileimg")]
        [Authorize]
        public async Task<IActionResult> DeleteProfileImage()
        {
            try
            {
                await _authenticationService.DeleteProfileImgAsync();
                return Ok(new { message = "Profile image deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
    }
}
