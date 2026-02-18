using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Gtk;

namespace installer;

public class InstallGTK
{
    public static Task InstallGTKAsync(Window window, List<Widget> originalWidgets)
    {
        var tcs = new TaskCompletionSource();

        Box root = (Box)window.Child;

        Application.Invoke((_, __) =>
        {
            foreach (Widget w in root.Children)
                root.Remove(w);

            window.SetDefaultSize(420, 150);

            var label = new Label("Sudo password required");
            label.Xalign = 0;

            var entry = new Entry { Visibility = false };

            var button = new Button("Continue");

            root.PackStart(label, false, false, 0);
            root.PackStart(entry, false, false, 0);
            root.PackStart(button, false, false, 0);

            button.Clicked += async (_, __2) =>
            {
                string password = entry.Text;

                // SWITCH TO PROGRESS UI
                foreach (Widget w in root.Children)
                    root.Remove(w);

                window.SetDefaultSize(420, 160);

                var progressLabel = new Label("Installing GTK...");
                progressLabel.Xalign = 0;

                var progress = new ProgressBar { ShowText = true };

                root.PackStart(progressLabel, false, false, 0);
                root.PackStart(progress, false, false, 0);

                window.ShowAll();

                var psi = new ProcessStartInfo
                {
                    FileName = "sudo",
                    Arguments = "-S pacman -S --noconfirm gtk3",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var proc = new Process();
                proc.StartInfo = psi;

                proc.OutputDataReceived += (_, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                    {
                        Application.Invoke((_, __) =>
                        {
                            progress.Pulse();
                            progress.Text = e.Data;
                        });
                    }
                };

                proc.Start();

                await proc.StandardInput.WriteLineAsync(password);
                await proc.StandardInput.FlushAsync();

                proc.BeginOutputReadLine();

                await proc.WaitForExitAsync();

                // KEEP PROGRESS SCREEN, JUST UPDATE TEXT
                Application.Invoke((_, __) =>
                {
                    progress.Fraction = 1;
                    progress.Text = "Done";
                    progressLabel.Text = "GTK installation finished.";
                });

                tcs.SetResult();
            };

            window.ShowAll();
        });

        return tcs.Task;
    }
}
