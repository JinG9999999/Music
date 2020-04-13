using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Music.HC.Models;
using Newtonsoft.Json;
using System.IO;

namespace Music.HC.Controllers
{
    public class musicController : Controller
    {
        // GET: music
        HttpClientHelper hc = new HttpClientHelper("http://localhost:57338/");
        public ActionResult Show()
        {
            bandsel();
            var json = hc.Get("api/music");
            var list = JsonConvert.DeserializeObject<PagedInfo>(json);
            return View(list);
        }
        [HttpPost]
        public ActionResult Show(string Name, string Cid, int currentPage = 1)
        {
            bandsel();
            var json = hc.Get(string.Format("api/music?Name={0}&Cid={1}&index={2}", Name, Cid, currentPage));
            var list = JsonConvert.DeserializeObject<PagedInfo>(json);
            return View(list);
        }
        public void bandsel()
        {
            var json = hc.Get("api/corp");
            var list = JsonConvert.DeserializeObject<List<CorpModel>>(json);
            ViewBag.bandsel = new SelectList(list, "Cid", "CorpName");
        }
        public ActionResult Add()
        {
            bandsel();
            return View();
        }
        [HttpPost]
        public ActionResult Add(MusicModel m, HttpPostedFileBase Cover)
        {
            m.Adder = Session["name"].ToString();
            if (Cover != null)
            {
                string path = "/Content/imgs/" + Path.GetFileName(Cover.FileName);
                Cover.SaveAs(Server.MapPath(path));
                m.Cover = path;
            }
            m.Tags = Request["Tags"];
            if (hc.Post("api/music", JsonConvert.SerializeObject(m)) == "1")
            {
                return Content("<script>alert('添加成功');location.href='/music/Show';</script>");
            }
            else
            {
                Response.Write("<script>alert('添加失败');</script>");
            }
            return View(m);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel m)
        {
            if (hc.Post("api/user", JsonConvert.SerializeObject(m)) == "1")
            {
                Session["name"] = m.UserName;
                return Content("<script>alert('登陆成功');location.href='/music/Show'</script>");
            }
            else
            {
                return Content("<script>alert('登录失败');</script>");
            }

        }

        public ActionResult Upt(int id)
        {
            bandsel();
            var json = hc.Get("api/music/" + id);
            var list = JsonConvert.DeserializeObject<MusicModel>(json);
            return View(list);
        }
        [HttpPost]
        public ActionResult Upt(MusicModel m, HttpPostedFileBase Cover)
        {
            if (Cover != null)
            {
                string path = "/Content/imgs/" + Path.GetFileName(Cover.FileName);
                Cover.SaveAs(Server.MapPath(path));
                m.Cover = path;
            }
            m.Tags = Request["Tags"];
            if (hc.Put("api/music", JsonConvert.SerializeObject(m)) == "1")
            {
                return Content("<script>alert('修改成功');location.href='/music/Show'</script>");
            }
            else
            {
                Response.Write("<script>alert('修改失败');</script>");
            }
            return View(m);
        }
        public ActionResult Del(string id)
        {
            if (hc.Delete("api/music/" + id) == "1")
            {
                return Content("<script>alert('删除成功');location.href='/music/Show'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');</script>");
            }

        }

        public ActionResult XQ(string id)
        {
            bandsel();
            var json = hc.Get("api/music/" + id);
            var list = JsonConvert.DeserializeObject<MusicModel>(json);
            return View(list);
        }
    }
}