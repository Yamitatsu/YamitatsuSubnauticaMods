namespace BetterSeamothStorage
{
    using System;
    using System.Reflection;
    using Harmony;
    using Modules;
    using SMLHelper.V2.Handlers;

    public static class Main
    {
        internal const string WorkBenchTab = "Storage";
        public static TechType StorageModuleMk2TechType; 
        public static TechType StorageModuleMk3TechType; 
        public static TechType StorageModuleMk4TechType; 
        public static TechType StorageModuleMk5TechType; 

        public static void Patch()
        {
            Console.WriteLine($"[BetterSeamothStorage:INFO] Started patching.");

            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, WorkBenchTab, "Storage Modules", SpriteManager.Get(TechType.VehicleStorageModule));
            
            var storageModuleMk2 = new StorageUpgradeMk2();
            StorageModuleMk2TechType = storageModuleMk2.TechType;
            storageModuleMk2.Patch();
            
            var storageModuleMk3 = new StorageUpgradeMk3();
            StorageModuleMk3TechType = storageModuleMk3.TechType;
            storageModuleMk3.Patch();
            
            var storageModuleMk4 = new StorageUpgradeMk4();
            StorageModuleMk4TechType = storageModuleMk4.TechType;
            storageModuleMk4.Patch();
            
            var storageModuleMk5 = new StorageUpgradeMk5();
            StorageModuleMk5TechType = storageModuleMk5.TechType;
            storageModuleMk5.Patch();

            var harmony = HarmonyInstance.Create("com.betterseamothstorage.psmod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
            Console.WriteLine($"[BetterSeamothStorage:INFO] Done patching.");
        }
    }
}