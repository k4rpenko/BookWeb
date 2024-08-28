using BDLearn.Controllers;
using LibraryDAL.Model;

namespace BDLearn.Models
{
    public class AdminModel
    {
        private string _IdAdmin { get; set; }
        public string IdAdmin
        {
            get => _IdAdmin; set
            {
                _IdAdmin = new JWT().GetUserIdFromToken(value);
            }
        }

        public string IdUser { get; set; }

        public DateTime? Blocked { get; set; }

    }
}
