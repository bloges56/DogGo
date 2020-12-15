using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models;
using DogGo.Repository;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        private IDogRepository _dogRepo;

        public DogsController(IDogRepository dogRepo)
        {
            _dogRepo = dogRepo;
        }
        // GET: Dogs
        public ActionResult Index()
        {
            List<Dog> dogs = _dogRepo.GetAllDogs();

            return View(dogs);
        }

        // GET: Dogs/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            return View(dog);
        }

        // GET: Dogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // TODO: Add insert logic here
                _dogRepo.AddDog(dog);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(dog);
            }
        }

        // GET: Dogs/Edit/5
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            return View(dog);
        }

        // POST: Dogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            /*try
            {*/
                // TODO: Add update logic here
                _dogRepo.UpdateDog(dog);
                return RedirectToAction(nameof(Index));
            /*}
            catch
            {
                return View(dog);
            }*/
        }

        // GET: Dogs/Delete/5
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            return View(dog);
        }

        // POST: Dogs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                // TODO: Add delete logic here
                _dogRepo.DeleteDog(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(dog);
            }
        }
    }
}