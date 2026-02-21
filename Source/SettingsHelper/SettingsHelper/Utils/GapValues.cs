namespace SettingsHelper;

/// <summary>
/// Configurable spacing values used by all SettingsHelper extension methods.
/// Adjust these to change the default vertical gaps between controls.
/// </summary>
[PublicAPI]
public static class GapValues
{
    /// <summary>Gets or sets the vertical gap added before each control. Default is 12f.</summary>
    public static float Gap { get; set; } = 12f;

    /// <summary>Gets or sets the vertical gap used around horizontal lines. Default is 3f.</summary>
    public static float LineGap { get; set; } = 3f;

    /// <summary>Resets <see cref="Gap"/> and <see cref="LineGap"/> to their default values (12f and 3f).</summary>
    public static void ResetToDefault()
    {
        Gap = 12f;
        LineGap = 3f;
    }
}
