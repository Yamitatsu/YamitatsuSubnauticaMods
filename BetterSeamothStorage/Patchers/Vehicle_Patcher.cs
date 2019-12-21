namespace BetterSeamothStorage.Patchers
{
    using Harmony;
    using Utils;

    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch(nameof(Vehicle.GetStorageInSlot))]
    internal class Vehicle_GetStorageInSlot_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(Vehicle __instance, int slotID, ref ItemsContainer __result)
        {
            __result = VehicleMgr.GetStorageInSlot(__instance, slotID);
            return false;
        }
    }
}