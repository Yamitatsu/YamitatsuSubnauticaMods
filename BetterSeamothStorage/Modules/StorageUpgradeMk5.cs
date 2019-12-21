namespace BetterSeamothStorage.Modules
{
    using System.Collections.Generic;
    using SMLHelper.V2.Crafting;

    public class StorageUpgradeMk5 : StorageUpgrade
    {
        public static readonly int StorageWidth = 8;
        public static readonly int StorageHeight = 10;
        
        public StorageUpgradeMk5()
            : base("StorageUpgradeMk5",
                "Tardis Storage Module",
                "The best storage module with 64 extra slots.\nIt's bigger on the inside !!"
                )
        {
            base.OnFinishedPatching += () => { Main.StorageModuleMk5TechType = this.TechType; };
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(Main.StorageModuleMk4TechType, 1),
                    new Ingredient(TechType.Titanium, 3),
                    new Ingredient(TechType.Lithium, 1)
                }
            };
        }
    }
}