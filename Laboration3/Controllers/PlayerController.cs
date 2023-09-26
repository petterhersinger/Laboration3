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
            ViewBag.antal = i;

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
            HttpContext.Session.SetString("antal", i.ToString());
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

                int result = pm.UpdatePlayer(player_id, updatedPlayer, out error);

                if (result > 0)
                {
                    return RedirectToAction("Details", new { player_id = player_id });
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
            ViewBag.antal = HttpContext.Session.GetString("antal");
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
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(PlayerList);
        }
        public IActionResult SelectTeam()
        {
            List<PlayerTeamModel> TeamList = new List<PlayerTeamModel>();
            PlayerMethod pm = new PlayerMethod();
            string error = "";
            TeamList = pm.GetPlayerTeam(out error);
            ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(TeamList);
        }
        [HttpGet]
        public IActionResult Filtering()
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            ViewModelPlayerTeam myModel = new ViewModelPlayerTeam
            {
                PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg),
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2;
            return View(myModel);
        }
        [HttpGet]
        public IActionResult Filtrering2()
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            ViewModelPlayerTeam myModel = new ViewModelPlayerTeam
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
        public IActionResult Filtrering2(string Aktivitet)
        {
            int i = Convert.ToInt32(Aktivitet);
            ViewData["Aktivitet"] = i;

            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            ViewModelPlayerTeam myModel = new ViewModelPlayerTeam
            {
                PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg),
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };

            List<TeamModel> TeamList = new List<TeamModel>();
            TeamList = tm.GetTeamList(out string errormsg3);
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["teamlist"] = TeamList;

            ViewBag.teamlist = TeamList;
            ViewBag.message = Team;

            return View(myModel);
        }
        [HttpGet]
        public IActionResult Filtrering3()
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            ViewModelPlayerTeam myModel = new ViewModelPlayerTeam
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
        public IActionResult Filtrering3(string Team)
        {
            int i = Convert.ToInt32(Team);
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            ViewModelPlayerTeam myModel = new ViewModelPlayerTeam
            {
                PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg, i),
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };

            List<TeamModel> TeamList = new List<TeamModel>();
            TeamList = tm.GetTeamList(out string errormsg3);

            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            ViewData["teamlist"] = TeamList;

            ViewBag.teamlist = TeamList;
            ViewBag.message = Team;
            ViewData["Team"] = i;

            return View(myModel);
        }
        [HttpGet]
        public IActionResult Sort(string sort)
        {
            PlayerTeamMethod pm = new PlayerTeamMethod();
            TeamMethod tm = new TeamMethod();

            List<PlayerTeamMethod> PlayerTeamModelList = pm.GetPlayerTeamModel(out string errormsg);

            string currentDirection = HttpContext.Session.GetString("Direction");

            bool ascending = true;

            if (currentDirection != null)
            {
                ascending = currentDirection == "asc";
            }

            ViewBag.Direction = ascending ? "asc" : "desc";

            if (sort == "fornamn")
            {
                if (ascending)
                {
                    PlayerTeamModelList = PlayerTeamModelList.OrderBy(s => s.Fornamn).ToList();
                    HttpContext.Session.SetString("Direction", "desc");
                }
                else
                {
                    PlayerTeamModelList = PlayerTeamModelList.OrderByDescending(s => s.Fornamn).ToList();
                    HttpContext.Session.SetString("Direction", "asc");
                }
            }
            else
            {
            }

            ViewModelPlayerTeam myModel = new ViewModelPlayerTeam
            {
                PlayerTeamModelList = PlayerTeamModelList,
                TeamModelList = tm.GetTeamList(out string errormsg2)
            };

            ViewBag.sorting = sort;

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
