using System.ComponentModel.DataAnnotations;

namespace Laboration3.Models
{
    public class PlayerModel
    {
        public PlayerModel() { }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Name For The Player")]
        public string Name { get; set; }
        public string Position { get; set; }
        public int IsStarting { get; set; }
        public int TeamId { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
