# SpecialCharactersLib

SpecialCharactersLib is a .Net library that provides a simple way to input special characters using keyboard shortcuts and shows a clickable overview of all configured shortcuts. The shortcut display uses Windows Form controls.

The library is available on NuGet: https://www.nuget.org/packages/FloseCode.SpecialCharactersLib/

## Example in VB.Net
```vb.net
Imports FloseCode.SpecialCharactersLib

' Define special characters
Dim groups As New List(Of SpecialCharactersLib.Group)
Dim group As New Group("French", New Globalization.CultureInfo("fr"))
group.SpecialCharacterShortcuts.Add(New SpecialCharacterShortcut("ç", "c", ModifierKey.Ctrl))
groups.Add(group)

' Show clickable form with special characters
Dim form As New SpecialCharactersLib.SpecialCharactersForm()
form.SpecialCharacterGroups = groups
form.ShowDockedToForm(Me)
AddHandler form.SpecialCharacterSelected, Sub(c As String)
                                              ' Handle special char
                                          End Sub

' Handle keyboard shortcuts for special characters
Dim handler As New ShortcutHandler()
handler.SpecialCharacterGroups = groups
AddHandler handler.SpecialCharacterSelected, Sub(c As String)
                                                 ' Handle special char
                                             End Sub

' Main form code
Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
    Return handler.ProcessCmdKey(msg, keyData) OrElse MyBase.ProcessCmdKey(msg, keyData)
End Function
```
## License

Copyright: Flose 2016 https://www.mal-was-anderes.de/

Licensed under the LGPLv3: http://www.gnu.org/licenses/lgpl-3.0.html
