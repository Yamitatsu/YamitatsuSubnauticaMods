namespace BetterVehicleStorage.Items
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    public class StorageModuleMk5 : StorageModule
    {
        private readonly TechType StorageModuleMk4TechType;
        public StorageModuleMk5(TechType storageModuleMk4TechType)
            : base(
                "StorageModuleMk5",
                "Storage Module Mk5",
                "An improved storage module with 80 slots.")
        {
            StorageWidth = 8;
            StorageHeight = 10;
            StorageModuleMk4TechType = storageModuleMk4TechType;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(StorageModuleMk4TechType, 1),
                    new Ingredient(TechType.Titanium, 3),
                    new Ingredient(TechType.Lithium, 1),
                }
            };
        }
    }
}