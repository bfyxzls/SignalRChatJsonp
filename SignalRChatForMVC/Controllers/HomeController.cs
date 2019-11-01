using SignalRChatForMVC.Model;
using SignalRChatForMVC.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace SignalRChatForMVC.Controllers
{
    public class HomeController : Controller
    {
        static List<UserDetail> UserBase = new List<UserDetail>();
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
        [HttpGet]
        public JsonpResult AjaxLogin(string userName, string password)
        {
            var entity = UserBase.FirstOrDefault(i => i.UserName == userName
               && i.Password == password);
            return this.Jsonp(entity);
        }
    }
}
