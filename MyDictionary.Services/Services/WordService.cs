using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDictionary.DataAccess;
using MyDictionary.DataAccess.Models;
using MyDictionary.DataAccess.Repositories;
using MyDictionary.Services.Dtos;
using MyDictionary.Services.Utils;
using MyDictionary.Web.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MyDictionary.Services.Services
{
    public class WordService : GenericService<Word>, IWordService
    {
        private IWordExampleRepos _wordExampleRepos;
        private IExampleRepos _exampleRepos;
        private IMapper _mapper;

        public WordService(IUnitOfWork<DictionaryDbContext> unitOfWork,
            IWordRepos repos,
            IWordExampleRepos wordExampleRepos,
            IExampleRepos exampleRepos,
            IMapper mapper
            ) : base(unitOfWork, repos)
        {
            _wordExampleRepos = wordExampleRepos;
            _exampleRepos = exampleRepos;
            _mapper = mapper;
        }
        public void AddWordAndExample(WordAndExampleDto dto)
        {
            _unitOfWork.BeginTransaction();
            var description = dto.Description;
            var distinctedWords = WordUtils.ExtractDistinct(description);
            //var tokens = Regex.Split(dto.Description, "\r\n").Where(x => x.Trim() != string.Empty).ToArray();
            //if (tokens.Length % 2 != 0)
            //{
            //    var errors = new List<string>() { "Khong the luu description" };

            //    throw new HttpResponseException(new HttpResponseMessage() { 
            //        Content = new StringContent("Khong the luu description"), StatusCode = HttpStatusCode.BadRequest });
            //}
            var idxSub = description.Length > 200 ? description.Substring(0, 200) : description.Substring(0, description.Length/2);
            var example = _exampleRepos.Create(new Example
            {
                Name = idxSub,
                Description = description,
                CreatedAt = DateTimeOffset.Now,
                Url = dto.Url
            });
            var wordsInExample = CheckWords(distinctedWords);

            _unitOfWork.Save();
            var newWordExample = wordsInExample.Select(x => new WordExample
            {
                ExampleId = example.Id,
                WordId = x.Id
            });
            _wordExampleRepos.AddListWordExample(newWordExample);
            _unitOfWork.Save();
            _unitOfWork.Commit();

        }

        public IEnumerable<NewWord> GetNewWords()
        {
            var firs = DateTime.Now.Second;
            var listNewWords = _mapper.Map<IEnumerable<WordDto>>(
                      _mainRepos.Filter(x => x.IsNew).ToList()).ToList();
            var wordExamples = _wordExampleRepos.FilterByWordId(listNewWords.Select(w => w.Id).ToList()).ToList();

            var sec = DateTime.Now.Second;
            var resultt = sec - firs;
            var result = listNewWords.Select(x =>
            {
                var firstExample = wordExamples.First(e => e.WordId == x.Id).Example.Description; ;
                var wordIndex = firstExample.IndexOf(x.Name);
                var firstSection = firstExample.Substring(wordIndex - 100 > 0 ? wordIndex - 100 : 0, 100);
                var secondSection = firstExample.Substring(wordIndex + x.Name.Length, 100 <= firstExample.Length ? 100 : firstExample.Length);


                return new NewWord
                {
                    Id = x.Id,
                    Name = x.Name,
                    FirstExampleSection = firstSection,
                    SecondExampleSection = secondSection,
                    CreatedAt = x.CreatedAt
                };


            });
            return result.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public bool UpdateAsNewWord(AddWordDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var word = _mainRepos.Filter(x => x.Name == dto.Word.ToLower().Trim()).FirstOrDefault();
                if (word == null)
                {
                    var newWord = _mainRepos.Create(new Word
                    {
                        IsNew = true,
                        Name = dto.Word,
                        CreatedAt = DateTimeOffset.Now
                    });
                    _unitOfWork.Save();

                    _wordExampleRepos.Create(new WordExample
                    {
                        ExampleId = dto.ExampleId,
                        WordId = newWord.Id
                    });
                }
                else
                {
                    word.IsNew = true;
                    _mainRepos.Update(word);
                }
                _unitOfWork.Save();

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private List<Word> CheckWords(IEnumerable<string> words)
        {
            var allWords = _mainRepos.Filter(x => true);
            var wordsInExample = new List<Word>();
            //   System.IO.File.WriteAllText("WEB.txt", words.Aggregate((x, y) => x + " " + y));
            //  System.IO.File.WriteAllText("DB.txt", allWords.Aggregate("", (x, y) => (x + " " + y.Name)));
            foreach (var word in words)
            {
                var currentWord = allWords.FirstOrDefault(x => word == x.Name);
                if (currentWord != null)
                {
                    // currentWord.Occurrences++;
                    //_mainRepos.Update(currentWord);
                    wordsInExample.Add(currentWord);
                }
                else
                {
                    var newWord = _mainRepos.Create(new Word
                    {
                        CreatedAt = DateTimeOffset.Now,
                        Name = word
                    });
                    wordsInExample.Add(newWord);
                }
            }

            return wordsInExample;
        }

    }
}
