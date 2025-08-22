using UnityEngine.UI;

namespace Structure.Utility.Extension;

public static partial class Extension
{
    public enum HorizontalOrigin
    {
        Left = 0,
        Right = 1
    }

    public enum VerticalOrigin
    {
        Bottom = 0,
        Top = 1
    }

    public enum Radial90Origin
    {
        BottomLeft = 0,
        BottomRight = 1,
        TopRight = 2,
        TopLeft = 3
    }

    public enum Radial180Origin
    {
        Bottom = 0,
        Right = 1,
        Top = 2,
        Left = 3
    }

    public enum Radial360Origin
    {
        Bottom = 0,
        Right = 1,
        Top = 2,
        Left = 3
    }

    public static Image SetHorizontal(this Image image, HorizontalOrigin origin)
    {
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Horizontal;
        image.fillOrigin = (int)origin;
        return image;
    }

    public static Image SetVertical(this Image image, VerticalOrigin origin)
    {
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Vertical;
        image.fillOrigin = (int)origin;
        return image;
    }

    public static Image SetRadial90(this Image image, Radial90Origin origin)
    {
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial90;
        image.fillOrigin = (int)origin;
        return image;
    }

    public static Image SetRadial180(this Image image, Radial180Origin origin)
    {
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial180;
        image.fillOrigin = (int)origin;
        return image;
    }

    public static Image SetRadial360(this Image image, Radial360Origin origin)
    {
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial360;
        image.fillOrigin = (int)origin;
        return image;
    }
}