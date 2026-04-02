using System;
using System.IO;
using System.Windows.Forms;

namespace End_of_Day_Income
{
    /// <summary>
    /// Manages business name persistence and retrieval from a configuration file.
    /// This class handles prompting the user for a business name if not found or empty.
    /// File is always stored as "Business Name.txt" in My Documents folder.
    /// </summary>
    public static class BusinessNameManager
    {
        private const string DEFAULT_BUSINESS_NAME = "My Business";
        private const string FILENAME = "Business Name.txt";

        private static string BusinessNameFilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            FILENAME);

        /// <summary>
        /// Gets the business name from "Business Name.txt" in My Documents, prompting the user if necessary.
        /// </summary>
        /// <param name="defaultName">The default name to use if user cancels (default: "My Business")</param>
        /// <param name="showSuccessMessage">Whether to show a success message after setting the name (default: true)</param>
        /// <returns>The business name from file or user prompt, or default if cancelled</returns>
        public static string GetBusinessName(
            string defaultName = DEFAULT_BUSINESS_NAME,
            bool showSuccessMessage = true)
        {
            string businessNameFile = BusinessNameFilePath;

            if (File.Exists(businessNameFile))
            {
                return LoadExistingBusinessName(businessNameFile, defaultName, showSuccessMessage);
            }
            else
            {
                return PromptForNewBusinessName(businessNameFile, defaultName, showSuccessMessage);
            }
        }

        /// <summary>
        /// Saves a business name to "Business Name.txt" in My Documents.
        /// </summary>
        /// <param name="businessName">The business name to save</param>
        /// <returns>True if saved successfully, false otherwise</returns>
        public static bool SaveBusinessName(string businessName)
        {
            if (string.IsNullOrWhiteSpace(businessName))
            {
                return false;
            }

            try
            {
                File.WriteAllText(BusinessNameFilePath, businessName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving business name: {ex.Message}", "Save Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Prompts the user to update the business name.
        /// </summary>
        /// <param name="currentName">The current business name</param>
        /// <returns>The new business name, or the current name if cancelled</returns>
        public static string UpdateBusinessName(string currentName)
        {
            using (var prompt = new BusinessNamePrompt())
            {
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    string newName = prompt.BusinessName;

                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        if (SaveBusinessName(newName))
                        {
                            MessageBox.Show($"Business name updated to: {newName}", "Business Name Updated",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return newName;
                        }
                    }
                }
            }

            return currentName;
        }

        private static string LoadExistingBusinessName(string businessNameFile, string defaultName, bool showSuccessMessage)
        {
            try
            {
                string businessName = File.ReadAllText(businessNameFile).Trim();

                if (string.IsNullOrWhiteSpace(businessName))
                {
                    return PromptForNewBusinessName(businessNameFile, defaultName, showSuccessMessage);
                }

                return businessName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business name: {ex.Message}\n\nUsing default: {defaultName}", 
                    "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return defaultName;
            }
        }

        private static string PromptForNewBusinessName(string businessNameFile, string defaultName, bool showSuccessMessage)
        {
            using (var prompt = new BusinessNamePrompt())
            {
                if (prompt.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show($"No business name entered. Using default: {defaultName}", "Business Name",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return defaultName;
                }

                string businessName = prompt.BusinessName;

                if (string.IsNullOrWhiteSpace(businessName))
                {
                    MessageBox.Show($"No business name entered. Using default: {defaultName}", "Business Name",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return defaultName;
                }

                try
                {
                    File.WriteAllText(businessNameFile, businessName);

                    if (showSuccessMessage)
                    {
                        MessageBox.Show($"Business name set to: {businessName}", "Business Name",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    return businessName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving business name: {ex.Message}\n\nUsing: {businessName}", 
                        "Save Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return businessName;
                }
            }
        }
    }
}
