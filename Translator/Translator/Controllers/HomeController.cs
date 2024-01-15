using Azure.AI.Translation.Text;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Translator.Models;

namespace Translator.Controllers
{
    public class HomeController : Controller
    {
        public IReadOnlyDictionary<string, TranslationLanguage> Languages { get; private set; } = new Dictionary<string, TranslationLanguage>();
        public HomeController()
        {
            InitializeAsync().Wait();
        }

        private async Task InitializeAsync()
        {
            await Task.Run(async () =>
            {
                await GetLanguages();
            });
        }

        private async Task GetLanguages()
        {
            Languages = await TranslatorService.GetLanguages();
        }

        public IActionResult Index()
        {
            return View(Languages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Translate(string input, string? sourceLang, string? targetLang)
        {
            if (sourceLang == "null")
            {
                sourceLang = null;
            }
            if (!string.IsNullOrEmpty(input))
            {
                var result = await TranslatorService.Translate(input, sourceLang, targetLang);
                return Json(result);
            }
            return BadRequest();
        }
    }
}
