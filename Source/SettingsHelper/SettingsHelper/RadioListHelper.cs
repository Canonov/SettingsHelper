using UnityEngine;

namespace SettingsHelper;

[PublicAPI]
public static class RadioListHelper
{
    extension(Listing_Standard listing)
    {
        public void AddLabeledRadioList(string header, string[] labels, ref string selectedValue, float? headerHeight = null)
        {
            listing.Gap(GapValues.Gap);
            
            listing.AddLabelLine(header, headerHeight);
            
            AddRadioList(listing, GenerateLabeledRadioValues(labels), ref selectedValue);
        }

        public void AddLabeledRadioList<T>(string header, Dictionary<string, T> options, ref T selectedValue, float? headerHeight = null)
        {
            listing.Gap(GapValues.Gap);
            
            listing.AddLabelLine(header, headerHeight);
            
            AddRadioList(listing, GenerateLabeledRadioValues<T>(options), ref selectedValue);
        }
    }

    // Helpers
    private static void AddRadioList<T>(Listing_Standard listing, List<LabeledRadioValue<T>> labeledValues, ref T val, float? height = null)
    {
        foreach (var radioValue in labeledValues)
        {
            listing.Gap(GapValues.Gap);
            Rect lineRect = listing.GetRect(height);
            if (Widgets.RadioButtonLabeled(lineRect, radioValue.Label, EqualityComparer<T>.Default.Equals(radioValue.Value, val)))
                val = radioValue.Value;
        }
    }

    private static List<LabeledRadioValue<string>> GenerateLabeledRadioValues(string[] labels)
    {
        return labels.Select(label => new LabeledRadioValue<string>(label, label)).ToList();
    }

    // (label, value) => (key, value)
    private static List<LabeledRadioValue<T>> GenerateLabeledRadioValues<T>(Dictionary<string, T> dict)
    {
        return dict.Select(pair => new LabeledRadioValue<T>(pair.Key, pair.Value)).ToList();
    }
}
