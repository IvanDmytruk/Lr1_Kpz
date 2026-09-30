using Lr_1.Enum;
namespace Lr_1.Models
{
    public class User
    {
     public string Username { get; set; }
     public string Email { get; set; }
     public string PasswordHash { get; set; }

     public List<RecommendationRequest> Requests { get; set; }
    }
}