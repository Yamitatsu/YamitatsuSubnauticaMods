﻿namespace BetterSeamothStorage.Patchers
{
    using Harmony;
    using Utils;

    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch("Start")]
    internal class SeaMoth_Start_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(SeaMoth __instance)
        {
            if (__instance.gameObject.GetComponent<InventoryManagement>() == null)
                __instance.gameObject.AddComponent<InventoryManagement>();
        }
    }

    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch("OnUpgradeModuleChange")]
    internal class SeaMoth_OnUpgradeModuleChange_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(ref SeaMoth __instance, int slotID, TechType techType, bool added)
        {
            __instance.storageInputs[slotID].SetEnabled(added && VehicleMgr.StorageTechTypes.Contains(techType));
        }
    }

    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch("IsAllowedToRemove")]
    internal class SeaMoth_IsAllowedToRemove_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(SeaMoth __instance, Pickupable pickupable, bool verbose, ref bool __result)
        {
            __result = VehicleMgr.IsAllowedToRemove(__instance, pickupable, verbose);
            return false;
        }
    }
}