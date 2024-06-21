<Query Kind="Program">
  <Namespace>System.Runtime.Serialization</Namespace>
</Query>

private const int MagicNumber = 10;

void Main()
{
    string colorString = "Brownj";
    ColorStrings? enumValue = default;
    var colorConvertor = new ColorConvertor<ColorStrings?>();
    enumValue = colorConvertor.GetEnumFromColorString(colorString);
    enumValue.Dump();
}


public class ColorConvertor<T>
{
    private Type _underlyingType;

    public ColorConvertor()
    {
        _underlyingType = Nullable.GetUnderlyingType(typeof(T));
    }
    
    public T GetEnumFromColorString(string colorString)
    {
        try
        {
            return (T)Enum.Parse(_underlyingType, colorString, true);
        }
        catch
        {
            return GetDefaultColor(MagicNumber, _underlyingType);
        }
    }

    //private T? GetDefaultColor<T>() where T: struct
    //{
    //    if (Enum.IsDefined(typeof(T), DefaultColor))
    //    {
    //        return (T?)Enum.Parse(typeof(T), DefaultColor, true);
    //    }
    //    
    //    if (Enum.IsDefined(typeof(T), MagicNumber))
    //    {
    //        return (T?)Enum.Parse(typeof(T), string.Format("{0:D}", MagicNumber));
    //    }
    //    
    //    return null;
    //}

    //    private static T? GetDefaultColor<T>(int enumAsInt, Type enumType) where T : struct
    //    {
    //        //Type enumType = typeof(T);
    //
    //        T value = (T)Enum.ToObject(enumType, enumAsInt);
    //        if (!Enum.IsDefined(enumType, value))
    //        {
    //            return null;
    //        }
    //
    //        return (T?)value;
    //    }

    private T GetDefaultColor(int enumAsInt, Type enumType)
    {
        T value = (T)Enum.ToObject(enumType, enumAsInt);
        return (T)value;
    }
}

// You can define other methods, fields, classes and namespaces here
public enum ColorStrings
{
    Yellow,
    White,
    Red,
    Purple,
    Orange,
    Gray,
    Green,
    Blue,
    Black,
    Brown,
    Other,
}