using UnityEngine;
using UnityEngine.UI;

public class ThaiFontAdjusterDemo : MonoBehaviour
{
    public Dropdown FontDropdown;
    public Font[] Fonts;
    public Text OriginalText;
    public Text AdjustedText;

    private void Start()
    {
        PrepareFontDropdown();
        ShowText();
    }

    private void PrepareFontDropdown()
    {
        FontDropdown.options.Clear();
        foreach (var font in Fonts)
            FontDropdown.options.Add(new Dropdown.OptionData(font.name));
        FontDropdown.captionText.text = Fonts[0].name;
    }

    public void OnFontDropdownValueChange()
    {
        ShowText();
    }

    private void ShowText()
    {
        var s =
            // top -> top.low
            "\x0E01\x0E34 \x0E01\x0E4C \x0E01\x0E38\x0E4C \x0E01\x0E34\x0E4C \x0E01\x0E4C\x0E33".Replace(" ", "") + " " +
            // top -> top.lowleft | top.left
            "\x0E1B\x0E34 \x0E1B\x0E4C \x0E1B\x0E38\x0E4C \x0E1B\x0E34\x0E4C \x0E1B\x0E4C\x0E33".Replace(" ", "") + " " +
            // lower -> lower.low
            "\x0E0E\x0E38 \x0E0F\x0E38".Replace(" ", "") + " " +
            // base.desclike -> base.descless
            "\x0E01\x0E38 \x0E0D\x0E38 \x0E10\x0E38".Replace(" ", "") + " ";

        ShowText(s);
    }

    private void ShowText(string s)
    {
        var font = Fonts[FontDropdown.value];
        OriginalText.font = font;
        OriginalText.text = s;
        AdjustedText.font = font;
        AdjustedText.text = ThaiFontAdjuster.Adjust(s);
    }
}
