namespace LoginApp.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string MeasurementName { get; set; } // Renamed property
        public double Weight { get; set; }
        public string OG_L { get; set; }
        public string Source { get; set; }
    }
}
