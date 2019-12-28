namespace BetterVehicleStorage.Patchers
{
    using System.Diagnostics;
    using Harmony;
    using Managers;
    using UnityEngine;
    using Utilities;

    [HarmonyPatch(typeof(Equipment))]
    [HarmonyPatch("AllowedToAdd")]
    public class Equipment_AllowedToAdd_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(Equipment __instance, string slot, Pickupable pickupable, bool verbose,
            ref bool __result)
        {
            return StorageModuleMgr.AllowedToAdd(__instance, slot, pickupable, verbose, ref __result);
        }
    }

    [HarmonyPatch(typeof(Equipment))]
    [HarmonyPatch("GetTechTypeInSlot")]
    public class Equipment_GetTechTypeInSlot_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(Equipment __instance, string slot, ref TechType __result)
        {
            var callingMethodName = new StackFrame(2).GetMethod().Name;
            return StorageModuleMgr.GetTechTypeInSlot(__instance, slot, ref __result, callingMethodName);
        }
    }
    
    [HarmonyPatch(typeof(uGUI_ItemsContainer))]
    [HarmonyPatch("OnResize")]
    public class uGUI_ItemsContainer_OnResize_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(ref uGUI_ItemsContainer __instance, int width, int height)
        {
            YoLog.Debug($"OnResize called with {width.ToString()};{height.ToString()}");
            StorageModuleMgr.fixOnResize(ref __instance, width, height);
        }
    }
}