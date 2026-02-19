using System.Collections.Generic;

namespace yoma;

public static class SidebarOptions
{
    static List<string> Options = new()
    {
        "Home",
        "Installed",
        "Updates",
        "Categories",
        "Settings"
    };

    public static List<string> GetOptions()
    {
        return Options;
    }

    public static void OnOptionPressed(string option)
    {
    }
}