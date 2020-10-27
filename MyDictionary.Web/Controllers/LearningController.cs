using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MyDictionary.Services.Services;
using MyDictionary.Web.Cached;

namespace MyDictionary.Web.Controllers
{
    public class LearningController : Controller
    {
        private ILearningService _learningService;
        private LearningCached _learningCached;
        public LearningController(ILearningService learningService, LearningCached learningCached)
        {
            _learningService = learningService;
            _learningCached = learningCached;
        }

        public IActionResult Index()
        {
            var model = _learningCached.GetNext();
            if (model.Equals(default(KeyValuePair<string, string>)))
            {
                _learningCached.InitCached(_learningService.InitRandomParagraph());
                model = _learningCached.GetNext();
            }

            return View(model);
        }
    }
}
