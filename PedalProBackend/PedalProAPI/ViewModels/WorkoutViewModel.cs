using System.ComponentModel.DataAnnotations.Schema;

namespace PedalProAPI.ViewModels
{
    public class WorkoutViewModel
    {
        public double Distance { get; set; }
        public string? Duration { get; set; }
        public int? HeartRate { get; set; }
        public int? WorkoutTypeId { get; set; }
    }
}
