namespace recipeApp
{
    class recipe
    {
        string[] nameOfIngredient;
        double[] quantity;
        string[] unitOfMeasurement;
        string[] steps;
        double[] initialQuantity;  // add this field to store the original quantity
        public string name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        private static List<recipe> recs = new List<recipe>();

        public recipe()
        {
            //Initializing empty arrays fpr ingredients,quantities,units as well as steps
            nameOfIngredient = new string[0];
            quantity = new double[0];
            unitOfMeasurement = new string[0];
            steps = new string[0];
            Ingredients = new List<Ingredient>();
        }
        public void userInput()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            //Prompting the user to enter the number of ingredients 
            Console.WriteLine("How many ingredients are you going to use?");
            int numOfIngredients = Convert.ToInt32(Console.ReadLine());

            //Initializing the arrays with the correct size
            nameOfIngredient = new string[numOfIngredients];
            quantity = new double[numOfIngredients];
            unitOfMeasurement = new string[numOfIngredients];
            steps = new string[numOfIngredients];

            for (int i = 0; i < numOfIngredients; i++)
            {
                //Prompting user to enter the details for each ingredient
                Console.WriteLine($"Enter the name,quantity and the unit of measurement of your ingredients #{i + 1}:");

                Console.WriteLine("Name of ingredient:");
                nameOfIngredient[i] = Console.ReadLine();

                Console.WriteLine("Quantity of the ingredient:");
                quantity[i] = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Unit of Measurement:");
                unitOfMeasurement[i] = Console.ReadLine();
            }


            //Prompting user to enter the number of steps
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("How many steps are there going to be in your recipe?");
            int numOfSteps = Convert.ToInt32(Console.ReadLine());

            //Initializing the steps array with the correct size
            steps = new string[numOfSteps];

            for (int i = 0; i < numOfSteps; i++)
            {
                //Prompting the user to enter the details for each step
                Console.WriteLine($"Write a description of what the user should do ( Step number_{i + 1}):");
                steps[i] = Console.ReadLine();
            }

            // store the original quantity
            initialQuantity = new double[numOfIngredients];
            Array.Copy(quantity, initialQuantity, numOfIngredients);
        }
        public void recipeOutput()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            //Outputs the details of the ingredients
            Console.WriteLine("*************************");
            Console.WriteLine("Recipe Ingredients:");
            Console.WriteLine("*************************");
            for (int i = 0; i < nameOfIngredient.Length; i++)
            {
                Console.WriteLine($"{quantity[i]} {unitOfMeasurement[i]} of {nameOfIngredient[i]}");
            }

            //Outputs the steps
            Console.WriteLine("*************************");
            Console.WriteLine("Recipe Steps:");
            Console.WriteLine("*************************");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}.{steps[i]}");
            }

        }
        public void scaling(double factor)
        {
            //Multiplying all the quantities by the scaling factor
            for (int i = 0; i < quantity.Length; i++)
            {
                quantity[i] = initialQuantity[i] * factor;
            }
        }
        public void resetQuantities()
        {
            //Resets all the new quatities to their original values
            for (int i = 0; i < quantity.Length; i++)
            {
                quantity[i] = initialQuantity[i];
            }
        }
        public void clearData()
        {
            //Resets all the arrays to empty
            nameOfIngredient = new string[0];
            quantity = new double[0];
            unitOfMeasurement = new string[0];
            steps = new string[0];
        }
        public void RecipeManager()
        {
            Console.WriteLine("Welcome to the recipe manager!");
            while (true)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Add a recipe");
                Console.WriteLine("2. View recipe list");
                Console.WriteLine("3. Exit");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                        AddRecipe();
                        break;

                    case "2":
                        ViewRecipeList();
                        break;

                    case "3":
                        Console.WriteLine("Exiting the recipe manager.Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option.Please try again");
                        break;
                }
            }


        }
        public class Ingredient
        {
            public string name { get; set; }
            public int calories { get; set; }
            public string foodGroup { get; set; }
        }
        private static void AddRecipe()
        {
            Console.WriteLine("\nAdding a new recipe...");

            recipe rec = new recipe();

            Console.WriteLine("Enter the recipe name:");
            rec.name = Console.ReadLine();

            while (true)
            {
                Ingredient ingredient = new Ingredient();

                Console.WriteLine("Enter ingredient name(or 'done' to finish):");
                string ingredientName = Console.ReadLine();

                if (ingredientName.ToLower() == "done")
                    break;

                ingredient.name = ingredientName;

                Console.WriteLine("Enter ingredient calories:");
                ingredient.calories = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter ingredient food group");
                ingredient.foodGroup = Console.ReadLine();

                rec.Ingredients.Add(ingredient);
            }
            recs.Add(rec);
            Console.WriteLine("Recipe added successfully");
        }
        private static void ViewRecipeList()
        {
            Console.WriteLine("\nRecipe List:");
            if (recs.Count == 0)
            {
                Console.WriteLine("No recipe found.");
                return;
            }
            List<recipe> sortedRecipe = recs.OrderBy(r => r.name).ToList();
            foreach (recipe rec in sortedRecipe)
            {
                Console.WriteLine("-" + rec.name);
            }
            Console.WriteLine("Enter the name of the recipe to view:");
            string recipeName = Console.ReadLine();

            recipe selectedRecipe = recs.FirstOrDefault(r => r.name == recipeName);
            if (selectedRecipe != null)
            {
                DisplayRecipe(selectedRecipe);
            }
            else
            {
                Console.WriteLine("Recipe not found");
            }
        }
        private static void DisplayRecipe(recipe rec)
        {
            Console.WriteLine("\nRecipe:" + rec.name);
            Console.WriteLine("Ingredients:");

            foreach (Ingredient ingredient in rec.Ingredients)
            {
                Console.WriteLine("-" + ingredient.name + "(" + ingredient.calories + "calories," + ingredient.foodGroup + ")");
            }
            int totalCalories = rec.Ingredients.Sum(i => i.calories);
            Console.WriteLine("Total Calcories:" + totalCalories);

            if (totalCalories > 300)
            {
                Console.WriteLine("This recipe exceeds 300 calories");
            }

        }


        class Execute
        {
            public static void Main(string[] args)
            {

                recipe rec = new recipe();


                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("How to use this application:");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("To input the recipe details, enter '1'");
                    Console.WriteLine("To output the full recipe, enter '2'");
                    Console.WriteLine("To scale the recipe, enter '3'");
                    Console.WriteLine("To reset the quantity of the ingredients, enter '4'");
                    Console.WriteLine("To clear all the data of the recipe, enter '5'");
                    Console.WriteLine("To open the recipe manager, enter '6'");
                    Console.WriteLine("To exit, enter '7'");
                    Console.WriteLine("--------------------------------------------------------------");

                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            rec.userInput();
                            break;

                        case "2":
                            rec.recipeOutput();
                            break;

                        case "3":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Enter a scaling factor: 0,5 or 2 or 3");
                            double factor = Convert.ToDouble(Console.ReadLine());
                            rec.scaling(factor);
                            break;

                        case "4":
                            rec.resetQuantities();
                            break;

                        case "5":
                            rec.clearData();
                            break;

                        case "6":
                            rec.RecipeManager();
                            break;


                        case "7":
                            Console.WriteLine("Closing Application...");
                            return;
                        default:
                            Console.WriteLine("Invaild option. Enter the correct option.");
                            break;

                    }



                }
            }
        }
    }
}