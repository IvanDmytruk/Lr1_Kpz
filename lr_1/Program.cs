using Lr_1.Data;
using Lr_1.Engine;
using Lr_1.Enum;
using Lr_1.Models;

string dataPath = Path.Combine(
    AppContext.BaseDirectory,
    "Data",
    "Data.json"
);

Data data = Data.Load(dataPath);

List<GenreRating> userGenres = new()
{
    new GenreRating
    {
        Genre = Genre.Action,
        Rating = 5
    },
    new GenreRating
    {
        Genre = Genre.Romance,
        Rating = 9
    },
    new GenreRating
    {
        Genre = Genre.Fantasy,
        Rating = 7
    }
};

List<CriterionRating> userCriteria = new()
{
    new CriterionRating
    {
        Criterion = Criterion.ActionLevel,
        Rating = 9
    },
    new CriterionRating
    {
        Criterion = Criterion.RomanceLevel,
        Rating = 7
    },
    new CriterionRating
    {
        Criterion = Criterion.DarknessLevel,
        Rating = 8
    }
};
AnimeStatus userStatus = AnimeStatus.Finished;

GenreEvaluator genreEvaluator = new GenreEvaluator();
CriterionEvaluator criterionEvaluator = new CriterionEvaluator();
SourceEvaluator sourceEvaluator = new SourceEvaluator();
StatusEvaluator statusEvaluator = new StatusEvaluator();

for (int i = 0; i < data.Animes.Count; i++)
{
    Anime anime = data.Animes[i];

    MatchResult genreResult = genreEvaluator.Evaluate(
        anime,
        userGenres
    );

    MatchResult criterionResult = criterionEvaluator.Evaluate(
        anime,
        userCriteria
    );

    MatchResult statusResult = statusEvaluator.Evaluate(
        anime,
        userStatus
    );

    Console.WriteLine(anime.AnimeName);

    Console.WriteLine();
    Console.WriteLine("Жанри");
    Console.WriteLine(
        $"Відповідність: {genreResult.Score:0.##}%"
    );
    Console.WriteLine(genreResult.Reason);

    Console.WriteLine();
    Console.WriteLine("Критерії");
    Console.WriteLine(
        $"Відповідність: {criterionResult.Score:0.##}%"
    );
    Console.WriteLine(criterionResult.Reason);

    Console.WriteLine();
    Console.WriteLine("Статус");
    Console.WriteLine(
        $"Відповідність: {statusResult.Score:0.##}%"
    );
    Console.WriteLine(statusResult.Reason);

    Console.WriteLine();
}
Console.WriteLine("Тестування завершено.");
