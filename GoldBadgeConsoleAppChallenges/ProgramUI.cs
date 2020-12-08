using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleChallengeClasses;

namespace GoldBadgeConsoleChallengeConsole
{
    class ProgramUI
    {
        private List<Ingredient> _ingredientList = new List<Ingredient>();
        private List<MenuItem> _menuItemList = new List<MenuItem>();

        private IngredientCRUD ingredientManipulator = new IngredientCRUD();
        private MenuItemCRUD menuItemManipulator = new MenuItemCRUD();

        public void Run()
        {
            SeedMenu();
            MainMenu();
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
            Console.WriteLine("What type of ingredient are you adding?");
            int typeCounter = 0;
            foreach(var ingredient in allIngredients)
            {
                typeCounter++;
                Console.WriteLine($"{typeCounter}. {ingredient.IngredientType.ToString()}");
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

            return newIngredient;
        }

        // Create a new menu item
        private MenuItem CreateMenuItem()
        {
            var newMenuItem = new MenuItem();

        // Set menu item number
        SetItemNumber:
            Console.Clear();
            Console.WriteLine("What is the item number?");
            string inputItemNumber = Console.ReadLine();
            bool parseItemNumber = int.TryParse(inputItemNumber, out int itemNumber);
            foreach(var menuItem in _menuItemList)
            {
                if (itemNumber == menuItem.MealNumber)
                {
                    Console.WriteLine($"There is already a meal #{itemNumber}. Would you like to replace this meal (y/n)?");
                    string replace = Console.ReadLine();
                    if (replace == "y")
                    {
                        EditMenuItem(itemNumber);
                    }
                    else
                    {
                        goto SetItemNumber;
                    }
                }
            }
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
            if (parseIngredients)
            {
                for (int x = 0; x < howManyIngredients; x++)
                {
                    Console.WriteLine("What ingredient would you like to add to the meal?");
                    string ingredientName = Console.ReadLine().ToLower();
                    foreach(var ingredient in _ingredientList)
                    {
                        if(ingredientName == ingredient.IngredientName.ToLower())
                        {
                            newMenuItem.IngredientsList.Add(ingredient);
                        }
                    }
                }
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

            return newMenuItem;
        }

        // Display all ingredients
        private void ViewAllIngredients()
        {
            Console.Clear();
            var allIngredients = ingredientManipulator.GetIngredientsList();
            foreach(var ingredient in allIngredients)
            {
                Console.WriteLine($"• {ingredient.IngredientName}: {ingredient.StockLevel} currently in stock");
            }
        }

        // Display all menu items
        private void ViewAllMenuItems()
        {
            Console.Clear();
            var allMenuItems = menuItemManipulator.GetMenuItems();
            foreach(var item in allMenuItems)
            {
                Console.WriteLine($"#{item.MealNumber}. {item.MealName}     ${item.MealPrice}");
            }
        }

        // Edit an existing ingredient
        private void EditIngredient(string ingredientName)
        {
            Console.Clear();
            var allIngredients = ingredientManipulator.GetIngredientsList();
            foreach(var ingredient in allIngredients)
            {
                if (ingredientName.ToLower() == ingredient.IngredientName.ToLower())
                {
                    var newIngredient = CreateIngredient();
                    bool wasEdited = ingredientManipulator.EditIngredientInList(ingredientName, newIngredient);
                    if (wasEdited)
                    {
                        Console.WriteLine("Ingredient successfully updated!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Could not update ingredient. Please try again.");
                        Console.ReadKey();
                    }
                }
            }
        }

        // Edit an existing menu item
        private void EditMenuItem(int menuItemNumber)
        {
            Console.Clear();
            var allMenuItems = menuItemManipulator.GetMenuItems();
            foreach(var meal in allMenuItems)
                {
                    if (menuItemNumber == meal.MealNumber)
                    {
                        var newMenuItem = CreateMenuItem();
                        bool wasEdited = menuItemManipulator.EditMenuItem(meal.MealName, newMenuItem);
                        if (wasEdited)
                        {
                            Console.WriteLine("Menu item successfully updated!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Could not update menu item. Please try again.");
                            Console.ReadKey();
                        }
                    }
                }
            }

        // Delete an existing ingredient
        private void DeleteIngredient(string ingredientName)
        {
            Console.Clear();
            var allIngredients = ingredientManipulator.GetIngredientsList();
            foreach(var ingredient in allIngredients)
            {
                if (ingredient.IngredientName.ToLower() == ingredientName.ToLower())
                {
                    bool wasDeleted = ingredientManipulator.RemoveIngredientFromList(ingredientName);
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
            }
        }

        // Delete an existing menu item
        private void DeleteMenuItem(int menuItemNumber)
        {
            Console.Clear();
            var allMenuItems = menuItemManipulator.GetMenuItems();
            foreach(var item in allMenuItems)
            {
                if (menuItemNumber == item.MealNumber)
                {
                    bool wasDeleted = menuItemManipulator.RemoveFromMenu(menuItemNumber);
                    if (wasDeleted)
                    {
                        Console.WriteLine("Menu item was successfully deleted!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Could not delete menu item. Please try again.");
                        Console.ReadKey();
                    }
                }
            }
        }

        // Pre-populate the lists of ingredients and meals
        private void SeedMenu()
        {
            var avocado = new Ingredient("Avocado", 0.75m, true, true, true, true, 20, IngredientCategory.Vegetable);
            var lettuce = new Ingredient("Lettuce", 1.00m, false, true, true, true, 15, IngredientCategory.Vegetable);
            var tomato = new Ingredient("Sliced Tomato", 0.50m, false, true, true, true, 50, IngredientCategory.Vegetable);
            var mushroom = new Ingredient("Mushroom", 0.50m, false, true, true, true, 100, IngredientCategory.Vegetable);
            var onion = new Ingredient("Red Onion", 0.50m, false, true, true, true, 30, IngredientCategory.Vegetable);

            var bacon = new Ingredient("Bacon", 1.00m, true, true, true, false, 50, IngredientCategory.Protein);
            var chicken = new Ingredient("Chicken", 3.00m, false, true, true, false, 20, IngredientCategory.Protein);
            var roastBeef = new Ingredient("Roast Beef", 4.00m, false, true, true, false, 50, IngredientCategory.Protein);
            var shrimp = new Ingredient("Shrimp", 5.00m, false, true, true, false, 250, IngredientCategory.Protein);
            var salmon = new Ingredient("Salmon", 5.00m, false, true, true, false, 20, IngredientCategory.Protein);

            var gouda = new Ingredient("Smoked Gouda", 0.75m, true, false, true, false, 25, IngredientCategory.Cheese);
            var shreddedParmesan = new Ingredient("Shredded Parmesan Cheese", 2.00m, false, false, true, false, 10, IngredientCategory.Cheese);
            var shreddedPepperJack = new Ingredient("Shredded Pepper Jack", 1.50m, false, false, true, false, 10, IngredientCategory.Cheese);
            var swiss = new Ingredient("Swiss Cheese Slice", 0.50m, false, false, true, false, 40, IngredientCategory.Cheese);
            var cheddar = new Ingredient("Aged Sharp Cheddar", 0.50m, false, false, true, false, 50, IngredientCategory.Cheese);

            var frenchFries = new Ingredient("Side of French Fries", 1.50m, true, true, true, true, 100, IngredientCategory.Side);
            var softDrink = new Ingredient("Fountain Drink", 2.00m, true, true, true, true, 250, IngredientCategory.Side);
            var chips = new Ingredient("Bag of Chips", 1.50m, true, true, true, true, 100, IngredientCategory.Side);
            var coleslaw = new Ingredient("Coleslaw", 1.50m, true, false, true, false, 50, IngredientCategory.Side);
            var cookie = new Ingredient("Chocolate chip cookie", 2.00m, true, false, false, false, 25, IngredientCategory.Side);

            var crouton = new Ingredient("Croutons", 1.00m, false, true, false, true, 30, IngredientCategory.Bread);
            var tortilla = new Ingredient("Tortilla Wrap", 0.75m, false, true, false, true, 100, IngredientCategory.Bread);
            var panini = new Ingredient("Panini Bread", 1.50m, false, true, false, true, 40, IngredientCategory.Bread);
            var sourdough = new Ingredient("Sourdough", 2.00m, false, true, false, true, 30, IngredientCategory.Bread);
            var rye = new Ingredient("Rye Toast", 2.00m, false, true, false, true, 30, IngredientCategory.Bread);

            var caesarDressing = new Ingredient("Caesar Dressing", 2.00m, false, false, true, false, 5, IngredientCategory.Sauce);
            var chipotle = new Ingredient("Chipotle Sauce", 0.50m, false, true, true, true, 5, IngredientCategory.Sauce);
            var hotSauce = new Ingredient("Hot Sauce", 1.00m, true, true, true, true, 15, IngredientCategory.Sauce);
            var spicyMustard = new Ingredient("Spicy Mustard", 1.00m, false, true, true, true, 15, IngredientCategory.Sauce);
            var guacamole = new Ingredient("Guacamole", 2.50m, true, true, true, true, 20, IngredientCategory.Sauce);

            var beans = new Ingredient("Beans", 0.75m, false, true, true, true, 25, IngredientCategory.Filling);
            var rice = new Ingredient("White Rice", 0.75m, false, true, true, true, 10, IngredientCategory.Filling);
            var tofu = new Ingredient("Tofu", 1.00m, false, true, true, true, 50, IngredientCategory.Filling);
            var polenta = new Ingredient("Polenta", 2.00m, false, true, true, true, 20, IngredientCategory.Filling);
            var shirataki = new Ingredient("Shirataki", 3.00m, false, true, true, true, 15, IngredientCategory.Filling);
        }
    }
}
