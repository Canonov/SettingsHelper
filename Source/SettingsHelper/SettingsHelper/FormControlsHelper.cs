using UnityEngine;

namespace SettingsHelper;

/// <summary>
/// Extension methods for <see cref="Listing_Standard"/> that add labeled form controls
/// such as text fields, checkboxes, and sliders.
/// </summary>
[PublicAPI]
public static class FormControlsHelper
{
    extension(Listing_Standard listing)
    {
        /// <summary>
        /// Adds a labeled text field with the label on the left and the input on the right.
        /// </summary>
        /// <param name="label">The label displayed to the left of the text field.</param>
        /// <param name="value">Reference to the string setting to read from and write to.</param>
        /// <param name="leftPartPct">Fraction of the line width allocated to the label. Default is 0.5.</param>
        public void AddLabeledTextField(string label, ref string value, float leftPartPct = 0.5f)
        {
            listing.Gap(GapValues.Gap);
            listing.LineRectSplitter(out Rect leftHalf, out Rect rightHalf, leftPartPct);

            // TODO: tooltips
            //Widgets.DrawHighlightIfMouseover(lineRect);
            //TooltipHandler.TipRegion(lineRect, "TODO: TIP GOES HERE");

            Widgets.Label(leftHalf, label);

            string buffer = value;
            value = Widgets.TextField(rightHalf, buffer);
        }

        /// <summary>
        /// Adds a labeled numerical text field that constrains input to a numeric range.
        /// </summary>
        /// <param name="label">The label displayed to the left of the text field.</param>
        /// <param name="settingsValue">Reference to the numeric setting to read from and write to.</param>
        /// <param name="leftPartPct">Fraction of the line width allocated to the label. Default is 0.5.</param>
        /// <param name="minValue">Minimum allowed value. Default is 1.</param>
        /// <param name="maxValue">Maximum allowed value. Default is 100000.</param>
        /// <typeparam name="T">A numeric struct type (int, float, etc.).</typeparam>
        public void AddLabeledNumericalTextField<T>(string label, ref T settingsValue, float leftPartPct = 0.5f,
            float minValue = 1f, float maxValue = 100000f) where T : struct
        {
            listing.Gap(GapValues.Gap);
            listing.LineRectSplitter(out Rect leftHalf, out Rect rightHalf, leftPartPct);

            // TODO: tooltips
            //Widgets.DrawHighlightIfMouseover(lineRect);
            //TooltipHandler.TipRegion(lineRect, "TODO: TIP GOES HERE");

            Widgets.Label(leftHalf, label);

            string buffer = settingsValue.ToString();
            Widgets.TextFieldNumeric<T>(rightHalf, ref settingsValue, ref buffer, minValue, maxValue);
        }

        /// <summary>
        /// Adds a labeled checkbox.
        /// </summary>
        /// <param name="label">The label displayed next to the checkbox.</param>
        /// <param name="settingsValue">Reference to the boolean setting to read from and write to.</param>
        public void AddLabeledCheckbox(string label, ref bool settingsValue)
        {
            listing.Gap(GapValues.Gap);
            listing.CheckboxLabeled(label, ref settingsValue);
        }

        /// <summary>
        /// Adds a labeled horizontal slider with configurable range, labels, and rounding.
        /// </summary>
        /// <param name="label">The label displayed to the left of the slider.</param>
        /// <param name="value">Reference to the float setting to read from and write to.</param>
        /// <param name="leftValue">Minimum slider value.</param>
        /// <param name="rightValue">Maximum slider value.</param>
        /// <param name="leftAlignedLabel">Optional label displayed at the left end of the slider.</param>
        /// <param name="rightAlignedLabel">Optional label displayed at the right end of the slider.</param>
        /// <param name="roundTo">Round the value to this increment. Use -1 for no rounding.</param>
        /// <param name="middleAlignment">If true, adds a notch at the center of the slider.</param>
        public void AddLabeledSlider(string label, ref float value, float leftValue, float rightValue,
            string leftAlignedLabel = null, string rightAlignedLabel = null, float roundTo = -1f,
            bool middleAlignment = false)
        {
            listing.Gap(GapValues.Gap);
            listing.LineRectSplitter(out Rect leftHalf, out Rect rightHalf);

            Widgets.Label(leftHalf, label);

            float bufferVal = value;
            // NOTE: this BottomPart will probably need some reworking if the height of rect is greater than a line
            value = Widgets.HorizontalSlider(rightHalf.BottomPart(0.70f), bufferVal, leftValue, rightValue,
                middleAlignment, null, leftAlignedLabel, rightAlignedLabel, roundTo);
        }

        /// <summary>
        /// Adds a labeled slider that maps enum values to slider positions.
        /// The slider displays the name of the currently selected enum value.
        /// </summary>
        /// <param name="label">The label displayed to the left of the slider.</param>
        /// <param name="value">Reference to the enum setting to read from and write to.</param>
        /// <typeparam name="T">An enum type whose ordinal values map to slider positions.</typeparam>
        public void AddLabeledSlider<T>(string label, ref T value) where T : Enum
        {
            Enum enu = value;

            listing.Gap(10);
            listing.LineRectSplitter(out Rect leftHalf, out Rect rightHalf);

            Widgets.Label(leftHalf, label);

            float bufferVal = Convert.ToInt32(enu);

            // NOTE: this BottomPart will probably need some reworking if the height of rect is greater than a line
            float tempVal = Widgets.HorizontalSlider(rightHalf.BottomPart(0.70f),
                value: bufferVal,
                min: 0f,
                max: Enum.GetValues(typeof(T)).Length - 1,
                middleAlignment: true,
                label: Enum.GetName(typeof(T), value),
                roundTo: 1);

            value = (T)Enum.ToObject(typeof(T), (int)tempVal);
        }

        /// <summary>
        /// Renders a horizontal slider without a label.
        /// </summary>
        /// <param name="val">The current slider value.</param>
        /// <param name="min">Minimum slider value.</param>
        /// <param name="max">Maximum slider value.</param>
        /// <param name="label">Optional label displayed above the slider.</param>
        /// <param name="leftAlignedLabel">Optional label displayed at the left end of the slider.</param>
        /// <param name="rightAlignedLabel">Optional label displayed at the right end of the slider.</param>
        /// <param name="roundTo">Round the value to this increment. Use -1 for no rounding.</param>
        /// <param name="middleAlignment">If true, adds a notch at the center of the slider.</param>
        /// <returns>The selected float value.</returns>
        public float Slider(float val, float min, float max, string label = null, string leftAlignedLabel = null,
            string rightAlignedLabel = null, float roundTo = -1f, bool middleAlignment = false)
        {
            Rect rect = listing.GetRect(22f);

            float result = Widgets.HorizontalSlider(rect, val, min, max, middleAlignment, label, leftAlignedLabel,
                rightAlignedLabel, roundTo);

            listing.Gap(listing.verticalSpacing);
            return result;
        }
    }
}
