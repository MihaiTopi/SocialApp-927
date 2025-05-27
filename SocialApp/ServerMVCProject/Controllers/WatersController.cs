namespace ServerMVCProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;

    [Route("water")]
    public class WatersController : Controller
    {
        private readonly IWaterIntakeService waterIntakeService;

        public WatersController(IWaterIntakeService waterIntakeService)
        {
            this.waterIntakeService = waterIntakeService;
        }

        // GET: Waters
        [HttpGet]
        public IActionResult Index()
        {
            var userIdString = this.HttpContext.Session.GetString("UserId");
            if (userIdString != null)
            {
                long userId = long.Parse(userIdString);
                this.waterIntakeService.AddUserIfNotExists(userId);
                var intake = this.waterIntakeService.GetWaterIntake(userId);
                this.ViewBag.CurrentIntake = intake;
                return this.View();
            }

            return this.Forbid();
        }

        [HttpPost]
        [Route("waters/AddWater")]
        public IActionResult AddWater(float amount)
        {
            var userIdString = this.HttpContext.Session.GetString("UserId");
            if (userIdString != null)
            {
                long userId = long.Parse(userIdString);

                Debug.WriteLine("lalalalalala adding*******************");
                this.waterIntakeService.AddUserIfNotExists(userId);
                var intake = this.waterIntakeService.GetWaterIntake(userId);
                this.waterIntakeService.UpdateWaterIntake(userId, intake + amount);
                return this.RedirectToAction("Index");
            }

            return this.Forbid();
        }

        [HttpPost]
        [Route("water/RemoveWater")]
        public IActionResult RemoveWater(float amount)
        {
            var userIdString = this.HttpContext.Session.GetString("UserId");
            if (userIdString != null)
            {
                long userId = long.Parse(userIdString);
                Debug.WriteLine("lalalalalala deleting*******************");
                switch (amount)
                {
                    case 0.3f:
                        this.waterIntakeService.RemoveWater300(userId);
                        break;
                    case 0.4f:
                        this.waterIntakeService.RemoveWater400(userId);
                        break;
                    case 0.5f:
                        this.waterIntakeService.RemoveWater500(userId);
                        break;
                    case 0.75f:
                        this.waterIntakeService.RemoveWater750(userId);
                        break;
                }

                return this.RedirectToAction("Index");
            }

            return this.Forbid();
        }

        // GET: Waters/Details/5
        /*[HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.WaterTrackers
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.U_Id == id);
            if (water == null)
            {
                return NotFound();
            }

            return View(water);
        }

        // GET: Waters/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Waters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("U_Id,water_intake")] Water water)
        {
            if (ModelState.IsValid)
            {
                _context.Add(water);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email", water.U_Id);
            return View(water);
        }

        // GET: Waters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.WaterTrackers.FindAsync(id);
            if (water == null)
            {
                return NotFound();
            }
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email", water.U_Id);
            return View(water);
        }

        // POST: Waters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("U_Id,water_intake")] Water water)
        {
            if (id != water.U_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(water);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterExists(water.U_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email", water.U_Id);
            return View(water);
        }

        // GET: Waters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.WaterTrackers
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.U_Id == id);
            if (water == null)
            {
                return NotFound();
            }

            return View(water);
        }

        // POST: Waters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var water = await _context.WaterTrackers.FindAsync(id);
            if (water != null)
            {
                _context.WaterTrackers.Remove(water);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterExists(long id)
        {
            return _context.WaterTrackers.Any(e => e.U_Id == id);
        }*/
    }
}