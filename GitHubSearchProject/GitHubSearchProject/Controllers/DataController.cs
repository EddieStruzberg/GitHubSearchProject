using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GitHubSearchProject.Controllers
{
    public class DataController : Controller
    {
        //API GateWay caller object. can be extended for new API Calls in future 
        DataGateWay GitData = new DataGateWay("https://api.github.com/search/repositories?", "q", "YOUR_SEARCH_KEYWORD");

        public JsonResult FindRepositories(string search)
        {
            //Recived Data from API call and return to the client
            var wantedData = GitData.GetFitRepositoriesData(search);
            return new JsonResult { Data = wantedData.items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}