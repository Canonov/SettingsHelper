namespace SettingsHelper;

[PublicAPI]
public static class GapValues
{
    public static float Gap { get; set; } = 12f;
    public static float LineGap { get; set; } = 3f;

    public static void ResetToDefault()
    {
        Gap = 12f;
        LineGap = 3f;
    }
}
