using System;

using VajaSloven.Core.Attributes;

namespace VajaSloven.Core.Enums
{
    public enum Syntax
    {
        Subject,
        Verb,
        Noune,
        Adjective,
        Preoposition,
    }

    [Flags]
    public enum VerbTens
    {
        Present = 0,
        Past = 2 ^ 0,
        Futur = 2 ^ 1
    }

    [Flags]
    public enum SubjectSyntax
    {
        [PropertyTrans("Jaz", "I")]
        JazM = 0,

        [PropertyTrans("Jaz", "I")]
        JazF = 2 ^ 0,
    }

    [Flags]
    public enum NounType
    {
        None = 0,
        PersonName = 2 ^ 0,
        Location = 2 ^ 1,
        Object = 2 ^ 2,
    }

    [Flags]
    public enum AdjectiveType
    {
        None = 0,
        Person = 2 ^ 0,
        Country = 2 ^ 1,
        Object = 2 ^ 2,
      
    }

    [Flags]
    public enum Count
    {
        None = 0,
        One = 2 ^ 0,
        Two = 2 ^ 1,
        Three = 2 ^ 2,
    }
}