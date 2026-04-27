# DJ-DBF2CSV

![Version](https://img.shields.io/badge/version-1.1.6-blue)

**🏠 [App Home Page](https://www.donaldjamescompany.com/windows-app-dj-dbf2csv.html)**

A standalone, portable Windows desktop application that bulk-converts dBASE / FoxPro / Visual FoxPro (`.DBF`) files to **CSV** (Comma Separated Values) or **TSV** (Tab Separated Values) format.

Built with **.NET 9** and **WPF** — ships as a single `.exe` with no installation required.

---

## Features

- **Bulk conversion** — converts every `.DBF` file found in a selected input folder in one pass
- **CSV or TSV output** — switch between comma-separated and tab-separated output at any time
- **Quote strings** — optional RFC 4180-compliant quoting of string fields (on by default)
- **Column headers** — optionally include or exclude the header row in output files (on by default)
- **Overwrite control** — choose to overwrite existing output files or skip them (on by default)
- **12 character-set encodings** — UTF-8, UTF-16, Windows-1252, ISO-8859-x, DOS code pages, CJK, Cyrillic, and more
- **Live status log** — auto-scrolling log with green-on-black display shows verbose progress in real time
- **Filename fidelity** — output filenames preserve the original `.DBF` casing; extensions are always lowercase (`.csv` / `.tsv`)
- **Portable** — single executable; no installer, no registry entries, no admin rights needed

---

## Requirements

| Requirement | Detail |
|---|---|
| OS | Windows 10 or later (64-bit) |
| Runtime | [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0) |

---

## Quick Start

1. Run `dj-dbf2csv.exe`
2. Click **Browse…** next to **Input (.DBF) Folder** and choose the folder containing your `.DBF` files
3. Click **Browse…** next to **Output (.CSV / .TSV) Folder** and choose a destination folder
4. Select a **Character Set** (defaults to UTF-8)
5. Choose **Comma separated** or **Tab separated** output
6. Toggle **Quote strings**, **Include column headers**, and **Overwrite file(s) if exist** as needed
7. Click **CONVERT**

---

## Building from Source

```bash
git clone https://github.com/DonaldJamesCompany/Dj-Dbf2Csv.git
cd Dj-Dbf2Csv
dotnet build
```

To publish a self-contained single-file executable:

```bash
dotnet publish dj-dbf2csv/dj-dbf2csv.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

---

## Dependencies

| Package | Version | Purpose |
|---|---|---|
| [DbfDataReader](https://github.com/yellowfeather/DbfDataReader) | 1.1.0 | Read dBASE III/IV/5 `.DBF` files |
| System.Text.Encoding.CodePages | (transitive) | Legacy code-page encoding support |

---

## License

This project is licensed under the [MIT License](LICENSE).

Copyright © Donald James Company.

---

## Documentation

Full user documentation is available in [`Docs/DJ-DBF2CSV_Manual.md`](Docs/DJ-DBF2CSV_Manual.md).

---

## Home Page

[https://www.donaldjamescompany.com/windows-app-dj-dbf2csv.html](https://www.donaldjamescompany.com/windows-app-dj-dbf2csv.html)
