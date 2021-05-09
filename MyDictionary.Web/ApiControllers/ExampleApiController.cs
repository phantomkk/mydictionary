using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDictionary.Service.Mongo.Dtos;
using MyDictionary.Services.Dtos;
using MyDictionary.Services.Services;
using MyDictionary.Web.Cached;
using MyDictionary.Web.Models;

namespace MyDictionary.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleApiController : ControllerBase
    {


        private readonly ILogger<ExampleApiController> _logger;
        private IWordService _wordService; 
        private IExampleService _exampleService;
        private CachedService<ExampleDto> _cachedService;
        private IMapper _mapper;

        public ExampleApiController(ILogger<ExampleApiController> logger,
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

        [HttpGet]
        [Route("GetExamples")]
        public IEnumerable<ExampleDto> GetExamples()
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
            return examples.OrderByDescending(x => x.CreatedAt).AsEnumerable() ?? new List<ExampleDto>();
        }

        [HttpGet("[controller]/Detail/{exampleId}")]
        public async Task<ExampleDetailDto> DetailAsync(string exampleId)
        {
            var example = await _exampleService.GetById(exampleId);
            var wordEntities = await _wordService.Filter(x => x.ExampleIds.Contains(exampleId) && x.IsNew);
            return new ExampleDetailDto
            {
                Example = _mapper.Map<ExampleDto>(example),
                NewWords = _mapper.Map<List<WordDto>>(wordEntities.Select(x => x.Name))
            };
        }

        [HttpPost]
        [Route("Add")]
        public bool Add(WordAndExampleDto data)
        {
            _wordService.AddWordAndExampleAsync(data);
            _cachedService.Clear();
            return true;
        }
    }
}