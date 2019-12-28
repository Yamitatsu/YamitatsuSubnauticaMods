namespace BetterVehicleStorage.Patchers
{
    using Harmony;
    using Managers;
    using Utilities;

    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch("OnUpgradeModuleChange")]
    internal class Exosuit_OnUpgradeModuleChange_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(ref Exosuit __instance, int slotID, TechType techType, bool added)
        {
            StorageModuleMgr.UpdateExosuitStorage(ref __instance, slotID, techType, added);
        }
    }
    
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch("IsAllowedToRemove")]
    internal class Exosuit_IsAllowedToRemove_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(Exosuit __instance, Pickupable pickupable, bool verbose, ref bool __result)
        {
            __result = StorageModuleMgr.IsAllowedToRemoveFromExosuit(__instance, pickupable, verbose);
            return false;
        }
    }
    
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch("OnUpgradeModuleUse")]
    internal class Exosuit_OnUpgradeModuleUse_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(ref Exosuit __instance, TechType techType, int slotID)
        {
            StorageModuleMgr.OnUpgradeModuleUseFromExosuit(__instance, techType, slotID);
        }
    }
    
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch("UpdateStorageSize")]
    internal class Exosuit_UpdateStorageSize_Patcher
    {
        [HarmonyPostfix]
        internal static void Postfix(Exosuit __instance)
        {
            StorageModuleMgr.UpdateExosuitStorageSize(ref __instance);
        }
    }

}