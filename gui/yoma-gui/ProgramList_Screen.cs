using Gtk;
using Gdk;

namespace yoma;

public class ProgramList_Screen
{
    private Gtk.Window window;

    public ProgramList_Screen NewScreen()
    {
        ProgramList_Screen screen = new ProgramList_Screen();
        screen.Show();
        return screen;
    }

    public void Show()
    {
        window = CurrentWindow.currentWindow;

        HBox root = new HBox(false, 0);

        // SIDEBAR
        VBox sidebar = GuiSidebar.CreateSidebar();
        root.PackStart(sidebar, false, false, 0);

        // RIGHT PANEL
        VBox right = new VBox(false, 8);
        right.BorderWidth = 12;

        // SEARCH BAR
        Entry search = new Entry();
        search.PlaceholderText = "Search programs...";
        search.HeightRequest = 36;
        right.PackStart(search, false, false, 0);

        // SCROLL
        ScrolledWindow scroll = new ScrolledWindow(null, null);
        scroll.Hexpand = true;
        scroll.Vexpand = true;

        FlowBox grid = new FlowBox();
        grid.SelectionMode = SelectionMode.None;
        grid.MaxChildrenPerLine = 6;
        grid.RowSpacing = 12;
        grid.ColumnSpacing = 12;

        // PROGRAM CARDS
        for (int i = 1; i <= 18; i++)
        {
            Frame card = new Frame();
            card.ShadowType = ShadowType.Out;   // ✅ FIXED
            card.SetSizeRequest(200, 150);

            VBox inside = new VBox(false, 6);
            inside.BorderWidth = 10;

            // PURPLE ICON
            DrawingArea icon = new DrawingArea();
            icon.SetSizeRequest(48, 48);

            icon.Drawn += (o, args) =>
            {
                var cr = args.Cr;
                cr.SetSourceRGB(0.55, 0.2, 0.8);
                cr.Rectangle(0, 0, 48, 48);
                cr.Fill();
            };

            Label name = new Label($"Program {i}");
            name.Xalign = 0;

            Button install = new Button("Install");
            install.HeightRequest = 32;

            inside.PackStart(icon, false, false, 0);
            inside.PackStart(name, false, false, 0);
            inside.PackEnd(install, false, false, 0);

            card.Add(inside);
            grid.Add(card);
        }

        scroll.Add(grid);
        right.PackStart(scroll, true, true, 0);

        root.PackStart(right, true, true, 0);

        window.Add(root);
        window.ShowAll();
    }
}
