#region header

// MongrelWiki - HomeController.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/07 2:14 PM.

#endregion

#region using

using System.Diagnostics;

using ArkaneSystems.MongrelWiki.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace ArkaneSystems.MongrelWiki.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController (ILogger<HomeController> logger) => this._logger = logger;

        public IActionResult Index () => this.View ();

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () => this.View (model: new ErrorViewModel
                                                           {
                                                               RequestId = Activity.Current?.Id ??
                                                                           this.HttpContext.TraceIdentifier,
                                                           });
    }
}
