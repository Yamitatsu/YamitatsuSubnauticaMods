namespace BetterVehicleStorage
{
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Handlers;
    using UnityEngine;
    using Utilities;

    public abstract class StorageModule : Craftable
    {
        public int StorageWidth = 4;
        public int StorageHeight = 4;
        protected StorageModule(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            base.OnFinishedPatching += () =>
            {
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.VehicleModule);
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Instant);
            };
        }

        public sealed override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public sealed override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public sealed override TechType RequiredForUnlock => TechType.Workbench;

        public sealed override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public override string[] StepsToFabricatorTab => new[] { Main.WorkBenchTab };

        public sealed override string AssetsFolder => "BetterVehicleStorage/Assets";

        public override GameObject GetGameObject()
        {
            GameObject prefab = CraftData.GetPrefabForTechType(TechType.VehicleStorageModule);
            return Object.Instantiate(prefab);
        }
    }
}