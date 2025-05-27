namespace ServerAPIProject.DTOs
{
    using ServerLibraryProject.Enums;

    public class ReactionDTO
    {
        required public ReactionType Type { get; set; }
    }
}
