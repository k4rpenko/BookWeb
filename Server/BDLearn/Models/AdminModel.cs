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

        private string _IdUser { get; set; }
        public string IdUser
        {
            get => _IdUser; set
            {
                _IdUser = new JWT().GetUserIdFromToken(value);
            }
        }

        public DateTime? Blocked { get; set; }

    }
}
