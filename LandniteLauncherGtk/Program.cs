using Gtk;

namespace LandniteLauncherGtk
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            SplashScreen splash = new SplashScreen();
            splash.Show();
            splash.Destroy();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
