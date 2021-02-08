#region header

// MongrelWiki - AppController.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/08 9:22 AM.

#endregion

#region using

using System.Reflection;
using System.Threading.Tasks;

using ArkaneSystems.MongrelWiki.Helpers.Initialization;
using ArkaneSystems.MongrelWiki.Services;

using Microsoft.AspNetCore.Mvc;

#endregion

namespace ArkaneSystems.MongrelWiki.Controllers.Api
{
    [Route (template: "api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly WikiService wiki;

        public AppController (WikiService service) => this.wiki = service;

        #region Core API methods

        // GET: /api/app/version
        [HttpGet (template: "version")]
        public async Task<ActionResult<string>> GetWikiVersion ()
            => Assembly.GetExecutingAssembly ().GetName ().Version!.ToString ();

        #endregion Core API methods

        #region Brute temp hacks

        [HttpGet (template: "size")]
        public async Task<ActionResult<int>> GetWikiSize ()
            => (await this.wiki.GetAllPagesAsync ()).Count;

        [HttpGet (template: "init")]
        public async Task<ActionResult<bool>> PerformDatabaseInitSteps () => await this.wiki.PerformDatabaseInitSteps ();

        #endregion Brute temp hacks
    }
}
