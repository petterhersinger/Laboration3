using Microsoft.AspNetCore.Mvc;
using Laboration3.Models;

namespace Laboration3.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult InsertPlayer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertPlayer(PlayerModel pd)
        {
            PlayerMethod pm = new PlayerMethod();
            int i = 0;
            string error = "";
            i = pm.InsertPlayer(pd, out error);
            ViewBag.error = error;
            ViewBag.amount = i;

            return View("InsertPlayer");
        }

        [HttpGet]
        public IActionResult Delete(int player_id)
        {
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            var player = pm.GetPlayer(player_id, out error);
            ViewBag.error = error;
            if (player != null)
            {
                return View(player);
            }
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete2(int player_id)
        {
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            int i = pm.DeletePlayer(player_id, out error);
            HttpContext.Session.SetString("amount", i.ToString());
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpGet]
        public IActionResult Edit(int player_id)
        {
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            var player = pm.GetPlayer(player_id, out error);

            if (player != null)
            {
                return View(player);
            }

            ViewBag.error = error; 
            return RedirectToAction("SelectWithDataSet");
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int player_id, PlayerModel updatedPlayer)
        {
            if (ModelState.IsValid)
            {
                PlayerMethod pm = new PlayerMethod();
                string error = "";

                int i = pm.UpdatePlayer(player_id, updatedPlayer, out error);

                if (i > 0)
                {
                    return RedirectToAction("Details", new { player_id });
                }
                else
                {
                    ViewBag.error = error;
                }
            }
            return View(updatedPlayer);
        }

        public IActionResult SelectWithDataSet()
        {
            List<PlayerModel> PlayerList = new List<PlayerModel>();
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            PlayerList = pm.GetPlayerWithDataSet(out error);
            ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;
            return View(PlayerList);
        }

        public IActionResult Details(int player_id)
        {
            PlayerModel Player = new PlayerModel();
            PlayerMethod pm = new PlayerMethod();
            Player = pm.GetPlayer(player_id, out string error);
            ViewBag.error = error;
            return View(Player);
        }

        public IActionResult SelectWithDataReader()
        {
            List<PlayerModel> PlayerList = new List<PlayerModel>();
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            PlayerList = pm.GetPlayerWithReader(out error);
            ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;
            return View(PlayerList);
        }
        public IActionResult SelectTeam()
        {
            List<PlayerTeamModel> TeamList = new List<PlayerTeamModel>();
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            TeamList = pm.GetPlayerTeam(out error);
            ViewBag.amount = HttpContext.Session.GetString("amount");
            ViewBag.error = error;
            return View(TeamList);
        }
        //[HttpGet]
        //public IActionResult Filtering()
        //{
        //    PlayerTeamMethod pm = new PlayerTeamMethod();
        //    TeamMethod tm = new TeamMethod();

        //    PlayerTeamViewModel myModel = new PlayerTeamViewModel
        //    {
        //        PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg),
        //        TeamModelList = tm.GetTeamList(out string errormsg2)
        //    };
        //    ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
        //    return View(myModel);
        //}
        //[HttpGet]
        //public IActionResult Filtering2()
        //{
        //    PlayerTeamMethod pm = new PlayerTeamMethod();
        //    TeamMethod tm = new TeamMethod();

        //    PlayerTeamViewModel myModel = new PlayerTeamViewModel
        //    {
        //        PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg),
        //        TeamModelList = tm.GetTeamList(out string errormsg2)
        //    };

        //    List<TeamModel> TeamList = new List<TeamModel>();
        //    TeamList = tm.GetTeamList(out string errormsg3);
        //    ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
        //    ViewData["teamlist"] = TeamList;

        //    ViewBag.teamlist = TeamList;

        //    return View(myModel);
        //}
        //[HttpPost]
        //public IActionResult Filtering2(string Team)
        //{
        //    int i = Convert.ToInt32(Team);
        //    ViewData["Team"] = i;

        //    PlayerTeamMethod pm = new PlayerTeamMethod();
        //    TeamMethod tm = new TeamMethod();

        //    PlayerTeamViewModel myModel = new PlayerTeamViewModel
        //    {
        //        PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg),
        //        TeamModelList = tm.GetTeamList(out string errormsg2)
        //    };

        //    List<TeamModel> TeamList = new List<TeamModel>();
        //    TeamList = tm.GetTeamList(out string errormsg3);
        //    ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
        //    ViewData["teamlist"] = TeamList;

        //    ViewBag.teamlist = TeamList;
        //    ViewBag.message = Team;

        //    return View(myModel);
        //}
        [HttpGet]
        public IActionResult Filtering()
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            PlayerTeamViewModel myModel = new PlayerTeamViewModel
            {
                PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg),
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };

            List<TeamModel> TeamList = new List<TeamModel>();
            TeamList = tm.GetTeamList(out string errormsg3);
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["teamlist"] = TeamList;

            ViewBag.teamlist = TeamList;

            return View(myModel);
        }
        [HttpPost]
        public IActionResult Filtering(string Team)
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            PlayerTeamViewModel myModel = new PlayerTeamViewModel
            {
                PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg, Team),
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };

            List<TeamModel> TeamList = new List<TeamModel>();
            TeamList = tm.GetTeamList(out string errormsg3);

            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["teamlist"] = TeamList;

            ViewBag.teamlist = TeamList;
            ViewBag.message = Team;
            ViewData["Team"] = Team;

            return View(myModel);
        }
        [HttpGet]
        public IActionResult Sort(string sorting)
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            List<PlayerTeamModel> PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg);

            string currentDirection = HttpContext.Session.GetString("Direction");

            bool ascending = true;

            if (currentDirection != null)
            {
                ascending = currentDirection == "asc";
            }

            ViewBag.Direction = ascending ? "asc" : "desc";

            if (sorting == "name")
            {
                if (ascending)
                {
                    PlayerTeamModelList = PlayerTeamModelList.OrderBy(s => s.Name).ToList();
                    HttpContext.Session.SetString("Direction", "desc");
                }
                else
                {
                    PlayerTeamModelList = PlayerTeamModelList.OrderByDescending(s => s.Name).ToList();
                    HttpContext.Session.SetString("Direction", "asc");
                }
            }
            else
            {
            }

            PlayerTeamViewModel myModel = new PlayerTeamViewModel
            {
                PlayerTeamModelList = PlayerTeamModelList,
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };

            ViewBag.sorting = sorting;

            return View(myModel);
        }
        [HttpGet]
        public IActionResult Search()
        {
            List<PlayerModel> PlayerList = new List<PlayerModel>();
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            PlayerList = pm.GetPlayerWithReader(out error);
            ViewBag.error = error;
            return View(PlayerList);
        }


        [HttpPost]
        public IActionResult Search(string input)
        {
            PlayerMethod pm = new PlayerMethod();
            string error = "";

            List<PlayerModel> player = pm.SearchPlayer(input, out string errormsg);

            ViewBag.error = errormsg;

            if (player != null)
            {
                return View(player);
            }

            return RedirectToAction("SelectWithDataSet");
        }
    }
}
