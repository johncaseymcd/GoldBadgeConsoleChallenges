using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldBadgeConsoleChallengeClasses;
using System.Collections.Generic;

namespace GoldBadgeConsoleAppTests
{
    [TestClass]
    public class MenuTest
    {
        Ingredient lettuce = new Ingredient("Lettuce", 1.00m, false, true, true, true, 15);
        Ingredient chicken = new Ingredient("Chicken", 3.00m, false, true, true, false, 20);
        Ingredient shreddedParmesan = new Ingredient("Shredded Parmesan Cheese", 2.00m, false, false, true, false, 10);
        Ingredient crouton = new Ingredient("Croutons", 1.00m, false, true, false, true, 30);
        Ingredient dressing = new Ingredient("Caesar Dressing", 2.00m, false, false, true, false, 5);
        List<Ingredient> _caesarSalad = new List<Ingredient>();
        MenuItemCRUD menuTester = new MenuItemCRUD();

        [TestMethod]
        public void MenuItemAddTest()
        {
            // Arrange
            _caesarSalad.Add(lettuce);
            _caesarSalad.Add(chicken);
            _caesarSalad.Add(shreddedParmesan);
            _caesarSalad.Add(crouton);
            _caesarSalad.Add(dressing);
            MenuItem chickenCaesarSalad = new MenuItem(1, "Chicken Caesar Salad", "A classic chicken caesar salad, made to order.", _caesarSalad, 9.00m);
            int beginningCount = menuTester.GetMenuItems().Count;

            // Act
            menuTester.AddToMenu(chickenCaesarSalad);

            // Assert
            int endingCount = menuTester.GetMenuItems().Count;
            Assert.IsTrue(endingCount > beginningCount, "Add was not successful.");
        }

        [TestMethod]
        public void MenuItemReadTest()
        {
            // Arrange
            _caesarSalad.Add(lettuce);
            _caesarSalad.Add(chicken);
            _caesarSalad.Add(shreddedParmesan);
            _caesarSalad.Add(crouton);
            _caesarSalad.Add(dressing);
            MenuItem chickenCaesarSalad = new MenuItem(1, "Chicken Caesar Salad", "A classic chicken caesar salad, made to order.", _caesarSalad, 10.00m);
            
            // Act
            menuTester.AddToMenu(chickenCaesarSalad);

            // Assert
            Assert.IsNotNull(menuTester.GetMenuItems(), "Read was not successful.");
        }

        [TestMethod]
        public void MenuItemEditTest()
        {
            // Arrange
            _caesarSalad.Add(lettuce);
            _caesarSalad.Add(chicken);
            _caesarSalad.Add(shreddedParmesan);
            _caesarSalad.Add(crouton);
            _caesarSalad.Add(dressing);
            MenuItem chickenCaesarSalad = new MenuItem(1, "Chicken Caesar Salad", "A classic chicken caesar salad, made to order.", _caesarSalad, 10.00m);
            menuTester.AddToMenu(chickenCaesarSalad);

            // Act
            Ingredient beans = new Ingredient("Beans", 0.75m, false, true, true, true, 25);
            Ingredient tortilla = new Ingredient("Tortilla Wrap", 0.75m, false, true, false, true, 100);
            Ingredient rice = new Ingredient("White Rice", 0.75m, false, true, true, true, 10);
            Ingredient shreddedPepperJack = new Ingredient("Shredded Pepper Jack", 2.00m, false, false, true, false, 10);
            Ingredient chipotle = new Ingredient("Chipotle Sauce", 0.50m, false, true, true, true, 5);
            List<Ingredient> _beanBurrito = new List<Ingredient>();
            _beanBurrito.Add(beans);
            _beanBurrito.Add(tortilla);
            _beanBurrito.Add(rice);
            _beanBurrito.Add(shreddedPepperJack);
            _beanBurrito.Add(chipotle);
            MenuItem beanAndRiceBurrito = new MenuItem(2, "Bean & Rice Burrito", "Beans, rice, and cheese dressed in our housemade chipotle sauce and wrapped snugly in a flour tortilla.", _beanBurrito, 4.75m);
            bool wasEdited = menuTester.EditMenuItem("Chicken caesar salad", beanAndRiceBurrito);

            // Assert
            Assert.IsTrue(wasEdited, "Edit was not successful.");
        }

        [TestMethod]
        public void MenuItemDeleteTest()
        {
            // Arrange
            _caesarSalad.Add(lettuce);
            _caesarSalad.Add(chicken);
            _caesarSalad.Add(shreddedParmesan);
            _caesarSalad.Add(crouton);
            _caesarSalad.Add(dressing);
            MenuItem chickenCaesarSalad = new MenuItem(1, "Chicken Caesar Salad", "A classic chicken caesar salad, made to order.", _caesarSalad, 10.00m);
            menuTester.AddToMenu(chickenCaesarSalad);

            // Act
            bool wasDeleted = menuTester.RemoveFromMenu("Chicken caesar salad");

            // Assert
            Assert.IsTrue(wasDeleted, "Delete was not successful.");
        }
    }
}
