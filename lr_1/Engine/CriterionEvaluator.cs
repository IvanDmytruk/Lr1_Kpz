using Lr_1.Enum;
using Lr_1.Models;

namespace Lr_1.Engine
{
    public class CriterionEvaluator
    {
        public MatchResult Evaluate(
            Anime anime,
            List<CriterionRating> userCriteria)
        {
            if (userCriteria == null || userCriteria.Count == 0)
            {
                return new MatchResult
                {
                    Score = 0,
                    Reason = "Користувач не вказав бажані критерії."
                };
            }

            List<double> scores = new();
            List<string> reasons = new();

            for (int i = 0; i < userCriteria.Count; i++)
            {
                CriterionRating userCriterion = userCriteria[i];

                CriterionRating? animeCriterion = FindCriterion(
                    anime,
                    userCriterion.Criterion
                );

                double score = CalculateCriterionMatch(
                    userCriterion,
                    animeCriterion
                );

                scores.Add(score);

                reasons.Add(
                    CreateReason(
                        userCriterion,
                        animeCriterion,
                        score
                    )
                );
            }

            double finalScore = CalculateFinalScore(
                scores,
                userCriteria
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

        private CriterionRating? FindCriterion(
            Anime anime,
            Criterion criterion)
        {
            for (int i = 0; i < anime.Criteria.Count; i++)
            {
                if (anime.Criteria[i].Criterion == criterion)
                {
                    return anime.Criteria[i];
                }
            }

            return null;
        }

        private double CalculateCriterionMatch(
            CriterionRating userCriterion,
            CriterionRating? animeCriterion)
        {
            if (animeCriterion == null)
            {
                return 0;
            }

            if (animeCriterion.Rating >= userCriterion.Rating)
            {
                return 100;
            }

            if (userCriterion.Rating <= 0)
            {
                return 0;
            }

            return animeCriterion.Rating / userCriterion.Rating * 100;
        }

        private double CalculateFinalScore(
            List<double> scores,
            List<CriterionRating> userCriteria)
        {
            if (scores.Count == 0 || userCriteria.Count == 0)
            {
                return 0;
            }

            double totalScore = 0;
            double totalWeight = 0;

            for (int i = 0; i < scores.Count; i++)
            {
                totalScore += scores[i] * userCriteria[i].Rating;
                totalWeight += userCriteria[i].Rating;
            }

            if (totalWeight == 0)
            {
                return 0;
            }

            return totalScore / totalWeight;
        }

        private string CreateReason(
            CriterionRating userCriterion,
            CriterionRating? animeCriterion,
            double score)
        {
            if (animeCriterion == null)
            {
                return $"{userCriterion.Criterion}: " +
                       "критерій відсутній.";
            }

            return $"{userCriterion.Criterion}: " +
                   $"бажано {userCriterion.Rating:0.##}, " +
                   $"в аніме {animeCriterion.Rating:0.##} " +
                   $"→ {score:0.##}%.";
        }
    }
}