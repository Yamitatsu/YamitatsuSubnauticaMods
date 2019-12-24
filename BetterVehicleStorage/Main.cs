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
            YoLog.Info("Patching started.");
            CraftTreeHandler.AddTabNode(
                CraftTree.Type.Workbench,
                WorkBenchTab,
                "Storage Modules",
                SpriteManager.Get(TechType.VehicleStorageModule));

            StorageModuleMgr.RegisterModules();

            var harmony = HarmonyInstance.Create("com.bettervehiclestorage.psmod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            YoLog.Info("Patching done.");
        }
    }
}