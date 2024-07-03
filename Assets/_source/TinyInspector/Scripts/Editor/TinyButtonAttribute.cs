using System;

namespace TinyInspector
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TinyButtonAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TinyPlayButtonAttribute : Attribute
    {
    }
}
