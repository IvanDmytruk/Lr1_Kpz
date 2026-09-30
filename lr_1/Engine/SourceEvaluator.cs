using Lr_1.Enum;
using Lr_1.Models;

namespace Lr_1.Engine
{
    public class SourceEvaluator
    {
        public MatchResult Evaluate(
            Anime anime,
            List<Source> userSources)
        {
            if (userSources == null || userSources.Count == 0)
            {
                return new MatchResult
                {
                    Score = 0,
                    Reason = "Користувач не вказав бажані джерела."
                };
            }

            List<double> scores = new();
            List<string> reasons = new();

            for (int i = 0; i < userSources.Count; i++)
            {
                Source userSource = userSources[i];

                Source? animeSource = FindSource(
                    anime,
                    userSource.Type
                );

                double score = CalculateSourceMatch(
                    userSource,
                    animeSource
                );

                scores.Add(score);

                reasons.Add(
                    CreateReason(
                        userSource,
                        animeSource,
                        score
                    )
                );
            }

            double finalScore = CalculateFinalScore(
                scores
            );

            return new MatchResult
            {
                Score = finalScore,
                Reason = string.Join(
                    Environment.NewLine,
                    reasons
                )
            };
        }

        private Source? FindSource(
            Anime anime,
            SourceType sourceType)
        {
            for (int i = 0; i < anime.Sources.Count; i++)
            {
                if (anime.Sources[i].Type == sourceType)
                {
                    return anime.Sources[i];
                }
            }

            return null;
        }

        private double CalculateSourceMatch(
            Source userSource,
            Source? animeSource)
        {
            if (animeSource == null)
            {
                return 0;
            }

            if (userSource.Type != animeSource.Type)
            {
                return 0;
            }

            return 100;
        }

        private double CalculateFinalScore(
            List<double> scores)
        {
            if (scores.Count == 0)
            {
                return 0;
            }

            double totalScore = 0;

            for (int i = 0; i < scores.Count; i++)
            {
                totalScore += scores[i];
            }

            return totalScore / scores.Count;
        }

        private string CreateReason(
            Source userSource,
            Source? animeSource,
            double score)
        {
            if (animeSource == null)
            {
                return $"Джерело {userSource.Type}: " +
                       "відсутнє в аніме → 0%.";
            }

            return $"Джерело {userSource.Type}: " +
                   $"присутнє в аніме, " +
                   $"статус джерела {animeSource.Status} " +
                   $"→ {score:0.##}%.";
        }
    }
}