using UnityEngine;

namespace SettingsHelper;

// REFERENCE: https://github.com/erdelf/GodsOfRimworld/blob/master/Source/Ankh/ModControl.cs
// REFERENCE: https://github.com/erdelf/PrisonerRansom/
[PublicAPI]
public static class LayoutHelper
{
    extension(Rect rect)
    {
        public Listing_Standard BeginListingStandardColumnned(int columns = 1)
        {
            var listing = new Listing_Standard() { ColumnWidth = (rect.width / columns) - (columns * 5f) };
            listing.Begin(rect);
            return listing;
        }
    }

    extension(Listing_Standard listing)
    {
        public void AddHorizontalLine(float? gap = null)
        {
            listing.Gap(gap ?? GapValues.LineGap);
            listing.GapLine(gap ?? GapValues.LineGap);
        }

        public void AddLabelLine(string label, float? height = null)
        {
            listing.Gap(GapValues.Gap);
            Rect lineRect = listing.GetRect(height);

            // TODO: tooltips
            //Widgets.DrawHighlightIfMouseover(lineRect);
            //TooltipHandler.TipRegion(lineRect, "TODO: TIP GOES HERE");

            Widgets.Label(lineRect, label);
        }

        public Rect GetRect(float? height = null)
        {
            return listing.GetRect(height ?? Text.LineHeight);
        }

        public Rect LineRectSplitter(out Rect leftHalf, float leftPartPct = 0.5f, float? height = null)
        {
            Rect lineRect = listing.GetRect(height);
            leftHalf = lineRect.LeftPart(leftPartPct).Rounded();
            return lineRect;
        }

        public Rect LineRectSplitter(out Rect leftHalf, out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
        {
            Rect lineRect = listing.LineRectSplitter(out leftHalf, leftPartPct, height);
            rightHalf = lineRect.RightPart(1f - leftPartPct).Rounded();
            return lineRect;
        }
    }
}
