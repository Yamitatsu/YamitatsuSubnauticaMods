namespace BetterSeamothStorage.Modules
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    public class StorageUpgradeMk4 : StorageUpgrade
    {
        public static readonly int StorageWidth = 8;
        public static readonly int StorageHeight = 8;
        
        public StorageUpgradeMk4()
            : base("StorageUpgradeMk4",
                "Storage Module Mk4",
                "An improved storage module with 48 extra slots."
                )
        {
            base.OnFinishedPatching += () => { Main.StorageModuleMk4TechType = this.TechType; };
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(Main.StorageModuleMk3TechType, 1),
                    new Ingredient(TechType.Titanium, 3),
                    new Ingredient(TechType.Lithium, 1)
                }
            };
        }
    }
}