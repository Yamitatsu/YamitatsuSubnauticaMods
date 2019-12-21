// namespace BetterSeamothStorage.Patchers
// {
//     using Harmony;
//     using Utils;
//
//     [HarmonyPatch(typeof(Exosuit))]
//     [HarmonyPatch("Start")]
//     internal class Exosuit_Start_Patcher
//     {
//         [HarmonyPostfix]
//         internal static void Postfix(Exosuit __instance)
//         {
//             if (__instance.gameObject.GetComponent<InventoryManagement>() == null)
//                 __instance.gameObject.AddComponent<InventoryManagement>();
//         }
//     }
//
//    
//
//     [HarmonyPatch(typeof(Exosuit))]
//     [HarmonyPatch("IsAllowedToRemove")]
//     internal class Exosuit_IsAllowedToRemove_Patcher
//     {
//         [HarmonyPrefix]
//         internal static bool Prefix(Exosuit __instance, Pickupable pickupable, bool verbose, ref bool __result)
//         {
//             __result = VehicleMgr.IsAllowedToRemove(__instance, pickupable, verbose);
//             return false;
//         }
//     }
//     
// }