using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenadWebApp.DTOModels;
using RenadWebApp.Models.DataModel;
using System.Diagnostics.Contracts;
using System;
using RenadWebApp.Models.DataModel.FinaicalModels;

namespace RenadWebApp.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index(int id)
        {
            var payment = await _db.ContractPayment
            .Where(x => x.ContractId == id)
            .Select(x => new PaymentModel
            {
                Id = x.Payment.Id,
                PaymentNumber = x.Payment.PaymentNumber,
                Payments = x.Payment.Payments,
                PaymentTax = x.Payment.PaymentTax,
                DateForPayment = x.Payment.DateForPayment,
                DueDate = x.Payment.DueDate,
                LateTime1 = x.Payment.LateTime1,
                RatioForEng = x.Payment.RatioForEng,
                DueEngRatio = x.Payment.DueEngRatio,
                DueTax = x.Payment.DueTax,
                PayMentValue = x.Payment.PayMentValue,
                Difference = x.Payment.Difference,
            })
            .ToListAsync();

            return View(payment);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentModel paymentModel)
        {
            var contract = await _db.Contracts.FindAsync(paymentModel.Id);

      
            if (contract.Approved == "No".ToLower())
            {
                return Ok(ViewBag.Message = "Please Approved On Contract...!");

            }
            PaymentModel payment = new()
            {

                PaymentNumber = paymentModel.PaymentNumber,
                Payments = paymentModel.Payments,
                DateForPayment = paymentModel.DateForPayment,
                DueDate = paymentModel.DueDate,
                LateTime1 = paymentModel.DateForPayment.Value.Day - paymentModel.DueDate.Value.Day,
                RatioForEng = (int)(paymentModel.Payments * 100 / 115) - (int)(paymentModel.Payments * 100 / 115) * 100 / (contract.RatioForEng + 100),
                //RatioForEng = paymentModel.Payments * contract.RatioForEng / 100,
                DueEngRatio = "NO".ToLower(),
                PaymentTax = paymentModel.Payments - (int)(paymentModel.Payments * 100 / 115),
                // PaymentTax = paymentModel.Payments * 15 / 100,
                DueTax = "NO".ToLower(),
                PayMentValue = paymentModel.PayMentValue,
                Difference = paymentModel.PayMentValue - paymentModel.Payments,
            };


            if (contract.RestOfContractShow < payment.Payments)
            {
                return Ok(ViewBag.Message = "Please Check The Total Of Contract");
            }

            _db.Payment.Add(payment);
            _db.SaveChanges();



            ContractPayment postUser = new ContractPayment { ContractId = contract.Id, PaymentId = payment.Id };
            _db.ContractPayment.Add(postUser);
            _db.SaveChanges();



            contract.RestOfContractShow = contract.RestOfContractShow - payment.Payments;

            if (payment.DueEngRatio != "NO".ToLower())
            {
                contract.RestOfEngShow = contract.TotalRatioForEng - payment.RatioForEng;
            }


            var SumPAyment = _db.ContractPayment.Where(x => x.ContractId == paymentModel.Id).Select(x => x.Payment.Payments).Sum();
            var SumEng = _db.ContractPayment.Where(x => x.ContractId == paymentModel.Id && x.Payment.DueEngRatio == "YES".ToLower()).Select(x => x.Payment.RatioForEng).Sum();
            var SumTax = _db.ContractPayment.Where(x => x.ContractId == paymentModel.Id && x.Payment.DueTax == "YES".ToLower()).Select(x => x.Payment.PaymentTax).Sum();
            var SumPAymentLate = _db.ContractPayment.Where(x => x.ContractId == paymentModel.Id).Select(x => x.Payment.Difference).Sum();
            contract.AmountLate = SumPAymentLate;

            contract.RestOfContractShow = contract.TotalOfContract - SumPAyment;

            contract.RestOfEngShow = contract.TotalRatioForEng - SumEng;
            contract.RestOfTAxShow = contract.TotalTax - SumTax;

            var amountlate = _db.ContractPayment.Where(x => x.ContractId == paymentModel.Id).Select(x => x.Payment.Difference).Sum();

            var client = await _db.ClientContract.FirstAsync(x=>x.Contract.Id==contract.Id);
            var clientId =await _db.Clients.FindAsync(client.ClientId);

            clientId.AmountLate = amountlate;


            _db.Clients.Update(client.Client);
            _db.SaveChanges();

            _db.Contracts.Update(contract);
            _db.SaveChanges();


            contract.RestOfEngShow = contract.TotalRatioForEng;
            _db.Contracts.Update(contract);
            _db.SaveChanges();

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }




        public async Task<IActionResult> Edit(int id, int contactId)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Payment Is Not Exist");
            }

            var course = await _db.Payment.FindAsync(id);
            if (course == null)
            {
                return Ok(ViewBag.Message = $"payment {course} Is Not Exist");

            }
            return View(course);

        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentModel paymentModel)
        {
            var payment = await _db.Payment.FindAsync(paymentModel.Id);

            if (payment == null)
            {
                return Ok(ViewBag.Message = "Payment Is Not Exist");
            }
            var EngRatio = _db.ContractPayment.Where(x => x.PaymentId ==payment.Id).Select(x => x.Contract.RatioForEng).FirstOrDefault();
            payment.Payments = paymentModel.Payments;
            payment.DueTax = paymentModel.DueTax;
            payment.DueEngRatio = paymentModel.DueEngRatio;
            payment.PayMentValue = paymentModel.PayMentValue;
            payment.DateForPayment = paymentModel.DateForPayment;
            payment.DueDate = paymentModel.DueDate;
            payment.Difference = paymentModel.PayMentValue - paymentModel.Payments;
            payment.LateTime1 = paymentModel.DateForPayment.Value.Day - paymentModel.DueDate.Value.Day;
            payment.PaymentTax = paymentModel.Payments - (int)(paymentModel.Payments * 100 / 115);

           // int ratio = EngRatio;
            payment.RatioForEng = (int)(paymentModel.Payments * 100 / 115) - (int)(paymentModel.Payments * 100 / 115) * 100 / (EngRatio + 100);



            _db.Payment.Update(payment);
            _db.SaveChanges();


            return RedirectToAction(nameof(HomeController.Index), "Home");


        }




        public async Task<ActionResult> ClientPayment(int id, string? search)
        {

            var payment = await _db.ClientContract
    .Where(x => x.ClientId == id)
    .SelectMany(x => x.Contract.ContractPayment).Select(x => new PayContractModel
    {
        ContractPay = x.Contract.ContractNumber,
        Id = x.Payment.Id,
        PaymentNumber = x.Payment.PaymentNumber,
        Payments = x.Payment.Payments,
        PaymentTax = x.Payment.PaymentTax,
        DateForPayment = x.Payment.DateForPayment,
        DueDate = x.Payment.DueDate,
        LateTime1 = x.Payment.LateTime1,
        RatioForEng = x.Payment.RatioForEng,
        DueEngRatio = x.Payment.DueEngRatio,
        DueTax = x.Payment.DueTax,
        PayMentValue = x.Payment.PayMentValue,
        Difference = x.Payment.Difference,
    })
    .ToListAsync();


            return View(payment);

        }


        // GET: FinaicalController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return Ok(ViewBag.Message = "Finaical Is Not Exist");
            }

            var course = await _db.Payment
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
        public async Task<IActionResult> Delete(PaymentModel payment)
        {

            var user = await _db.Payment.FindAsync(payment.Id);
            if (user == null)
            {
                return NotFound();
            }

            var paymentIds = _db.ContractPayment
               .Where(x => x.PaymentId == payment.Id)
               .Select(x => x.ContractId);

            var fincont = await _db.ContractPayment
               .SingleOrDefaultAsync(x => x.PaymentId == payment.Id && paymentIds.Contains(x.ContractId));

            if (fincont == null)
            {
                return NotFound();
            }

            _db.ContractPayment.Remove(fincont);
            _db.Payment.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }




    }
}
