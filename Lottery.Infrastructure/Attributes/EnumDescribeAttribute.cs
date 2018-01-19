using System;

namespace Lottery.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumDescribeAttribute : Attribute
    {
        private readonly string _describe;

        public EnumDescribeAttribute(string describe)
        {
            _describe = describe;
        }

        public string Describe {
            get { return _describe; }
        }
    }
}