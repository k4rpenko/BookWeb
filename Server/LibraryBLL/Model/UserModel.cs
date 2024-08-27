using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibraryDAL.Model
{
    [Table("BookHome")]
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public string? RefreshToken { get; set; }
        public List<string>? Books { get; set; } = new List<string>();
        public DateTime CreatData { get; set; } = DateTime.UtcNow;
        public DateTime updatedData { get; set; } = DateTime.UtcNow;
        public DateTime? Blocked { get; set; }
        private string _role;
        public required string Role
        {
            get => _role;
            set
            {
                IsValid(value);
                _role = value;
            }
        }

        private void IsValid(string role)
        {
            if (role != "Admin" && role != "User")
            {
                throw new InvalidOperationException("Role must be either 'Admin' or 'User'.");
            }
        }
    }
}
