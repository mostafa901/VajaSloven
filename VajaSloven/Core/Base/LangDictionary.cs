using System;
using System.Linq;

using VajaSloven.Core.Enums;

namespace VajaSloven.Core.Base
{
    public abstract class Word
    {
        public const string TOBE = "To Be";
        public readonly char[] vowels = new char[5] { 'a', 'i', 'o', 'e', 'u' };
        public string SlovenKey { get; set; }
        public string EnglishTrans { get; set; }
        public string WordSyntax { get; set; }

        public Word()
        {
        }

        public Word(string slovenKey, string trans)
        {
            SlovenKey = slovenKey;
            EnglishTrans = trans;
        }
    }


    public class NounWord : Word
    {
        public string JazM { get; set; }
        public string JazF { get; set; }
        public NounType Noun { get; set; }

        public NounWord(string slovenKey, string englishTrans, NounType noun) : base(slovenKey, englishTrans)
        {
            var subjects = Enum.GetValues(typeof(SubjectSyntax)).Cast<SubjectSyntax>();
            //auto conjugate and store conjugated form
            foreach (var subj in subjects)
            {
                var prop = GetType().GetProperty($"{subj}");
                prop.SetValue(this, Conjugate(subj));
            }
            Noun = noun;
        }

        private string Conjugate(SubjectSyntax subj)
        {
            string result = SlovenKey;
            switch (subj)
            {
                case SubjectSyntax.JazM:
                    break;

                case SubjectSyntax.JazF:
                    result += "a";
                    break;

                default:
                    break;
            }

            return result;
        }

        internal string Get(SubjectSyntax subject)
        {
            return GetType().GetProperty(subject.ToString()).GetValue(this).ToString();
        }

        internal string GetEnglishVersion()
        {
                return EnglishTrans;
        }

    }
    public class AdjectiveWord : Word
    {
        public string JazM { get; set; }
        public string JazF { get; set; }
        public AdjectiveType Adjective { get; set; }

        public AdjectiveWord(string slovenKey, string englishTrans, AdjectiveType adjective) : base(slovenKey, englishTrans)
        {
            var subjects = Enum.GetValues(typeof(SubjectSyntax)).Cast<SubjectSyntax>();
            //auto conjugate and store conjugated form
            foreach (var subj in subjects)
            {
                var prop = GetType().GetProperty($"{subj}");
                prop.SetValue(this, Conjugate(subj));
            }
            Adjective = adjective;
        }

        private string Conjugate(SubjectSyntax subj)
        {
            string result = SlovenKey;
            switch (subj)
            {
                case SubjectSyntax.JazM:
                    break;

                case SubjectSyntax.JazF:
                    result += "a";
                    break;

                default:
                    break;
            }

            return result;
        }

        internal string Get(SubjectSyntax subject)
        {
            return GetType().GetProperty(subject.ToString()).GetValue(this).ToString();
        }

        internal string GetEnglishVersion()
        {
                return EnglishTrans;
        }
    }

    public class VerbWord : Word
    {
        public VerbWord(string slovenKey, string baseConjForm, string englishTrans) : base(slovenKey, englishTrans)
        {
            BasePresentConjForm = baseConjForm;
            BaseTenseConjForm = baseConjForm;

            Initialize();
        }

        public void Initialize()
        {
            var subjects = Enum.GetValues(typeof(SubjectSyntax)).Cast<SubjectSyntax>();
            var verbTens = Enum.GetValues(typeof(VerbTens)).Cast<VerbTens>();
            //auto conjugate and store conjugated form
            foreach (var subj in subjects)
            {
                foreach (var tens in verbTens)
                {
                    var prop = GetType().GetProperty($"{subj}{tens}");
                    prop.SetValue(this, Conjugate(tens, subj));
                }
            }
        }

        public string BasePresentConjForm { get; set; }
        public string BaseTenseConjForm { get; set; }
        public string JazMPast { get; set; }
        public string JazMPresent { get; set; }
        public string JazMFutur { get; set; }
        public string JazFPast { get; set; }
        public string JazFPresent { get; set; }
        public string JazFFutur { get; set; }

        public string Conjugate(VerbTens verbTens, SubjectSyntax subject)
        {
            string result = BasePresentConjForm;
            switch (subject)
            {
                case SubjectSyntax.JazM:
                    {
                        switch (verbTens)
                        {
                            case VerbTens.Present:
                                result = BasePresentConjForm + "m";
                                break;

                            case VerbTens.Past:
                            case VerbTens.Futur:

                                result = BasePresentConjForm + "l";
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case SubjectSyntax.JazF:
                    switch (verbTens)
                    {
                        case VerbTens.Present:
                            result = BasePresentConjForm + "m";
                            break;

                        case VerbTens.Past:
                        case VerbTens.Futur:
                            result = BasePresentConjForm + "la";
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
            switch (verbTens)
            {
                case VerbTens.Present:
                    break;

                case VerbTens.Past:
                    {
                        result = "sem " + result;
                    }
                    break;

                case VerbTens.Futur:
                    result = "bom " + result;
                    break;

                default:
                    break;
            }
            return result;
        }

        public string GetSlovenianVersion(SubjectSyntax subject, VerbTens tens, bool start = false)
        {
            var result = GetType().GetProperty($"{subject}{tens}").GetValue(this).ToString();
            if (start && tens != VerbTens.Present)
            {
                string[] words = new string[2];
                var results = result.Split(' ');
                words[0] = results[1];
                words[1] = results[0];
                result = String.Join(" ", words);
            }

            return result;
        }

        public string GetEnglishVersion(SubjectSyntax subject, VerbTens tens)
        {
            string result = EnglishTrans;
            switch (subject)
            {
                case SubjectSyntax.JazF:
                case SubjectSyntax.JazM:
                    switch (tens)
                    {
                        case VerbTens.Present:
                            {
                                result = "am";

                                if (EnglishTrans != TOBE)
                                {
                                    result = $"{result} {EnglishTrans}ing";
                                }
                            }
                            break;

                        case VerbTens.Past:
                            {
                                result = "was";

                                if (EnglishTrans != TOBE)
                                {
                                    result = $"{result} {EnglishTrans}ing";
                                }
                            }
                            break;

                        case VerbTens.Futur:
                            {
                                result = "will be";

                                if (EnglishTrans != TOBE)
                                {
                                    result = $"will {EnglishTrans}";
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                    break;

                default:
                    break;
            }
            return result;
        }
    }
}