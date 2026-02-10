namespace SettingsHelper;

internal class LabeledRadioValue<T>(string label, T value)
{
    public string Label { get; } = label;
    public T Value { get; } = value;
}