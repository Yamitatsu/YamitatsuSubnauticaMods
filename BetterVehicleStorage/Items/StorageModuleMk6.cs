namespace BetterVehicleStorage.Items
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    public class StorageModuleMk6 : StorageModule
    {
        private readonly TechType StorageModuleMk5TechType;
        public StorageModuleMk6(TechType storageModuleMk5TechType)
            : base(
                "StorageModuleMk6",
                "Tardis Storage Module",
                "The best storage module. Open up the 4 inventories of the Seamoth with 80 slots each.\nNot compatible with the other storage modules.\nIt's bigger on the inside !!")
        {
            StorageWidth = 8;
            StorageHeight = 10;
            ShouldBeTheOnlyOne = true;
            OpenAllSlots = true;
            StorageModuleMk5TechType = storageModuleMk5TechType;
        }

        public override string[] StepsToFabricatorTab => new[] { "SeamothMenu" };

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(StorageModuleMk5TechType, 4),
                    new Ingredient(TechType.Lithium, 1),
                    new Ingredient(TechType.PrecursorIonCrystal, 1),
                }
            };
        }
    }
}