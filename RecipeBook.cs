using ST10122437_PROG6221_POE_;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    public class RecipeBook
    {
        private List<Recipe> recipes;       //Created List to Store Recipes

        public RecipeBook()
        {
            recipes = new List<Recipe>();       //Calling List which stores the recipes

        }

        public List<Recipe> GetRecipes()
        {
            return recipes.ToList(); // Return a copy or a new list of recipes
        }

        // Method to filter recipes by food group
        public List<Recipe> FilterRecipesByFoodGroup(string foodGroup)
        {
            return recipes.Where(r => r.ingredients.Any(i => i.FoodGroup.Equals(foodGroup, StringComparison.OrdinalIgnoreCase)))
                          .ToList();
        }

        public List<Recipe> FilterRecipesByIngredient(string ingredientName)
        {
            return recipes.Where(r => r.ingredients.Any(i => i.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase)))
                          .ToList();
        }

        // Method to filter recipes by maximum calories
        public List<Recipe> FilterRecipesByCalories(int maxCalories)
        {
            return recipes.Where(r => r.CalculateTotalCalories() <= (double)maxCalories)
                          .ToList();
        }


        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);        //Method to Store recipres
        }

        public Recipe GetRecipeByName(string name)
        {
            return recipes.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));  //Logic to search data of Recipes entered by the user during the session
        }

        public void DisplayRecipes()
        {
            Console.WriteLine("\nRecipes:");
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();//Ordering Method (Alphabetically) when displaying data of Recipes when option is selected on console
            foreach (var recipe in sortedRecipes)
            {
                Console.WriteLine(recipe.Name);         //Display all Recipe data, ordered
            }
        }
    }
}
