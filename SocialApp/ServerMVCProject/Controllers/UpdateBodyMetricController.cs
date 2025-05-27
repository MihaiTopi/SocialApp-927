namespace ServerMVCProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;
    using ServerMVCProject.Models;

    [Route("bodymetrics")]
    public class UpdateBodyMetricController : Controller
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public UpdateBodyMetricController(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository;
            this.userService = userService;
        }

        [Route("update")]
        [HttpGet]
        public IActionResult Update()
        {
            var userIdString = this.HttpContext.Session.GetString("UserId");
            if (userIdString != null)
            {
                long userId = long.Parse(userIdString);
                string username = this.userService.GetById(userId).Username;
                if (string.IsNullOrEmpty(username))
                {
                    return this.RedirectToAction("Index", "Home");
                }

                // Create the view model and populate it with the username
                var viewModel = new UpdateBodyMetricViewModel
                {
                    Username = username,
                };

                return this.View(viewModel);
            }

            return this.Forbid();
        }

        [HttpPost("update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateBodyMetricViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                // Find user by username
                var user = this.userService.GetUserByUsername(model.Username);
                if (user == null)
                {
                    this.ModelState.AddModelError("", $"User '{model.Username}' not found");
                    return this.View(model);
                }

                // Update the user's body metrics
                this.bodyMetricRepository.UpdateUserBodyMetrics(
                    user.Id,
                    model.Weight,
                    model.Height,
                    model.TargetWeight);

                this.ViewBag.Message = "Body metrics updated successfully!";

                // After successful update, redirect to the goals page
                // return RedirectToAction("Update", new { username = model.Username });
                return this.RedirectToAction("Dashboard", "Dashboard");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating body metrics: {ex.Message}");
                this.ViewBag.Message = $"Error updating body metrics: {ex.Message}";
                return this.View(model);
            }
        }

        public IActionResult Success()
        {
            this.ViewBag.Message = "Body metrics updated successfully! (Redirect to Goal/Update will work when implemented)";
            return this.View(); // Stay on the same page with a message
        }
    }
}