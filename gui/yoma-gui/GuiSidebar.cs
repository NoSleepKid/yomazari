using Gtk;

namespace yoma;

public class GuiSidebar
{
    public static VBox CreateSidebar()
    {
        VBox sidebar = new VBox(false, 8);
        sidebar.WidthRequest = 230;
        sidebar.BorderWidth = 14;

        Label title = new Label("<b>Yomazari</b>");
        title.UseMarkup = true;
        title.Xalign = 0;
        sidebar.PackStart(title, false, false, 6);

        foreach (string option in SidebarOptions.GetOptions())
        {
            Button btn = new Button(option);
            btn.HeightRequest = 38;
            btn.Clicked += (_, __) => SidebarOptions.OnOptionPressed(option);
            sidebar.PackStart(btn, false, false, 0);
        }

        sidebar.ShowAll();
        return sidebar;
    }
}