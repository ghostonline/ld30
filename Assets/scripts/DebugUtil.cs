using System.Diagnostics;
using System;

public class DebugUtil
{
    [Conditional("DEBUG")]
    public static void Assert(bool condition)
    {
        Assert(condition, "Assertion failed");
    }

    [Conditional("DEBUG")]
    public static void Assert(bool condition, string msg)
    {
        if (!condition) throw new Exception(msg);
    }
}
