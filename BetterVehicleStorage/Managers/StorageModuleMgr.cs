namespace BetterVehicleStorage.Managers
{
    using System.Collections.Generic;
    using Items;
    using Utilities;

    public static class StorageModuleMgr
    {
        public static readonly IDictionary<TechType, StorageModule> Modules = new Dictionary<TechType, StorageModule>();

        public static void RegisterModules()
        {
            var storageModuleMk2TechType = RegisterModule(new StorageModuleMk2());
            var storageModuleMk3TechType = RegisterModule(new StorageModuleMk3(storageModuleMk2TechType));
            var storageModuleMk4TechType = RegisterModule(new StorageModuleMk4(storageModuleMk3TechType));
            var storageModuleMk5TechType = RegisterModule(new StorageModuleMk5(storageModuleMk4TechType));
            RegisterModule(new StorageModuleMk6(storageModuleMk5TechType));
        }

        public static void PatchModules()
        {
            foreach (var storageModule in Modules.Values)
            {
                storageModule.Patch();
            }
        }
        
        private static TechType RegisterModule(StorageModule storageModule)
        {
            YoLog.Debug($"Registering {storageModule.GetType().Name}...");
            Modules.Add(storageModule.TechType, storageModule);
            YoLog.Debug($"Registering done with TechType {storageModule.TechType.ToString()}");
            return storageModule.TechType;
        }
    }
}