using System.ComponentModel.DataAnnotations;

namespace Kontrolmatik.Data.Tables
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}