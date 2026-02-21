using SettingsHelper.ColorPicker;
using UnityEngine;

namespace SettingsHelper;

/// <summary>
/// Extension methods for <see cref="Listing_Standard"/> that add color picker buttons.
/// </summary>
[PublicAPI]
public static class ColorPickerListingHelper
{
    extension(Listing_Standard listing)
    {
        /// <summary>
        /// Adds a labeled button that opens a <see cref="Dialog_ColourPicker"/>.
        /// The selected color is passed to the callback when applied.
        /// </summary>
        /// <param name="label">The label displayed to the left of the button.</param>
        /// <param name="color">The initial color to display in the picker.</param>
        /// <param name="callback">Action invoked with the selected color when applied.</param>
        /// <param name="buttonText">The button label text. Default is "Change".</param>
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

        /// <summary>
        /// Adds a labeled button that opens a <see cref="Dialog_ColourPicker"/>
        /// and sets the result via reflection on a field of the container object.
        /// </summary>
        /// <param name="label">The label displayed to the left of the button.</param>
        /// <param name="color">The initial color to display in the picker.</param>
        /// <param name="fieldName">The name of the <see cref="Color"/> field on <paramref name="colorContainer"/> to update.</param>
        /// <param name="colorContainer">The object containing the field to update via reflection.</param>
        /// <param name="buttonText">The button label text. Default is "Change".</param>
        public void AddColorPickerButton(string label, Color color, string fieldName, object colorContainer, string buttonText = "Change")
        {
            listing.AddColorPickerButton(label, color, (Color c) => colorContainer.GetType().GetField(fieldName).SetValue(colorContainer, c), buttonText);
        }
    }
}
