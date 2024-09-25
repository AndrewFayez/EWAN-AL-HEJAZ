using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenadWebApp.DTOModels;
using RenadWebApp.Models.DataModel;
using RenadWebApp.Models.DataModel.FinaicalModels;

namespace RenadWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: HomeController
        public  ActionResult Index(string search)
        {
            List<ClientModels> employees =  _db.Clients.ToList();
            return View(_db.Clients.Where(x => x.Name.StartsWith(search) || search == null).ToList());

        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( ClientModels client)
        {
            var FindClient = await _db.Clients.SingleOrDefaultAsync(x => x.Name == client.Name);
            if (FindClient != null)
            {
                return Ok(ViewBag.Message = "This Client Name Is Already Exist...!");
            }
            ClientModels clientModel = new()
            {
                Name = client.Name,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                NumberOfProject = client.NumberOfProject,
                RestFromAmount = client.RestFromAmount,
                TotalAmount = client.TotalAmount,
                Approved = "No".ToLower(),
                CreatedDate = client.CreatedDate.Value.Date,

            };

            _db.Clients.Add(clientModel);
            _db.SaveChanges();


          

            return RedirectToAction(nameof(Index));
        }





        // GET: HomeController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Client Is Not Exist");
            }

            var course = await _db.Clients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return Ok(ViewBag.Message = $"Client {course} Is Not Exist");

            }
            return View(course);
        }

        // POST: EngController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClientModels clientModel)
        {
            var client = await _db.Clients.FindAsync(clientModel.Id);

            if (client == null)
            {
                return Ok(ViewBag.Message = "Client Is Not Exist");
            }
            client.Approved = clientModel.Approved.ToLower();
            client.Name = clientModel.Name;
            client.Email = clientModel.Email;
            client.PhoneNumber = clientModel.PhoneNumber;
            client.NumberOfProject = clientModel.NumberOfProject;
            client.CreatedDate = clientModel.CreatedDate;

           
            var allContractAmont = _db.Clients.SelectMany(x => x.ClientContract.Where(x => x.ClientId == id && x.Contract.Approved=="Yes".ToLower()).Select(x => x.Contract.TotalOfContract)).Sum();
            var allContractRast = _db.Clients.SelectMany(x => x.ClientContract.Where(x=>x.ClientId==id && x.Contract.Approved == "Yes".ToLower()).Select(x => x.Contract.RestOfContractShow)).Sum();

            client.TotalAmount = allContractAmont;
            client.RestFromAmount = allContractRast;

            _db.Clients.Update(client);
            _db.SaveChanges();


            return RedirectToAction(nameof(Index));


        }




        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
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




        public async Task<ActionResult> IndexContract(int id )
        {
           

            List<ContractModel> payment = await  _db.ClientContract
           .Where(x => x.ClientId == id)
           .Select(x => new ContractModel
           {
               Id = x.Contract.Id,
               ProjectName= x.Contract.ProjectName,
               ContractNumber=  x.Contract.ContractNumber,
               CreatedDate = x.Contract.CreatedDate,
               Duration = x.Contract.Duration,
               DeliveryDate= x.Contract.DeliveryDate,
               TotalOfContract = x.Contract.TotalOfContract,
               ProjectBy = x.Contract.ProjectBy,
               RatioForEng =  x.Contract.RatioForEng,
               TotalRatioForEng = x.Contract.TotalRatioForEng,
               TotalTax = x.Contract.TotalTax,
               TotalAmount =  x.Contract.TotalAmount,
               Approved = x.Contract.Approved,
               AmountLate = x.Contract.AmountLate,
           })
           .ToListAsync();

            return View(payment);
        }




        public ActionResult CreateContract()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateContract(ContractModel contractModel)
        {

          

            var client = await _db.Clients.FindAsync(contractModel.Id);
            if (client.Approved=="No".ToLower())
            {
                return Ok(ViewBag.Message = "Please Approved On Project...!");

            }

            var num = await _db.Contracts.SingleOrDefaultAsync(x => x.ContractNumber == contractModel.ContractNumber);
            if (num != null)
            {
                return Ok(ViewBag.Message = "Change Number Of Contract Please ....!");

            }

            ContractModel contract = new()
            {
                Approved = "NO".ToLower(),
                ProjectName =contractModel.ProjectName,
                ContractNumber = contractModel.ContractNumber,
                CreatedDate = contractModel.CreatedDate,
                Duration = contractModel.Duration,
                DeliveryDate = contractModel.CreatedDate.Value.AddMonths((int)contractModel.Duration),
                // DeliveryDate = DateTime.Now.AddMonths((int)contractModel.Duration),
                ProjectBy = contractModel.ProjectBy,
                RatioForEng = contractModel.RatioForEng,
                TotalOfContract = contractModel.TotalOfContract,
                TotalAmount = (int)(contractModel.TotalOfContract * 100 / 115),
                TotalTax = contractModel.TotalOfContract - (int)(contractModel.TotalOfContract * 100 / 115),
                TotalRatioForEng = (int)(contractModel.TotalOfContract * 100 / 115) - (int)(contractModel.TotalOfContract * 100 / 115) * 100 / (contractModel.RatioForEng + 100),
                RestOfContractShow = contractModel.TotalOfContract,
                RestOfEngShow = contractModel.TotalRatioForEng,
                RestOfTAxShow = contractModel.TotalTax,
            };
            var eng = await _db.Eng.SingleOrDefaultAsync(x => x.EngName.Contains(contract.ProjectBy));
            if (eng == null)
            {
                return Ok(ViewBag.Message = "This Eng Name Is Not Exist...!");

            }


            _db.Contracts.Add(contract);
            _db.SaveChanges();

            //client.TotalAmount = contract.TotalAmount;
            //client.RestFromAmount = contract.RestOfContractShow;

            //_db.Clients.Update(client);
            //_db.SaveChanges();

            EngContract postUser = new EngContract { ContractId = contract.Id, EngId = eng.Id };
            _db.EngContract.Add(postUser);
            _db.SaveChanges();

            ClientContract clientContract = new ClientContract { ContractId = contract.Id, ClientId = client.Id };
            _db.ClientContract.Add(clientContract);
            _db.SaveChanges();

            var allContractAmont = _db.Clients.SelectMany(x => x.ClientContract.Where(x => x.ClientId == client.Id && x.Contract.Approved == "Yes".ToLower()).Select(x => x.Contract.TotalOfContract)).Sum();
            var allContractRast = _db.Clients.SelectMany(x => x.ClientContract.Where(x => x.ClientId == client.Id && x.Contract.Approved == "Yes".ToLower()).Select(x => x.Contract.RestOfContractShow)).Sum();

            client.TotalAmount = allContractAmont;
            client.RestFromAmount = allContractRast;

            _db.Clients.Update(client);
            _db.SaveChanges();


            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> EditContract(int? id)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Client Is Not Exist");
            }

            var course = await _db.Contracts
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return Ok(ViewBag.Message = "Client Is Not Exist");

            }
            return View(course);
        }


        [HttpPost, ActionName("EditContract")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContract(int id, ContractModel contractModel)
        {
            var client = await _db.Contracts.FindAsync(id);
            if (client == null)
            {
                return Ok(ViewBag.Message = "Contract Is Not Exist");
            }


            var SumPAyment = _db.ContractPayment.Where(x => x.ContractId == id).Select(x => x.Payment.Payments).Sum();
            var SumEng = _db.ContractPayment.Where(x => x.ContractId == id && x.Payment.DueEngRatio == "YES".ToLower()).Select(x => x.Payment.RatioForEng).Sum();
            var SumTax = _db.ContractPayment.Where(x => x.ContractId == id && x.Payment.DueTax == "YES".ToLower()).Select(x => x.Payment.PaymentTax).Sum();

            client.Approved = contractModel.Approved.ToLower();
            client.TotalOfContract = contractModel.TotalOfContract;
            client.RestOfContractShow = contractModel.TotalOfContract - SumPAyment;

            client.ProjectName = contractModel.ProjectName;
            client.ContractNumber = contractModel.ContractNumber;
            client.CreatedDate = contractModel.CreatedDate;
            client.Duration = contractModel.Duration;
            client.RatioForEng = contractModel.RatioForEng;
            client.ProjectBy = contractModel.ProjectBy;
            client.DeliveryDate = contractModel.CreatedDate.Value.AddMonths((int)contractModel.Duration);
            client.TotalAmount = (int)(contractModel.TotalOfContract * 100 / 115);
            client.TotalTax = contractModel.TotalOfContract - (int)(contractModel.TotalOfContract * 100 / 115);
            client.TotalRatioForEng = (int)(contractModel.TotalOfContract * 100 / 115) - (int)(contractModel.TotalOfContract * 100 / 115) * 100 / (contractModel.RatioForEng + 100);
            client.RestOfEngShow = client.TotalRatioForEng - SumEng;
            client.RestOfTAxShow = client.TotalTax - SumTax;

            var SumPAymentLate = _db.ContractPayment.Where(x => x.ContractId == id).Select(x => x.Payment.Difference).Sum();
            client.AmountLate = SumPAymentLate;

            _db.Contracts.Update(client);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));

        }



        public async Task<ActionResult> DetailsContract(int id)
        {
            var employeeEdit = await _db.Contracts.SingleAsync(emp => emp.Id == id);
              
      
            return View(employeeEdit);

        }





        ////////////////////////////////////////////////////////////////////////////////////////
     
    }
}
