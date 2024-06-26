using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ST10122437_PROG6221_POE_;
using RecipeApp;

namespace ST10122437_POE_PROG6221_TalhahThokan_Final
{
    public partial class MainWindow : Window
    {
        private RecipeBook recipeBook;      // Instance of RecipeBook to manage recipes
        private Recipe currentRecipe;       // Currently selected recipe for editing/viewing

        public MainWindow()
        {
            InitializeComponent();
            recipeBook = new RecipeBook();   // Initialize RecipeBook instance
            LoadRecipes();                   // Load recipes into the UI
        }

        // Load recipes into RecipeList
        private void LoadRecipes()
        {
            RecipeList.ItemsSource = recipeBook.GetRecipes();
        }

        // Handle adding ingredients to currentRecipe
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string name = IngredientNameInput.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter ingredient name.");
                return;
            }

            // Validate and parse quantity
            if (!double.TryParse(IngredientQuantityInput.Text, out double quantity))
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            string unit = IngredientUnitInput.Text;
            int calories;
            if (!int.TryParse(IngredientCaloriesInput.Text, out calories))
            {
                MessageBox.Show("Invalid calories.");
                return;
            }

            string foodGroup = IngredientFoodGroupInput.Text;

            // Add ingredient to current recipe and update UI
            currentRecipe.AddIngredient(name, quantity, unit, calories, foodGroup);
            IngredientList.ItemsSource = currentRecipe.ingredients;
        }

        // Handle adding steps to currentRecipe
        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            string step = StepInput.Text;
            if (string.IsNullOrWhiteSpace(step))
            {
                MessageBox.Show("Please enter step description.");
                return;
            }

            // Add step to current recipe and update UI
            currentRecipe.AddStep(step);
            StepList.ItemsSource = currentRecipe.steps;
            StepInput.Text = "Step Description";
        }

        // Save currentRecipe to recipeBook
        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameInput.Text;
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Please enter recipe name.");
                return;
            }

            recipeBook.AddRecipe(currentRecipe); // Save current recipe to recipe book
            LoadRecipes();                      // Reload recipes into RecipeList
            ClearRecipeFields();                // Clear input fields
            MessageBox.Show("Recipe saved successfully!");
        }

        // Clear all input fields and reset currentRecipe
        private void ClearRecipeFields()
        {
            RecipeNameInput.Text = "Recipe Name";
            IngredientNameInput.Text = "Name";
            IngredientQuantityInput.Text = "Quantity";
            IngredientUnitInput.Text = "Unit";
            IngredientCaloriesInput.Text = "Calories";
            IngredientFoodGroupInput.Text = "Food Group";
            StepInput.Text = "Step Description";
            IngredientList.ItemsSource = null;
            StepList.ItemsSource = null;
            currentRecipe = null;
        }

        // Handle filtering recipes based on user input (ingredient, food group, calories)
        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            string filterType = Microsoft.VisualBasic.Interaction.InputBox("Enter filter type: ingredient, food group, or calories", "Filter Recipes", "ingredient");
            if (filterType.ToLower() == "ingredient")
            {
                string ingredientName = Microsoft.VisualBasic.Interaction.InputBox("Enter ingredient name:", "Filter by Ingredient", "");
                FilterRecipesByIngredient(ingredientName);
            }
            else if (filterType.ToLower() == "food group")
            {
                string foodGroup = Microsoft.VisualBasic.Interaction.InputBox("Enter food group:", "Filter by Food Group", "");
                FilterRecipesByFoodGroup(foodGroup);
            }
            else if (filterType.ToLower() == "calories")
            {
                string maxCaloriesStr = Microsoft.VisualBasic.Interaction.InputBox("Enter maximum number of calories:", "Filter by Calories", "300");
                if (int.TryParse(maxCaloriesStr, out int maxCalories))
                {
                    FilterRecipesByCalories(maxCalories);
                }
                else
                {
                    MessageBox.Show("Invalid number of calories.");
                }
            }
            else
            {
                MessageBox.Show("Invalid filter type.");
            }
        }

        // Filter recipes by ingredient name and update RecipeList
        private void FilterRecipesByIngredient(string ingredientName)
        {
            RecipeList.ItemsSource = recipeBook.FilterRecipesByIngredient(ingredientName);
        }

        // Filter recipes by food group and update RecipeList
        private void FilterRecipesByFoodGroup(string foodGroup)
        {
            RecipeList.ItemsSource = recipeBook.FilterRecipesByFoodGroup(foodGroup);
        }

        // Filter recipes by maximum calories and update RecipeList
        private void FilterRecipesByCalories(int maxCalories)
        {
            RecipeList.ItemsSource = recipeBook.FilterRecipesByCalories(maxCalories);
        }

        // Handle selection change in RecipeList to display selected recipe details
        private void RecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeList.SelectedItem != null)
            {
                currentRecipe = (Recipe)RecipeList.SelectedItem;
                DisplayRecipe(currentRecipe);
            }
        }

        // Display details of the selected recipe in RecipeDetails
        private void DisplayRecipe(Recipe recipe)
        {
            RecipeDetails.Text = $"Recipe: {recipe.Name}\n\nIngredients:\n";
            foreach (var ingredient in recipe.ingredients)
            {
                RecipeDetails.Text += $"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories) - {ingredient.FoodGroup}\n";
            }
            RecipeDetails.Text += $"\nSteps:\n";
            for (int i = 0; i < recipe.steps.Count; i++)
            {
                RecipeDetails.Text += $"{i + 1}. {recipe.steps[i]}\n";
            }
            RecipeDetails.Text += $"\nTotal Calories: {recipe.CalculateTotalCalories()}";
        }

        // Handle scaling currentRecipe by a factor entered by the user
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            string scaleInput = Microsoft.VisualBasic.Interaction.InputBox("Enter scale factor (e.g., 0.5, 2, 3)", "Scale Recipe", "2");
            if (double.TryParse(scaleInput, out double factor))
            {
                currentRecipe.ScaleRecipe(factor);
                DisplayRecipe(currentRecipe);
            }
            else
            {
                MessageBox.Show("Invalid scale factor.");
            }
        }

        // Reset ingredient quantities in currentRecipe to their original values
        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe.ResetQuantities();
            DisplayRecipe(currentRecipe);
        }

        // Clear currentRecipe and all input fields
        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe.ClearRecipe();
            ClearRecipeFields();
            LoadRecipes();
        }
    }
}