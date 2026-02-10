using UnityEngine;

namespace SettingsHelper;

public delegate void WindowContentsHandler(Rect inRect);

public class SettingsWindow : Window
{
    private readonly WindowContentsHandler doWindowContentsDelegate;

    public SettingsWindow(WindowContentsHandler handler)
    {
        doCloseButton = true;
        doCloseX = true;
        forcePause = true;
        absorbInputAroundWindow = true;
        doWindowContentsDelegate = handler;
    }

    public override Vector2 InitialSize => new Vector2(900f, 700f);

    public override void DoWindowContents(Rect inRect) => this.doWindowContentsDelegate(inRect);
}

[PublicAPI]
public static class SubSettingsWindowHelper
{
    extension(Listing_Standard listing)
    {
        public void AddSubSettingsButton(string label, WindowContentsHandler handler)
        {
            listing.Gap(GapValues.Gap);
            Rect lineRect = listing.GetRect();

            // TODO: button sizing...
            if (Widgets.ButtonText(lineRect, label))
                Find.WindowStack.Add(new SettingsWindow(handler));
        }
    }
}
