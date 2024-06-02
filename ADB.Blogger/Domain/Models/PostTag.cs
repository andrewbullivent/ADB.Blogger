namespace ADB.Blogger.Domain.Models
{
    public class PostTag : EntityBase
    {
        public Guid? PostId { get; set; }
        public Guid? TagId { get; set; }
        public Post? Post { get; set; } = null!;
        public Tag? Tag { get; set; } = null!;
    }
}
