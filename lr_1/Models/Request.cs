using Lr_1.Enum;
namespace Lr_1.Models
{
    public class UserPreferences
    {
     public List<GenreRating> Genres { get; set; }
     public List<CriterionRating> Criteria { get; set; }

     public TargetAudience? TargetAudience { get; set; }

     public AnimeStatus? PreferredStatus { get; set; }

     public string Description { get; set; }
    }
    public class RecommendationRequest
    {
     public DateTime CreatedAt { get; set; }

     public UserPreferences Preferences { get; set; }

     public List<string> RecommendedAnime { get; set;}
    }
}