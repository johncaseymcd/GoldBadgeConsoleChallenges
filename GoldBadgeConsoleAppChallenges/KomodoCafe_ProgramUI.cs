using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleChallengeClasses;

namespace GoldBadgeConsoleChallengeConsole
{
    class KomodoCafeProgramUI
    {
        IngredientCRUD ingredientManipulator = new IngredientCRUD();
        MenuItemCRUD menuItemManipulator = new MenuItemCRUD();

        List<Ingredient> _ingredientList = new List<Ingredient>();
        List<MenuItem> _menu = new List<MenuItem>();

        public void Run()
        {
            SeedMenu();

            MainMenu();
        }

        // Main program menu
        private void MainMenu()
        {
        MainMenu:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome! What would you like to do?\n\n" +
                "1. Create an ingredient or meal\n" +
                "2. View all ingredients or meals\n" +
                "3. Edit an ingredient or meal\n" +
                "4. Delete an ingredient or meal\n" +
                "5. Exit");
            string inputChoice = Console.ReadLine();
            bool parseChoice = int.TryParse(inputChoice, out int whatToDo);
            if (parseChoice)
            {
                switch (whatToDo)
                {
                    case 1: // Create a new ingredient or meal
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Press I to create an ingredient or press M to create a meal.");
                        string createWhich= Console.ReadLine().ToLower();
                        if (createWhich == "i")
                        {
                            var createdIngredient = CreateIngredient();
                            _ingredientList.Add(createdIngredient);
                            goto MainMenu;
                        }
                        else if (createWhich == "m")
                        {
                            var createdMeal = CreateMenuItem();
                            _menu.Add(createdMeal);
                            goto MainMenu;
                        }
                        else
                        {
                            PressAnyKey();
                            goto MainMenu;
                        }
                    case 2: // View list of all ingredients or meals
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press I to view all ingredients or press M to view all meals.");
                        string viewWhich = Console.ReadLine().ToLower();
                        if (viewWhich == "i")
                        {
                            ViewAllIngredients();
                            Console.ReadKey();
                            goto MainMenu;
                        }
                        else if (viewWhich == "m")
                        {
                            ViewAllMenuItems();
                            Console.ReadKey();
                            goto MainMenu;
                        }
                        else
                        {
                            PressAnyKey();
                            goto MainMenu;
                        }
                    case 3: // Edit an ingredient or meal
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Press I to edit an ingredient or press M to edit a meal.");
                        string editWhich = Console.ReadLine().ToLower();
                        if (editWhich == "i")
                        {
                            ViewAllIngredients();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter the name of the ingredient to edit:");
                            string whichIngredientToEdit = Console.ReadLine();
                            EditIngredient(whichIngredientToEdit);
                            goto MainMenu;
                        }
                        else if (editWhich == "m")
                        {
                            ViewAllMenuItems();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter the meal number to edit:");
                            string inputMealToEdit = Console.ReadLine();
                            bool parseMealToEdit = int.TryParse(inputMealToEdit, out int whichMealToEdit);
                            if (parseMealToEdit)
                            {
                                EditMenuItem(whichMealToEdit);
                                goto MainMenu;
                            }
                            else
                            {
                                PressAnyKey();
                                goto MainMenu;
                            }
                        }
                        else
                        {
                            PressAnyKey();
                            goto MainMenu;
                        }
                    case 4: // Delete an ingredient or meal
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Press I to delete an ingredient or press M to delete a meal.");
                        string deleteWhich = Console.ReadLine().ToLower();
                        if (deleteWhich == "i")
                        {
                            ViewAllIngredients();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Enter the name of the ingredient to delete:");
                            string whichIngredientToDelete = Console.ReadLine();
                            DeleteIngredient(whichIngredientToDelete);
                            goto MainMenu;
                        }
                        else if (deleteWhich == "m")
                        {
                            ViewAllMenuItems();
                            Console.WriteLine("Enter the meal number to delete:");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            string inputMealToDelete = Console.ReadLine();
                            bool parseMealToDelete = int.TryParse(inputMealToDelete, out int whichMealToDelete);
                            if (parseMealToDelete)
                            {
                                DeleteMenuItem(whichMealToDelete);
                                goto MainMenu;
                            }
                            else
                            {
                                PressAnyKey();
                                goto MainMenu;
                            }
                        }
                        else
                        {
                            PressAnyKey();
                            goto MainMenu;
                        }
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Press any key to confirm exit or press H to return to the home menu.");
                        var toExit = Console.ReadKey().Key;
                        if (toExit == ConsoleKey.H)
                        {
                            goto MainMenu;
                        }
                        else
                        {
                            break;
                        }
                    default:
                        PressAnyKey();
                        goto MainMenu;
                }
            }
            else
            {
                PressAnyKey();
                goto MainMenu;
            }
        }

        // Helper method to handle errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.ReadKey();
        }

        // Create a new ingredient
        private Ingredient CreateIngredient()
        {
            var newIngredient = new Ingredient();
            var allIngredients = ingredientManipulator.GetIngredientsList();

        // Set type of ingredient from enum
        SetIngredientType:
            Console.Clear();
            Console.WriteLine("What type of ingredient are you adding?");
            int typeCounter = 0;
            foreach(var ingredientType in Enum.GetValues(typeof(IngredientCategory)))
            {
                typeCounter++;
                Console.WriteLine($"{typeCounter}. {ingredientType}");
            }
            string inputType = Console.ReadLine();
            bool parseType = int.TryParse(inputType, out int whichType);
            if (parseType)
            {
                switch (whichType)
                {
                    case 1:
                        newIngredient.IngredientType = IngredientCategory.Bread;
                        goto SetIngredientName;
                    case 2:
                        newIngredient.IngredientType = IngredientCategory.Cheese;
                        goto SetIngredientName;
                    case 3:
                        newIngredient.IngredientType = IngredientCategory.Filling;
                        goto SetIngredientName;
                    case 4:
                        newIngredient.IngredientType = IngredientCategory.Protein;
                        goto SetIngredientName;
                    case 5:
                        newIngredient.IngredientType = IngredientCategory.Sauce;
                        goto SetIngredientName;
                    case 6:
                        newIngredient.IngredientType = IngredientCategory.Side;
                        goto SetIngredientName;
                    case 7:
                        newIngredient.IngredientType = IngredientCategory.Vegetable;
                        goto SetIngredientName;
                    default:
                        PressAnyKey();
                        goto SetIngredientType;
                }
            }
            else
            {
                PressAnyKey();
                goto SetIngredientType;
            }

        // Set ingredient name
        SetIngredientName:
            Console.Clear();
            Console.WriteLine("Enter the name of the ingredient:");
            string ingredientName = Console.ReadLine();
            foreach(var ingredient in allIngredients)
            {
                if (ingredientName.ToLower() == ingredient.IngredientName.ToLower())
                {
                    Console.WriteLine("Ingredient with that name already exists. Please edit that entry or enter a new name.");
                    Console.ReadKey();
                    goto SetIngredientName;
                }
            }
            newIngredient.IngredientName = ingredientName;

        // Set ingredient price
        SetPrice:
            Console.Clear();
            Console.WriteLine($"How much does {newIngredient.IngredientName} cost?");
            string inputPrice = Console.ReadLine();
            bool parsePrice = decimal.TryParse(inputPrice, out decimal ingredientPrice);
            if (parsePrice)
            {
                var roundedPrice = decimal.Round(ingredientPrice, 2);
                newIngredient.IngredientPrice = roundedPrice;
            }
            else
            {
                PressAnyKey();
                goto SetPrice;
            }

        // Determine whether the ingredient is an add-on with an upcharge at checkout
        SetAddOn:
            Console.Clear();
            Console.WriteLine($"Is {newIngredient.IngredientName} an add-on (y/n)?");
            string addOn = Console.ReadLine().ToLower();
            if (addOn == "y")
            {
                newIngredient.IsAddOn = true;
            }
            else if (addOn == "n")
            {
                newIngredient.IsAddOn = false;
            }
            else
            {
                PressAnyKey();
                goto SetAddOn;
            }

        // Determine if the ingredient is dairy-free
        SetDairyFree:
            Console.Clear();
            Console.WriteLine($"Is {newIngredient.IngredientName} dairy-free (y/n)?");
            string dairyFree = Console.ReadLine().ToLower();
            if (dairyFree == "y")
            {
                newIngredient.IsDairyFree = true;
            }
            else if (dairyFree == "n")
            {
                newIngredient.IsDairyFree = false;
            }
            else
            {
                PressAnyKey();
                goto SetDairyFree;
            }

        // Determine if the ingredient is gluten-free
        SetGlutenFree:
            Console.Clear();
            Console.WriteLine($"Is {newIngredient.IngredientName} gluten-free (y/n)?");
            string glutenFree = Console.ReadLine().ToLower();
            if (glutenFree == "y")
            {
                newIngredient.IsGlutenFree = true;
            }
            else if (glutenFree == "n")
            {
                newIngredient.IsGlutenFree = false;
            }
            else
            {
                PressAnyKey();
                goto SetGlutenFree;
            }

        // Determine if the ingredient is vegan
        SetVegan:
            Console.Clear();
            Console.WriteLine($"Is {newIngredient.IngredientName} vegan (y/n)?");
            string vegan = Console.ReadLine().ToLower();
            if (vegan == "y")
            {
                newIngredient.IsVegan = true;
            }
            else if (vegan == "n" || !newIngredient.IsDairyFree) // If it isn't dairy free, it isn't vegan!
            {
                newIngredient.IsVegan = false;
            }
            else
            {
                PressAnyKey();
                goto SetVegan;
            }

        // Set the stock level
        SetStock:
            Console.Clear();
            Console.WriteLine($"How much stock of {newIngredient.IngredientName} do we currently have?");
            string stock = Console.ReadLine();
            bool parseStock = int.TryParse(stock, out int howMuchStock);
            if (parseStock)
            {
                newIngredient.StockLevel = howMuchStock;
            }
            else
            {
                PressAnyKey();
                goto SetStock;
            }

            ingredientManipulator.AddToIngredientsList(newIngredient);
            return newIngredient;
        }

        // Create a new menu item
        private MenuItem CreateMenuItem()
        {
            var newMenuItem = new MenuItem();

        // Set menu item number
        SetItemNumber:
            Console.Clear();
            Console.WriteLine("What is the meal number?");
            string inputItemNumber = Console.ReadLine();
            bool parseItemNumber = int.TryParse(inputItemNumber, out int itemNumber);
            if (parseItemNumber)
            {
                newMenuItem.MealNumber = itemNumber;
            }
            else
            {
                PressAnyKey();
                goto SetItemNumber;
            }

            // Set menu item name
            Console.Clear();
            Console.WriteLine("What is this meal called?");
            newMenuItem.MealName = Console.ReadLine();

            // Set menu item description
            Console.Clear();
            Console.WriteLine("Please enter a brief description of this meal.");
            newMenuItem.MealDescription = Console.ReadLine();

        // Add ingredients to menu item
        AddIngredients:
            Console.Clear();
            Console.WriteLine("How many ingredients are in this meal?");
            string inputIngredients = Console.ReadLine();
            bool parseIngredients = int.TryParse(inputIngredients, out int howManyIngredients);
            var newMenuItemIngredients = new List<Ingredient>();
            if (parseIngredients)
            {
                for (int x = 0; x < howManyIngredients; x++)
                {
                EnterIngredientName:
                    Console.Clear();
                    ViewAllIngredients();
                    Console.WriteLine("What ingredient would you like to add to the meal?");
                    string ingredientName = Console.ReadLine().ToLower();
                    var ingredientToAdd = ingredientManipulator.FindIngredientByName(ingredientName);
                    if (ingredientToAdd != null)
                    {
                        newMenuItemIngredients.Add(ingredientToAdd);
                    }
                    else
                    {
                        Console.WriteLine("Ingredient not found. Please try again.");
                        goto EnterIngredientName;
                    }
                    
                }
                newMenuItem.IngredientsList = newMenuItemIngredients;
            }
            else
            {
                PressAnyKey();
                goto AddIngredients;
            }

        // Set meal price
        SetPrice:
            Console.Clear();
            decimal suggestedPrice = 0.00m;
            foreach (var ingredient in newMenuItem.IngredientsList)
            {
                suggestedPrice += decimal.Round(ingredient.IngredientPrice, 2);
            }
            Console.WriteLine($"The suggested price for this meal is ${suggestedPrice}. Would you like to edit this price (y/n)?");
            string toEdit = Console.ReadLine().ToLower();
            if (toEdit == "y")
            {
            EditSuggestedPrice:
                Console.WriteLine("Enter the price for this meal:");
                string inputPrice = Console.ReadLine();
                bool parsePrice = decimal.TryParse(inputPrice, out decimal mealPrice);
                if (parsePrice)
                {
                    newMenuItem.MealPrice = decimal.Round(mealPrice, 2);
                }
                else
                {
                    PressAnyKey();
                    goto EditSuggestedPrice;
                }
            }
            else if (toEdit == "n")
            {
                newMenuItem.MealPrice = suggestedPrice;
            }
            else
            {
                PressAnyKey();
                goto SetPrice;
            }

            menuItemManipulator.AddToMenu(newMenuItem);
            return newMenuItem;
        }

        // Display all ingredients
        private void ViewAllIngredients()
        {
            Console.Clear();
            var allIngredients = ingredientManipulator.GetIngredientsList();
            foreach(var ingredient in allIngredients)
            {
                if(ingredient.IngredientType == IngredientCategory.Bread)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
                else if (ingredient.IngredientType == IngredientCategory.Cheese)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
                else if (ingredient.IngredientType == IngredientCategory.Filling)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
                else if (ingredient.IngredientType == IngredientCategory.Protein)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
                else if (ingredient.IngredientType == IngredientCategory.Sauce)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
                else if (ingredient.IngredientType == IngredientCategory.Side)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
                else if (ingredient.IngredientType == IngredientCategory.Vegetable)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"* {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
                }
            }
        }

        // Display all menu items
        private void ViewAllMenuItems()
        {
            Console.Clear();
            var allMenuItems = menuItemManipulator.GetMenuItems();
            foreach(var item in allMenuItems)
            {
                Console.WriteLine($"#{item.MealNumber}. {item.MealName} - {item.MealDescription}\n" +
                    $"    ${item.MealPrice}\n");
            }
        }

        // Edit an existing ingredient
        private void EditIngredient(string ingredientName)
        {
            Console.Clear();
            var ingredientToEdit = ingredientManipulator.FindIngredientByName(ingredientName);
            if (ingredientToEdit != null)
            {
                var newIngredient = CreateIngredient();
                bool wasEdited = ingredientManipulator.EditIngredientInList(ingredientName, newIngredient);
                if (wasEdited)
                {
                    ingredientManipulator.RemoveIngredientFromList(newIngredient.IngredientName);
                    Console.WriteLine("Ingredient successfully edited!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Could not edit ingredient. Please try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Ingredient not found. Please try again.");
                Console.ReadKey();
            }
        }

        // Edit an existing menu item
        private void EditMenuItem(int menuItemNumber)
        {
            Console.Clear();
            var mealToEdit = menuItemManipulator.FindMenuItemByNumber(menuItemNumber);
            if (mealToEdit != null)
            {
                var newMeal = CreateMenuItem();
                bool wasEdited = menuItemManipulator.EditMenuItem(menuItemNumber, newMeal);
                if (wasEdited)
                {
                    menuItemManipulator.RemoveFromMenu(newMeal.MealNumber);
                    Console.WriteLine("Meal successfully edited!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Could not edit meal. Please try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Meal not found. Please try again.");
                Console.ReadKey();
            }
        }

        // Delete an existing ingredient
        private void DeleteIngredient(string ingredientName)
        {
            Console.Clear();
            var ingredientToDelete = ingredientManipulator.FindIngredientByName(ingredientName);
            if (ingredientToDelete != null)
            {
                bool wasDeleted = ingredientManipulator.RemoveIngredientFromList(ingredientToDelete.IngredientName);
                if (wasDeleted)
                {
                    Console.WriteLine("Ingredient was successfully deleted!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Could not delete ingredient. Please try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Ingredient not found. Please try again.");
                Console.ReadKey();
            }
        }

        // Delete an existing menu item
        private void DeleteMenuItem(int menuItemNumber)
        {
            Console.Clear();
            var mealToDelete = menuItemManipulator.FindMenuItemByNumber(menuItemNumber);
            if (mealToDelete != null)
            {
                bool wasDeleted = menuItemManipulator.RemoveFromMenu(mealToDelete.MealNumber);
                if (wasDeleted)
                {
                    Console.WriteLine("Meal was successfully deleted!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Could not delete meal. Please try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Meal not found. Please try again.");
                Console.ReadKey();
            }
        }

        // Pre-populate the lists of ingredients and meals
        private void SeedMenu()
        {
            // Create and add bread options to ingredient list
            var crouton = new Ingredient("Croutons", 1.00m, false, true, false, true, 30, IngredientCategory.Bread);
            ingredientManipulator.AddToIngredientsList(crouton);
            var tortilla = new Ingredient("Tortilla Wrap", 0.75m, false, true, false, true, 100, IngredientCategory.Bread);
            ingredientManipulator.AddToIngredientsList(tortilla);
            var panini = new Ingredient("Panini Bread", 1.50m, false, true, false, true, 40, IngredientCategory.Bread);
            ingredientManipulator.AddToIngredientsList(panini);
            var sourdough = new Ingredient("Sourdough", 2.00m, false, true, false, true, 30, IngredientCategory.Bread);
            ingredientManipulator.AddToIngredientsList(sourdough);
            var rye = new Ingredient("Rye Toast", 2.00m, false, true, false, true, 30, IngredientCategory.Bread);
            ingredientManipulator.AddToIngredientsList(rye);

            // Create and add cheese options to ingredient list
            var gouda = new Ingredient("Smoked Gouda", 0.75m, true, false, true, false, 25, IngredientCategory.Cheese);
            ingredientManipulator.AddToIngredientsList(gouda);
            var shreddedParmesan = new Ingredient("Shredded Parmesan Cheese", 2.00m, false, false, true, false, 10, IngredientCategory.Cheese);
            ingredientManipulator.AddToIngredientsList(shreddedParmesan);
            var shreddedPepperJack = new Ingredient("Shredded Pepper Jack", 1.50m, false, false, true, false, 10, IngredientCategory.Cheese);
            ingredientManipulator.AddToIngredientsList(shreddedPepperJack);
            var swiss = new Ingredient("Swiss Cheese Slice", 0.50m, false, false, true, false, 40, IngredientCategory.Cheese);
            ingredientManipulator.AddToIngredientsList(swiss);
            var cheddar = new Ingredient("Aged Sharp Cheddar", 0.50m, false, false, true, false, 50, IngredientCategory.Cheese);
            ingredientManipulator.AddToIngredientsList(cheddar);

            // Create and add filling options to ingredient list
            var beans = new Ingredient("Beans", 0.75m, false, true, true, true, 25, IngredientCategory.Filling);
            ingredientManipulator.AddToIngredientsList(beans);
            var rice = new Ingredient("White Rice", 0.75m, false, true, true, true, 10, IngredientCategory.Filling);
            ingredientManipulator.AddToIngredientsList(rice);
            var tofu = new Ingredient("Tofu", 1.00m, false, true, true, true, 50, IngredientCategory.Filling);
            ingredientManipulator.AddToIngredientsList(tofu);
            var polenta = new Ingredient("Polenta", 2.00m, false, true, true, true, 20, IngredientCategory.Filling);
            ingredientManipulator.AddToIngredientsList(polenta);
            var shirataki = new Ingredient("Shirataki", 3.00m, false, true, true, true, 15, IngredientCategory.Filling);
            ingredientManipulator.AddToIngredientsList(shirataki);

            // Create and add protein options to ingredient list
            var bacon = new Ingredient("Bacon", 1.00m, true, true, true, false, 50, IngredientCategory.Protein);
            ingredientManipulator.AddToIngredientsList(bacon);
            var chicken = new Ingredient("Chicken", 3.00m, false, true, true, false, 20, IngredientCategory.Protein);
            ingredientManipulator.AddToIngredientsList(chicken);
            var roastBeef = new Ingredient("Roast Beef", 4.00m, false, true, true, false, 50, IngredientCategory.Protein);
            ingredientManipulator.AddToIngredientsList(roastBeef);
            var shrimp = new Ingredient("Shrimp", 5.00m, false, true, true, false, 250, IngredientCategory.Protein);
            ingredientManipulator.AddToIngredientsList(shrimp);
            var salmon = new Ingredient("Salmon", 5.00m, false, true, true, false, 20, IngredientCategory.Protein);
            ingredientManipulator.AddToIngredientsList(salmon);

            // Create and add sauce options to ingredient list
            var caesarDressing = new Ingredient("Caesar Dressing", 2.00m, false, false, true, false, 5, IngredientCategory.Sauce);
            ingredientManipulator.AddToIngredientsList(caesarDressing);
            var chipotle = new Ingredient("Chipotle Sauce", 0.50m, false, true, true, true, 5, IngredientCategory.Sauce);
            ingredientManipulator.AddToIngredientsList(chipotle);
            var hotSauce = new Ingredient("Hot Sauce", 1.00m, true, true, true, true, 15, IngredientCategory.Sauce);
            ingredientManipulator.AddToIngredientsList(hotSauce);
            var spicyMustard = new Ingredient("Spicy Mustard", 1.00m, false, true, true, true, 15, IngredientCategory.Sauce);
            ingredientManipulator.AddToIngredientsList(spicyMustard);
            var guacamole = new Ingredient("Guacamole", 2.50m, true, true, true, true, 20, IngredientCategory.Sauce);
            ingredientManipulator.AddToIngredientsList(guacamole);

            // Create and add side options to ingredient list
            var frenchFries = new Ingredient("Side of French Fries", 1.50m, true, true, true, true, 100, IngredientCategory.Side);
            ingredientManipulator.AddToIngredientsList(frenchFries);
            var softDrink = new Ingredient("Fountain Drink", 2.00m, true, true, true, true, 250, IngredientCategory.Side);
            ingredientManipulator.AddToIngredientsList(softDrink);
            var chips = new Ingredient("Bag of Chips", 1.50m, true, true, true, true, 100, IngredientCategory.Side);
            ingredientManipulator.AddToIngredientsList(chips);
            var coleslaw = new Ingredient("Coleslaw", 1.50m, true, false, true, false, 50, IngredientCategory.Side);
            ingredientManipulator.AddToIngredientsList(coleslaw);
            var cookie = new Ingredient("Chocolate chip cookie", 2.00m, true, false, false, false, 25, IngredientCategory.Side);
            ingredientManipulator.AddToIngredientsList(cookie);

            // Create and add vegetable options to ingredient list
            var avocado = new Ingredient("Avocado", 0.75m, true, true, true, true, 20, IngredientCategory.Vegetable);
            ingredientManipulator.AddToIngredientsList(avocado);
            var lettuce = new Ingredient("Lettuce", 1.00m, false, true, true, true, 15, IngredientCategory.Vegetable);
            ingredientManipulator.AddToIngredientsList(lettuce);
            var tomato = new Ingredient("Sliced Tomato", 0.50m, false, true, true, true, 50, IngredientCategory.Vegetable);
            ingredientManipulator.AddToIngredientsList(tomato);
            var mushroom = new Ingredient("Mushroom", 0.50m, false, true, true, true, 100, IngredientCategory.Vegetable);
            ingredientManipulator.AddToIngredientsList(mushroom);
            var onion = new Ingredient("Red Onion", 0.50m, false, true, true, true, 30, IngredientCategory.Vegetable);
            ingredientManipulator.AddToIngredientsList(onion);

            // Create and add a BLT to the menu
            var bltIngredients = new List<Ingredient>()
            {
                rye,
                swiss,
                bacon,
                spicyMustard,
                chips,
                softDrink,
                lettuce,
                tomato
            };
            decimal bltPrice = 0;
            foreach(var ingredient in bltIngredients)
            {
                bltPrice += decimal.Round(ingredient.IngredientPrice, 2);
            }
            var blt = new MenuItem(1, "BLT", "A classic BLT on rye with Swiss cheese and spicy mustard. Comes with chips and a drink", bltIngredients, bltPrice);
            menuItemManipulator.AddToMenu(blt);

            // Create and add a Bean & Rice Burrito to the menu
            var beanAndRiceBurritoIngredients = new List<Ingredient>()
            {
                tortilla, 
                shreddedPepperJack,
                beans,
                rice,
                chicken,
                chipotle
            };
            decimal beanAndRiceBurritoPrice = 0;
            foreach(var ingredient in beanAndRiceBurritoIngredients)
            {
                beanAndRiceBurritoPrice += decimal.Round(ingredient.IngredientPrice, 2);
            }
            var beanAndRiceBurrito = new MenuItem(2, "Bean & Rice Burrito", "Chicken on a bed of beans and rice, wrapped in a flour tortilla with shredded pepper jack cheese and our housemade chipotle sauce. Add guacamole for $1.00 more.", beanAndRiceBurritoIngredients, beanAndRiceBurritoPrice);
            menuItemManipulator.AddToMenu(beanAndRiceBurrito);

            // Create and add Shrimp Fried Rice to the menu
            var shrimpFriedRiceIngredients = new List<Ingredient>() 
            {
                rice,
                tofu,
                shrimp,
                tomato,
                onion
            };
            decimal shrimpFriedRicePrice = 0;
            foreach(var ingredient in shrimpFriedRiceIngredients)
            {
                shrimpFriedRicePrice += decimal.Round(ingredient.IngredientPrice, 2);
            }
            var shrimpFriedRice = new MenuItem(3, "Shrimp Fried Rice", "Asian-inspired fried rice with shrimp, tofu, and veggies.", shrimpFriedRiceIngredients, shrimpFriedRicePrice);
            menuItemManipulator.AddToMenu(shrimpFriedRice);

            // Create and add Grilled Cheese to the menu
            var grilledCheeseIngredients = new List<Ingredient>()
            {
                panini,
                gouda,
                roastBeef,
                spicyMustard,
                frenchFries,
                softDrink
            };
            decimal grilledCheesePrice = 0;
            foreach(var ingredient in grilledCheeseIngredients)
            {
                grilledCheesePrice += decimal.Round(ingredient.IngredientPrice, 2);
            }
            var grilledCheese = new MenuItem(4, "Grilled Cheese", "Grilled cheese, but fancy. Roast beef and smoked gouda, grilled to melty perfection on a panini. Comes with fries and a soft drink.", grilledCheeseIngredients, grilledCheesePrice);
            menuItemManipulator.AddToMenu(grilledCheese);

            // Create and add a Caesar Salad to the menu
            var caesarSaladIngredients = new List<Ingredient>()
            {
                crouton,
                shreddedParmesan,
                chicken,
                caesarDressing,
                lettuce, 
                onion
            };
            decimal caesarSaladPrice = 0;
            foreach(var ingredient in caesarSaladIngredients)
            {
                caesarSaladPrice += decimal.Round(ingredient.IngredientPrice, 2);
            }
            var caesarSalad = new MenuItem(5, "Caesar Salad", "A traditional Caesar salad made with locally sourced produce and housemade dressing.", caesarSaladIngredients, caesarSaladPrice);
            menuItemManipulator.AddToMenu(caesarSalad);
        }
    }
}
