using ApusAnimalMotel.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApusAnimalMotel
{
    public partial class FoodForm : Form
    {
        public Recipe recipe;

        /// <summary>
        /// Default constructor FoodForm
        /// </summary>
        public FoodForm()
        {
            InitializeComponent();
            recipe = new Recipe();
        }

        private void AddRecipeButton_Click(object sender, EventArgs e)
        {
            recipe.Name = NameOfRecipeTextBox.Text;
            this.Close();
        }

        /// <summary>
        /// Evend handler when clicking the button CancelIngredient
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void CancelRecipeButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult dialogResult = MessageBox.Show("Do you really want to cancel?", "Are you sure?", buttons);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Evend handler when clicking the button Add ingredient
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void AddIngredientButton_Click(object sender, EventArgs e)
        {
            recipe.Ingredients.Add(NameOfIngredientTextBox.Text);
            UpdateIngredientsListBox(recipe.Ingredients);
            NameOfIngredientTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Update ListBox with ingredients
        /// </summary>
        /// <param name="ingredients">the ingredients to add</param>
        private void UpdateIngredientsListBox(List<string> ingredients)
        {
            ClearIngredientsAddedListBox();
            for (int i = 0; i < ingredients.Count; i++)
            {
                AddedIngredientsListBox.Items.Add(ingredients[i]);
            }
        }

        /// <summary>
        /// Clears the ingredientsListBox
        /// </summary>
        private void ClearIngredientsAddedListBox()
        {
            AddedIngredientsListBox.Items.Clear();
        }

        /// <summary>
        /// Evend handler when clicking the button Delete ingredient
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void DeleteIngredientButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult dialogResult = MessageBox.Show("Do you really want to delete?", "Are you sure?", buttons);
            if (dialogResult == DialogResult.Yes)
            {
                recipe.Ingredients.RemoveAt(AddedIngredientsListBox.SelectedIndex);
                AddedIngredientsListBox.Items.RemoveAt(AddedIngredientsListBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Evend handler when clicking the button Change ingredient
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void ChangeIngredientButton_Click(object sender, EventArgs e)
        {
            int changeAt = AddedIngredientsListBox.SelectedIndex;
            if (NameOfIngredientTextBox.Text != string.Empty && changeAt != -1)
            {
                recipe.Ingredients[changeAt] = NameOfIngredientTextBox.Text;
            }

            UpdateIngredientsListBox(recipe.Ingredients);
            NameOfIngredientTextBox.Text = string.Empty;
        }
    }
}
