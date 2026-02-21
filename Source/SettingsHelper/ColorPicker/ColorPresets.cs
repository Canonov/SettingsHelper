using UnityEngine;

namespace SettingsHelper.ColorPicker;

/// <summary>
/// Manages a set of color presets for the <see cref="Dialog_ColourPicker"/>.
/// </summary>
public class ColorPresets
{
    /// <summary>Gets the number of preset slots (always 6).</summary>
    public int Count { get { return this.Colors.Length; } }

    private Color[] Colors { get; set; }
    private int SelectedIndex { get; set; }

    /// <summary>Gets or sets whether any preset color has been changed.</summary>
    public bool IsModified { get; set; }

    /// <summary>
    /// Initializes a new set of 6 color presets with no selection.
    /// </summary>
    public ColorPresets()
    {
        this.Colors = new Color[6];
        this.Deselect();
        this.IsModified = false;
    }

    /// <summary>Clears the current preset selection.</summary>
    public void Deselect()
    {
        this.SelectedIndex = -1;
    }

    /// <summary>Returns the color of the currently selected preset.</summary>
    /// <returns>The selected preset color.</returns>
    public Color GetSelectedColor()
    {
        return this.Colors[this.SelectedIndex];
    }

    /// <summary>Returns whether a preset is currently selected.</summary>
    /// <returns><c>true</c> if a preset is selected; otherwise <c>false</c>.</returns>
    public bool HasSelected()
    {
        return this.SelectedIndex != -1;
    }

    /// <summary>Returns whether the preset at the given index is currently selected.</summary>
    /// <param name="i">The preset index to check.</param>
    /// <returns><c>true</c> if the preset at index <paramref name="i"/> is selected.</returns>
    public bool IsSelected(int i)
    {
        return this.SelectedIndex == i;
    }

    /// <summary>Sets the color at the given preset index, marking presets as modified if the color changed.</summary>
    /// <param name="i">The preset index.</param>
    /// <param name="c">The color to set.</param>
    public void SetColor(int i, Color c)
    {
        if (!this.Colors[i].Equals(c))
        {
            this.Colors[i] = c;
            this.IsModified = true;
        }
    }

    /// <summary>Selects the preset at the given index.</summary>
    /// <param name="i">The preset index to select.</param>
    public void SetSelected(int i)
    {
        this.SelectedIndex = i;
    }

    internal void SetSelectedColor(Color c)
    {
        this.Colors[this.SelectedIndex] = c;
        this.IsModified = true;
    }

    /// <summary>Gets or sets the color at the given preset index.</summary>
    /// <param name="i">The preset index.</param>
    public Color this[int i]
    {
        get
        {
            return this.Colors[i];
        }
        set
        {
            this.SetColor(i, value);
        }
    }
}
