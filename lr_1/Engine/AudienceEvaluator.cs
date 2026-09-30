using Lr_1.Enum;
using Lr_1.Models;

namespace Lr_1.Engine
{
    public class AudienceEvaluator
    {
        public MatchResult Evaluate(
            Anime anime,
            TargetAudience userAudience)
        {
            double score = CalculateAudienceMatch(
                userAudience,
                anime.TargetAudience
            );

            string reason = CreateReason(
                userAudience,
                anime.TargetAudience,
                score
            );

            return new MatchResult
            {
                Score = score,
                Reason = reason
            };
        }

        private double CalculateAudienceMatch(
            TargetAudience userAudience,
            TargetAudience animeAudience)
        {
            if (userAudience == animeAudience)
            {
                return 100;
            }

            return 0;
        }

        private string CreateReason(
            TargetAudience userAudience,
            TargetAudience animeAudience,
            double score)
        {
            return $"Бажана аудиторія: {userAudience}, " +
                   $"аудиторія аніме: {animeAudience} " +
                   $"→ {score:0.##}%.";
        }
    }
}