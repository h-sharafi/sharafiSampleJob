using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.ActivityVM;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityOutput>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityOutput>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<ActivityOutput> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Not found" });

                return new ActivityOutput
                {
                    Id = activity.Id,
                    lat1 = activity.lat1,
                    lat2 = activity.lat2,
                    lon1 = activity.lon1,
                    lon2 = activity.lon2
                };
            }
        }
    }
}