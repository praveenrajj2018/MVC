using System.ComponentModel.DataAnnotations;

namespace LoginApp.ViewModels
{
    public class NutrientViewModel
    {
        public int Id { get; set; } // Assuming you may have an Id property

        [Required(ErrorMessage = "Base weight is required.")]
        public double BaseWeight { get; set; }

        [Required(ErrorMessage = "Calories are required.")]
        public double Calories { get; set; }

        [Required(ErrorMessage = "Total fat is required.")]
        public double TotalFat { get; set; }

        [Required(ErrorMessage = "Saturated fat is required.")]
        public double SaturatedFat { get; set; }

        [Required(ErrorMessage = "Cholesterol is required.")]
        public double Cholesterol { get; set; }

        [Required(ErrorMessage = "Sodium is required.")]
        public double Sodium { get; set; }

        [Required(ErrorMessage = "Carbohydrate is required.")]
        public double Carbohydrate { get; set; }
    }
}