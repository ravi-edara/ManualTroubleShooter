namespace ManualTroubleShooter
{
    public class ManualSection
    {
        public string FileName { get; set; }
        
        public string Name { get; set; }

        public string PageNumber { get; set; }

        public string? Summary { get; set; }
    }

    public static class Constants
    {
        // can be of different sections here (eg:- Service, Warranty etc)
        public static string TroubleShooting
        {
            get { return "Trouble Shooting"; }
        }
    }
}
