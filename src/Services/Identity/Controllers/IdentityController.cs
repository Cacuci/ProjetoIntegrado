using Duende.IdentityServer;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityServerTools _tools;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IdentityServerTools tools)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tools = tools;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(userModel.UserName,
                                                                  userModel.Password,
                                                                  false,
                                                                  false);

            if (!result.Succeeded)
            {
                return BadRequest("Usuário ou senha inválidos");
            }

            var user = await _userManager.FindByNameAsync(userModel.UserName);

            var token = await _tools.IssueClientJwtAsync(
                clientId: user.Id,
                lifetime: 3600);

            return Ok(token);
        }
    }
}
