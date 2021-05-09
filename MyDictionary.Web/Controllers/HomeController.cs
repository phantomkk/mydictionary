using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Services.Dtos;
using MyDictionary.Services.Services;
using MyDictionary.Web.Cached;
using MyDictionary.Web.Models;

namespace MyDictionary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWordService _wordService; 
        private CachedService<NewWord> _cachedService;
        private IMapper _mapper;
        public HomeController(ILogger<HomeController> logger,
            IWordService wordService, 
            CachedService<NewWord> cachedService,
            IMapper mapper)
        {
            _logger = logger;
            _wordService = wordService; 
            _cachedService = cachedService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync(string searchValue)
        { 
            IEnumerable<NewWord> result = _cachedService.GetCachedData();
            if (result == null)
            {
                  result = await _wordService.GetNewWords();
                _cachedService.SaveToCached(result);
            } 
            return View(new ListWordViewDto { Words = result.ToList() });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> AddWordAsync([FromBody]AddWordDto dto)
        {
            var result = await _wordService.UpdateAsNewWordAsync(dto);
            if (result)
            {
                _cachedService.Clear();
            }
            return result;
        }

        public async Task<bool> DeleteNewWordAsync(string wordId)
        {
            var word = await _wordService.GetById(wordId);
            if (word != null)
            {
                word.IsNew = false;
                await _wordService.Update(word);
            }
            _cachedService.Clear();
            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
