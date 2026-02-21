using UnityEngine;

namespace SettingsHelper;

/// <summary>
/// Delegate for a method that draws custom window contents within a given <see cref="Rect"/>.
/// </summary>
/// <param name="inRect">The rect area available for drawing content.</param>
public delegate void WindowContentsHandler(Rect inRect);

/// <summary>
/// A generic RimWorld <see cref="Window"/> that delegates its content drawing to a <see cref="WindowContentsHandler"/>.
/// Includes a close button, close X, forces pause, and absorbs input around the window.
/// </summary>
public class SettingsWindow : Window
{
    private readonly WindowContentsHandler doWindowContentsDelegate;

    /// <summary>
    /// Creates a new settings window that draws its contents using the specified handler.
    /// </summary>
    /// <param name="handler">The delegate responsible for drawing the window contents.</param>
    public SettingsWindow(WindowContentsHandler handler)
    {
        doCloseButton = true;
        doCloseX = true;
        forcePause = true;
        absorbInputAroundWindow = true;
        doWindowContentsDelegate = handler;
    }

    /// <summary>Gets the initial window size (900x700).</summary>
    public override Vector2 InitialSize => new Vector2(900f, 700f);

    /// <inheritdoc />
    public override void DoWindowContents(Rect inRect) => this.doWindowContentsDelegate(inRect);
}

/// <summary>
/// Extension methods for <see cref="Listing_Standard"/> that add buttons for opening sub-settings windows.
/// </summary>
[PublicAPI]
public static class SubSettingsWindowHelper
{
    extension(Listing_Standard listing)
    {
        /// <summary>
        /// Adds a button that opens a secondary <see cref="SettingsWindow"/> when clicked.
        /// </summary>
        /// <param name="label">The button label text.</param>
        /// <param name="handler">The delegate responsible for drawing the sub-settings window contents.</param>
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
