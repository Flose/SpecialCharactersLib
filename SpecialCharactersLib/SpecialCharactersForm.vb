Imports System.Windows.Forms

Public Class SpecialCharactersForm
    Private specialCharactersPanels As New List(Of SpecialCharactersPanel)
    Private dockLocation As DockStyle
    Private previousLocation As Drawing.Point
    Private WithEvents ownerForm As Form

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
            If Me.Visible Then
                SetLocation()
            End If
        End Set
    End Property

    Private Sub HandleSpecialCharacterSelected(c As String)
        RaiseEvent SpecialCharacterSelected(c)
    End Sub

    Private Sub SetLocation()
        Dim screenArea = Screen.FromControl(ownerForm).WorkingArea
        If ownerForm.Top + ownerForm.Height > screenArea.Height Then
            Me.Top = screenArea.Height - Me.Height
        Else
            Me.Top = ownerForm.Top + (ownerForm.Height - Me.Height)
        End If
        If dockLocation = DockStyle.Left Then
            Me.Left = Math.Max(0, ownerForm.Left - Me.Width)
        Else
            If ownerForm.Right + Me.Width > screenArea.Width Then
                Me.Left = screenArea.Width - Me.Width
            Else
                Me.Left = ownerForm.Right
            End If
        End If
        previousLocation = Me.Location
    End Sub

    Private Sub ParentFormMove(ByVal sender As Object, ByVal e As EventArgs) Handles ownerForm.Move, ownerForm.Resize
        If previousLocation <> Me.Location Then
            ' Form has been moved out of its dock location
            Exit Sub
        End If

        SetLocation()
    End Sub

    Public Sub ShowDockedToForm(owner As Form, Optional dockLocation As DockStyle = DockStyle.Left)
        Me.dockLocation = dockLocation
        Me.ownerForm = owner
        SetLocation()
        Me.Show(owner)
    End Sub

    Private Sub SpecialCharactersForm_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Not Me.Visible Then
            Me.ownerForm = Nothing
        End If
    End Sub
End Class
