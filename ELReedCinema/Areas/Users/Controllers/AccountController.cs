using ELReedCinema.Areas.Users.Data;
using ELReedCinema.Areas.Users.Domain;
using ELReedCinema.Areas.Users.Models;
using ELReedCinema.AuthOptions;
using ELReedCinema.Model.DTO;
using ELReedCinema.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<AppUser> userManager;
        private readonly AccountDbContext dbContext;
        private readonly ITokenGenerator tokenGenerator;

        public AccountController(
            IMediator mediator, 
            UserManager<AppUser> userManager,
            AccountDbContext dbContext,
            ITokenGenerator tokenGenerator)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.tokenGenerator = tokenGenerator;
        }

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromForm] UserAuthDTO dto)
        {
            var user = await userManager.FindByNameAsync(dto.Login);
            if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized();

            var token = tokenGenerator.CreateJwtToken(user.Email, user.Id);
            //вынести в отдельный Сервис
            ///TODO
            var refreshToken = new RefreshToken
            {
                Token = tokenGenerator.CreateRefreshToken(),
                AppUser = user,
                ExpiresAt = DateTime.Now + TimeSpan.FromDays(30),
                AppUserId = user.Id
            };

            var list = dbContext.RefreshTokens.Where(x => x.AppUserId == user.Id).ToList();
            dbContext.RefreshTokens.RemoveRange(list);

            await dbContext.RefreshTokens.AddAsync(refreshToken);
            await dbContext.SaveChangesAsync();

            var UserTokens = new UserTokens
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token
            };
            /////////////

            return Ok(UserTokens);
        }


        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<UserTokens>> Refresh(string refreshToken)
        {
            var oldToken = dbContext.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            var user = dbContext.Users.FirstOrDefault(x => x.Id == oldToken.AppUserId);
            if (oldToken == null)
                return Unauthorized();

            else if(oldToken!=null && oldToken.ExpiresAt < DateTime.Now)
            {
                dbContext.RefreshTokens.Remove(oldToken);
                await dbContext.SaveChangesAsync();
                return Unauthorized();
            }
            else
            {
                var newToken = new RefreshToken
                {
                    Token = tokenGenerator.CreateRefreshToken(),
                    AppUserId = oldToken.AppUserId,
                    ExpiresAt = DateTime.Now + TimeSpan.FromDays(30),
                };
                await dbContext.RefreshTokens.AddAsync(newToken);
                dbContext.RefreshTokens.Remove(oldToken);
                await dbContext.SaveChangesAsync();

                var userTokens = new UserTokens
                {
                    AccessToken = tokenGenerator.CreateJwtToken(user.Email, user.Id),
                    RefreshToken = newToken.Token
                };

                return userTokens;
            }


        }


        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationAsync([FromForm] CreateProfileCommand command)
        {
            try
            {
                await mediator.Send(command);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
