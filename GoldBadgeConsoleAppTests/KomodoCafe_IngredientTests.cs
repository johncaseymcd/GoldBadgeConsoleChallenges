using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldBadgeConsoleChallengeClasses;
using System.Collections.Generic;

namespace GoldBadgeConsoleChallengeTests
{
    [TestClass]
    public class IngredientTest
    {
        Ingredient avocado = new Ingredient("Avocado", 0.75m, true, true, true, true, 12, IngredientCategory.Vegetable);
        IngredientCRUD ingredientTester = new IngredientCRUD();

        [TestMethod]
        public void IngredientAddTest()
        {
            // Arrange
            int beginningCount = ingredientTester.GetIngredientsList().Count;

            // Act
            ingredientTester.AddToIngredientsList(avocado);

            // Assert
            int endingCount = ingredientTester.GetIngredientsList().Count;
            Assert.IsTrue(endingCount > beginningCount, "Add was not successful.");
        }

        [TestMethod]
        public void IngredientReadTest()
        {
            // No arrangement necessary

            // Act
            ingredientTester.AddToIngredientsList(avocado);

            // Assert
            Assert.IsNotNull(ingredientTester.GetIngredientsList(), "Read was not successful.");
        }

        [TestMethod]
        public void IngredientEditTest()
        {
            // Arrange
            var chicken = new Ingredient("Chicken", 4.00m, false, true, true, false, 20, IngredientCategory.Protein);

            // Act
            ingredientTester.AddToIngredientsList(avocado);
            bool wasEdited = ingredientTester.EditIngredientInList("Avocado", chicken);

            // Assert
            Assert.IsTrue(wasEdited, "Edit was not successful.");
        }

        [TestMethod]
        public void IngredientDeleteTest()
        {
            // Arrange
            ingredientTester.AddToIngredientsList(avocado);

            // Act
            bool wasDeleted = ingredientTester.RemoveIngredientFromList("Avocado");

            // Assert
            
            Assert.IsTrue(wasDeleted, "Delete was not successful.");
        }
    }
}
