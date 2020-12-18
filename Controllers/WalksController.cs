using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        IWalksRepository _walksRepo;
        IDogRepository _dogsRepo;

        public WalksController(IWalksRepository walksRepo, IDogRepository dogsRepo)
        {
            _walksRepo = walksRepo;
            _dogsRepo = dogsRepo;
        }
        // GET: Walks
        public ActionResult Index()
        {
            return View();
        }

        // GET: Walks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Walks/Create
        public ActionResult Create(int id)
        {
            WalkFormViewModel vm = new WalkFormViewModel()
            {
                Dogs = _dogsRepo.GetDogsByOwnerId(GetCurrentUserId()),
                Walk = new Walks()
                {
                    WalkerId = id,
                    Date = DateTime.Today
                }
            };
            return View(vm);
        }

        // POST: Walks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WalkFormViewModel vm)
        {
            vm.Walk.Duration = 0;
            vm.Walk.WalkStatusId = 1;
            vm.Dogs = _dogsRepo.GetDogsByOwnerId(GetCurrentUserId());

            try
            {
                // TODO: Add insert logic here
                _walksRepo.AddWalk(vm.Walk);
                return RedirectToAction("Details", "Owners", new { id = GetCurrentUserId() } );
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: Walks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Walks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Walks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Walks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}