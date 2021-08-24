using ELReedCinema.Areas.Users.Data;
using ELReedCinema.Areas.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELReedCinema.Areas.Users.Domain.CreateProfile
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AccountDbContext dbContext;

        public CreateProfileCommandHandler(UserManager<AppUser> userManager,AccountDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid Registration!");
            }

            dbContext.Users.Add(user);

            return Unit.Value;
        }
    }
}
