using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.ActivityCalculation;
using Application.ActivityVM;
using Application.Errors;
using Application.UserService;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Query : IRequest<Distance>
        {
            public Guid Id { get; set; }
            public string lon1 { get; set; }
            public string lat1 { get; set; }
            public string lon2 { get; set; }
            public string lat2 { get; set; }

            //public string AppUserId { get; set; }

        }

        //public class CommandValidator : AbstractValidator<Command>
        //{
        //    public CommandValidator()
        //    {

        //    }
        //}

        public class Handler : IRequestHandler<Query, Distance>
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

            public async Task<Distance> Handle(Query request, CancellationToken cancellationToken)
            {
                double disp = 0;
                try
                {
                     disp = ActicityDisp.DistanceBetweenPlaces(request.lon1, request.lat1, request.lon2, request.lat2);
                }
                catch (Exception ex)
                {

                    throw new RestException(HttpStatusCode.BadRequest, ex.Message);
                }
              

                //save to database

                var user = await _userService.CurrentUser();
                var cutentUser = await _userManager.FindByNameAsync(user.Username);
                var activity = new Domain.Activity
                {
                    Id = request.Id,
                    lon1 = request.lon1,
                    lat1 = request.lat1,
                    lon2 = request.lon2,
                    lat2 = request.lat2,
                    AppUserId = cutentUser.Id
                };

                _context.Activities.Add(activity);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return new Distance
                {
                    dis = disp.ToString()
                };

                throw new Exception("Problem saving changes");
            }
          

           
        }
    }
}