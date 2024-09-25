using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenadWebApp.DTOModels;
using RenadWebApp.Models.DataModel;

namespace RenadWebApp.Controllers
{
    public class EngController : Controller
    {

        private readonly ApplicationDbContext _db;

        public EngController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: EngController
        public async Task<ActionResult> Index()
        {
           var TotalRatioForEng = _db.EngContract.Where(x=>x.Contract.Approved == "Yes".ToLower()).Select(x =>x.Contract.TotalRatioForEng ).Sum();
            var RestOfEngShow = _db.EngContract.Where(x => x.Contract.Approved == "Yes".ToLower()).Select(x => x.Contract.RestOfEngShow).Sum();

            var EngTake = TotalRatioForEng - RestOfEngShow;

            var Eng = await _db.Eng.Select(x => new
            {
                x.Id,
                x.EngName,
                x.PhoneNumber,
                x.Email,
                x.Offices,
                TotalRatioForEng,
                RestOfEngShow,
                EngTake,
            }).ToListAsync();


            return View(Eng);

        }

        // GET: EngController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var payment = await _db.Eng
           .Where(x => x.Id == id)
           .SelectMany(x =>x.EngContract.Select(x=> new
           {
               EngName = x.Eng.EngName,
               ContractNumber = x.Contract.ContractNumber,
               ProjectName = x.Contract.ProjectName,
               TotalOfContract = x.Contract.TotalOfContract,
               TotalAmount = x.Contract.TotalAmount,
               RestOfContractShow = x.Contract.RestOfContractShow,
               RatioForEng = x.Contract.RatioForEng,
               TotalRatioForEng = x.Contract.TotalRatioForEng,
               RestOfEngShow = x.Contract.RestOfEngShow,
              

           }))
             .ToListAsync();
            return View(payment);
        }



        //public async Task<ActionResult> StatmentForEng(int id)
        //{
        //    var payment = await _db.Eng
        //   .Where(x => x.Id == id)
        //   .SelectMany(x => x.EngExtraBill.Select(x => new
        //   {
        //     Name =   x.Eng.EngName,
        //     ExtraName =x.ExtraBills.Name,
        //     billNumber = x.ExtraBills.NumberOfBill,
        //     TotalEng = x.ExtraBills.TotalEngRatio,


        //   }))
        //     .ToListAsync();
        //    return View(payment);
        //}



        // GET: EngController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EngController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EngModel Eng)
        {

            var findEng = await _db.Eng.SingleOrDefaultAsync(x => x.EngName == Eng.EngName);
            if(findEng!= null)
            {
                return Ok(ViewBag.Message = "This Name Is Already Exist...!");
            }
            EngModel engModel = new()
            {
                EngName = Eng.EngName,
                Email = Eng.Email,
                PhoneNumber = Eng.PhoneNumber,
                CompOrFree = Eng.CompOrFree,
                Offices = Eng.Offices,
                Connection = Eng.Connection,
                Meeting = Eng.Meeting,
                TimeOfMeeting = Eng.TimeOfMeeting,
                LastCommunication = Eng.LastCommunication,
                Created = Eng.Created,
            };

            _db.Eng.Add(engModel);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: EngController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Eng Is Not Exist");
            }

            var course = await _db.Eng
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return Ok(ViewBag.Message = $"Eng {course} Is Not Exist");

            }
            return View(course);
        }

        // POST: EngController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EngModel engModel)
        {
            var eng = await _db.Eng.FindAsync(engModel.Id);

            if (eng == null)
            {
                return Ok(ViewBag.Message = "Engineer Is Not Exist");
            }

            eng.EngName = engModel.EngName;
            eng.PhoneNumber = engModel.PhoneNumber;
            eng.Offices = engModel.Offices;
            eng.Connection = engModel.Connection;
            eng.Meeting = engModel.Meeting;
            eng.TimeOfMeeting = engModel.TimeOfMeeting;
            eng.LastCommunication = engModel.LastCommunication;
            eng.CompOrFree = engModel.CompOrFree;
            eng.Email = engModel.Email;

          

            _db.Eng.Update(eng);
            _db.SaveChanges();


            if (eng.EngName == engModel.EngName)
            {
                return Ok(ViewBag.Message = "This Engineer Name Is Already Exist");

            }

            return RedirectToAction(nameof(Index));
            
          
        }

        // GET: EngController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EngController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
