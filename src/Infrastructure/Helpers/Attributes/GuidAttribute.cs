
using System;

namespace Helpers.Attributes
{
    public class GuidAttribute : Attribute
    {
        public GuidAttribute(string guid)
        {
            Guid = Guid.Parse(guid);
        }

        public Guid Guid { get; }
    }
}