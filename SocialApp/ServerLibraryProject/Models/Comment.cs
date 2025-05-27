namespace ServerLibraryProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public long Id { get; set; }

        public required long UserId { get; set; }

        public required long PostId { get; set; }

        public required string Content { get; set; }

        public required DateTime CreatedDate { get; set; }
    }
}
