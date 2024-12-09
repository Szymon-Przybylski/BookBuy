using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTests.Utilities
{
    public class AuthenticationHelper
    {
        public static Dictionary<string, object> GetJwtBearerForUser(string username)
        {
            return new Dictionary<string, object>
            {
                {
                    ClaimTypes.Name, username
                }
            };
        }
    }
}
