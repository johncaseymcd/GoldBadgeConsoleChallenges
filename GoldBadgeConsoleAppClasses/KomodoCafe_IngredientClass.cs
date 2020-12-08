using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleChallengeClasses
{
    public enum IngredientCategory
    {
        Bread = 1,
        Cheese,
        Filling,
        Protein,
        Sauce,
        Side,
        Vegetable
    }
    public class Ingredient
    {
        public string IngredientName { get; set; }
        public decimal IngredientPrice { get; set; }
        public bool IsAddOn { get; set; }
        public bool IsDairyFree { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegan { get; set; }
        public int StockLevel { get; set; }
        public IngredientCategory IngredientType { get; set; }

        public Ingredient() { }
        public Ingredient(string ingredientName, decimal ingredientPrice, bool isAddOn, bool isDairyFree, bool isGlutenFree, bool isVegan, int stockLevel, IngredientCategory ingredientType)
        {
            IngredientName = ingredientName;
            IngredientPrice = ingredientPrice;
            IsAddOn = isAddOn;
            IsDairyFree = isDairyFree;
            IsGlutenFree = isGlutenFree;
            IsVegan = isVegan;
            StockLevel = stockLevel;
            IngredientType = ingredientType;
        }
    }

    public class IngredientCRUD
    {
        private List<Ingredient> _listOfIngredients = new List<Ingredient>();

        // Helper method to find an ingredient by name
        private Ingredient FindIngredientByName(string ingredientName)
        {
            foreach(var ingredient in _listOfIngredients)
            {
                if (ingredientName.ToLower() == ingredient.IngredientName.ToLower())
                {
                    return ingredient;
                }
            }

            return null;
        }

        // Add an ingredient to the field list of ingredients
        public void AddToIngredientsList(Ingredient newIngredient)
        {
            _listOfIngredients.Add(newIngredient);
        }
        
        // Return the current list of all ingredients
        public List<Ingredient> GetIngredientsList()
        {
            return _listOfIngredients;
        }

        // Edit an existing ingredient
        public bool EditIngredientInList(string oldIngredientToEdit, Ingredient newIngredient)
        {
            var editIngredient = FindIngredientByName(oldIngredientToEdit);

            if (editIngredient != null)
            {
                editIngredient.IngredientName = newIngredient.IngredientName;
                editIngredient.IngredientPrice = newIngredient.IngredientPrice;
                editIngredient.IsAddOn = newIngredient.IsAddOn;
                editIngredient.IsDairyFree = newIngredient.IsDairyFree;
                editIngredient.IsGlutenFree = newIngredient.IsGlutenFree;
                editIngredient.IsVegan = newIngredient.IsVegan;
                editIngredient.StockLevel = newIngredient.StockLevel;
                editIngredient.IngredientType = newIngredient.IngredientType;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete an existing ingredient
        public bool RemoveIngredientFromList(string oldIngredientToDelete)
        {
            var deleteIngredient = FindIngredientByName(oldIngredientToDelete);

            if (deleteIngredient == null)
            {
                return false;
            }

            int ingredientCount = _listOfIngredients.Count;
            _listOfIngredients.Remove(deleteIngredient);

            if (ingredientCount > _listOfIngredients.Count)
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
