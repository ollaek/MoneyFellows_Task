using MF_Task.Infrastructure.Data;
using MF_Task.Service.DTOs;
using Microsoft.AspNetCore.Identity;

namespace MF_Task.Service.Commands.Handlers
{
    public class RegisterUserCommandHandler : BaseCommandHandler<RegisterUserCommand, BaseResponseDTO<object>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<BaseResponseDTO<object>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Check if username or email already exists
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                return CreateFailureResponse("Username is already taken.");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                return CreateFailureResponse("Email is already taken.");
            }

            // Create a new user
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Optionally assign roles or additional logic
                await _userManager.AddToRoleAsync(user, "User");

                return CreateSuccessResponse("User registered successfully.");
            }

            return CreateFailureResponse("User registration failed", result.Errors.Select(e => e.Description).ToList());
        }
    }
}