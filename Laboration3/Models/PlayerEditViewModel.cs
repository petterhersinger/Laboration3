namespace Laboration3.Models
{
    public class PlayerEditViewModel
    {
        public PlayerModel Player { get; set; }
        public IEnumerable<TeamModel> Teams { get; set; }
    }
}