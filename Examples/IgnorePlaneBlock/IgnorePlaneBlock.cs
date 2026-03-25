using System;
using System.Collections.Generic;
using HarmonyLib;

[HarmonyPatch]
public static class IgnorePlaneBlockPatches {
    [HarmonyPatch(typeof(StructureUtils), nameof(StructureUtils.BlockOpposingPlaneBuilding))]
    [HarmonyPostfix]
    public static void BuildAnyPlane(ref bool __result) {
        __result = false;
    }
}

public class IgnorePlaneBlock {
    private HarmonySupport m_harmonySupport;

    public void OnEnable(Dictionary<string, object> dependencies) {
        if (!dependencies.TryGetValue("harmony_support", out var harmonySupportObj)) {
            throw new Exception("harmony_support not found");
        }
        
        m_harmonySupport = (HarmonySupport)harmonySupportObj;
        m_harmonySupport.PatchAll("IgnorePlaneBlock", typeof(IgnorePlaneBlockPatches).Assembly);
    }

    public void OnDisable() {
        m_harmonySupport.UnpatchAll("IgnorePlaneBlock");
    }
}