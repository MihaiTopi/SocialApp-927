namespace ServerMVCProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;
    using ServerMVCProject.Models;

    public class DashboardController : Controller
    {
        private readonly ICalorieService _calorieService;
        private readonly IUserService _userService;

        public DashboardController(ICalorieService calorieService, IUserService userService)
        {
            _calorieService = calorieService;
            _userService = userService;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                long userId = long.Parse(userIdString);
                // Get user by username
                var user = _userService.GetById(userId);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User '{user.Username}' not found. Using default values.";
                    return View(new CaloriesViewModel
                    {
                        Username = user.Username,
                        DailyGoal = 2000,
                        CaloriesConsumed = 0,
                        CaloriesBurned = 0
                    });
                }

                // Get actual calorie data for the user
                var viewModel = new CaloriesViewModel
                {
                    Username = user.Username,
                    DailyGoal = _calorieService.GetGoal(user.Id),
                    CaloriesConsumed = _calorieService.GetFood(user.Id),
                    CaloriesBurned = _calorieService.GetExercise(user.Id)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Dashboard: {ex.Message}");
                ViewBag.ErrorMessage = $"Error retrieving user data: {ex.Message}";

                return View(new CaloriesViewModel
                {
                    Username = "",
                    DailyGoal = 2000,
                    CaloriesConsumed = 0,
                    CaloriesBurned = 0
                });
            }
        }
    }
}