// Models/Nutrient.cs
namespace LoginApp.Models
{
    public class Nutrient
    {
        public int Id { get; set; }
        public float BaseWeight { get; set; }
        public int Calories { get; set; }
        public float TotalFat { get; set; }
        public float SaturatedFat { get; set; }
        public int Cholesterol { get; set; }
        public int Sodium { get; set; }
        public int Carbohydrate { get; set; }
    }
}
