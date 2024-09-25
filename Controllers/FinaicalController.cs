using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenadWebApp.DTOModels;
using RenadWebApp.Models.DataModel;
using RenadWebApp.Models.DataModel.FinaicalModels;
using System.Globalization;

namespace RenadWebApp.Controllers
{
    public class FinaicalController : Controller
    {



        private readonly ApplicationDbContext _db;

        public FinaicalController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: FinaicalController

        public ActionResult Index(string search)
        {
            List<FinaicalRequest> employees = _db.FinaicalRequests.ToList();
            return View(_db.FinaicalRequests.Where(x => x.ContractNumber.StartsWith(search) || search == null).ToList());

        }

        public async Task<ActionResult> IndexClient(int id,string? search)
        {
           
                var payment = await _db.ClientContract
                    .Where(x => x.ClientId == id)
                    .SelectMany(x=>x.Contract.ContractFinaical).Select(x => new FinaicalRequest
                    {
                        Id = x.Finaical.Id,
                        FinaicalNumber = x.Finaical.FinaicalNumber,
                      Titel = x.Finaical.Titel,
                      ContractNumber = x.Finaical.ContractNumber,
                      ClientName = x.Finaical.ClientName,
                      Description = x.Finaical.Description,
                      CreatedDate = x.Finaical.CreatedDate,
                    })
                    .ToListAsync();


            return View(payment);

        }

        public async Task<ActionResult> IndexContract(int id, string? search)
        {

            var payment = await _db.ContractFinaical
                    .Where(x => x.ContractId == id)
                    .Select(x => new FinaicalRequest
                    {
                        Id = x.Finaical.Id,  
                        FinaicalNumber= x.Finaical.FinaicalNumber,
                        Titel = x.Finaical.Titel,
                        ContractNumber = x.Finaical.ContractNumber,
                        ClientName = x.Finaical.ClientName,
                        Description = x.Finaical.Description,
                        CreatedDate = x.Finaical.CreatedDate,
                      //  ContractId= x.Contract.Id ,  
                    })
                    .ToListAsync();


            return View(payment);

        }


        // GET: FinaicalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinaicalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinaicalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create(FinaicalRequest fina)
        {
            var contract = await _db.Contracts.FindAsync(fina.Id);
            FinaicalRequest finaical = new()
            {
                FinaicalNumber = fina.FinaicalNumber,
                Titel = fina.Titel,
                ContractNumber = contract.ContractNumber,
                ClientName = fina.ClientName,
                Description = fina.Description,
                CreatedDate = fina.CreatedDate.Value.Date,
        };


          
            _db.FinaicalRequests.Add(finaical);
            _db.SaveChanges();


            ContractFinaical postUser = new ContractFinaical { ContractId = contract.Id, FinaicalId = finaical.Id };
            _db.ContractFinaical.Add(postUser);
            _db.SaveChanges();

           

            return RedirectToAction(nameof(Index));
        }

        // GET: EngController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Finaical Is Not Exist");
            }

            var course = await _db.FinaicalRequests
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return Ok(ViewBag.Message = $"Finaical {course} Is Not Exist");

            }
            return View(course);
        }

        // POST: EngController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FinaicalRequest finaical)
        {
            var fin = await _db.FinaicalRequests.FindAsync(finaical.Id);

            if (fin == null)
            {
                return Ok(ViewBag.Message = "Finaical Is Not Exist");
            }

           
            fin.Titel = finaical.Titel;
            fin.ClientName = finaical.ClientName;
            fin.Description = finaical.Description;
            fin.CreatedDate = finaical.CreatedDate;
            fin.FinaicalNumber = finaical.FinaicalNumber;


            _db.FinaicalRequests.Update(fin);
            _db.SaveChanges();



            return RedirectToAction(nameof(Index));
        }




        // GET: FinaicalController/Delete/5
        public async Task<ActionResult> Delete(int id )
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Finaical Is Not Exist");
            }

            var course = await _db.FinaicalRequests
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return Ok(ViewBag.Message = $"Finaical {course} Is Not Exist");

            }
            return View(course);
        }

        // POST: FinaicalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(FinaicalRequest finaicalRequest, int contId)
        {

            var user = await _db.FinaicalRequests.FindAsync(finaicalRequest.Id);
            if (user == null)
            {
                return NotFound();
            }

            var contractIds = _db.ContractFinaical
               .Where(x => x.FinaicalId == finaicalRequest.Id)
               .Select(x => x.ContractId);

            var fincont = await _db.ContractFinaical
               .SingleOrDefaultAsync(x => x.FinaicalId == finaicalRequest.Id && contractIds.Contains(x.ContractId));

            if (fincont == null)
            {
                return NotFound();
            }

            _db.ContractFinaical.Remove(fincont);
            _db.FinaicalRequests.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
