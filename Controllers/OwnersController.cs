using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models;
using DogGo.Repository;
using DogGo.Repositories;
using DogGo.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        private IOwnerRepository _ownerRepo;
        private IDogRepository _dogRepo;
        private IWalkerRepository _walkerRepo;
        private INeighborhoodRepository _neighborhoodRepo;

        public OwnersController(IOwnerRepository ownerRepo, IDogRepository dogRepo, IWalkerRepository walkerRepo, INeighborhoodRepository neighborhoodrepo)
        {
            _ownerRepo = ownerRepo;
            _dogRepo = dogRepo;
            _walkerRepo = walkerRepo;
            _neighborhoodRepo = neighborhoodrepo;
        }

        [Authorize]
        // GET: Owners
        public ActionResult Index()
        {
            int userId = GetCurrentUserId();
            return RedirectToAction("Details", new { id = userId });
        }

        [Authorize]
        // GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            if(id != GetCurrentUserId())
            {
                return NotFound();
            }
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        [Authorize]
        // GET: Owners/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        [Authorize]
        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OwnerFormViewModel ownerFormViewModel)
        {
            try
            {
                _ownerRepo.AddOwner(ownerFormViewModel.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ownerFormViewModel);
            }
        }

        [Authorize]
        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = _ownerRepo.GetOwnerById(id),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        [Authorize]
        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OwnerFormViewModel vm)
        {
            try
            {
                _ownerRepo.UpdateOwner(vm.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vm);
            }
        }

        [Authorize]
        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);

            return View(owner);
        }

        [Authorize]
        // POST: Owners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            Owner owner = _ownerRepo.GetOwnerByEmail(viewModel.Email);

            if (owner == null)
            {
                return Unauthorized();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
                new Claim(ClaimTypes.Email, owner.Email),
                new Claim(ClaimTypes.Role, "DogOwner"),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Details", new { id = owner.Id });
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}