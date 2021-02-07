#region header

// MongrelWiki - Program.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/07 2:14 PM.

#endregion

#region using

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

#endregion

namespace ArkaneSystems.MongrelWiki
{
    public static class Program
    {
        public static void Main (string[] args)
        {
            Program.CreateHostBuilder (args: args).Build ().Run ();
        }

        private static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args: args)
                .ConfigureWebHostDefaults (configure: webBuilder => { webBuilder.UseStartup<Startup> (); });
    }
}
