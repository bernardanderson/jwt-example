using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cors;

namespace jwtStormpath.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            string authorization = Request.Headers["Authorization"];

            // If you were magically able to get Authorized without proper jwt
            if (authorization == null)
            {
                return new string[] { "No jwt detected" };
            }

            // Again, if you were magically able to get Authorized without proper jwt
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                string token = authorization.Substring("Bearer ".Length).Trim();

                JwtSecurityTokenHandler testJwt = new JwtSecurityTokenHandler();
                var decodedToken = testJwt.ReadToken(token) as JwtSecurityToken;

                var userName = decodedToken.Claims.First(claim => claim.Type == "sub").Value;

                return new string[] { userName };
            }

            // Again, again, if you were magically able to get Authorized without proper jwt
            return new string[] { "No jwt detected" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpOptions]
        public void Options()
        {
        }
    }
}
