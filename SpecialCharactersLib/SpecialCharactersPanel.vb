Imports System.ComponentModel
Imports System.Windows.Forms

Public Class SpecialCharactersPanel
    Public Event SpecialCharacterSelected(c As String)

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Culture As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

    <DefaultValue(2)>
    Public Property ColumnCount As Integer
        Get
            Return tlpMain.ColumnCount
        End Get
        Set(value As Integer)
            tlpMain.ColumnCount = value
            If tlpMain.ColumnStyles.Count = value Then
                Exit Property
            End If

            tlpMain.ColumnStyles.Clear()
            For i = 0 To tlpMain.ColumnCount - 1
                tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            Next
            UpdateRowCount(tlpMain.Controls.Count)
        End Set
    End Property

    Dim specialChars As IList(Of SpecialCharacterShortcut)
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public WriteOnly Property SpecialCharacterShortcuts As IList(Of SpecialCharacterShortcut)
        Set(specialChars As IList(Of SpecialCharacterShortcut))
            Me.specialChars = specialChars
            tlpMain.Controls.Clear()
            UpdateRowCount(specialChars.Count)
            Dim handledChars As New Dictionary(Of String, Boolean) ' TODO replace with Set, when upgrading .Net framework
            ' Put chars together where both upper and lower case exists
            For Each sc In specialChars
                If handledChars.ContainsKey(sc.SpecialCharacter) OrElse sc.SpecialCharacter.Length <> 1 OrElse Not Char.IsUpper(sc.SpecialCharacter(0)) Then
                    Continue For
                End If
                Dim lowerCaseChar = Char.ToLower(sc.SpecialCharacter(0), Culture)
                For Each sc2 In specialChars
                    If handledChars.ContainsKey(sc2.SpecialCharacter) Or sc2.SpecialCharacter = sc.SpecialCharacter Or sc2.SpecialCharacter <> lowerCaseChar Then
                        Continue For
                    End If

                    handledChars.Add(sc.SpecialCharacter, True)
                    handledChars.Add(sc2.SpecialCharacter, True)
                    tlpMain.Controls.Add(CreateSpecialCharacterShortcutPanel(sc))
                    tlpMain.Controls.Add(CreateSpecialCharacterShortcutPanel(sc2))
                    Exit For
                Next
            Next
            ' Add remaining chars
            For Each sc In specialChars
                If handledChars.ContainsKey(sc.SpecialCharacter) Then
                    Continue For
                End If

                tlpMain.Controls.Add(CreateSpecialCharacterShortcutPanel(sc))
            Next
        End Set
    End Property

    Private Sub UpdateRowCount(count As Integer)
        tlpMain.RowCount = (count + ColumnCount - 1) \ ColumnCount
        If tlpMain.RowCount <> tlpMain.RowStyles.Count Then
            tlpMain.RowStyles.Clear()
            For i = 0 To tlpMain.RowCount - 1
                tlpMain.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
        End If
    End Sub

    Private Function CreateSpecialCharacterShortcutPanel(specialChar As SpecialCharacterShortcut) As Control
        Dim button As New Button
        With button
            .Text = specialChar.SpecialCharacter
            .Anchor = AnchorStyles.Left
            .FlatStyle = FlatStyle.Popup
            .AutoSize = True
            .AutoSizeMode = AutoSizeMode.GrowAndShrink
            .Margin = New Padding(0)
            AddHandler .Click, AddressOf ButtonClick
        End With

        Dim label As New Label
        With label
            .Text = specialChar.ShortcutText
            .Anchor = AnchorStyles.Left
            .AutoSize = True
            .Margin = New Padding(0)
        End With

        Dim panel As New FlowLayoutPanel
        With panel
            .Margin = New Padding(2, 2, 0, 2)
            .AutoSize = True
            .AutoSizeMode = AutoSizeMode.GrowAndShrink
            .WrapContents = False
            .Controls.Add(button)
            .Controls.Add(label)
        End With
        Return panel
    End Function

    Private Sub ButtonClick(sender As Object, e As EventArgs)
        RaiseEvent SpecialCharacterSelected(DirectCast(sender, Button).Text)
    End Sub
End Class
