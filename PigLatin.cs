using System;
using System.Linq;

public static class PigLatin
{
    public static string Translate(string word)
    {
        var spplitedWord = word.Split(" ");
        var finalWord = string.Empty;
        
        foreach(var singleWord in spplitedWord)
        {
            finalWord += $"{RunTranslation(singleWord)} ";
        }

        return finalWord.TrimEnd();
    }

    private static string RunTranslation(string word)
    {
        if (StartsWithVowels(word)) return $"{word}ay";

        string consonants = $"{word[0]}";
        string restOfWord = string.Empty;

        for (var i = 1; i < word.Length; i++)
        {
            if (IsAVowel($"{word[i]}", consonants, word.Length))
            {
                restOfWord = word[i..];
                break;
            }
            consonants += word[i];
        }
        return $"{restOfWord}{consonants}ay";
    }

    private static bool StartsWithVowels(string word)
    {
        var startVowels = CustomInitialVowels.Concat(DefaultVowels);
        return startVowels.Any(v => word.ToLower().StartsWith(v));
    }

    private static bool FitsYRule(string consonants, string charToValid, int wordLength)
    {
        if (!charToValid.Equals("y")) return false;
        
        // if y is on a two letter word or with a consonant cluster
        return wordLength == 2 || consonants.Length > 1;
    }

    private static bool IsAVowel(string charToValid, string consonants, int wordLength)
    {
        var lowerChar = charToValid.ToLower();
        var lowerConsonants = consonants.ToLower();

        // qu RULE
        if (lowerChar.Equals("u") && lowerConsonants[lowerConsonants.Length - 1] == 'q') return false;

        return DefaultVowels.Any(d => d.Equals(lowerChar))|| FitsYRule(consonants, lowerChar, wordLength);
    }

    private static string[] DefaultVowels = new string[] { "a", "e", "i", "o", "u"};
    private static string[] CustomInitialVowels = new string[] { "xr", "yt" };
}