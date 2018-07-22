using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib;
using Lib.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JPManagement.Controllers
{
    public class RaptorController : Controller
    {

        private readonly IRaptorRepo _raptorRepo;


        public RaptorController(IRaptorRepo raptorRepo)
        {
            _raptorRepo = raptorRepo;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _raptorRepo.TrackAllRaptors());
        }


        [HttpGet]
        public IActionResult RegisterRaptor()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegisterRaptor(Raptor raptor)
        {
            if (ModelState.IsValid)
            {
                _raptorRepo.Insert(raptor);
            }
            return RedirectToAction("Index");
        }


        //GET ALL CARS

        //GET ALL CARS LOCATION
    }
}