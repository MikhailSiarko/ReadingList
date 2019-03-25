using System;

namespace ReadingList.Models.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}