# DJ-DBF2CSV — User Manual

> **Version:** 1.1.6  
> **Platform:** Windows 10 or later (64-bit)  
> **Runtime:** .NET 9 Desktop Runtime  
> **Home Page:** [https://www.donaldjamescompany.com/windows-app-dj-dbf2csv.html](https://www.donaldjamescompany.com/windows-app-dj-dbf2csv.html)

---

## Table of Contents

1. [Overview](#1-overview)
2. [System Requirements](#2-system-requirements)
3. [Installation](#3-installation)
4. [Starting the Application](#4-starting-the-application)
5. [User Interface Reference](#5-user-interface-reference)
   - 5.1 [Input (.DBF) Folder](#51-input-dbf-folder)
   - 5.2 [Output Folder](#52-output-folder)
   - 5.3 [Character Set](#53-character-set)
   - 5.4 [Output Options](#54-output-options)
   - 5.5 [File Options](#55-file-options)
   - 5.6 [Status Log](#56-status-log)
   - 5.7 [Buttons](#57-buttons)
6. [Step-by-Step Conversion Guide](#6-step-by-step-conversion-guide)
7. [Output File Format](#7-output-file-format)
   - 7.1 [CSV (Comma Separated Values)](#71-csv-comma-separated-values)
   - 7.2 [TSV (Tab Separated Values)](#72-tsv-tab-separated-values)
   - 7.3 [Quote Strings](#73-quote-strings)
8. [Supported Character Sets](#8-supported-character-sets)
9. [Supported DBF File Types](#9-supported-dbf-file-types)
10. [Troubleshooting](#10-troubleshooting)
11. [Frequently Asked Questions](#11-frequently-asked-questions)

---

## 1. Overview

**DJ-DBF2CSV** is a portable, standalone Windows desktop application that converts dBASE (`.DBF`) database files into plain-text **CSV** or **TSV** files in bulk. It is designed to be simple, fast, and require no installation.

Key capabilities:

- Select a source folder and convert **all** `.DBF` files in it at once
- Output to either **CSV** (comma-separated) or **TSV** (tab-separated) format
- Control **string quoting** per RFC 4180
- Optionally **include or exclude column headers** in the output
- Choose whether to **overwrite or skip** existing output files
- Output filenames **preserve the original casing** of the source `.DBF` file; extensions are always lowercase
- Choose from **12 common character set encodings** to match the source data
- Monitor progress in real time via the built-in **auto-scrolling Status log**

---

## 2. System Requirements

| Component | Requirement |
|---|---|
| Operating System | Windows 10 (version 1903 or later) or Windows 11, 64-bit |
| .NET Runtime | [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0) |
| Disk Space | < 5 MB for the application |
| Memory | 50 MB RAM minimum; more for very large DBF files |

---

## 3. Installation

DJ-DBF2CSV is fully portable — no installer is required.

1. Copy `dj-dbf2csv.exe` to any folder on your computer (e.g. `C:\Tools\DJ-DBF2CSV\`)
2. Ensure the [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0) is installed on the machine
3. Double-click `dj-dbf2csv.exe` to launch the application

> **Tip:** You can place a shortcut to `dj-dbf2csv.exe` on your Desktop or Taskbar for quick access.

---

## 4. Starting the Application

Double-click `dj-dbf2csv.exe`. The main window will open, centred on screen, with the title **DJ-DBF2CSV**.

No configuration file or initial setup is needed.

---

## 5. User Interface Reference

```
┌──────────────────────────────────────────────────────────────────────┐
│  DJ-DBF2CSV v1.1.6                                                   │
├──────────────────────────────────────────────────────────────────────┤
│  Input (.DBF) Folder:   [ path/to/dbf/folder           ] [Browse…]  │
│  Output (.CSV) Folder:  [ path/to/output/folder        ] [Browse…]  │
│  Character Set:         [ UTF-8                       ▼]            │
│  Output Options:        ● Comma separated  ○ Tab separated          │
│                         ☑ Quote strings  ☑ Include column headers   │
│  File Options:          ☑ Overwrite file(s) if exist                │
├──────────────────────────────────────────────────────────────────────┤
│  Status                                                              │
│  ┌────────────────────────────────────────────────────────────────┐  │
│  │  (auto-scrolling conversion log — green text on dark bg)      │  │
│  └────────────────────────────────────────────────────────────────┘  │
├──────────────────────────────────────────────────────────────────────┤
│  [ EXIT ]  [ RESET ]  [ CONVERT ]                                    │
└──────────────────────────────────────────────────────────────────────┘
```

### 5.1 Input (.DBF) Folder

| Element | Description |
|---|---|
| Text box | Displays the path of the selected input folder (read-only) |
| **Browse…** button | Opens a folder-picker dialog to select the source folder |

The application will scan this folder for all files with the `.DBF` extension (non-recursive — sub-folders are not scanned). At least one `.DBF` file must exist in the chosen folder for conversion to proceed.

---

### 5.2 Output Folder

| Element | Description |
|---|---|
| Label | Reads **"Output (.CSV) Folder:"** or **"Output (.TSV) Folder:"** depending on the selected separator |
| Text box | Displays the path of the selected output folder (read-only) |
| **Browse…** button | Opens a folder-picker dialog to select the destination folder |

Output files are written to this folder with the same base name as the source `.DBF` file and the appropriate extension (`.csv` or `.tsv`). The output filename **preserves the original casing** of the `.DBF` filename; the extension is always lowercase.

> **Example:** `Customers.DBF` → `Customers.csv` (or `Customers.tsv`)

Whether an existing file is overwritten or skipped is controlled by the **Overwrite file(s) if exist** checkbox in the File Options row.

---

### 5.3 Character Set

A dropdown list of common character encodings. Select the encoding that matches how text was stored in the source `.DBF` files.

| Option | Encoding | Typical Use |
|---|---|---|
| UTF-8 *(default)* | UTF-8 | Modern files; universal |
| UTF-16 (Unicode) | UTF-16 LE | Windows Unicode files |
| Windows-1252 (Western) | CP1252 | Western European; legacy Windows |
| ISO-8859-1 (Latin-1) | ISO-8859-1 | Western European; Unix/web legacy |
| ISO-8859-2 (Latin-2) | ISO-8859-2 | Central/Eastern European |
| CP437 (DOS Latin US) | CP437 | Original IBM PC / US DOS |
| CP850 (DOS Latin-1) | CP850 | Western European DOS |
| Shift-JIS (Japanese) | Shift_JIS | Japanese |
| GB2312 (Chinese Simplified) | GB2312 | Simplified Chinese |
| Big5 (Chinese Traditional) | Big5 | Traditional Chinese |
| KOI8-R (Russian) | KOI8-R | Russian Unix legacy |
| Windows-1251 (Cyrillic) | CP1251 | Cyrillic; legacy Windows |

> **Tip:** If output text contains garbled characters (mojibake), try a different encoding that matches the original system locale where the DBF was created.

---

### 5.4 Output Options

#### Separator

| Radio Button | Delimiter | File Extension |
|---|---|---|
| **Comma separated** *(default)* | `,` | `.csv` |
| **Tab separated** | `\t` (tab) | `.tsv` |

Switching between these options immediately updates the **Output Folder** label and changes the extension used for output files.

#### Quote strings

| State | Behaviour |
|---|---|
| ☑ Checked *(default)* | All string (text) field values are wrapped in double-quotes `"…"` |
| ☐ Unchecked | String values are written bare; quoting is applied only when a field value contains the delimiter character, a double-quote, or a newline (always-safe quoting) |

When quoting is active, any double-quote character inside a field value is escaped by doubling it (`""`) per RFC 4180.

> **Note:** Column header names (first row) are never quoted, regardless of this setting.

#### Include column headers

| State | Behaviour |
|---|---|
| ☑ Checked *(default)* | The first row of every output file contains the field names from the source `.DBF` |
| ☐ Unchecked | The header row is omitted; the output file begins directly with data rows |

---

### 5.5 File Options

#### Overwrite file(s) if exist

| State | Behaviour |
|---|---|
| ☑ Checked *(default)* | If an output file with the same name already exists it will be **overwritten** |
| ☐ Unchecked | If an output file with the same name already exists it will be **skipped**; the status log will note `Skipped` for that file |

The final summary line in the status log reports counts for **Succeeded**, **Skipped**, and **Failed** separately.

---

### 5.6 Status Log

A scrollable, read-only text area that displays verbose output during conversion. Text is rendered in **bright green on a dark background**. The log **auto-scrolls** to the latest line as each entry is added.

It reports:

- Number of `.DBF` files found
- Output format, quoting, header, overwrite mode, and encoding in use
- Each file being converted and its result (`Done`, `Skipped`, or `[ERROR]`)
- A final summary line: `Conversion complete. Succeeded: N  Skipped: N  Failed: N`

The log is **not cleared automatically** between runs; use **RESET** to clear it.

---

### 5.7 Buttons

| Button | Behaviour |
|---|---|
| **EXIT** | Closes the application immediately |
| **RESET** | Clears both folder paths; resets Character Set to UTF-8; restores all Output Options and File Options to their defaults (all checkboxes checked, Comma separated selected); clears the status log; disables the CONVERT button |
| **CONVERT** | Starts the bulk conversion. Disabled until both an input and output folder have been selected. Also disabled while a conversion is in progress. |

---

## 6. Step-by-Step Conversion Guide

1. **Launch** `dj-dbf2csv.exe`
2. Click **Browse…** next to **Input (.DBF) Folder** → navigate to the folder containing your `.DBF` files → click **Select Folder**
3. Click **Browse…** next to the **Output Folder** → navigate to or create the destination folder → click **Select Folder**
4. Select the appropriate **Character Set** from the dropdown (use UTF-8 if unsure)
5. Choose **Comma separated** (produces `.csv`) or **Tab separated** (produces `.tsv`)
6. Set **Quote strings** — leave checked for maximum compatibility, or uncheck for bare unquoted output
7. Set **Include column headers** — leave checked to include field names as the first row, or uncheck to output data only
8. Set **Overwrite file(s) if exist** — leave checked to replace any existing files, or uncheck to skip files that already exist
9. Click **CONVERT**
10. Watch the **Status** log as it auto-scrolls through progress and results
11. When the log shows `Conversion complete`, your output files are ready in the selected output folder

---

## 7. Output File Format

### 7.1 CSV (Comma Separated Values)

- Fields are separated by a comma (`,`)
- Output files use the `.csv` extension
- Compatible with Microsoft Excel, Google Sheets, LibreOffice Calc, and most data tools

**Example (Quote strings ON):**
```
ID,FIRST_NAME,LAST_NAME,BALANCE
"1","Alice","Smith","1500.00"
"2","Bob","O'Brien","200.50"
```

**Example (Quote strings OFF):**
```
ID,FIRST_NAME,LAST_NAME,BALANCE
1,Alice,Smith,1500.00
2,Bob,O'Brien,200.50
```

---

### 7.2 TSV (Tab Separated Values)

- Fields are separated by a tab character
- Output files use the `.tsv` extension
- Useful when field values themselves may contain commas

**Example (Quote strings OFF):**
```
ID	FIRST_NAME	LAST_NAME	BALANCE
1	Alice	Smith	1500.00
2	Bob	O'Brien	200.50
```

---

### 7.3 Quote Strings

Quoting follows **RFC 4180** rules:

- Fields that contain the delimiter, a double-quote (`"`), or a newline are **always** quoted, regardless of the Quote strings setting
- An embedded `"` is escaped as `""` (double double-quote)
- Column headers are **never** quoted

---

## 8. Supported Character Sets

See the table in [Section 5.3](#53-character-set) for the full list. The most common choices are:

- **UTF-8** — use for modern DBF files or when unsure
- **Windows-1252** — legacy dBASE files created on Western European Windows systems
- **CP850** — legacy dBASE/FoxPro files created in a DOS environment

---

## 9. Supported DBF File Types

DJ-DBF2CSV uses the **DbfDataReader** library and supports:

| Format | Notes |
|---|---|
| dBASE III | Classic format |
| dBASE IV | Adds memo fields |
| dBASE 5 | Extended field types |
| FoxPro / Visual FoxPro | Compatible subset |
| Clipper | Compatible subset |

Soft-deleted records (records marked as deleted within the DBF but not yet physically removed) are **automatically skipped** and not written to the output file.

Memo (`.DBT` / `.FPT`) fields are read as text when the associated memo file is present alongside the `.DBF` file.

---

## 10. Troubleshooting

| Symptom | Likely Cause | Solution |
|---|---|---|
| Application won't start | .NET 9 Desktop Runtime not installed | Install from [microsoft.com/download/dotnet/9.0](https://dotnet.microsoft.com/download/dotnet/9.0) |
| **CONVERT** button stays disabled | Input or output folder not selected | Ensure both Browse fields show a valid path |
| `[WARN] No .DBF files found` | Input folder contains no `.DBF` files | Verify the folder — DBF files are not searched recursively in sub-folders |
| Garbled text in output | Wrong character set selected | Try Windows-1252, CP850, or CP437 for legacy DOS/Windows DBF files |
| `[ERROR]` on a specific file | Corrupt, locked, or unsupported DBF variant | Check the file is not open in another application; try opening it in a DBF viewer |
| Output file skipped unexpectedly | **Overwrite file(s) if exist** is unchecked | Check the checkbox or remove/rename the existing output file |

---

## 11. Frequently Asked Questions

**Q: Does it convert sub-folders recursively?**  
A: No. Only `.DBF` files directly inside the selected input folder are processed. Sub-folders are ignored.

**Q: Can I convert a single file instead of a whole folder?**  
A: Not directly — select a folder that contains only that one `.DBF` file, or copy the file to a temporary folder first.

**Q: Will existing output files be overwritten?**  
A: By default, yes. If you uncheck **Overwrite file(s) if exist**, any file that already exists in the output folder will be skipped instead, and the status log will report it as `Skipped`.

**Q: What happens to deleted records in the DBF?**  
A: They are skipped automatically. Only active (non-deleted) records are written to the output.

**Q: The first row of my CSV has column names in uppercase — is that normal?**  
A: Yes. DBF field names are stored in uppercase internally; DJ-DBF2CSV writes them as-is in the header row. If you don't want a header row at all, uncheck **Include column headers**.

**Q: Why does the output filename use the same casing as the `.DBF` file?**  
A: DJ-DBF2CSV preserves the original filename casing so files are easy to match back to their source. Only the extension is normalised to lowercase (`.csv` or `.tsv`).

**Q: Can I use the output files with Microsoft Excel?**  
A: Yes. Open Excel, use **File → Open**, change the file filter to "All Files", and select the `.csv` or `.tsv` file. Excel's import wizard will guide you through delimiter and encoding selection.

**Q: Which separator should I choose?**  
A: Use **Comma separated (CSV)** for general-purpose use and maximum compatibility. Use **Tab separated (TSV)** if your data contains commas in text fields (e.g. addresses, notes) and you want cleaner output without heavy quoting.

---

*DJ-DBF2CSV — © Donald James Company — Released under the [MIT License](../LICENSE) — [App Home Page](https://www.donaldjamescompany.com/windows-app-dj-dbf2csv.html)*
