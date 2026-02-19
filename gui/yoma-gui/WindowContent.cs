using Gtk;

namespace yoma;

public static class WindowContent
{
    public static void Set(Widget widget)
    {
        var window = CurrentWindow.currentWindow;

        foreach (Widget child in window.Children)
            window.Remove(child);

        window.Add(widget);
        window.ShowAll();
    }
}