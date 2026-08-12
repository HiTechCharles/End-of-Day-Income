using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DailySafe
{
    /// <summary>
    /// Manages business name persistence and retrieval from a configuration file.
    /// Stores the file in a user-resolved Documents folder (Known Folder / OneDrive / user choice).
    /// </summary>
    public static class BusinessNameManager
    {
        private const string DEFAULT_BUSINESS_NAME = "My Business";
        private const string FILENAME = "Business Name.txt";
        private const string FOLDER_OVERRIDE_FILE = "BusinessPath.txt"; // stored under %APPDATA%\BFY

        // Known folder GUID for Documents
        private static readonly Guid FOLDERID_Documents = new Guid("FDD39AD0-238F-46AF-ADB4-6C85480369C7");

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);

        private static string GetKnownFolderPath(Guid folderId)
        {
            IntPtr outPath = IntPtr.Zero;
            try
            {
                int hr = SHGetKnownFolderPath(folderId, 0, IntPtr.Zero, out outPath);
                if (hr == 0 && outPath != IntPtr.Zero)
                {
                    string path = Marshal.PtrToStringUni(outPath);
                    return path;
                }
            }
            catch
            {
                // ignore failures and return null
            }
            finally
            {
                if (outPath != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(outPath);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the resolved full path to Business Name.txt. May prompt the user on first run.
        /// </summary>
        private static string BusinessNameFilePath
        {
            get
            {
                // 1) persisted override
                string persisted = GetPersistedFolderOverride();
                if (!string.IsNullOrWhiteSpace(persisted))
                {
                    return Path.Combine(persisted, FILENAME);
                }

                // 2) Known Folder (Documents)
                string knownDocs = GetKnownFolderPath(FOLDERID_Documents);
                if (!string.IsNullOrWhiteSpace(knownDocs))
                {
                    string knownCandidate = Path.Combine(knownDocs, FILENAME);
                    if (File.Exists(knownCandidate)) return knownCandidate;
                }

                // 3) OneDrive env if present and file exists
                string oneDriveEnv = Environment.GetEnvironmentVariable("OneDrive");
                if (!string.IsNullOrWhiteSpace(oneDriveEnv))
                {
                    string oneDriveCandidate = Path.Combine(oneDriveEnv, "Documents", FILENAME);
                    if (File.Exists(oneDriveCandidate)) return oneDriveCandidate;
                }

                // 4) If none found, ask the user once to pick a folder (persist choice)
                string userSelected = PromptUserForFolder();
                if (!string.IsNullOrWhiteSpace(userSelected))
                {
                    return Path.Combine(userSelected, FILENAME);
                }

                // 5) Fallback to Environment.SpecialFolder.MyDocuments (even if it doesn't exist)
                string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (!string.IsNullOrWhiteSpace(myDocs))
                {
                    return Path.Combine(myDocs, FILENAME);
                }

                // 6) Last resort: current directory
                return Path.Combine(Directory.GetCurrentDirectory(), FILENAME);
            }
        }

        /// <summary>
        /// Reads the persisted folder override if present.
        /// </summary>
        private static string GetPersistedFolderOverride()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (string.IsNullOrWhiteSpace(appData)) return null;

                string folder = Path.Combine(appData, "BFY");
                string file = Path.Combine(folder, FOLDER_OVERRIDE_FILE);
                if (File.Exists(file))
                {
                    string value = File.ReadAllText(file).Trim();
                    if (!string.IsNullOrWhiteSpace(value)) return value;
                }
            }
            catch
            {
                // ignore
            }

            return null;
        }

        private static void PersistFolderOverride(string selectedFolder)
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (string.IsNullOrWhiteSpace(appData)) return;

                string folder = Path.Combine(appData, "BFY");
                Directory.CreateDirectory(folder);
                string file = Path.Combine(folder, FOLDER_OVERRIDE_FILE);
                File.WriteAllText(file, selectedFolder ?? string.Empty);
            }
            catch
            {
                // ignore
            }
        }

        private static string PromptUserForFolder()
        {
            try
            {
                using (var dlg = new FolderBrowserDialog())
                {
                    dlg.Description = "Select the folder to store Business Name.txt (usually your Documents folder).";
                    dlg.ShowNewFolderButton = true;

                    // initialize to Documents if available
                    string init = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (!string.IsNullOrWhiteSpace(init) && Directory.Exists(init)) dlg.SelectedPath = init;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        PersistFolderOverride(dlg.SelectedPath);
                        return dlg.SelectedPath;
                    }
                }
            }
            catch
            {
                // ignore UI errors
            }

            return null;
        }

        /// <summary>
        /// Move stray file from app working directory to target if appropriate (silent).
        /// </summary>
        private static void TryMigrateStrayFile(string targetPath)
        {
            try
            {
                string stray = Path.Combine(Directory.GetCurrentDirectory(), FILENAME);
                if (!File.Exists(stray)) return;
                if (File.Exists(targetPath)) return; // don't overwrite existing target

                string targetDir = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrWhiteSpace(targetDir)) Directory.CreateDirectory(targetDir);

                try { File.Move(stray, targetPath); return; }
                catch { }
                try { File.Copy(stray, targetPath); File.Delete(stray); } catch { }
            }
            catch { }
        }

        public static string GetBusinessName(string defaultName = DEFAULT_BUSINESS_NAME, bool showSuccessMessage = true)
        {
            string businessNameFile = BusinessNameFilePath;

            // attempt to migrate stray file if present
            TryMigrateStrayFile(businessNameFile);

            if (File.Exists(businessNameFile))
            {
                return LoadExistingBusinessName(businessNameFile, defaultName, showSuccessMessage);
            }

            return PromptForNewBusinessName(businessNameFile, defaultName, showSuccessMessage);
        }

        public static bool SaveBusinessName(string businessName)
        {
            if (string.IsNullOrWhiteSpace(businessName)) return false;

            try
            {
                string path = BusinessNameFilePath;
                string dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(dir)) Directory.CreateDirectory(dir);
                File.WriteAllText(path, businessName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving business name: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

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
                            MessageBox.Show($"Business name updated to: {newName}", "Business Name Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show($"Error loading business name: {ex.Message}\n\nUsing default: {defaultName}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return defaultName;
            }
        }

        private static string PromptForNewBusinessName(string businessNameFile, string defaultName, bool showSuccessMessage)
        {
            using (var prompt = new BusinessNamePrompt())
            {
                if (prompt.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show($"No business name entered. Using default: {defaultName}", "Business Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return defaultName;
                }

                string businessName = prompt.BusinessName;
                if (string.IsNullOrWhiteSpace(businessName))
                {
                    MessageBox.Show($"No business name entered. Using default: {defaultName}", "Business Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return defaultName;
                }

                try
                {
                    string dir = Path.GetDirectoryName(businessNameFile);
                    if (!string.IsNullOrWhiteSpace(dir)) Directory.CreateDirectory(dir);
                    File.WriteAllText(businessNameFile, businessName);

                    if (showSuccessMessage)
                    {
                        MessageBox.Show($"Business name set to: {businessName}", "Business Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    return businessName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving business name: {ex.Message}\n\nUsing: {businessName}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return businessName;
                }
            }
        }

        /// <summary>
        /// Returns the folder used to store Business Name.txt (directory only).
        /// </summary>
        public static string GetBusinessFolder()
        {
            try
            {
                string path = BusinessNameFilePath;
                string dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(dir)) return dir;
            }
            catch { }

            try
            {
                string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (!string.IsNullOrWhiteSpace(myDocs)) return myDocs;
            }
            catch { }

            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        /// <summary>
        /// Allows the user to change the folder where Business Name.txt is stored.
        /// The new selection is persisted under %APPDATA%\DailySafe\BusinessPath.txt.
        /// Returns the new folder path if changed, otherwise null.
        /// </summary>
        public static string ChangeBusinessFolder()
        {
            try
            {
                string selected = PromptUserForFolder();
                if (string.IsNullOrWhiteSpace(selected)) return null;

                string newTarget = Path.Combine(selected, FILENAME);
                string old = Path.Combine(Directory.GetCurrentDirectory(), FILENAME);
                if (File.Exists(old) && !string.Equals(Path.GetFullPath(old), Path.GetFullPath(newTarget), StringComparison.OrdinalIgnoreCase))
                {
                    try { string newDir = Path.GetDirectoryName(newTarget); if (!string.IsNullOrWhiteSpace(newDir)) Directory.CreateDirectory(newDir); File.Move(old, newTarget); }
                    catch { try { File.Copy(old, newTarget); File.Delete(old); } catch { } }
                }

                return selected;
            }
            catch { return null; }
        }
    }
}
