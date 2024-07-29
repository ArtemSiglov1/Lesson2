namespace Lesson2.Data.Models
{
    public class Accounts
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Login { get; set; }
        public string Password{ get; set; }
    }
}
