using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Models;
    using AlgorithmsRanking.Services;

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ResearchRepository _db;

        public AccountController(ResearchRepository repository)
        {
            _db = repository;
        }


        [HttpGet("who-am-i")]
        public IActionResult WhoAmI()
        {
            try
            {
                var user = new UserInfo
                {
                    Id = Int32.Parse(User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value),
                    PersonId = Int32.Parse(User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value),
                    UserName = User?.Identity?.Name,
                    DisplayName = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    Role = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                    AvatarUri = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Uri)?.Value,
                    Email = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    Phone = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value
                };

                return new JsonResult(user);
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserCredentials cred)
        {
            if (cred == null || String.IsNullOrEmpty(cred.UserName) || String.IsNullOrEmpty(cred.Password))
            {
                return BadRequest(new ApiError("401", "Не удалось авторизоваться", "Не введены логин и/или пароль"));
            }

            var account = await _db.GetAccountAsync(cred.UserName);
            var person = await _db.GetPersonAsync(account.PersonId);

            if (account == null)
            {
                return NotFound(new ApiError("401", "Не удалось авторизоваться", "Пользователь с таким именем не найден"));
            }

            if (cred.Password != account.Password)
            {
                return BadRequest(new ApiError("401", "Не удалось авторизоваться", "Введен неправильный пароль"));
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, account.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim(ClaimTypes.Uri, account.AvatarUri)
            };

            if (person != null)
            {
                claims.Add(new Claim(ClaimTypes.PrimarySid, person.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.GivenName, person.ShortName));
                claims.Add(new Claim(ClaimTypes.Email, person.Email));
                claims.Add(new Claim(ClaimTypes.MobilePhone, person.Phone));
            }

            var id = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );

            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError("403", "Не удалось авторизоваться", ex.Message));
            }

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody]int accountId)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { loggedOut = true });
        }
    }
}
