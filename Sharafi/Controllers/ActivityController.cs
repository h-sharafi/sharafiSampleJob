using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.ActivityCalculation;
using Application.ActivityVM;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sharafi.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<List<ActivityOutput>>> Get()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityOutput>> Get(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
        [HttpPost]
        public async Task<ActionResult<Distance>> DispDistance(ActivityCraete activity)
        {
            return await Mediator.Send(new Create.Query {lat1 = activity.lat1, lat2 = activity.lat2, lon1= activity.lon1, lon2= activity.lon2 });
        }

    }
}
