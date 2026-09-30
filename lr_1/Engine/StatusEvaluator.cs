using Lr_1.Enum;
using Lr_1.Models;

namespace Lr_1.Engine
{
    public class StatusEvaluator
    {
        public MatchResult Evaluate(
            Anime anime,
            AnimeStatus userStatus)
        {
            AnimeStatus? animeStatus = FindStatus(anime);

            double score = CalculateStatusMatch(
                userStatus,
                animeStatus
            );

            string reason = CreateReason(
                userStatus,
                animeStatus,
                score
            );

            return new MatchResult
            {
                Score = score,
                Reason = reason
            };
        }

        private AnimeStatus? FindStatus(Anime anime)
        {
            return anime.Status;
        }

        private double CalculateStatusMatch(
            AnimeStatus userStatus,
            AnimeStatus? animeStatus)
        {
            if (animeStatus == null)
            {
                return 0;
            }

            if (userStatus == animeStatus)
            {
                return 100;
            }

            return 0;
        }

        private string CreateReason(
            AnimeStatus userStatus,
            AnimeStatus? animeStatus,
            double score)
        {
            if (animeStatus == null)
            {
                return $"Статус аніме не визначений. " +
                       $"Бажаний статус: {userStatus}.";
            }

            return $"Бажаний статус: {userStatus}, " +
                   $"статус аніме: {animeStatus} " +
                   $"→ {score:0.##}%.";
        }
    }
}