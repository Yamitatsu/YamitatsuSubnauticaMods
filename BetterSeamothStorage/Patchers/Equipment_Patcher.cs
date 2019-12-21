namespace BetterSeamothStorage.Patchers
{
    using System;
    using Harmony;
    using Utils;

    [HarmonyPatch(typeof(Equipment))]
    [HarmonyPatch("AllowedToAdd")]
    internal class Equipment_AllowedToAdd_Patcher
    {
        [HarmonyPrefix]
        internal static bool Prefix(Equipment __instance, string slot, Pickupable pickupable, bool verbose, ref bool __result)
        {
            TechType techTypeInSlot = pickupable.GetTechType();
            if (VehicleMgr.StorageTechTypes.Contains(techTypeInSlot))
            {
                if (!VehiculeSlotMgr.IsEligibleForStorage(slot))
                {
                    __result = false;
                    ErrorMessage.AddMessage("Better Seamoth Storage:\nStorage module not allowed for this slot!");
                    return false;
                }
            }

            return true;
        }
    }
}