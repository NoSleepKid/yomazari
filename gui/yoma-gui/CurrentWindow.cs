using System;
using Gtk;

namespace yoma;

public static class CurrentWindow
{
    public static Window? currentWindow;

    public static void UpdateCurrentWindow(Window window)
    {
        currentWindow = window;
    }
}