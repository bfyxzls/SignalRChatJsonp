using SignalRChatForMVC.Model;
using SignalRChatForMVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace SignalRChatForMVC.Controllers
{
    public class HomeController : Controller
    {
        static List<UserDetail> UserBase = new List<UserDetail>();
        static Dictionary<string, UserDetail> TokenList = new Dictionary<string, UserDetail>();
        static HomeController()
        {
            UserBase.Add(new UserDetail { UserID = "1", UserName = "zzl", Password = "123456" });
            UserBase.Add(new UserDetail { UserID = "2", UserName = "zhz", Password = "123456" });
            UserBase.Add(new UserDetail { UserID = "3", UserName = "lind", Password = "123456" });
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var entity = UserBase.FirstOrDefault(i => i.UserName == form["userName"]
                && i.Password == form["password"]);
            if (entity != null)
            {
                Session["userID"] = entity.UserID;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "用户名密码不正确...");
            return View();
        }
        /// <summary>
        /// 用户名密码登陆，返回token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonpResult AjaxLogin(string userName, string password)
        {
            var entity = UserBase.FirstOrDefault(i => i.UserName == userName
               && i.Password == password);
            if (entity != null)
            {
                string token = Guid.NewGuid().ToString();
                TokenList.Add(Guid.NewGuid().ToString(), entity);
                return this.Jsonp(new { flag = true, token = token });
            }
            else
            {
                return this.Jsonp(new { flag = false, message = "用户名密码错误" });

            }
        }
        /// <summary>
        /// 通过token获取当前登陆的用户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonpResult GetUser(string token)
        {
            if (!TokenList.ContainsKey(token))
            {
                return this.Jsonp(new { flag = false, message = "token不存在" });

            }
            return this.Jsonp(TokenList[token]);
        }
    }
}
