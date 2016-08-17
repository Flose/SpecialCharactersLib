Public Class Group
    Public ReadOnly Property Name As String
    Public ReadOnly Property Culture As Globalization.CultureInfo
    Public ReadOnly SpecialCharacterShortcuts As New List(Of SpecialCharacterShortcut)

    Public Sub New(name As String, Optional culture As Globalization.CultureInfo = Nothing)
        Me.Name = name
        Me.Culture = culture
    End Sub

    Public Function ShouldShow() As Boolean
        Dim currentKeyboardLayoutId = Windows.Forms.InputLanguage.CurrentInputLanguage.Culture.KeyboardLayoutId And &HFF
        ' Hide group if empty or its culture is the same as the current keyboard layout
        Return SpecialCharacterShortcuts.Count > 0 AndAlso
           (Culture Is Nothing OrElse currentKeyboardLayoutId <> (Culture.KeyboardLayoutId And &HFF))
    End Function
End Class

Public Class SpecialCharacterShortcut
    Public ReadOnly Property SpecialCharacter As String
    Public ReadOnly Property ShortcutKeyData As Windows.Forms.Keys
    Public ReadOnly Property ShortcutText As String

    Public Sub New(specialCharacter As String, shortcutKeyData As Windows.Forms.Keys, shortcutText As String)
        Me.SpecialCharacter = specialCharacter
        Me.ShortcutKeyData = shortcutKeyData
        Me.ShortcutText = shortcutText
    End Sub

    Public Sub New(specialCharacter As String, shortcutKey As Char, shortcutModifier As ModifierKey)
        Me.SpecialCharacter = specialCharacter
        Dim shortcutKeyData = DirectCast(AscW(Char.ToUpper(shortcutKey)), Windows.Forms.Keys)
        If Char.IsUpper(shortcutKey) Then
            shortcutKeyData = shortcutKeyData Or Windows.Forms.Keys.Shift
        End If
        Select Case shortcutModifier
            Case ModifierKey.Alt
                shortcutKeyData = shortcutKeyData Or Windows.Forms.Keys.Alt
            Case ModifierKey.Ctrl
                shortcutKeyData = shortcutKeyData Or Windows.Forms.Keys.Control
            Case ModifierKey.CtrlAlt
                shortcutKeyData = shortcutKeyData Or Windows.Forms.Keys.Alt Or Windows.Forms.Keys.Control
        End Select

        Me.ShortcutText = String.Format("{0} + {1}", shortcutModifier, shortcutKey)
        Me.ShortcutKeyData = shortcutKeyData
    End Sub
End Class

Public Enum ModifierKey
    None = 0
    Ctrl = 1
    Alt = 2
    CtrlAlt = 3
End Enum