﻿Public Class ShortcutHandler
    Public Event SpecialCharacterSelected(c As String)

    Dim specialCharGroups As IList(Of Group)
    Public WriteOnly Property SpecialCharacterGroups As IList(Of Group)
        Set(value As IList(Of Group))
            Me.specialCharGroups = value
        End Set
    End Property

    ' Microsoft.VisualBasic.Devices.Keyboard
    Private Declare Auto Function GetKeyState Lib "User32.dll" (KeyCode As Integer) As Short
    Private ReadOnly Property CapsLock() As Boolean
        Get
            Return (GetKeyState(20) And 1S) <> 0S
        End Get
    End Property

    Public Shadows Function ProcessCmdKey(ByRef msg As Windows.Forms.Message, keyData As Windows.Forms.Keys) As Boolean
        If specialCharGroups Is Nothing OrElse specialCharGroups.Count = 0 Then
            Return False
        End If

        Dim capsLockPressed As Boolean
        Try
            capsLockPressed = CapsLock
        Catch ex As Exception
            capsLockPressed = False
        End Try
        If capsLockPressed Then
            keyData = keyData Xor Windows.Forms.Keys.Shift
        End If
        For Each g In specialCharGroups
            For Each sc In g.SpecialCharacterShortcuts
                If keyData <> sc.ShortcutKeyData Then
                    Continue For
                End If

                RaiseEvent SpecialCharacterSelected(sc.SpecialCharacter)
                Return True
            Next
        Next

        Return False
    End Function
End Class
