namespace BetterSeamothStorage.Utils
{
    using System.Collections.Generic;

    internal static class VehiculeSlotMgr
    {
        internal static readonly List<string> SeamothEligibleForStorageSlots = new List<string>()
        {
            "SeamothModule1",
            "SeamothModule2",
            "SeamothModule3",
            "SeamothModule4"
        };
        internal static readonly List<string> ExosuitEligibleForStorageSlots = new List<string>()
        {
            "ExosuitModule1",
            "ExosuitModule2",
            "ExosuitModule3",
            "ExosuitModule4"
        };

        internal static bool IsEligibleForStorage(string slotName)
        {
            return SeamothEligibleForStorageSlots.Contains(slotName) || ExosuitEligibleForStorageSlots.Contains(slotName);
        }
    }
}