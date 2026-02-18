using Gtk;
using System;
using System.Threading.Tasks;
using Application = Gtk.Application;

namespace installer;

public static class Program
{
    public static Window window;
    public static Box root;

    static ProgressBar progress;
    static Label label;

    public static void Main()
    {
        Application.Init();

        window = new Window("Installer");
        window.SetDefaultSize(420, 160);
        window.Resizable = false;
        window.BorderWidth = 12;
        window.DeleteEvent += (o, a) => Application.Quit();

        root = new Box(Orientation.Vertical, 8);
        window.Add(root);

        ShowMenu();

        window.ShowAll();
        Application.Run();
    }

    public static void ClearUI()
    {
        foreach (Widget c in root.Children)
            root.Remove(c);
    }

    static void ShowMenu()
    {
        ClearUI();

        var title = new Label("Choose action");
        title.Xalign = 0;

        var install = new Button("Install");
        var uninstall = new Button("Uninstall");

        install.Clicked += async (_, __) =>
        {
            // 1️⃣ Switch to GTK install progress UI
            CreateProgressUI();
            UpdateProgress(0, "Starting GTK installation...");

            // 2️⃣ Run GTK install
            await InstallGTK.InstallGTKAsync(window, null);

            // 3️⃣ Once GTK install finishes, show waiting for copier
            // Force GTK main loop to update
            Application.Invoke((_, __2) =>
            {
                UpdateProgress(0, "Waiting for copier...");
            });
        };

        uninstall.Clicked += (_, __) => { /* your uninstall code here */ };

        root.PackStart(title, false, false, 0);
        root.PackStart(install, false, false, 0);
        root.PackStart(uninstall, false, false, 0);

        window.ShowAll();
    }

    public static void CreateProgressUI()
    {
        ClearUI();

        label = new Label("Starting...");
        label.Xalign = 0;

        progress = new ProgressBar();
        progress.ShowText = true;

        root.PackStart(label, false, false, 0);
        root.PackStart(progress, false, false, 0);

        window.ShowAll();
    }

    public static void UpdateProgress(double fraction, string text)
    {
        Application.Invoke((_, __) =>
        {
            // Ensure progress UI exists
            if (progress == null || label == null)
                CreateProgressUI();

            fraction = Math.Clamp(fraction, 0, 1);

            progress.Fraction = fraction;
            progress.Text = fraction >= 0 ? $"{(int)(fraction * 100)}%" : "";
            label.Text = text ?? "";
        });
    }
}
