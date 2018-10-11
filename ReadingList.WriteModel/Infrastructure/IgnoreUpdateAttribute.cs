using System;

namespace ReadingList.WriteModel.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}