using System;
using Gtk;
using yoma;
using Pango;

public static class Init
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Yoma Package Manager");
        Console.WriteLine("A package manager made for Arch Linux based operating systems and RexOS.");
        
        Gtk.Application.Init();

        // Main window
        CurrentWindow.UpdateCurrentWindow(new Window("Yomazari Package Manager")
        {
            DefaultWidth = 800,
            DefaultHeight = 600
        });

        var window = CurrentWindow.currentWindow!;
        window.DeleteEvent += (o, e) => Gtk.Application.Quit();

        // VBox to center spinner + label
        var vbox = new Box(Orientation.Vertical, 20)
        {
            Homogeneous = false
        };

        // Spinner
        var spinner = new Spinner();
        spinner.SetSizeRequest(200, 200);
        spinner.Start();

        // Label
        var label = new Label("Loading...");

        // Pack spinner + label centered
        vbox.PackStart(spinner, true, false, 0);
        vbox.PackStart(label, false, false, 0);

        window.Add(vbox);
        window.ShowAll();

        // Call async network check after 100ms
        GLib.Timeout.Add(100, () =>
        {
            _ = StartTesting(); // fire & forget
            return false; // run once
        });

        Gtk.Application.Run();
    }

    public static async System.Threading.Tasks.Task StartTesting()
    {
        var window = CurrentWindow.currentWindow!;
        
        if (await NetworkChecks.IsReadyWithNetwork())
        {
            Gtk.Application.Invoke(delegate
            {
                OpenMenu();
            });
        }
        else
        {
            foreach (Widget child in window.Children)
            {
                window.Remove(child);
            }

            var label = new Label
            {
                Text = "Whoops! You need an internet connection to use yomazari!\nPlease acquire a working network connection!"
            };

            label.ModifyFont(FontDescription.FromString("Sans Bold 12")); // 24pt, adjust if you want even bigger

            window.Add(label);
            window.ShowAll();

        }
    }

    public static void OpenMenu()
    {
        var window = CurrentWindow.currentWindow;
        
        foreach (Widget child in window.Children)
        {
            window.Remove(child);
        }
    }
}