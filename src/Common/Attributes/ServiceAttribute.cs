using System;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public bool AsSelf { get; set; }
        public bool AsSingleton { get; set; }
    }
}