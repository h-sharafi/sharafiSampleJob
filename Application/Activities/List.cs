using Application.ActivityVM;
using Application.UserService;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<ActivityOutput>> { }

        public class Handler : IRequestHandler<Query, List<ActivityOutput>>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IUSerService _userService;

            public Handler(DataContext context, UserManager<AppUser> userManager, IUSerService userService)
            {
                _context = context;
                _userManager = userManager;
                _userService = userService;
            }


            public async Task<List<ActivityOutput>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userService.CurrentUser();
                var cutentUser = await _userManager.FindByNameAsync(user.Username);

                return await _context.Activities.Where(a => a.AppUserId == cutentUser.Id).Select(activity => new ActivityOutput
                {
                    Id= activity.Id,
                    lat1 = activity.lat1,
                    lat2 = activity.lat2,
                    lon1 = activity.lon1,
                    lon2 = activity.lon2
                }).ToListAsync();

            }
        }
    }
}
