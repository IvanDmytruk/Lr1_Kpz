using Lr_1.Enum;
using Lr_1.Models;

namespace Lr_1.Engine
{
    public class GenreEvaluator
    {
        public MatchResult Evaluate(
            Anime anime,
            List<GenreRating> userGenres)
        {
            if (userGenres == null || userGenres.Count == 0)
            {
                return new MatchResult
                {
                    Score = 0,
                    Reason = "Користувач не вказав бажані жанри."
                };
            }

            List<double> scores = new();
            List<string> reasons = new();

            for (int i = 0; i < userGenres.Count; i++)
            {
                GenreRating userGenre = userGenres[i];

                GenreRating? animeGenre = FindGenre(
                    anime,
                    userGenre.Genre
                );

                double score = CalculateGenreMatch(
                    userGenre,
                    animeGenre
                );

                scores.Add(score);

                reasons.Add(
                    CreateReason(
                        userGenre,
                        animeGenre,
                        score
                    )
                );
            }

            double finalScore = CalculateFinalScore(
                scores,
                userGenres
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

        private GenreRating? FindGenre(
            Anime anime,
            Genre genre)
        {
            for (int i = 0; i < anime.Genres.Count; i++)
            {
                if (anime.Genres[i].Genre == genre)
                {
                    return anime.Genres[i];
                }
            }

            return null;
        }

        private double CalculateGenreMatch(
            GenreRating userGenre,
            GenreRating? animeGenre)
        {
            if (animeGenre == null)
            {
                return 0;
            }

            if (animeGenre.Rating >= userGenre.Rating)
            {
                return 100;
            }

            if (userGenre.Rating <= 0)
            {
                return 0;
            }

            return animeGenre.Rating / userGenre.Rating * 100;
        }

        private double CalculateFinalScore(
            List<double> scores,
            List<GenreRating> userGenres)
        {
            if (scores.Count == 0 || userGenres.Count == 0)
            {
                return 0;
            }

            double totalScore = 0;
            double totalWeight = 0;

            for (int i = 0; i < scores.Count; i++)
            {
                totalScore += scores[i] * userGenres[i].Rating;
                totalWeight += userGenres[i].Rating;
            }

            if (totalWeight == 0)
            {
                return 0;
            }

            return totalScore / totalWeight;
        }

        private string CreateReason(
            GenreRating userGenre,
            GenreRating? animeGenre,
            double score)
        {
            if (animeGenre == null)
            {
                return $"{userGenre.Genre}: жанр відсутній.";
            }

            return $"{userGenre.Genre}: " +
                   $"бажано {userGenre.Rating:0.##}, " +
                   $"в аніме {animeGenre.Rating:0.##} " +
                   $"→ {score:0.##}%.";
        }
    }
}