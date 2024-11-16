using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MF_Task.Service.DTOs;
using MF_Task.Infrastructure.Data;

namespace MF_Task.Service.Queries.Handlers
{
    public class LoginQueryHandler : BaseQueryHandler<LoginQuery, BaseResponseDTO<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginQueryHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public override async Task<BaseResponseDTO<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return CreateFailureResponse("Invalid username or password.");
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (result.Succeeded)
        {
            // Generate JWT Token
            var token = GenerateJwtToken(user);

            return CreateSuccessResponse("Login successful", token);
        }

        return CreateFailureResponse("Invalid username or password.");
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private BaseResponseDTO<string> CreateSuccessResponse(string message, string token)
    {
        return new BaseResponseDTO<string>(true, message, null, token);
    }

    private BaseResponseDTO<string> CreateFailureResponse(string message)
    {
        return new BaseResponseDTO<string>(false, message, new List<string>());
    }
}
}
