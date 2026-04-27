using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DbfDataReader;
using Microsoft.Win32;

namespace dj_dbf2csv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Register additional encodings (e.g. Windows-1252, CP850, etc.)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();
        }

        /// <summary>True when Tab separated is selected; false for Comma separated.</summary>
        private bool IsTabMode => RbTab.IsChecked == true;

        private void Separator_Changed(object sender, RoutedEventArgs e)
        {
            if (LblOutputFolder is null) return; // guard during InitializeComponent
            LblOutputFolder.Content = IsTabMode ? "Output (.TSV) Folder:" : "Output (.CSV) Folder:";
        }

        // ── Folder browse ────────────────────────────────────────────────────

        private void BrowseInput_Click(object sender, RoutedEventArgs e)
        {
            var folder = PickFolder("Select Input (.DBF) Folder");
            if (folder is null) return;
            TxtInputFolder.Text = folder;
            UpdateConvertButton();
        }

        private void BrowseOutput_Click(object sender, RoutedEventArgs e)
        {
            var label = IsTabMode ? "TSV" : "CSV";
            var folder = PickFolder($"Select Output (.{label}) Folder");
            if (folder is null) return;
            TxtOutputFolder.Text = folder;
            UpdateConvertButton();
        }

        /// <summary>Opens a folder-picker dialog and returns the chosen path, or null.</summary>
        private static string? PickFolder(string description)
        {
            var dialog = new OpenFolderDialog { Title = description };
            return dialog.ShowDialog() == true ? dialog.FolderName : null;
        }

        // ── Button handlers ──────────────────────────────────────────────────

        private void BtnExit_Click(object sender, RoutedEventArgs e) => Close();

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            TxtInputFolder.Text = string.Empty;
            TxtOutputFolder.Text = string.Empty;
            CmbEncoding.SelectedIndex = 0;
            RbComma.IsChecked = true;
            ChkQuoteStrings.IsChecked = true;
            ChkIncludeHeaders.IsChecked = true;
            TxtStatus.Clear();
            BtnConvert.IsEnabled = false;
        }

        private async void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            var inputDir = TxtInputFolder.Text;
            var outputDir = TxtOutputFolder.Text;
            var encodingTag = ((ComboBoxItem)CmbEncoding.SelectedItem).Tag?.ToString() ?? "utf-8";
            bool tabMode = IsTabMode;
            bool quoteStrings = ChkQuoteStrings.IsChecked == true;
            bool includeHeaders = ChkIncludeHeaders.IsChecked == true;
            char separator = tabMode ? '\t' : ',';
            string extension = tabMode ? ".tsv" : ".csv";
            string formatLabel = tabMode ? "TSV" : "CSV";

            Encoding encoding;
            try
            {
                encoding = Encoding.GetEncoding(encodingTag);
            }
            catch (Exception ex)
            {
                Log($"[ERROR] Could not resolve encoding '{encodingTag}': {ex.Message}");
                return;
            }

            var dbfFiles = Directory.GetFiles(inputDir, "*.dbf", SearchOption.TopDirectoryOnly);
            if (dbfFiles.Length == 0)
            {
                Log("[WARN] No .DBF files found in the selected input folder.");
                return;
            }

            SetUiEnabled(false);
            Log($"Starting conversion — {dbfFiles.Length} file(s) found.");
            Log($"Output format   : {formatLabel} (separator: {(tabMode ? "Tab" : "Comma")})");
            Log($"Quote strings   : {(quoteStrings ? "Yes" : "No")}");
            Log($"Column headers  : {(includeHeaders ? "Yes" : "No")}");
            Log($"Output encoding : {encoding.EncodingName}");
            Log($"Output folder   : {outputDir}");
            Log(new string('─', 60));

            int succeeded = 0;
            int failed = 0;

            await Task.Run(() =>
            {
                foreach (var dbfPath in dbfFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(dbfPath);
                    var outPath = Path.Combine(outputDir, fileName + extension);

                    try
                    {
                        AppendLog($"  Converting : {Path.GetFileName(dbfPath)} …");
                        ConvertDbf(dbfPath, outPath, encoding, separator, quoteStrings, includeHeaders);
                        AppendLog($"  Done       : {Path.GetFileName(outPath)}");
                        succeeded++;
                    }
                    catch (Exception ex)
                    {
                        AppendLog($"  [ERROR]    : {Path.GetFileName(dbfPath)} — {ex.Message}");
                        failed++;
                    }
                }
            });

            Log(new string('─', 60));
            Log($"Conversion complete. Succeeded: {succeeded}  Failed: {failed}");
            SetUiEnabled(true);
        }

        // ── Conversion logic ─────────────────────────────────────────────────

        private static void ConvertDbf(string dbfPath, string outPath, Encoding outEncoding, char separator, bool quoteStrings, bool includeHeaders)
        {
            var options = new DbfDataReaderOptions { SkipDeletedRecords = true };

            using var dbfReader = new DbfDataReader.DbfDataReader(dbfPath, options);
            using var writer = new StreamWriter(outPath, append: false, encoding: outEncoding);

            // Write header row (headers are never quoted)
            if (includeHeaders)
            {
                var columnNames = new List<string>(dbfReader.FieldCount);
                for (int i = 0; i < dbfReader.FieldCount; i++)
                    columnNames.Add(dbfReader.GetName(i));
                writer.WriteLine(BuildRow(columnNames, separator, quoteStrings: false));
            }

            // Write data rows
            while (dbfReader.Read())
            {
                var values = new List<string>(dbfReader.FieldCount);
                for (int i = 0; i < dbfReader.FieldCount; i++)
                {
                    var value = dbfReader.IsDBNull(i) ? string.Empty : dbfReader.GetValue(i)?.ToString() ?? string.Empty;
                    values.Add(value);
                }
                writer.WriteLine(BuildRow(values, separator, quoteStrings));
            }
        }

        /// <summary>
        /// Builds a delimited row. When <paramref name="quoteStrings"/> is true, all fields are
        /// wrapped in double-quotes and embedded double-quotes are escaped per RFC 4180.
        /// Fields that contain the separator or newlines are always quoted regardless of the flag.
        /// </summary>
        private static string BuildRow(IEnumerable<string> fields, char separator, bool quoteStrings)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var field in fields)
            {
                if (!first) sb.Append(separator);
                first = false;

                bool mustQuote = field.Contains(separator) || field.Contains('"')
                                 || field.Contains('\n') || field.Contains('\r');

                if (mustQuote || quoteStrings)
                {
                    sb.Append('"');
                    sb.Append(field.Replace("\"", "\"\""));
                    sb.Append('"');
                }
                else
                {
                    sb.Append(field);
                }
            }
            return sb.ToString();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void UpdateConvertButton()
        {
            BtnConvert.IsEnabled =
                !string.IsNullOrWhiteSpace(TxtInputFolder.Text) &&
                !string.IsNullOrWhiteSpace(TxtOutputFolder.Text);
        }

        private void SetUiEnabled(bool enabled)
        {
            Dispatcher.Invoke(() =>
            {
                BtnConvert.IsEnabled = enabled;
                BtnReset.IsEnabled = enabled;
                BtnExit.IsEnabled = enabled;
                BrowseInputBtn.IsEnabled = enabled;
                BrowseOutputBtn.IsEnabled = enabled;
                CmbEncoding.IsEnabled = enabled;
                RbComma.IsEnabled = enabled;
                RbTab.IsEnabled = enabled;
                ChkQuoteStrings.IsEnabled = enabled;
                ChkIncludeHeaders.IsEnabled = enabled;
            });
        }

        /// <summary>Appends a line to the status box from any thread.</summary>
        private void AppendLog(string message) =>
            Dispatcher.Invoke(() => TxtStatus.AppendText(message + Environment.NewLine));

        /// <summary>Logs a line from the UI thread directly.</summary>
        private void Log(string message) =>
            TxtStatus.AppendText(message + Environment.NewLine);
    }
}
