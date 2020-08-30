using ApusAnimalMotel.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel
{
    /// <summary>
    /// RecipeClass containing information about a Recipe, namely Name and it´s Ingredients 
    /// </summary>
    public class Recipe
    {
        public ListManager<string> Ingredients;
        public string Name { get; set; }

        /// <summary>
        /// Staff constructor, instansiates Ingredients
        /// </summary>
        public Recipe()
        {
            Ingredients = new ListManager<string>();
        }

        /// <summary>
        /// ToString containing the Name of the recipe and its ingredients
        /// </summary>
        /// <returns>Returns a ToString containing the Name of the recipe and its ingredients</returns>
        public override string ToString()
        {
            return $"{Name} {ConvertIngredientsToString()}";
        }

        /// <summary>
        /// Make string out of ingredients
        /// </summary>
        /// <returns>stringrepresentation if ingredients</returns>
        private string ConvertIngredientsToString()
        {
            StringBuilder ingredients = new StringBuilder();
            foreach (string ingredient in Ingredients.ToStringList())
            {
                ingredients.Append(ingredient);
                ingredients.Append(", ");
            }

            return ingredients.ToString();
        }
    }
}
