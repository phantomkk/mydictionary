using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyDictionary.DataAccess.Models;
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
        private IWordExampleService _wordExampleService;
        private CachedService<NewWord> _cachedService;
        private IMapper _mapper;
        public HomeController(ILogger<HomeController> logger,
            IWordService wordService,
            IWordExampleService wordExampleService,
            CachedService<NewWord> cachedService,
            IMapper mapper)
        {
            _logger = logger;
            _wordService = wordService;
            _wordExampleService = wordExampleService;
            _cachedService = cachedService;
            _mapper = mapper;
        }

        public IActionResult Index(string searchValue)
        { 
            IEnumerable<NewWord> result = _cachedService.GetCachedData();
            if (result == null)
            {
                  result = _wordService.GetNewWords();
                _cachedService.SaveToCached(result);
            } 
            return View(new ListWordViewDto { Words = result.ToList() });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public bool AddWord([FromBody]AddWordDto dto)
        {
            var result = _wordService.UpdateAsNewWord(dto);
            if (result)
            {
                _cachedService.Clear();
            }
            return result;
        }

        public bool DeleteNewWord(int wordId)
        {
            var word = _wordService.GetById(wordId);
            if (word != null)
            {
                word.IsNew = false;
                _wordService.Update(word);
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
