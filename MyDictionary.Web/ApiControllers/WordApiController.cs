﻿using System;
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

namespace MyDictionary.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordApiController : ControllerBase
    {

        private readonly ILogger<WordApiController> _logger;
        private IWordService _wordService; 
        private CachedService<NewWord> _cachedService;
        private IMapper _mapper;
        public WordApiController(ILogger<WordApiController> logger,
            IWordService wordService, 
            CachedService<NewWord> cachedService,
            IMapper mapper)
        {
            _logger = logger;
            _wordService = wordService; 
            _cachedService = cachedService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetNewWords")]
        public async Task<IEnumerable<NewWord>> GetNewWordsAsync()
        {
            IEnumerable<NewWord> result = _cachedService.GetCachedData();
            if (result == null)
            {
                result = await _wordService.GetNewWords();
                _cachedService.SaveToCached(result);
            }
            return result;
        }
        [HttpGet]
        [Route("Search")]
        public IEnumerable<WordDto> Search(string searchValue)
        {
            IEnumerable<WordDto> list = _mapper.Map<IEnumerable<WordDto>>(
                    _wordService.Filter(x => searchValue != null && x.Name.Contains(searchValue)));

            return list.OrderByDescending(x => x.CreatedAt).ToList();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<bool> AddAsync([FromBody]AddWordDto dto)
        {
            var result = await _wordService.UpdateAsNewWordAsync(dto);
            if (result)
            {
                _cachedService.Clear();
            }
            return result;
        }

        [HttpGet]
        [Route("DeleteNewWord")]
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

    }
}