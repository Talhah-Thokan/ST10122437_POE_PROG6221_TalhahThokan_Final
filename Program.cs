using ST10122437_PROG6221_POE_;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)         
        {
            RecipeBook recipeBook = new RecipeBook();   // Init RecipeBook Class created to allow for multiple Recipe's to be stored during run time session

            while (true)
            {
                Console.WriteLine("\nRecipe Application"); //Instruction List for App dueing Console Start
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. View Recipe");
                Console.WriteLine("3. List Recipes");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine(); //Allows to accept the user response

                switch (choice)
                {
                    case "1":
                        AddRecipe(recipeBook);
                        break;
                    case "2":
                        ViewRecipe(recipeBook);
                        break;
                    case "3":                                                               //Case statement used to enable options for the user 
                        recipeBook.DisplayRecipes();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddRecipe(RecipeBook recipeBook)
        {
            Console.Write("Enter recipe name: ");
            string name = Console.ReadLine();                               //Add Recipe Method used to allow user to create an entry and store 
            Recipe recipe = new Recipe(name);

            Console.Write("Number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());                     //List Num of Ingredients used 

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredient {i + 1}:");
                Console.Write("Name: ");
                string ingredientName = Console.ReadLine();
                Console.Write("Quantity: ");
                double quantity = double.Parse(Console.ReadLine());                         //For loop used to continue asking user for data until number of ingredients are completed as per user data
                Console.Write("Unit of Measurement: ");
                string unit = Console.ReadLine();
                Console.Write("Calories: ");
                int calories = int.Parse(Console.ReadLine());
                Console.Write("Food Group: ");
                string foodGroup = Console.ReadLine();

                recipe.AddIngredient(ingredientName, quantity, unit, calories, foodGroup);  //Store ingredients into Recipe for later queries during run sessio
            }

            Console.Write("Number of steps: ");         //List Num of Steps used 
            int numSteps = int.Parse(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");          //For loop used to continue asking user for data until all steps are completed as per user data
                string step = Console.ReadLine();
                recipe.AddStep(step);
            }

            recipeBook.AddRecipe(recipe);       //Add to To RecipeBook for later queries during run time
            recipe.CheckCalories(); //Calling Calorie check to view if Recipe exceeds 300 calories
        }

        static void ViewRecipe(RecipeBook recipeBook)
        {
            Console.Write("Enter recipe name to view: ");           //View Method to view recipes entered into the RecipeBook Earlier in the session
            string name = Console.ReadLine();
            Recipe recipe = recipeBook.GetRecipeByName(name);
            if (recipe != null)
            {
                recipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");                                 //Validation Check if recipe name does not match data entered bu the user
            }
        }
    }
}
