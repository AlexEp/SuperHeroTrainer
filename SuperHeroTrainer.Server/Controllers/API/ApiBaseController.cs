using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        public string UserId
        {
            get
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return userId;
            }
        }
    }
}
