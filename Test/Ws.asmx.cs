using Bal;
using Dal;
using Dal.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Test
{
    /// <summary>
    /// Summary description for Ws
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Ws : System.Web.Services.WebService
    {
        UserDb DbCon;
        [WebMethod()]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public List<User> GetUsers()
        {
            DbCon = new UserDb();
            return DbCon.Get();
        }
        [WebMethod()]
        [ScriptMethod( ResponseFormat = ResponseFormat.Json)]
        public int AddUser(User user)
        {
            DbCon = new UserDb();
            return DbCon.Add(user);
        }


    }
}
