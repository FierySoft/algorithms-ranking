using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Models;
    using AlgorithmsRanking.Entities;
    using AlgorithmsRanking.Services;

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ResearchRepository _db;
        private readonly HttpContext _httpContext;

        public AccountController(ResearchRepository repository, IHttpContextAccessor contextAccessor)
        {
            _db = repository;
            _httpContext = contextAccessor.HttpContext;
        }


        [Authorize(Policy = "ReadOnlyAccess")]
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

            if (account.Person != null)
            {
                claims.Add(new Claim(ClaimTypes.PrimarySid, account.Person.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.GivenName, account.Person.ShortName));
                claims.Add(new Claim(ClaimTypes.Email, account.Person.Email));
                claims.Add(new Claim(ClaimTypes.MobilePhone, account.Person.Phone));
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

                var loginActivity = new AccountActivity
                {
                    AccountId = account.Id,
                    IpAddress = _httpContext.Connection.RemoteIpAddress.ToString(),
                    Operation = "Вход в систему",
                    At = DateTime.Now
                };

                await _db.CreateAccountActivityAsync(loginActivity);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError("403", "Не удалось авторизоваться", ex.Message));
            }

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody]LogoutForm logout)
        {
            var logoutActivity = new AccountActivity
            {
                AccountId = logout.AccountId,
                IpAddress = _httpContext.Connection.RemoteIpAddress.ToString(),
                Operation = "Выход из системы",
                At = DateTime.Now
            };

            await _db.CreateAccountActivityAsync(logoutActivity);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { loggedOut = true });
        }

        public class LogoutForm
        {
            public int AccountId { get; set; }
        }
    }
}
