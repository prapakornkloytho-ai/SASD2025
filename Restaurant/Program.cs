namespace Restaurant
{
    internal static class Program
    {
        /// <summary>
        /// Prapakorn Kloytho ID 144
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Application.Run(new KitchenForm()); //Åº·Ôé§
            if (args.Length > 0 && args[0].ToLower() == "-k")
                Application.Run(new KitchenForm());
            else
                Application.Run(new WaiterForm());
        }
    }
}