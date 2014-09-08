using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StarTrader.Entities;
using StarTrader.Models;

namespace StarTrader.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            Dictionary<string, string> userNames;

            using (var identityCtx = new IdentityDbContext())
            {
                userNames = identityCtx.Users.ToDictionary(u => u.Id, u => u.UserName);
            }

            using (var ctx = new StarTraderContext())
            {
                return View(new GameEntryViewModel(userNames, ctx.GameEntries.ToList()));
            }
        }

        [HttpPost]
        public ActionResult NewGame(GameEntryViewModel gameEntryViewModel)
        {
            using (var ctx = new StarTraderContext())
            {
                ctx.GameEntries.Add(new GameEntry(User.Identity.GetUserId(), gameEntryViewModel.NewGameName));
                ctx.SaveChanges();
            }

            return Redirect("/Games/Index");
        }

        public ActionResult Join(int gameId)
        {
            using (var ctx = new StarTraderContext())
            {
                var gameEntry = ctx.GameEntries.Find(gameId);
                gameEntry.AddPlayer(User.Identity.GetUserId());
                ctx.SaveChanges();
            }

            return Redirect("/Games/Index");            
        }

        public ActionResult Leave(int gameId)
        {
            using (var ctx = new StarTraderContext())
            {
                var gameEntry = ctx.GameEntries.Find(gameId);
                gameEntry.RemovePlayer(User.Identity.GetUserId());
                ctx.SaveChanges();
            }

            return Redirect("/Games/Index");
        }
    }
}