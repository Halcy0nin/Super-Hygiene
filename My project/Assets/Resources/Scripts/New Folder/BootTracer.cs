using UnityEngine;

public static class BootTracer
{
    public static void Log(string context)
    {
        Debug.Log($"[BOOT TRACE] {Time.realtimeSinceStartup:F3}s - {context}");
    }
}
