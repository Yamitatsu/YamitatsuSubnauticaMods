namespace BetterVehicleStorage.Patchers
{
    using Harmony;
    using Managers;

    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch("OnUpgradeModuleChange")]
    internal class Seamoth_OnUpgradeModuleChange_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(ref SeaMoth __instance, int slotID, TechType techType, bool added)
        {
            StorageModuleMgr.UpdateSeamothStorage(ref __instance, slotID, techType, added);
        }
    }
    
    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch("OnUpgradeModuleUse")]
    internal class Seamoth_OnUpgradeModuleUse_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(ref SeaMoth __instance, TechType techType, int slotID)
        {
            StorageModuleMgr.OnUpgradeModuleUse(__instance, techType, slotID);
        }
    }

    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch("IsAllowedToRemove")]
    internal class SeaMoth_IsAllowedToRemove_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(SeaMoth __instance, Pickupable pickupable, bool verbose, ref bool __result)
        {
            __result = StorageModuleMgr.IsAllowedToRemove(__instance, pickupable, verbose);
            return false;
        }
    }

}