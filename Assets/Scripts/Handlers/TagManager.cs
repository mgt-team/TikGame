using System;

public class TagManager
{

    public static string GetTagNameByEnum<T>(T value)
    {
        return Enum.GetName(typeof(T), value);
    }
}


