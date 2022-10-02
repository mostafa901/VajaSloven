using System;

namespace VajaSloven.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class PropertyTransAttribute : Attribute
    {

        public PropertyTransAttribute(string sloveWord, string englishWord)
        {
            SloveWord = sloveWord;
            EnglishWord = englishWord;
        }

        public string SloveWord { get; }
        public string EnglishWord { get; }
    }
}