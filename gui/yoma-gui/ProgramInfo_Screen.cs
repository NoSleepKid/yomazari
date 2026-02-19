using Gtk;

namespace yoma;

public static class ProgramInfo_Screen
{
    public static void Open(string programName)
    {
        VBox root = new VBox(false, 12);
        root.BorderWidth = 20;
        root.Hexpand = true;
        root.Vexpand = true;

        Label title = new Label("<b>" + programName + "</b>");
        title.UseMarkup = true;
        title.Xalign = 0;

        Label desc = new Label("Program description placeholder.");
        desc.Xalign = 0;

        Button back = new Button("Back");
        back.Clicked += (_, __) =>
        {
            new ProgramList_Screen().Show();
        };

        root.PackStart(title, false, false, 0);
        root.PackStart(desc, false, false, 0);
        root.PackEnd(back, false, false, 0);

        WindowContent.Set(root);
    }
}