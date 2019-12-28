namespace BetterVehicleStorage.Patchers
{
    using System.Diagnostics;
    using Harmony;
    using Managers;
    using Utilities;

    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch(nameof(Vehicle.GetStorageInSlot))]
    internal class Vehicle_GetStorageInSlot_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(Vehicle __instance, int slotID, TechType techType, ref ItemsContainer __result)
        {
            if (!StorageModuleMgr.IsStorageModule(techType)) return true;
            var callingMethodName = new StackFrame(2).GetMethod().Name;
            __result = StorageModuleMgr.GetStorageInSlot(ref __instance, slotID, techType, callingMethodName);
            return false;
        }
    }
}