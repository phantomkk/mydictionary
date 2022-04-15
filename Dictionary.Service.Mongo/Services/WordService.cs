using AutoMapper;
using Infrastructure.MongoDb;
using MongoDb.DataAccess.Entities;
using MyDictionary.Service.Mongo.Dtos;
using MyDictionary.Services.Dtos;
using MyDictionary.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Services.Services
{
    public class WordService : GenericService<Word>, IWordService
    {
        private IBaseRepository<Example> _exampleRepos;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public WordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _exampleRepos = unitOfWork.CreateRepo<Example>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task AddWordAndExampleAsync(WordAndExampleDto dto)
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
            var idxSub = description.Length > 200 ? description.Substring(0, 200) : description.Substring(0, description.Length / 2);
            var example = await _exampleRepos.Create(new Example
            {
                Name = idxSub,
                Description = description,
                CreatedAt = DateTimeOffset.Now,
                Url = dto.Url
            });
            await CheckWords(distinctedWords, example.Id.ToString());

            await _unitOfWork.CommitTransaction();

        }

        public async Task<IEnumerable<NewWord>> GetNewWords()
        {
            var firs = DateTime.Now.Second;
            var lst =
                      await _mainRepos.Filter(x => x.IsNew);
            var listNewWords = _mapper.Map<IEnumerable<WordDto>>(lst).ToList();

            var sec = DateTime.Now.Second;
            var resultt = sec - firs;
            var result = await Task.WhenAll(listNewWords.Select(async x =>
           {
               var firstExample = (await _exampleRepos.Filter(e=> e.Description.Contains(x.Name))).FirstOrDefault();


               var wordIndex = firstExample.Description.IndexOf(x.Name);
               var firstSection = firstExample.Description.Substring(wordIndex - 100 > 0 ? wordIndex - 100 : 0, 100);
               var endSecIndex = wordIndex + x.Name.Length + 100 >= firstExample.Description.Length ?   firstExample.Description.Length - (wordIndex + x.Name.Length) : 100;
               var secondSection = firstExample.Description.Substring(wordIndex + x.Name.Length, endSecIndex);


               return new NewWord
               {
                   Id = x.Id,
                   Name = x.Name,
                   FirstExampleSection = firstSection,
                   SecondExampleSection = secondSection,
                   CreatedAt = x.CreatedAt
               };


           }));
            return result.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public async Task<bool> UpdateAsNewWordAsync(AddWordDto dto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var word = (await _mainRepos.Filter(x => x.Name == dto.Word.ToLower().Trim())).FirstOrDefault();
                if (word == null)
                {
                    var newWord = _mainRepos.Create(new Word
                    {
                        IsNew = true,
                        Name = dto.Word,
                        CreatedAt = DateTimeOffset.Now,
                        ExampleIds = new List<string> { dto.ExampleId }
                    });
                }
                else
                {
                    word.IsNew = true;
                    await _mainRepos.Update(word);
                }

                await _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.AbortTransaction();
                throw ex;
            }
        }

        private async Task<List<Word>> CheckWords(IEnumerable<string> words, string exampleId)
        {
            var allWords = await _mainRepos.Filter(x => true);
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
                    currentWord.ExampleIds.Add(exampleId);

                    await _unitOfWork.CreateRepo<Word>().Update(currentWord);
                }
                else
                {
                    var newWord = await _mainRepos.Create(new Word
                    {
                        CreatedAt = DateTimeOffset.Now,
                        Name = word,
                        ExampleIds = new List<string> { exampleId }
                    });
                    wordsInExample.Add(newWord);
                }
            }

            return wordsInExample;
        }

    }
}
