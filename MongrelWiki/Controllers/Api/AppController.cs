#region header

// MongrelWiki - AppController.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/08 9:22 AM.

#endregion

#region using

using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

#endregion

namespace ArkaneSystems.MongrelWiki.Controllers.Api
{
    [Route (template: "api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        // GET: /api/app/version
        [HttpGet (template: "version")]
        public async Task<ActionResult<string>> GetWikiVersion ()
            => Assembly.GetExecutingAssembly ().GetName ().Version!.ToString ();
    }
}
