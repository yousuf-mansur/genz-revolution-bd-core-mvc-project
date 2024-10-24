using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenZRevolutionBD.Models;
using GenZRevolutionBD.Models.ViewModels;
using Microsoft.Data.SqlClient;

namespace GenZRevolutionBD.Controllers
{
    public class SuperHeroesController : Controller
    {
        private readonly SuperHeroDbContext _db;
        private readonly IWebHostEnvironment _env;

        public SuperHeroesController(SuperHeroDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _db.SuperHeroes.Include(s => s.SuperPower).ThenInclude(s => s.Power).ToListAsync());
        }


        public IActionResult AddSuperPowers(int? id)
        {
            ViewBag.powers = new SelectList(_db.Powers, "PowerId", "PowerName", id.ToString() ?? "");
            return PartialView("_AddSuperPowers");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SuperHeroVM svm, int[] powerId)
        {
            if (ModelState.IsValid)
            {
                SuperHero superHero = new SuperHero()
                {
                    SuperHeroName = svm.SuperHeroName,
                    DateOfDeath = svm.DateOfDeath,
                    Age = svm.Age
                };


                //Picture Upload

                var file = svm.PictureFile;
                string webroot = _env.WebRootPath;
                string folder = "Upload";
                string picFileName = DateTime.Now.Ticks.ToString() + "_" + Path.GetFileName(svm.PictureFile.FileName);
                string fileToSave = Path.Combine(webroot, folder, picFileName);


                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        svm.PictureFile.CopyTo(stream);
                        superHero.Picture = "/" + folder + "/" + picFileName;
                    }

                }

                //Get Details Table Data
                foreach (var item in powerId)
                {
                    SuperPower sp = new SuperPower()
                    {
                        SuperHero = superHero,
                        SuperHeroId = superHero.SuperHeroId,
                        PowerId = item
                    };
                    _db.SuperPoweres.Add(sp);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var superHero = await _db.SuperHeroes.FirstOrDefaultAsync(x => x.SuperHeroId == id);

            SuperHeroVM svm = new SuperHeroVM()
            {
                ID = superHero.SuperHeroId,
                SuperHeroName = superHero.SuperHeroName,
                DateOfDeath = superHero.DateOfDeath,
                Age = superHero.Age
            };
            var existPower = _db.SuperPoweres.Where(x => x.SuperHeroId == id).ToList();
            foreach (var item in existPower)
            {
                svm.PowerList.Add(item.PowerId);
            }
            return View(svm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SuperHeroVM svm, int[] powerId)
        {
            if (ModelState.IsValid)
            {
                SuperHero superHero = new SuperHero()
                {
                    SuperHeroName = svm.SuperHeroName,
                    DateOfDeath = svm.DateOfDeath,
                    Age = svm.Age
                };               
                

                //Picture Upload
                var file = svm.PictureFile;
                string existPic = svm.Picture;
               


                if (file != null)
                {
                    string webroot = _env.WebRootPath;
                    string folder = "Upload";
                    string picFileName = DateTime.Now.Ticks.ToString() + "_" + Path.GetFileName(svm.PictureFile.FileName);
                    string fileToSave = Path.Combine(webroot, folder, picFileName);
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        svm.PictureFile.CopyTo(stream);
                        superHero.Picture = "/" + folder + "/" + picFileName;
                    }
                }
                else
                {
                    superHero.Picture = existPic;
                }

                //Details table manipulate
                var existPower = _db.SuperPoweres.Where(x => x.SuperHeroId == superHero.SuperHeroId).ToList();
                
                
                //Remove
                foreach (var item in existPower)
                {
                    _db.SuperPoweres.Remove(item);
                }
                
                //Add
                foreach (var item in powerId)
                {
                    SuperPower sp = new SuperPower()
                    {
                        
                        SuperHeroId = superHero.SuperHeroId,
                        PowerId = item
                    };
                    _db.SuperPoweres.Add(sp);
                }
                _db.Update(superHero);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var superhero= await _db.SuperHeroes.FirstOrDefaultAsync(s=>s.SuperHeroId == id);
            if (superhero == null)
            {
                return NotFound();
            }

            return View(superhero);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.Database.ExecuteSqlRawAsync("EXEC DeleteSuperHero @SuperHeroId", new SqlParameter("@SuperHeroId", id));
            return RedirectToAction(nameof(Index));
        }


        private bool SuperHeroExists(int id)
        {
            return _db.SuperHeroes.Any(e => e.SuperHeroId == id);
        }
    }
}
