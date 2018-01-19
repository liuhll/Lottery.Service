﻿using System.Web.Http;
using ENode.Commanding;

namespace Lottery.WebApi.Areas.BackOffice.Controllers.v1
{
    [RoutePrefix("v1/backoffice")]
    public class RoleController : BoBaseApiV1Controller
    {
        public RoleController(ICommandService commandService) : base(commandService)
        {
        }

        [Route("role")]
        [HttpPost]
        public string CreateRole()
        {
            return "";
        }
    }
}
