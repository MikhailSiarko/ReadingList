using System;

namespace ReadingList.WriteModel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}