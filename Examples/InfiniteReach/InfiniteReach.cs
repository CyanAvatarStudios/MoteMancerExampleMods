using System;
using System.Collections.Generic;
using HarmonyLib;

[HarmonyPatch]
public static class InfiniteReachPatches {
    [HarmonyPatch(typeof(Upgrades), nameof(Upgrades.GetReachDistance))]
    [HarmonyPostfix]
    public static void InfiniteReachRange(ref int __result) {
        __result = 16671667;
    }
}

public class InfiniteReach {
    private HarmonySupport m_harmonySupport;

    public void OnEnable(Dictionary<string, object> dependencies) {
        if (!dependencies.TryGetValue("harmony_support", out var harmonySupportObj)) {
            throw new Exception("harmony_support not found");
        }
        
        m_harmonySupport = (HarmonySupport)harmonySupportObj;
        m_harmonySupport.PatchAll("InfiniteReach", typeof(InfiniteReachPatches).Assembly);
    }

    public void OnDisable() {
        m_harmonySupport.UnpatchAll("InfiniteReach");
    }
}