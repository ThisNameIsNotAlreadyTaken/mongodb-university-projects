using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using M101DotNet.WebApp.Repositories;

namespace M101DotNet.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var i = 0;
            for (var bit = 0; bit < 32; bit++)
            {
                i |= bit << bit;
            }

            ViewBag.Message = i.ToString();
            return View();
        }

        public async Task<List<Week2Repository.Grade>> Homework22()
        {
            var repository = new Week2Repository();

            var data = await repository.Homework22Async();

            return data;
        }
    }
}