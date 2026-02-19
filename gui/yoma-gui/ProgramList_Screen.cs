using Gtk;
using Gdk;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace yoma;

public class ProgramList_Screen
{
    private Gtk.Window? window;
    private FlowBox? grid;
    private ScrolledWindow? scroll;

    const int CARD_WIDTH = 220;
    const int CARD_HEIGHT = 180;
    const int SIDEBAR_WIDTH = 180;
    const int CARD_SPACING = 16;

    public void Show()
    {
        window = CurrentWindow.currentWindow;

        // ROOT LAYOUT
        HBox root = new HBox(false, 0);
        root.Hexpand = true;
        root.Vexpand = true;

        // SIDEBAR
        VBox sidebar = GuiSidebar.CreateSidebar();
        sidebar.Hexpand = false;
        sidebar.Vexpand = true;
        sidebar.WidthRequest = SIDEBAR_WIDTH;
        root.PackStart(sidebar, false, false, 0);

        // RIGHT PANEL
        VBox right = new VBox(false, 8);
        right.BorderWidth = 12;
        right.Hexpand = true;
        right.Vexpand = true;

        // SEARCH
        Entry search = new Entry();
        search.PlaceholderText = "Search programs...";
        search.HeightRequest = 36;
        right.PackStart(search, false, false, 0);

        // SCROLLED WINDOW
        scroll = new ScrolledWindow();
        scroll.Hexpand = true;
        scroll.Vexpand = true;

        // FLOWBOX
        grid = new FlowBox();
        grid.SelectionMode = SelectionMode.None;
        grid.RowSpacing = CARD_SPACING;
        grid.ColumnSpacing = CARD_SPACING;
        grid.Hexpand = true;
        grid.Vexpand = true;

        scroll.Add(grid);
        right.PackStart(scroll, true, true, 0);
        root.PackStart(right, true, true, 0);

        WindowContent.Set(root);

        // Adjust columns after the FlowBox is realized
        grid.SizeAllocated += (_, args) =>
        {
            AdjustColumns(args.Allocation.Width);
        };

        // LOADING LABEL
        Label loading = new Label("Loading programs...");
        grid.Add(loading);
        grid.ShowAll();

        _ = LoadPrograms();
    }

    private void AdjustColumns(int gridWidth)
    {
        if (grid == null) return;

        // subtract sidebar + safety
        int usable = gridWidth - CARD_SPACING;
        if (usable < CARD_WIDTH) usable = CARD_WIDTH;

        int columns = usable / (CARD_WIDTH + CARD_SPACING);
        if (columns < 1) columns = 1;
        if (columns > 6) columns = 6;

        grid.MaxChildrenPerLine = (uint)columns;
        grid.QueueResize();
    }

    private async Task LoadPrograms()
    {
        if (grid == null) return;

        List<string> programs = await ProgramList_ListQuery.GetProgramsAsync();

        Application.Invoke(delegate
        {
            if (grid == null) return;

            foreach (Widget w in grid.Children)
                grid.Remove(w);

            if (programs == null || programs.Count == 0)
                programs = new List<string> { "Example App", "Test Tool", "Sample Program", "Another App" };

            foreach (var name in programs)
                AddProgramCard(name);

            grid.ShowAll();
        });
    }

    private void AddProgramCard(string programName)
    {
        if (grid == null) return;

        Frame card = new Frame();
        card.ShadowType = ShadowType.Out;
        card.SetSizeRequest(CARD_WIDTH, CARD_HEIGHT);

        VBox inside = new VBox(false, 6);
        inside.BorderWidth = 10;

        DrawingArea icon = new DrawingArea();
        icon.SetSizeRequest(64, 64);

        icon.Drawn += (o, args) =>
        {
            var cr = args.Cr;
            cr.SetSourceRGB(0.55, 0.2, 0.8); // purple temp icon
            cr.Rectangle(0, 0, 64, 64);
            cr.Fill();
        };

        Label name = new Label(programName);
        name.Xalign = 0;

        Button info = new Button("View Info");
        info.HeightRequest = 32;
        info.Clicked += (_, __) =>
        {
            ProgramInfo_Screen.Open(programName);
        };

        inside.PackStart(icon, false, false, 0);
        inside.PackStart(name, false, false, 0);
        inside.PackEnd(info, false, false, 0);

        card.Add(inside);
        grid.Add(card);
    }
}
