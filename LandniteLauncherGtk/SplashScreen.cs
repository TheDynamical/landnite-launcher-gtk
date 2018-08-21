using System;
namespace LandniteLauncherGtk
{
    public partial class SplashScreen : Gtk.Window
    {
        public SplashScreen() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
