namespace ToDoListApp.Models
{
    public class User
    {
        public int Id { get; set; }

        // Using `required` ensures these properties must be initialized.
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
    }
}
