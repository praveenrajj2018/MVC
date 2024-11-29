using System.ComponentModel.DataAnnotations;

namespace LoginApp.ViewModels
{
    public class MeasurementViewModel
    {
        public int Id { get; set; } // Assuming there is an Id property

        [Required(ErrorMessage = "Measurement name is required.")]
        public string MeasurementName { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "OG_L is required.")]
        public string OG_L { get; set; }

        [Required(ErrorMessage = "Source is required.")]
        public string Source { get; set; }
    }
}