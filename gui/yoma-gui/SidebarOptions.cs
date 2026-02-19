using System.Collections.Generic;

namespace yoma;

public static class SidebarOptions
{
    static List<string> Options = new()
    {
        "Yoma Packages",
        "AUR Packages",
        "Pacman Packages",
        "Flatpak Packages",
    };

    public static List<string> GetOptions()
    {
        return Options;
    }

    public static void OnOptionPressed(string option)
    {
    }
}