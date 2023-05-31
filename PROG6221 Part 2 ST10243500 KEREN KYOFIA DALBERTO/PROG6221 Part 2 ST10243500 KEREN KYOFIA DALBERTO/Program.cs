

using System;
using System.Collections.Generic;

namespace RecipeApp
{
    //Displays  recipe's properties such as name, quantity, unit, calories, and food group
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

    }
    //shows recipe's step and  description./
    class Step
    {
        public string Information { get; set; }
    }
    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }
        public int TotalCalories { get; private set; }



        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
            TotalCalories = 0;
        }
        //diplays that the method doesn't return a value
        public void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                Quantity = quantity,
                Unit = unit,
                Calories = calories,
                FoodGroup = foodGroup,

            };

            Ingredients.Add(ingredient);
            TotalCalories += calories;
        }

        public void AddStep(string information)
        {
            var step = new Step { Information = information};
            Steps.Add(step);
        }


    }
    // collects recipes and shows methods to add, remove, and retrieve recipes
    class RecipeProcessor
    {
        // private list of recipes that stores all the recipes managed by the Recipe processor/
        private List<Recipe> recipes;

        public RecipeProcessor()
        {
            recipes = new List<Recipe>();
        }
        //This method adds a recipe to the list
        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            recipes.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
        }
        //this method removes a recipe from the list
        public void RemoveRecipe(Recipe recipe)
        {
            recipes.Remove(recipe);
        }
        //Tthis method returns the list of recipes
        public List<Recipe> GetRecipeList()
        {
            return recipes;
        }
        // retrieves a recipe from the list based on its name.

        public Recipe GetRecipeByName(string name)
        {
            return recipes.Find(recipe => recipe.Name == name);
        }
    }


    delegate void RecipeNotification(Recipe recipe);

    // allows the methods to interact with the Recipe Processor
    class UI
    {
        private RecipeProcessor recipeManager;
        private RecipeNotification notificationDelegate;

        public UI()
        {
            recipeManager = new RecipeProcessor();
            notificationDelegate = RecipeExceedsCalorieLimit;
        }

        public void Run()
        {

            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Welcome To The Recipe App Processor");
                    Console.WriteLine("*********");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add a recipe");
                    Console.WriteLine("2. Delete a recipe");
                    Console.WriteLine("3. Display recipe list");
                    Console.WriteLine("4. Display recipe details");
                    Console.WriteLine("5. Clear all data");
                    Console.WriteLine("6. Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Red;
                            AddRecipe();
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            RemoveRecipe();
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Red;
                            DisplayRecipeList();
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Green;
                            DisplayRecipeDetails();
                            break;

                        case 5:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            ClearAllData();
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            exit = true;
                            Console.WriteLine("Exiting apllication!");
                            break;
                        default:
                            Console.WriteLine("Incorrect input!");
                            break;
                    }

                    Console.WriteLine();
                }

                catch
                {
                    Console.WriteLine("TRY AGAIN");
                }
            }
        }
        // allows the user to enter the details of a recipe,  including name, ingredients, and steps, and adds it to the Recipe processor.
        private void AddRecipe()
        {
            Console.WriteLine("Enter recipe name:");
            string name = Console.ReadLine();

            Recipe recipe = new Recipe(name);

            Console.WriteLine("Enter number of ingredients:");
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter ingredient {i + 1} name:");
                string ingredientName = Console.ReadLine();

                Console.WriteLine($"Enter ingredient {i + 1} quantity:");
                double quantity = double.Parse(Console.ReadLine());

                Console.WriteLine($"Enter ingredient {i + 1} unit of measurement:");
                string unitOfMeasurement = Console.ReadLine();

                Console.WriteLine($"Enter ingredient {i + 1} calories:");
                int calories = int.Parse(Console.ReadLine());

                Console.WriteLine($"Enter ingredient {i + 1} food group:");
                string foodGroup = Console.ReadLine();

                recipe.AddIngredient(ingredientName, quantity, unitOfMeasurement, calories, foodGroup);
            }

            Console.WriteLine("Enter number of steps:");
            int numSteps = int.Parse(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1} description:");
                string description = Console.ReadLine();
                recipe.AddStep(description);
            }

            recipeManager.AddRecipe(recipe);
            CheckRecipeCalorieLimit(recipe);
            Console.WriteLine($"Recipe '{name}' added successfully!");
        }
        //The RemoveRecipe method allows the user to remove a recipe from the Recipe Manager by entering its name
        private void RemoveRecipe()
        {
            Console.WriteLine("Enter recipe name to remove:");
            string name = Console.ReadLine();
            Recipe recipe = recipeManager.GetRecipeByName(name);

            if (recipe != null)
            {
                recipeManager.RemoveRecipe(recipe);
                Console.WriteLine($"Recipe '{name}' removed successfully!");
            }
            else
            {
                Console.WriteLine($"Recipe '{name}' Does not exist!");
            }
        }
        //this method displays a list of all the recipes' names
        private void DisplayRecipeList()
        {
            List<Recipe> recipeList = recipeManager.GetRecipeList();

            Console.WriteLine("Recipe list:");

            foreach (Recipe recipe in recipeList)
            {
                Console.WriteLine(recipe.Name);
            }
        }

        private void DisplayRecipeDetails()
        {
            Console.WriteLine("Enter recipe name to display details:");
            string name = Console.ReadLine();

            Recipe recipe = recipeManager.GetRecipeByName(name);

            if (recipe != null)
            {
                Console.WriteLine($"Recipe '{name}' details:");
                Console.WriteLine("Ingredients:");

                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}, {ingredient.Calories} calories, {ingredient.FoodGroup} food group");
                }

                Console.WriteLine("Steps:");

                foreach (Step step in recipe.Steps)
                {
                    Console.WriteLine($"- {step.Information}");
                }

                Console.WriteLine($"Total calories: {recipe.TotalCalories}");

                if (recipe.TotalCalories > 300)
                {
                    notificationDelegate(recipe);
                }
            }
            else
            {
                Console.WriteLine($"Recipe '{name}' Does not exist!");
            }
        }

        private void ClearAllData()
        {
            Console.WriteLine("Are you sure you want to clear all data? (Yes/No)");
            string answer = Console.ReadLine().ToUpper();

            if (answer == "YES")
            {
                recipeManager = new RecipeProcessor();
                Console.WriteLine("All data has been cleared.");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }
        //this notifies the user when a recipe exceeds 300 calories
        private void RecipeExceedsCalorieLimit(Recipe recipe)
        {
            Console.WriteLine("WARNING: Recipe exceeds 300 calories!");
        }
        //The method checks if a recipe exceeds 300 calories and triggers the notificationDelegate
        private void CheckRecipeCalorieLimit(Recipe recipe)
        {
            if (recipe.TotalCalories > 300)
            {
                notificationDelegate(recipe);
            }
        }
    }

    class Program
    {
        //The Run method starts the user interface loop, displaying a menu of options and executing the selected option./
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.Run();
        }
    }
}