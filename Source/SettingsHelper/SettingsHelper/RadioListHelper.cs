using UnityEngine;

namespace SettingsHelper;

/// <summary>
/// Extension methods for <see cref="Listing_Standard"/> that add labeled radio button lists.
/// </summary>
[PublicAPI]
public static class RadioListHelper
{
    extension(Listing_Standard listing)
    {
        /// <summary>
        /// Adds a header label followed by radio buttons for each string value.
        /// The label and value of each radio button are the same string.
        /// </summary>
        /// <param name="header">Header text displayed above the radio buttons.</param>
        /// <param name="labels">Array of strings used as both labels and values.</param>
        /// <param name="selectedValue">Reference to the currently selected string value.</param>
        /// <param name="headerHeight">Optional height of the header line.</param>
        public void AddLabeledRadioList(string header, string[] labels, ref string selectedValue, float? headerHeight = null)
        {
            listing.Gap(GapValues.Gap);

            listing.AddLabelLine(header, headerHeight);

            AddRadioList(listing, GenerateLabeledRadioValues(labels), ref selectedValue);
        }

        /// <summary>
        /// Adds a header label followed by radio buttons where display labels map to typed values.
        /// </summary>
        /// <param name="header">Header text displayed above the radio buttons.</param>
        /// <param name="options">Dictionary mapping display labels to their corresponding values.</param>
        /// <param name="selectedValue">Reference to the currently selected value.</param>
        /// <param name="headerHeight">Optional height of the header line.</param>
        /// <typeparam name="T">The type of the radio button values.</typeparam>
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
