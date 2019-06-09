using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace GitHubSearchProject
{
    public class DataGateWay
    {
        public GitData Data { get; set; }
        private HttpWebRequest request; 
        private string url;
        private string searchParamName;

        public DataGateWay(string url, string searchParamName, string search)
        {
            Init(url, searchParamName);
            InitRequest(search);
            Data = new GitData();
        }

        private void Init(string url, string searchParamName)
        {
            this.url = url;
            this.searchParamName = searchParamName + "=";
        }
        /// <summary>
        /// Build the API Request and sets the appropriate init for it
        /// </summary>
        /// <param name="search"></param>
        private void InitRequest(string search)
        {
            search = search == null ? "YOUR_SEARCH_KEYWORD" : search;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            request = (HttpWebRequest)WebRequest.Create(url + searchParamName + search);
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.UserAgent = "[Chrome]";
            request.Accept = "*/*";
        }
        /// <summary>
        /// Creates the wanted API call, execute and returns the Data
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public GitData GetFitRepositoriesData(string search)
        {
            InitRequest(search);
            GetDataResponse();
            return Data;
        }
        /// <summary>
        /// Creates an API call and gets the Data
        /// </summary>
        private void GetDataResponse()
        {
            var Response = this.request.GetResponse();
            string htmlString;
            using (var reader = new StreamReader(Response.GetResponseStream()))
            {
                htmlString = reader.ReadToEnd();
            }
            Response.Dispose();
            Response.Close();
            Data = new JavaScriptSerializer().Deserialize<GitData>(htmlString);
        }
        /// <summary>
        /// Git Data object built for GitAPI
        /// </summary>
        public class GitData
        {
            public string total_count { get; set; }
            public string incomplete_results { get; set; }
            public List<Dictionary<string, object>> items { get; set; }

        }
    }
}