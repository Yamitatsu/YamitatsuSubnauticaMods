namespace BetterVehicleStorage
{
    using System.Reflection;
    using Harmony;
    using Managers;
    using SMLHelper.V2.Handlers;
    using Utilities;

    public static class Main
    {
        internal const string WorkBenchTab = "Storage";

        public static void Patch()
        {
            CraftTreeHandler.AddTabNode(
                CraftTree.Type.Workbench,
                WorkBenchTab,
                "Storage Modules",
                SpriteManager.Get(TechType.VehicleStorageModule));

            StorageModuleMgr.RegisterModules();
            
            CraftDataHandler.SetQuickSlotType(TechType.VehicleStorageModule, QuickSlotType.Instant);

            var harmony = HarmonyInstance.Create("Yamitatsu.BetterVehicleStorage");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}