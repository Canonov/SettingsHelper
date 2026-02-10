using UnityEngine;

namespace SettingsHelper;

[PublicAPI]
public static class FormControlsHelper
{
    extension(Listing_Standard listing)
    {
        public void AddLabeledTextField(string label, ref string settingsValue, float leftPartPct = 0.5f)
        {
            listing.Gap(GapValues.Gap);
            listing.LineRectSplitter(out Rect leftHalf, out Rect rightHalf, leftPartPct);

            // TODO: tooltips
            //Widgets.DrawHighlightIfMouseover(lineRect);
            //TooltipHandler.TipRegion(lineRect, "TODO: TIP GOES HERE");

            Widgets.Label(leftHalf, label);

            string buffer = settingsValue;
            settingsValue = Widgets.TextField(rightHalf, buffer);
        }

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

        public void AddLabeledCheckbox(string label, ref bool settingsValue)
        {
            listing.Gap(GapValues.Gap);
            listing.CheckboxLabeled(label, ref settingsValue);
        }

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
