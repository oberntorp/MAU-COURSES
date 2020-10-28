using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace QuizApplication
{
/// <summary>
    /// This is a utility class for creating message boxes with different cations in a neater way
    /// </summary>
    public class MessageBoxes
    {
        /// <summary>
        /// This message shows a supplied message and the caption "Information"
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        public static void ShowInformationMessageBox(string messageToShow)
        {
            MessageBox.Show(messageToShow, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// This message shows a supplied message and the caption "An error occured"
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        public static void ShowErrorMessageBox(string messageToShow)
        {
            MessageBox.Show(messageToShow, "An error occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// This message shows a supplied message and the caption "Action required"
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        public static void ShowActionMessageBox(string message)
        {
            MessageBox.Show(message, "Action required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// This message shows a supplied message and the caption "You have not saved", with the possibility for the user to stop the action
        /// </summary>
        /// <param name="messageToShow">Message that will be shown</param>
        /// <returns>DialogResult (User responding yes/no)</returns>
        public static MessageBoxResult ShowSaveWarningMessageBox(string messageToShow)
        {
            return MessageBox.Show(messageToShow, "You have not saved", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }
    }
}
