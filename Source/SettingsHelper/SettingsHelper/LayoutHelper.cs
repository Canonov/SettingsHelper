using UnityEngine;

namespace SettingsHelper;

// REFERENCE: https://github.com/erdelf/GodsOfRimworld/blob/master/Source/Ankh/ModControl.cs
// REFERENCE: https://github.com/erdelf/PrisonerRansom/

/// <summary>
/// Extension methods for <see cref="Listing_Standard"/> and <see cref="Rect"/>
/// that provide layout utilities such as column support and rect splitting.
/// </summary>
[PublicAPI]
public static class LayoutHelper
{
    extension(Rect rect)
    {
        /// <summary>
        /// Creates and begins a <see cref="Listing_Standard"/> divided into evenly spaced columns.
        /// </summary>
        /// <param name="columns">Number of columns. Default is 1.</param>
        /// <returns>The started <see cref="Listing_Standard"/>. Call <c>End()</c> when finished.</returns>
        public Listing_Standard BeginListingStandardColumnned(int columns = 1)
        {
            var listing = new Listing_Standard() { ColumnWidth = (rect.width / columns) - (columns * 5f) };
            listing.Begin(rect);
            return listing;
        }
    }

    extension(Listing_Standard listing)
    {
        /// <summary>
        /// Adds a horizontal line separator with optional gap spacing.
        /// </summary>
        /// <param name="gap">Gap size in pixels before and after the line. Defaults to <see cref="GapValues.LineGap"/>.</param>
        public void AddHorizontalLine(float? gap = null)
        {
            listing.Gap(gap ?? GapValues.LineGap);
            listing.GapLine(gap ?? GapValues.LineGap);
        }

        /// <summary>
        /// Adds a text label occupying a full line.
        /// </summary>
        /// <param name="label">The label text to display.</param>
        /// <param name="height">Optional height of the line. Defaults to <c>Text.LineHeight</c>.</param>
        public void AddLabelLine(string label, float? height = null)
        {
            listing.Gap(GapValues.Gap);
            Rect lineRect = listing.GetRect(height);

            // TODO: tooltips
            //Widgets.DrawHighlightIfMouseover(lineRect);
            //TooltipHandler.TipRegion(lineRect, "TODO: TIP GOES HERE");

            Widgets.Label(lineRect, label);
        }

        /// <summary>
        /// Allocates a <see cref="Rect"/> of the specified height from the listing.
        /// </summary>
        /// <param name="height">Height of the rect. Defaults to <c>Text.LineHeight</c>.</param>
        /// <returns>The allocated <see cref="Rect"/>.</returns>
        public Rect GetRect(float? height = null)
        {
            return listing.GetRect(height ?? Text.LineHeight);
        }

        /// <summary>
        /// Allocates a line rect and outputs the left portion.
        /// </summary>
        /// <param name="leftHalf">The left portion of the line rect.</param>
        /// <param name="leftPartPct">Fraction of the line allocated to the left portion. Default is 0.5.</param>
        /// <param name="height">Optional height of the line. Defaults to <c>Text.LineHeight</c>.</param>
        /// <returns>The full line <see cref="Rect"/>.</returns>
        public Rect LineRectSplitter(out Rect leftHalf, float leftPartPct = 0.5f, float? height = null)
        {
            Rect lineRect = listing.GetRect(height);
            leftHalf = lineRect.LeftPart(leftPartPct).Rounded();
            return lineRect;
        }

        /// <summary>
        /// Allocates a line rect and splits it into left and right portions.
        /// </summary>
        /// <param name="leftHalf">The left portion of the line rect.</param>
        /// <param name="rightHalf">The right portion of the line rect.</param>
        /// <param name="leftPartPct">Fraction of the line allocated to the left portion. Default is 0.5.</param>
        /// <param name="height">Optional height of the line. Defaults to <c>Text.LineHeight</c>.</param>
        /// <returns>The full line <see cref="Rect"/>.</returns>
        public Rect LineRectSplitter(out Rect leftHalf, out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
        {
            Rect lineRect = listing.LineRectSplitter(out leftHalf, leftPartPct, height);
            rightHalf = lineRect.RightPart(1f - leftPartPct).Rounded();
            return lineRect;
        }
    }
}
