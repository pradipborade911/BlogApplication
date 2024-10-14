using System.ComponentModel.DataAnnotations;

namespace BlogApplication.DTOs
{
    public class PostRequestDTO
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Excerpt { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Tags")]
        public string TagsList { get; set; }
    }
}
