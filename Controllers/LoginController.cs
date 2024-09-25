using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using RenadWebApp.DTOModels;
using RenadWebApp.Models.DataModel;

namespace RenadWebApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index(string search)
        {
            List<LoginModel> employees = _db.Login.ToList();
            return View(_db.Login.Where(x => x.username.StartsWith(search) || search == null).ToList());

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var User = from m in _db.Login select m;
                User = User.Where(s => s.username.Contains(model.username));
                if (User.Count() != 0)
                {
                    if (User.First().username.Contains("Admin")) {
                        if (User.First().password == model.password)
                        {
                            return RedirectToAction("Create", "Login");

                        }
                    }
                    if (User.First().password == model.password)
                    {
                        return RedirectToAction("Index", "Home");

                    }
                }
            }
            ViewBag.Message = "Fail";
            return View();
        }




        public ActionResult Create()
        {
            return View();
        }

        // POST: EngController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LoginModel User)
        {

            var findEng = await _db.Login.SingleOrDefaultAsync(x => x.username == User.username);
            if (findEng != null)
            {
                return Ok(ViewBag.Message = "This Name Is Already Exist...!");
            }
            LoginModel userModel = new()
            {
              username = User.username,
              password = User.password, 
            };

            _db.Login.Add(userModel);
            _db.SaveChanges();

            return RedirectToAction("Login","Login");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Eng Is Not Exist");
            }

            var course = await _db.Login
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.userId == id);
            if (course == null)
            {
                return Ok(ViewBag.Message = $"Eng {course} Is Not Exist");

            }
            return View(course);
        }

        // POST: EngController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LoginModel userModel)
        {
            var eng = await _db.Login.FindAsync(userModel.userId);

            if (eng == null)
            {
                return Ok(ViewBag.Message = "User Is Not Exist");
            }

            eng.username = userModel.username;
            eng.password = userModel.password;
          



            _db.Login.Update(eng);
            _db.SaveChanges();

            return RedirectToAction("Index","Login");


        }



        // GET: FinaicalController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _db.Login.Find(id);
            if (user == null)
            {
                ViewBag.Message = "User Is Not Exist";
                return View("Error");
            }
            return View(user);
        }

        // POST: FinaicalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, LoginModel userModel)
        {
          var users = await _db.Login.SingleOrDefaultAsync(m => m.userId == id);
            if (User == null)
            {
                return Ok(ViewBag.Message = "User Is Not Exist");
            }

            _db.Login.Remove(users);
            _db.SaveChanges();
            return RedirectToAction("Index", "Login");

        }

    }
}
