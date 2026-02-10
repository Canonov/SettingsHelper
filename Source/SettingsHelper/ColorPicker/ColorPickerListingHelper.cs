using SettingsHelper.ColorPicker;
using UnityEngine;

namespace SettingsHelper;

[PublicAPI]
public static class ColorPickerListingHelper
{
    extension(Listing_Standard listing)
    {
        public void AddColorPickerButton(string label, Color color, Action<Color> callback, string buttonText = "Change")
        {
            listing.Gap(GapValues.Gap);
            Rect lineRect = listing.GetRect();

            float textSize = Text.CalcSize(buttonText).x + 10f;
            float rightSize = textSize + 5f + lineRect.height;
            Rect rightPart = lineRect.RightPartPixels(textSize + 5f + lineRect.height);

            // draw button leaving room for color rect in rightHalf rect (plus some padding)
            if (Widgets.ButtonText(rightPart.LeftPartPixels(textSize), buttonText))
                Find.WindowStack.Add(new Dialog_ColourPicker(color, callback));
            GUI.color = color;
            // draw square with color in rightHalf rect
            GUI.DrawTexture(rightPart.RightPartPixels(rightPart.height), BaseContent.WhiteTex);
            GUI.color = Color.white;

            Rect leftPart = lineRect.LeftPartPixels(lineRect.width - rightSize);
            Widgets.Label(leftPart, label);
        }

        public void AddColorPickerButton(string label, Color color, string fieldName, object colorContainer, string buttonText = "Change")
        {
            listing.AddColorPickerButton(label, color, (Color c) => colorContainer.GetType().GetField(fieldName).SetValue(colorContainer, color), buttonText);
        }
    }
}
