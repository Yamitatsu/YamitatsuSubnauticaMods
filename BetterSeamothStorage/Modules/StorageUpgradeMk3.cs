namespace BetterSeamothStorage.Modules
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    public class StorageUpgradeMk3 : StorageUpgrade
    {
        public static readonly int StorageWidth = 8;
        public static readonly int StorageHeight = 6;
        
        public StorageUpgradeMk3()
            : base("StorageUpgradeMk3",
                "Storage Module Mk3",
                "An improved storage module with 32 extra slots."
                )
        {
            base.OnFinishedPatching += () => { Main.StorageModuleMk3TechType = this.TechType; };
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(Main.StorageModuleMk2TechType, 1),
                    new Ingredient(TechType.Titanium, 3),
                    new Ingredient(TechType.Lithium, 1)
                }
            };
        }
    }
}