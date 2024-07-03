using System.ComponentModel;

namespace MyRecipes.ViewModels
{
    public class User
    {
        public string UserId { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }

    }
}
