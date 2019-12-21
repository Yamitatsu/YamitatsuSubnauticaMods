namespace BetterSeamothStorage
{
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Handlers;
    using UnityEngine;

    public abstract class StorageUpgrade : Craftable
    {
        protected StorageUpgrade(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            base.OnFinishedPatching += PostPatch;
        }

        protected virtual TechType PrefabTemplate { get; } = TechType.VehicleStorageModule;

        public sealed override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public sealed override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        public sealed override TechType RequiredForUnlock => TechType.BaseUpgradeConsole;
        public sealed override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;
        public sealed override string[] StepsToFabricatorTab => new[] { Main.WorkBenchTab };
        public sealed override string AssetsFolder => "BetterSeamothStorage/Assets";

        public override GameObject GetGameObject()
        {
            GameObject prefab = CraftData.GetPrefabForTechType(this.PrefabTemplate);
            return Object.Instantiate(prefab);
        }

        private void PostPatch()
        {
            CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.VehicleModule);
            CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
           
        }
        
    }
}