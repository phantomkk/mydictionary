using System;
using System.Collections.Generic;
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
using MyDictionary.Web.Utils;

namespace MyDictionary.Web.Controllers
{
    public class ExampleController : Controller
    {
        private readonly ILogger<ExampleController> _logger;
        private IWordService _wordService;
        private IWordExampleService _wordExampleService;
        private IExampleService _exampleService;
        private CachedService<ExampleDto> _cachedService;
        private IMapper _mapper;

        public ExampleController(ILogger<ExampleController> logger,
            IWordService wordService,
            IExampleService exampleService,
            IWordExampleService wordExampleService, 
            CachedService<ExampleDto> cachedService,
            IMapper mapper)
        {
            _logger = logger;
            _wordService = wordService;
            _exampleService = exampleService;
            _wordExampleService = wordExampleService;
            _cachedService = cachedService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var examples = _cachedService.GetCachedData();
            if (examples == null)
            {
                examples = _mapper.Map<IEnumerable<ExampleDto>>(_exampleService.Filter(x => true));
                if (examples != null)
                {
                    _cachedService.SaveToCached(examples);
                }
            }
            var words = _wordExampleService.FilterByExampleIds(x => examples.Select(x=>x.Id).Any(id => id == x.ExampleId))
                .Where(x=> x.Word.IsNew).ToList();
            foreach(var example in examples)
            {
                example.Words = _mapper.Map<IEnumerable<WordExample>, IEnumerable<WordExampleDto>>(
                    words.Where(x => x.ExampleId == example.Id).ToList()).ToList();

            }
            return View(new ListExampleViewDto
            {
                Examples = examples.OrderByDescending(x => x.CreatedAt).AsEnumerable() ?? new List<ExampleDto>()
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet("[controller]/Detail/{exampleId}")]
        public IActionResult Detail(int exampleId)
        {
            var example = _exampleService.GetById(exampleId); 
            var wordEntities = _wordExampleService.FilterByExampleId(exampleId).ToList(); 
            return View(new ExampleDetailDto { 
                Example = _mapper.Map<ExampleDto>(example), 
                NewWords = _mapper.Map<List<WordDto>>(wordEntities.Select(x=>x.Word).Where(x=>x.IsNew).ToList())});
        }

        [HttpPost("[controller]/Add")]
        public IActionResult Add([FromForm]WordAndExampleDto data)
        {
            _wordService.AddWordAndExample(data);
            _cachedService.Clear();
            return Redirect("Index");
        }

    }
}