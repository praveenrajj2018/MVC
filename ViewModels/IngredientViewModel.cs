using System.ComponentModel.DataAnnotations;

namespace LoginApp.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Source { get; set; }
        public string Classification { get; set; }
    }
}
