using System;

namespace ReadingList.Domain.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}