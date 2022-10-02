using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using VajaSloven.Core.Base;
using VajaSloven.Core.Enums;

namespace VajaSlovenTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //I play happy
            var verbWord = new VerbWord("Igrati", "Igra", "Play");
            var adjWord = new AdjectiveWord("Dober", "Good", AdjectiveType.Person);
            adjWord.JazF = "Dobra";
            var p1 = new SentencePattern04(SubjectSyntax.JazF, VerbTens.Present, verbWord, adjWord).Generate();
            for (int i = 0; i < p1.Count; i++)
            {
                Debug.WriteLine(p1[i]);
            }
        }
   

        [TestMethod]
        public void TestMethod2()
        {
        //I am Moustafa
            var verbWord = new VerbWord("Biti", "Bi", VerbWord.TOBE);
            verbWord.JazFPresent = "sem";
            verbWord.JazMPresent = "sem";
           // var adjWord = new AdjectiveWord("Vesel", "Happy", AdjectiveType.Person);
            var nounWord = new NounWord("Moustafa", "Moustafa", NounType.PersonName);
            var p1 = new SentencePattern01(SubjectSyntax.JazM, VerbTens.Futur, verbWord, nounWord).Generate();
            for (int i = 0; i < p1.Count; i++)
            {
                Debug.WriteLine(p1[i]);
            }
        }
    }
}