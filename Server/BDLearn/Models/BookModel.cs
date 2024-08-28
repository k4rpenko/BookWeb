using BDLearn.Controllers;

namespace BDLearn.Models
{
    public class BookModel
    {
        private string _IdUser { get; set; }

        public string IdBook { get; set; }
        public string IdUser { get => _IdUser; set
            {
                _IdUser = new JWT().GetUserIdFromToken(value);
            }
        }
    }
}
