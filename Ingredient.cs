namespace ST10122437_PROG6221_POE_
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }            //Getters and Setters for Ingredient Data to be utlisied during run time sessions
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;            //Assign to Var for Getters and Setters generated
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}
