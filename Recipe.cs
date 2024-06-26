using System;
using System.Collections.Generic;

namespace ST10122437_PROG6221_POE_
{
    public delegate void CalorieNotification(string message); //Delegate used to notify Calories when exceeding 300

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> ingredients;
        public List<string> steps;                     //Var list used through the session
        public List<Ingredient> originalIngredients;

        public event CalorieNotification OnCalorieExceed;       //Delegate notification for Calories Exceeding

        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();                                     //Calling all Var created and assigning
            originalIngredients = new List<Ingredient>();
        }

        public void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            var ingredient = new Ingredient(name, quantity, unit, calories, foodGroup); //init Ingredient and assigning
            ingredients.Add(ingredient);                                        
            originalIngredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup)); //calling Add Method for Ingredients
        }

        public void AddStep(string step)
        {
            steps.Add(step);  //Calling Adding Steps method
        }

        public void DisplayRecipe()
        {
            Console.WriteLine("Recipe:" + Name); //Display Recipe
            Console.WriteLine("Ingredients:"); //Display Ingredients
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories) - {ingredient.FoodGroup}"); //Display Reciper Data as per User addition
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");  //Display Number of steps and all steps data as user created entries
            }

            Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories()}"); //Display number of Calories user entered as per all ingredients Data Sum
        }

        public void ScaleRecipe(double factor)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity *= factor;
            }
            Console.WriteLine($"\nRecipe scaled by factor {factor}.");     //Display the scalled data if Recipe needed to changeScale by a factoor 
            DisplayRecipe();
        }

        public void ResetQuantities()
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity = originalIngredients[i].Quantity;
            }
            Console.WriteLine("\nQuantities reset to original values.");        //Reset All Recipe Data
            DisplayRecipe();
        }

        public void ClearRecipe()
        {
            ingredients.Clear();
            steps.Clear();
            originalIngredients.Clear();            //Clear All Recipe Data
            Console.WriteLine("\nRecipe cleared.");
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;                       //Method to Calculate All Calories from Each Ingredient
            }
            return totalCalories;
        }

        public void CheckCalories()
        {
            if (CalculateTotalCalories() > 300) //Norification Logic if Data exceeds 300
            {
                OnCalorieExceed?.Invoke($"Warning, The Recipe: {Name} exceeds 300 calories!"); //Delegate Notification used to check if Calories Exceed 300, Send notification
            }
        }
    }
}
