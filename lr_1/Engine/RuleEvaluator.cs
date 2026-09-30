using Lr_1.Models;

namespace Lr_1.Engine
{
    public class RuleEvaluator
    {
        public MatchResult Evaluate(
            MatchResult genreResult,
            MatchResult criterionResult)
        {
            List<string> reasons = new();
            int rulesPassed = 0;
            int totalRules = 2;

            bool genreRule = genreResult.Score >= 70;

            if (genreRule)
            {
                rulesPassed++;

                reasons.Add(
                    "Правило жанрової відповідності виконано."
                );
            }
            else
            {
                reasons.Add(
                    "Правило жанрової відповідності не виконано."
                );
            }

            bool criterionRule = criterionResult.Score >= 70;

            if (criterionRule)
            {
                rulesPassed++;

                reasons.Add(
                    "Правило відповідності критеріїв виконано."
                );
            }
            else
            {
                reasons.Add(
                    "Правило відповідності критеріїв не виконано."
                );
            }

            double score =
                (double)rulesPassed /
                totalRules *
                100;

            return new MatchResult
            {
                Score = score,
                Reason = string.Join(
                    Environment.NewLine,
                    reasons
                )
            };
        }
    }
}