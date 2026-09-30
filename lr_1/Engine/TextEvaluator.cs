using Lr_1.Models;

namespace Lr_1.Engine
{
    public class TextEvaluator
    {
        public MatchResult Evaluate(
            Anime anime,
            string userDescription)
        {
            if (string.IsNullOrWhiteSpace(userDescription))
            {
                return new MatchResult
                {
                    Score = 0,
                    Reason = "Користувач не вказав текстовий опис побажань."
                };
            }

            List<string> userWords = ExtractWords(
                userDescription
            );

            List<string> animeWords = ExtractWords(
                anime.Description
            );

            List<string> matchedWords = FindMatches(
                userWords,
                animeWords
            );

            double score = CalculateTextScore(
                userWords,
                matchedWords
            );

            string reason = CreateReason(
                matchedWords,
                score
            );

            return new MatchResult
            {
                Score = score,
                Reason = reason
            };
        }

        private List<string> ExtractWords(
            string text)
        {
            string normalizedText = text
                .ToLower()
                .Trim();

            char[] separators =
            {
                ' ',
                ',',
                '.',
                '!',
                '?',
                ':',
                ';',
                '-',
                '(',
                ')'
            };

            string[] words = normalizedText.Split(
                separators,
                StringSplitOptions.RemoveEmptyEntries
            );

            return new List<string>(words);
        }

        private List<string> FindMatches(
            List<string> userWords,
            List<string> animeWords)
        {
            List<string> matchedWords = new();

            for (int i = 0; i < userWords.Count; i++)
            {
                for (int j = 0; j < animeWords.Count; j++)
                {
                    if (userWords[i] == animeWords[j])
                    {
                        if (!matchedWords.Contains(userWords[i]))
                        {
                            matchedWords.Add(userWords[i]);
                        }

                        break;
                    }
                }
            }

            return matchedWords;
        }

        private double CalculateTextScore(
            List<string> userWords,
            List<string> matchedWords)
        {
            if (userWords.Count == 0)
            {
                return 0;
            }

            return (double)matchedWords.Count /
                   userWords.Count *
                   100;
        }

        private string CreateReason(
            List<string> matchedWords,
            double score)
        {
            if (matchedWords.Count == 0)
            {
                return "Збігів у текстовому описі не знайдено → 0%.";
            }

            return $"Знайдені слова: " +
                   $"{string.Join(", ", matchedWords)} " +
                   $"→ {score:0.##}%.";
        }
    }
}