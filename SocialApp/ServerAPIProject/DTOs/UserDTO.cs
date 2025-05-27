namespace ServerAPIProject.DTOs
{
    public class UserDTO
    {
        required public string Name { get; set; }

        required public string Email { get; set; }

        required public string HashPassword { get; set; }

        required public string Image { get; set; }
    }
}