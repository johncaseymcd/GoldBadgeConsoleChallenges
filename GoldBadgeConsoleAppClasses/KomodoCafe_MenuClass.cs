using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleChallengeClasses
{
    public class MenuItem
    {
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public List<Ingredient> IngredientsList { get; set; }
        public decimal MealPrice { get; set; }

        public MenuItem() { }
        public MenuItem(int mealNumber, string mealName, string mealDescription, List<Ingredient> ingredientsList, decimal mealPrice)
        {
            MealNumber = mealNumber;
            MealName = mealName;
            MealDescription = mealDescription;
            IngredientsList = ingredientsList;
            MealPrice = mealPrice;
        }
    }

    public class MenuItemCRUD
    {
        private List<MenuItem> _menu= new List<MenuItem>();

        // Helper method to find a menu item by number
        public MenuItem FindMenuItemByNumber(int menuItemNumber)
        {
            foreach(var item in _menu)
            {
                if (menuItemNumber == item.MealNumber)
                {
                    return item;
                }
            }

            return null;
        }

        // Add a menu item to the menu
        public void AddToMenu(MenuItem newMenuItem)
        {
            _menu.Add(newMenuItem);
        }

        // Return a list of all menu items
        public List<MenuItem> GetMenuItems()
        {
            return _menu;
        }

        // Edit an existing menu item
        public bool EditMenuItem(int editItemNumber, MenuItem newMenuItem)
        {
            var editMenuItem = FindMenuItemByNumber(editItemNumber);

            if (editMenuItem != null)
            {
                editMenuItem.MealNumber = newMenuItem.MealNumber;
                editMenuItem.MealName = newMenuItem.MealName;
                editMenuItem.MealDescription = newMenuItem.MealDescription;
                editMenuItem.IngredientsList = newMenuItem.IngredientsList;
                editMenuItem.MealPrice = newMenuItem.MealPrice;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete an existing menu item
        public bool RemoveFromMenu(int mealNumber)
        {
            var deleteMenuItem = FindMenuItemByNumber(mealNumber);

            if (deleteMenuItem == null)
            {
                return false;
            }

            int menuItemCount = _menu.Count;
            _menu.Remove(deleteMenuItem);

            if (menuItemCount > _menu.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
