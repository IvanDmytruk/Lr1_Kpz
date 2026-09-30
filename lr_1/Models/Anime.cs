using Lr_1.Enum;
namespace Lr_1.Models
{
    public class Anime
    {
      public string AnimeName { get; set; }
      public string Description { get; set; }
      public DateOnly ReleaseDate { get; set; }

      public TargetAudience TargetAudience { get; set; }

      public List<GenreRating> Genres { get; set; }
      public List<CriterionRating> Criteria { get; set; }

      public AnimeStatus Status { get; set; }

      public List<Source> Sources { get; set; }
    }
}