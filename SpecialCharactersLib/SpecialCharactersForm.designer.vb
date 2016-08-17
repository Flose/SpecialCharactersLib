<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SpecialCharactersForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.flpMain = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'flpMain
        '
        Me.flpMain.AutoSize = True
        Me.flpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpMain.Location = New System.Drawing.Point(3, 3)
        Me.flpMain.Name = "flpMain"
        Me.flpMain.Size = New System.Drawing.Size(0, 0)
        Me.flpMain.TabIndex = 0
        Me.flpMain.WrapContents = False
        '
        'SpecialCharactersForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(210, 224)
        Me.Controls.Add(Me.flpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Location = New System.Drawing.Point(20000, 20000)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SpecialCharactersForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = ""
        Me.Text = "Special characters"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents flpMain As Windows.Forms.FlowLayoutPanel
End Class
