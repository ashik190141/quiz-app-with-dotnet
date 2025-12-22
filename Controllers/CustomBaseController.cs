using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuizApp.Controllers
{
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        protected string? GetUserId()
        {
            return User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        }

        protected string? GetUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }

        protected int? GetUserRole()
        {
            var roleString = User.FindFirst(ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(roleString) && int.TryParse(roleString, out int roleId))
            {
                return roleId;
            }
            return null;
        }

        protected bool IsAdminUser()
        {
            var roleId = GetUserRole();
            return roleId == (int)Roles.SuperAdmin;
        }

        protected string? GetJwtToken()
        {
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                Console.WriteLine(authorizationHeader.Substring("Bearer ".Length).Trim());
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }
    }
}