using ApusAnimalMotel.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApusAnimalMotel
{
    /// <summary>
    /// RecipeClass containing information about a Recipe, namely Name and it´s Ingredients 
    /// </summary>
    public class Recipe
    {
        public List<string> Ingredients;
        public string Name { get; set; }

        /// <summary>
        /// Staff constructor, instansiates Ingredients
        /// </summary>
        public Recipe()
        {
            Ingredients = new List<string>();
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
            return String.Join(", ", Ingredients);
        }
    }
}
