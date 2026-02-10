using UnityEngine;

#if DEBUG

namespace SettingsHelper.ColorPicker.Debugging;

internal class MainTabWindow_ColourPicker : MainTabWindow
{
    private readonly Color _bgColor = Color.grey;
    private Texture2D _bgTex;

    public MainTabWindow_ColourPicker()
    {
        _bgTex = SolidColorMaterials.NewSolidColorTexture(_bgColor);
    }

    public override void DoWindowContents(Rect inRect)
    {
        GUI.DrawTexture(inRect, _bgTex);
        var buttonRect = new Rect(0f, 0f, 200f, 35f);
        buttonRect = buttonRect.CenteredOnXIn(inRect).CenteredOnXIn(inRect);

        if (Widgets.ButtonText(buttonRect, "Change Colour"))
        {
            Find.WindowStack.Add(new Dialog_ColourPicker(_bgColor,
                color => _bgTex = SolidColorMaterials.NewSolidColorTexture(color)));
        }
    }
}

#endif