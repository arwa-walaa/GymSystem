using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnaliticsService _analiticsService;

        public HomeController(IAnaliticsService  analiticsService)
        {
            _analiticsService = analiticsService;
        }
        public IActionResult Index()
        {
            var analiticsData = _analiticsService.GetAnaliticsData();

            return View(analiticsData);
        }
    }
}
