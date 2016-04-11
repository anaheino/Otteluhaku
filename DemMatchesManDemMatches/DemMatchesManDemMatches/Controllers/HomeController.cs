using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
/// <summary>
/// Kontrolleriluokka. Hyvin yksinkertainen, hoitaa tarvittavat asiat MatchModelsin ja Index.cshtml:n välillä. 
/// </summary>
namespace DemMatchesManDemMatches.Models
{
    public class HomeController : Controller
    {      
        MatchModels m = new MatchModels();
        // GET: Match
        //haetaan json, heitetää se malleille jossa luodaan itse luokka ja palautetaan lista luokista, passataan viewssiin.
        public ActionResult Index()
        {
            bootStrap();
            return View(m.getMatches);
        }

        //postmetodi, samanlainen kuin ylempi mutta palauttaa etsityt ottelut.
        [HttpPost]
        public  ActionResult Index(string first, string second)
        {
            bootStrap();
            return View(m.searchMatch(first, second));
        }

        //perus esilatausfunktio. Lukee datan, tekee siitä dictin ja luo itse luokan kutsumalla addMatches funktiota modelsissa.
        public void bootStrap()
        {
            string path = Server.MapPath("~/Resources/matches.json");
            StreamReader r = new StreamReader(path);
            String line = r.ReadToEnd();
            StringReader a = new StringReader(line);
            List<Dictionary<String, dynamic>> matsit = JsonConvert.DeserializeObject<List<Dictionary<String, dynamic>>>(line);
            m.addMatches(matsit);
        }
    }
}
