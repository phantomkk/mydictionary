using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Services.Services
{
    public interface ILearningService
    {
        void RefreshParagraphs();

        Stack<KeyValuePair<string, string>> InitRandomParagraph();


    }
}
