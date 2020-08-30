using ApusAnimalMotel.Interfaces;
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
    public partial class StaffForm : Form
    {
        public Staff staff;
        public StaffForm()
        {
            InitializeComponent();
            staff = new Staff();
        }

        private void AddQualificationButton_Click(object sender, EventArgs e)
        {
            staff.Qualifications.Add(NameOfQualificationOfStaffTextBox.Text);
            UpdateQualificationsListBox(staff.Qualifications);
            NameOfQualificationOfStaffTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Update ListBox with ingredients
        /// </summary>
        /// <param name="ingredients">the ingredients to add</param>
        private void UpdateQualificationsListBox(IListManager<string> qualifications)
        {
            ClearQualificationsAddedListBox();
            for (int i = 0; i < qualifications.Count; i++)
            {
                AddedQualificationsListBox.Items.Add(qualifications.GetAt(i));
            }
        }

        /// <summary>
        /// Clears the ingredientsListBox
        /// </summary>
        private void ClearQualificationsAddedListBox()
        {
            AddedQualificationsListBox.Items.Clear();
        }

        /// <summary>
        /// Evend handler when clicking the button Change qualification
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void ChangeQualificationButton_Click(object sender, EventArgs e)
        {
            int changeAt = AddedQualificationsListBox.SelectedIndex;
            if (NameOfQualificationOfStaffTextBox.Text != string.Empty && changeAt != -1)
            {
                staff.Qualifications.ChangeAt(NameOfQualificationOfStaffTextBox.Text, changeAt);
            }

            UpdateQualificationsListBox(staff.Qualifications);
            NameOfQualificationOfStaffTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Evend handler when clicking the button Delete qualification
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void DeleteQualificationButton_Click(object sender, EventArgs e)
        {
            if (staff.Qualifications.DeleteAt(AddedQualificationsListBox.SelectedIndex))
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dialogResult = MessageBox.Show("Do you really want to delete?", "Are you sure?", buttons);
                if (dialogResult == DialogResult.Yes)
                {
                    AddedQualificationsListBox.Items.RemoveAt(AddedQualificationsListBox.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Evend handler when clicking the button AddStaff
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void AddStaffButton_Click(object sender, EventArgs e)
        {
            staff.Name = NameOfStaffTextBox.Text;
            this.Close();
        }

        /// <summary>
        /// Evend handler when clicking the button Cancel ingredient
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void CancelStaffButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult dialogResult = MessageBox.Show("Do you really want to cancel?", "Are you sure?", buttons);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
