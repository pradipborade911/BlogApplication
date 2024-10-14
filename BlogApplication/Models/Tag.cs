namespace BlogApplication.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Tags { get; set; }

    }
}
