using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TagManager
{

    public static string GetTagNameByEnum<T>(T value)
    {
        return Enum.GetName(typeof(T), value);
    }

    public static bool CompareTagWithTagsList<T>(string tagToCompare, IEnumerable<T> tagEnumList)
    {
        return tagEnumList.Select(GetTagNameByEnum)
            .Any(tag => tag.Equals(tagToCompare));
    }

    public static bool CompareCollisionTag(Collision collision, TagEnum tagToCheck)
    {
        return collision.gameObject.CompareTag(GetTagNameByEnum(tagToCheck));
    }
}


