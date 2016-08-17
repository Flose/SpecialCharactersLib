Imports System.Windows.Forms

Public Class SpecialCharactersForm
    Dim specialCharactersPanels As New List(Of SpecialCharactersPanel)

    Public Event SpecialCharacterSelected(c As String)

    Public Sub New()
        InitializeComponent()
    End Sub

    Public WriteOnly Property SpecialCharacterGroups As IList(Of Group)
        Set(groups As IList(Of Group))
            SuspendLayout()
            For Each p In specialCharactersPanels
                RemoveHandler p.SpecialCharacterSelected, AddressOf HandleSpecialCharacterSelected
            Next
            flpMain.Controls.Clear()
            specialCharactersPanels.Clear()
            Dim currentKeyboardLayoutId = InputLanguage.CurrentInputLanguage.Culture.KeyboardLayoutId And &HFF
            For Each group In groups
                If group.SpecialCharacterShortcuts.Count = 0 OrElse
                  (group.Culture IsNot Nothing AndAlso currentKeyboardLayoutId = (group.Culture.KeyboardLayoutId And &HFF)) Then
                    ' Hide group if its culture is the same as the current keyboard layout
                    Continue For
                End If

                Dim label As New Label
                With label
                    .Text = String.Format("{0}:", group.Name)
                    .AutoSize = True
                    .Margin = New Padding(2)
                End With
                flpMain.Controls.Add(label)

                Dim shortcutPanel As New SpecialCharactersPanel()
                With shortcutPanel
                    .SpecialCharacterShortcuts = group.SpecialCharacterShortcuts
                    If group.Culture IsNot Nothing Then
                        .Culture = group.Culture
                    End If
                End With
                flpMain.Controls.Add(shortcutPanel)
                specialCharactersPanels.Add(shortcutPanel)
                AddHandler shortcutPanel.SpecialCharacterSelected, AddressOf HandleSpecialCharacterSelected
            Next
            ResumeLayout()
        End Set
    End Property

    Private Sub HandleSpecialCharacterSelected(c As String)
        RaiseEvent SpecialCharacterSelected(c)
    End Sub
End Class
