using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApusAnimalMotel.Utilities
{
    /// <summary>
    /// This is a utility class for creating message boxes with different cations in a neater way
    /// </summary>
    public class MessageBoxUtility
    {
        /// <summary>
        /// This message shows a supplied message and the caption "Information"
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        public static void ShowInformationMessageBox(string messageToShow)
        {
            MessageBox.Show(messageToShow, "Information");
        }

        /// <summary>
        /// This message shows a supplied message and the caption "An error occured"
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        public static void ShowErrorMessageBox(string messageToShow)
        {
            MessageBox.Show(messageToShow, "An error occured");
        }

        /// <summary>
        /// This message shows a supplied message and the caption "Action required"
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        public static void ShowActionMessageBox(string message)
        {
            MessageBox.Show(message, "Action required");
        }

        /// <summary>
        /// This message shows a supplied message and the caption "You have not saved", with the possibility for the user to stop the action
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        /// <returns>DialogResult (User responding yes/no)</returns>
        public static DialogResult ShowSaveWarningMessageBox(string messageToShow)
        {
            return MessageBox.Show(messageToShow, "You have not saved", MessageBoxButtons.YesNo);
        }
    }
}
