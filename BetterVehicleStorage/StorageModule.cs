namespace BetterVehicleStorage
{
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Handlers;
    using UnityEngine;
    using Utilities;

    public abstract class StorageModule : Craftable
    {
        public static int StorageWidth = 4;
        public static int StorageHeight = 4;
        public static bool ShouldBeTheOnlyOne = false;
        public static bool OpenAllSlots = false;
        protected StorageModule(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            base.OnFinishedPatching += () =>
            {
                YoLog.Debug($"Registering done with TechType");
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.VehicleModule);
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                YoLog.Debug($"Registering done with TechType {this.TechType.ToString()}");
            };
        }

        public sealed override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public sealed override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public sealed override TechType RequiredForUnlock => TechType.BaseUpgradeConsole;

        public sealed override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public sealed override string[] StepsToFabricatorTab => new[] { Main.WorkBenchTab };

        public sealed override string AssetsFolder => "BetterVehicleStorage/Assets";

        public override GameObject GetGameObject()
        {
            GameObject prefab = CraftData.GetPrefabForTechType(TechType.VehicleStorageModule);
            return Object.Instantiate(prefab);
        }
    }
}