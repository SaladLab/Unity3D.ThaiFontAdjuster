# Under the hood

## The structure of Thai letter clusters.

Thai letter use composition of multiple character to make one thai letter.
For example, letter กิ้ has 3 unicode characters.

> ก + ◌ี + ◌้ → กิ้
> - ก: Base consonant KO KAI (U+0E01)
> - ◌ี: Upper vowel SARA I (U+0E34)
> - ◌้: Top tone mark MAI THO (U+0E49)

Because font cannot have full composed glyphs for Thai letters,
font renderer has to support composition of glyphs to compose single letter.

Simply drawing multiple glyphs at sample position can compose glyphs.
But for letter ปิ้

> ป + ◌ี + ◌้ → ปิ้
> - ก: Base consonant PO PLA (U+0E1B)
> - ◌ี: Upper vowel SARA I (U+0E34)
> - ◌้: Top tone mark MAI THO (U+0E49)

Base consonant ป has an ascender with right side, upper vowel and top tone mark
should be positioned slight shifted to left to avoid conflict of glyphs.

## GSUB and GPOS

Opentype font provides GSUB and GPOS to deal with these problem.
GSUB is used for substituting glyphs and can control thing like:

> ญ + ◌ุ → ญุ
> - ญ: Base consonant YO YING (U+0E0D)
> - ◌ุ: Lower vowel SARA U (U+0E38)

Consonant ญ has lower part and when combined with lower vowel like SARA U,
lower part of ญ should be removed. GSUB will handle like this:

> ญ + ◌ุ → ญ_descless + ◌ุ

GPOS is used for positioning glyphs, which can control vowel and tone mark positioning.

> Glyph ◌้ positioning
> - ก + ◌้ → ก้
> - ก + ◌ี + ◌้ → กิ้
> - ป + ◌้ → ป้
> - ป + ◌ี + ◌้ → ปิ้

Regular Thai fonts have many GPOS data for positiong glyphs delicately.
With GSUB and GPOS, Thai letter can be constructed neatly.

## The C90 encoding for Thai

Without GPOS support, they already solved glyph positioning problem.
Font was developed to have extended glyphs for upper vowel, lower vowel and tone mark
to have various positions like left-one, top-one, etc.
Thai letters are categoried into various graphical glyph classes, and
context patterns for base glyphs with vowels and tone marks is written like:

> Glyph ◌้ positioning
> - ก + ◌้ → ก้ + ◌้_low
> - ก + ◌ี + ◌้ → ก + ◌ี + ◌้
> - ป + ◌้ → ป + ◌้_lowleft
> - ป + ◌ี + ◌้ → ป + ◌ี_left + ◌้_left

For more information about this, read [Thai fonts, TUGboat, Volume 21 (2000), No. 2](https://www.tug.org/TUGboat/tb21-2/tb67lemb.pdf) and
[The C90 encoding for Thai](http://www.bakoma-tex.com/doc/fonts/enc/c90/c90.pdf).

And they have been using this strategy for long time, regular Thai font have
extended glyphs which meets C90 encoding.
For example [Garuda](http://www.nectec.or.th/pub/review-software/font/national-fonts.html)
font has Thai glyphs

![ThaiFont](https://raw.githubusercontent.com/SaladLab/Unity3D.ThaiFontAdjuster/master/docs/Font.png)

In addition this standard glyphs, extended glyphs are included.

![ThaiFontExtended](https://raw.githubusercontent.com/SaladLab/Unity3D.ThaiFontAdjuster/master/docs/FontExtended.png)

Extended glyphs consist of:

| Code      | Description         | C90 Category  |
| --------- | ------------------- | ------------- |
| U+F700    | uni0E10.descless    | base.descless |
| U+F701~04 | uni0E34~37.left     | upper.left    |
| U+F705~09 | uni0E48~4C.lowleft  | top.lowleft   |
| U+F70A~0E | uni0E48~4C.low      | top.low       |
| U+F70F    | uni0E0D.descless    | base.descless |
| U+F710~12 | uni0E31,4D,47.left  | upper.left    |
| U+F713~17 | uni0E48~4C.left     | top.left      |
| U+F718~1A | uni0E38~3A.low      | lower.low     |

## Unity3D

Because Unity3D lacks support for GSUB and GPOS, C90 encoding is used for controlling
the position of glyphs.
GPOS is impossible to mimic without support but GSUB is possible with
simple finding and replacing string.
