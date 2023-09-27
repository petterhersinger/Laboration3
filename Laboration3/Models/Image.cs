using System.ComponentModel.DataAnnotations;

namespace Laboration3.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public int PlayerId { get; set; }
        public string ImagePath { get; set; }
    }
}
