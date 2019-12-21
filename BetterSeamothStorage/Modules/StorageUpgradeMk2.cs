namespace BetterSeamothStorage.Modules
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    public class StorageUpgradeMk2 : StorageUpgrade
    {
        public static readonly int StorageWidth = 8;
        public static readonly int StorageHeight = 4;
        
        public StorageUpgradeMk2()
            : base("StorageUpgradeMk2",
                "Storage Module Mk2",
                "An improved storage module with 16 extra slots."
                )
        {
            base.OnFinishedPatching += () => { Main.StorageModuleMk2TechType = this.TechType; };
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.VehicleStorageModule, 1),
                    new Ingredient(TechType.Titanium, 3),
                    new Ingredient(TechType.Lithium, 1)
                }
            };
        }
    }
}