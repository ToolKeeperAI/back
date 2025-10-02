namespace Service.Settings
{
    public class AppSettings
    {
        public ModelPrecisionSettings ModelPrecisionSettings { get; set; }
    }

    public class ModelPrecisionSettings
    {
        public double ConfidenceTreshold { get; set; } = 0.96;
    }
}
