using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDb.DataAccess.Entities;
using MyDictionary.Service.Mongo.Dtos;
using MyDictionary.Services.Dtos;
using MyDictionary.Services.Services;
using MyDictionary.Web.Cached;
using MyDictionary.Web.Models;

namespace MyDictionary.Web.Controllers
{
    public class ExampleController : Controller
    {
        private readonly ILogger<ExampleController> _logger;
        private IWordService _wordService; 
        private IExampleService _exampleService;
        private CachedService<ExampleDto> _cachedService;
        private IMapper _mapper;

        public ExampleController(ILogger<ExampleController> logger,
            IWordService wordService,
            IExampleService exampleService, 
            CachedService<ExampleDto> cachedService,
            IMapper mapper)
        {
            _logger = logger;
            _wordService = wordService;
            _exampleService = exampleService; 
            _cachedService = cachedService;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            var examples = _cachedService.GetCachedData();
            if (examples == null)
            {
                examples = _mapper.Map<IEnumerable<ExampleDto>>(await _exampleService.Filter(x => true));
                if (examples != null)
                {
                    _cachedService.SaveToCached(examples);
                }
            }
            var exampleIds = examples.Select(x => x.Id.ToString()).ToList();
            var words = await _wordService.Filter(x =>x.IsNew && x.ExampleIds.Any(id => exampleIds.Contains(id))); 
            foreach(var example in examples)
            {
                example.Words = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDto>>(words.Where(x=>x.ExampleIds.Contains(example.Id))).ToList();

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
        public async System.Threading.Tasks.Task<IActionResult> DetailAsync(string exampleId)
        {
            var example = await _exampleService.GetById(exampleId);
            var wordEntities = await _wordService.Filter(x => x.ExampleIds.Contains(exampleId) && x.IsNew);
            return View(new ExampleDetailDto { 
                Example = _mapper.Map<ExampleDto>(example), 
                NewWords = _mapper.Map<List<WordDto>>(wordEntities)
            });
        }

        [HttpPost("[controller]/Add")]
        public IActionResult Add([FromForm]WordAndExampleDto data)
        {
            _wordService.AddWordAndExampleAsync(data);
            _cachedService.Clear();
            return Redirect("Index");
        }

    }
}