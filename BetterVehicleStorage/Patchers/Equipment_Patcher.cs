namespace BetterVehicleStorage.Patchers
{
    using Harmony;
    using Managers;

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
}