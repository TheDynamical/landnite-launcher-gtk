using System;
using System.IO;
using System.Reflection;
using System.Text;
using Gtk;

public partial class MainWindow : Gtk.Window
{

    string launcherPath;
    string defaultGamePath;
    string customGamePath;
    string gamePath;
    bool isCustomGamePath;
    bool isLaunchable;
    string gameName = "Landnite";
    string gameExtention = "/PL.app";
    string configExtention = "/config.lh";

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        SetPosition(WindowPosition.Center);

        FindLauncherPath();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    void FindLauncherPath()
    {
        Console.WriteLine("Finding Game...");
        launcherPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Console.WriteLine("Launcher Path: " + launcherPath);

        defaultGamePath = launcherPath + gameExtention;

        SettingGamePath();
    }

    void SettingGamePath()
    {
        if (!isCustomGamePath)
        {
            gamePath = defaultGamePath;
            Console.WriteLine("Set game path to default (" + gamePath + ")");
        }

        else
        {
            gamePath = customGamePath;
            Console.WriteLine("Set game path to custom (" + gamePath + ")");
        }

        CheckIfGameExists();
    }

    protected void playButtonClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Play button clicked with label state: " + playButton.Label);

        if (isLaunchable)
        {
            LaunchGame();
        }

        else
        {
            OpenNoGameDialog();
        }
    }

    void OpenNoGameDialog()
    {
        MessageDialog one = new MessageDialog(this,
                                                 DialogFlags.DestroyWithParent, MessageType.Question, ButtonsType.YesNo, "No game can be found. Do you have the game stored elsewhere?\r\n\r\nClicking 'No' will take you to a download page, clicking 'Yes' will allow you to locate the folder where the game is stored.");

        int response = one.Run();

        Console.WriteLine(response);

        one.Destroy();

        if (response == -8)
        {
            OpenGameDialog();
        }

        if (response == -9)
        {
            System.Diagnostics.Process.Start("http://bit.ly/VisitLandnite");

        }
    }

    void OpenGameDialog()
    {
        FileChooserDialog folderChooser =
            new FileChooserDialog("Find the " + gameName + " game folder",
                                     this,
                                     FileChooserAction.SelectFolder,
                                     "Cancel", ResponseType.Cancel,
                                     "Open", ResponseType.Accept);

        if (folderChooser.Run() == (int)ResponseType.Accept)
        {
            string folderName = folderChooser.Filename;
            Console.WriteLine("Selected " + folderName);

            customGamePath = folderName + gameExtention;
            Console.WriteLine("Custom game path set to: " + customGamePath);

            isCustomGamePath = true;
            Console.WriteLine("Set custom game path to: " + isCustomGamePath);
        }

        folderChooser.Destroy();

        SettingGamePath();
    }

    void CheckIfGameExists()
    {

        // if (File.Exists(gamePath))
             GameDoesExist();

      //   if (!File.Exists(gamePath))
          //   GameDoesNotExist();
    }

    void GameDoesExist()
    {
        Console.WriteLine("Game is launchable");
        isLaunchable = true;

        playButton.Label = "PLAY";
    }

    void GameDoesNotExist()
    {
        Console.WriteLine("Game is not launchable");
        isLaunchable = false;

        playButton.Label = "GAME NOT FOUND";
        playButton.TooltipText = gameName + " is not installed in the current directory. Click for further options.";
    }

    void LaunchGame()
    {
        Console.WriteLine("Launching game at: " + gamePath);
        System.Diagnostics.Process.Start(gamePath);
    }

    void OpenAboutDialog()
    {
        AboutDialog about = new AboutDialog();
        about.ProgramName = "Landnite Launcher";
        about.Version = "0.1";
        about.Copyright = "© Liam Hall";
        about.Comments = @"A simple app to launch the game you actually want.";
        about.Website = "https://bit.ly/VisitLandnite";
        //about.Logo = new Gdk.Pixbuf("battery.png");
        about.SetPosition(WindowPosition.CenterAlways);
        about.Run();
        about.Destroy();
    }

    protected void aboutButtonClicked(object sender, EventArgs e)
    {
        OpenAboutDialog();
    }

    protected void changeDirectoryClicked(object sender, EventArgs e)
    {
        OpenGameDialog();
    }
}
