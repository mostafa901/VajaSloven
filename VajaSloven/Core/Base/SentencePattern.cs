using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Kobi.Utility.Extentions;

using VajaSloven.Core.Attributes;
using VajaSloven.Core.Enums;

namespace VajaSloven.Core.Base
{
    public abstract class SentencePattern
    {
        protected SubjectSyntax Subject;
        protected VerbTens Tens;
        protected VerbWord Verb;
        protected AdjectiveWord AdjWord;
        protected NounWord NounWord;

        protected abstract Type[] EnglishPatern { get; set; }

        private string ResolvePattern(Type[] pattern)
        {
            List<string> words = new List<string>();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == typeof(SubjectSyntax)) words.Add(Subject.GetEnumAttribute<PropertyTransAttribute>().SloveWord);
                if (pattern[i] == typeof(VerbWord)) words.Add(Verb.GetSlovenianVersion(Subject, Tens, i == 0));
                if (pattern[i] == typeof(AdjectiveType)) words.Add(AdjWord.Get(Subject));
                if (pattern[i] == typeof(NounType)) words.Add(NounWord.Get(Subject));
            }
            string sentence = String.Join(" ", words);
            return sentence.ToSentenceCase();
        }

        protected string GetEnglishVersion(Type[] pattern)
        {
            List<string> words = new List<string>();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == typeof(SubjectSyntax)) words.Add(Subject.GetEnumAttribute<PropertyTransAttribute>().EnglishWord);
                if (pattern[i] == typeof(VerbWord)) words.Add(Verb.GetEnglishVersion(Subject, Tens));
                if (pattern[i] == typeof(AdjectiveType)) words.Add(AdjWord.GetEnglishVersion());
                if (pattern[i] == typeof(NounType)) words.Add(NounWord.GetEnglishVersion());
            }
            string sentence = String.Join(" ", words).ToSentenceCase();
            if (NounWord?.Noun == NounType.PersonName)
            {
                sentence= sentence.Replace(NounWord.EnglishTrans, NounWord.GetEnglishVersion(), StringComparison.OrdinalIgnoreCase);
            }
            return sentence;
        }

        protected List<string> Generate(IEnumerable<FieldInfo> patterns)
        {
            List<string> sentences = new List<string>();
            foreach (var prop in patterns)
            {
                sentences.Add(ResolvePattern(prop.GetValue(this) as Type[]));
            }
            return sentences;
        }

        public virtual List<string> Generate()
        {
            List<string> sentences = new List<string>();
            var props = GetType().GetFields(BindingFlags.NonPublic |
                         BindingFlags.Instance).Where(o => o.Name.StartsWith("pattern"));
            sentences = Generate(props);
            return sentences;
        }
    }

    /// <summary> PersonAdjective </br> I am Ahmed</summary>
    public class SentencePattern01 : SentencePattern
    {
        private Type[] pattern04 = new Type[3] { typeof(SubjectSyntax), typeof(VerbWord), typeof(NounType) };
        private Type[] pattern041 = new Type[2] { typeof(NounType), typeof(VerbWord) };
        private Type[] pattern042 = new Type[2] { typeof(VerbWord), typeof(NounType) };
        private Type[] pattern043 = new Type[3] { typeof(NounType), typeof(SubjectSyntax), typeof(VerbWord) };

        public SentencePattern01(SubjectSyntax subject, VerbTens tens, VerbWord verbWord, NounWord nounWord)
        {
            Debug.Assert(nounWord.Noun == NounType.PersonName);
            EnglishPatern = new Type[3] { typeof(SubjectSyntax), typeof(VerbWord), typeof(NounType) };
            this.Subject = subject;
            this.Tens = tens;
            this.Verb = verbWord;
            this.NounWord = nounWord;
        }

        protected override Type[] EnglishPatern { get; set; }

        public override List<string> Generate()
        {
            var sentences = base.Generate();
            sentences.Add(GetEnglishVersion(EnglishPatern));
            return sentences;
        }
    }

    /// <summary> PersonAdjective </br> He is Happy </summary>
    public class SentencePattern04 : SentencePattern
    {
        private Type[] pattern04 = new Type[3] { typeof(SubjectSyntax), typeof(VerbWord), typeof(AdjectiveType) };
        private Type[] pattern041 = new Type[2] { typeof(AdjectiveType), typeof(VerbWord) };
        private Type[] pattern042 = new Type[2] { typeof(VerbWord), typeof(AdjectiveType) };
        private Type[] pattern043 = new Type[3] { typeof(AdjectiveType), typeof(SubjectSyntax), typeof(VerbWord) };

        public SentencePattern04(SubjectSyntax subject, VerbTens tens, VerbWord verbWord, AdjectiveWord adjWord)
        {
            Debug.Assert(adjWord.Adjective == AdjectiveType.Person);
            EnglishPatern = new Type[3] { typeof(SubjectSyntax), typeof(VerbWord), typeof(AdjectiveType) };
            this.Subject = subject;
            this.Tens = tens;
            this.Verb = verbWord;
            this.AdjWord = adjWord;
        }

        protected override Type[] EnglishPatern { get; set; }

        public override List<string> Generate()
        {
            var sentences = base.Generate();
            sentences.Add(GetEnglishVersion(EnglishPatern));
            return sentences;
        }
    }

    public class SentencePattern02 : SentencePattern
    {
        public const Enums.NounType NounSyntax = Enums.NounType.Object;
        public const Enums.Count CountSyntax = Enums.Count.One;

        protected override Type[] EnglishPatern { get; set; }

        //string sample: This is an Apple
    }

    public class SentencePattern03 : SentencePattern
    {
        public const Enums.NounType NounSyntax = Enums.NounType.Location;
        protected override Type[] EnglishPatern { get; set; }

        //string sample: This is Egypt
    }
}