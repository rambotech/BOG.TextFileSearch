# BOG.TextFileSearch

Not a fork, but inspired by JustAnOtherDev/FindIn

Searches text based files for matches with regular expressions or plain text.

This app adds ...
- Error handling to skip inaccessible folders and/or files.
- Ability to clone and save search parameters as named searches for reuse from a dropdown.

Windows Form using .NET 9.0.

---
Version History:
1.0.4.0 -- 6 Apr 2026
- Add support for wildcards in the lowest folder name of a path
- Add a new context menu option to copy all selected file matches to the clipboard as a list. the source folder list and ignore folder list to add a new folder spec with wildcards, e.g. c:\src\*\bin
  - Intended for a files manifest to make a change.

1.0.3.0 -- 15 Mar 2026
- Added support for multiple source folders.
- Added support for wilcards in spource folders, e.g. c:\src\*;
- Added form FolderTool to manage the source and ignored folder lists in a multi-line text field, one folder spec per line.
- Added double-click on source folder list and ignore folder list to open the tool.

1.0.2.0 -- 24 Feb 2026
- Added ability to cancel search before completion.

1.0.1.1 -- 22 Feb 2026
- Misc bug fixes for storage of form state.

1.0.1.0 -- 20 Feb 2026
- Added optional search filter for file time range.
- Updated to more logical layout on the screen.

1.0.0.1 -- 18 Feb 2026
- Minor cleanup
- App Icon added to form
- App / Assembly info displayed in form title.

1.0.0.0 -- 16 Feb 2026
- initial release

0.1.0.0 -- 
- Alpha 

