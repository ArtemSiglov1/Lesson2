namespace Lesson2.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string SecondName { get; set; }
        public string FullName { get; set; }
        public int? ProfissionId { get; set; }
        public Profession Profession { get; set; }
        public List<RoleUsers> RoleUsers { get; set; }
    }
}