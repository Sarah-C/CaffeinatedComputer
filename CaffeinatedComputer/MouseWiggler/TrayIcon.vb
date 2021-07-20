
Friend Class TrayIcon
    Inherits ApplicationContext

    'Component declarations
    Private WithEvents TrayIcon As NotifyIcon = Nothing
    Private TrayIconContextMenu As ContextMenuStrip = Nothing
    Private WithEvents CloseMenuItem As ToolStripMenuItem = Nothing

    Public Sub New()
        AddHandler Application.ApplicationExit, AddressOf OnApplicationExit
        InitializeComponent()
        TrayIcon.Visible = True
        Power.EnableConstantDisplayAndPower(True)
    End Sub

    Private Sub InitializeComponent()
        TrayIcon = New NotifyIcon()
        TrayIcon.Text = "Caffeinated computer!"

        TrayIcon.Icon = My.Resources.coffee

        TrayIconContextMenu = New ContextMenuStrip()
        CloseMenuItem = New ToolStripMenuItem()
        TrayIconContextMenu.SuspendLayout()

        ' TrayIconContextMenu
        Me.TrayIconContextMenu.Items.AddRange(New ToolStripItem() {Me.CloseMenuItem})
        Me.TrayIconContextMenu.Name = "TrayIconContextMenu"
        Me.TrayIconContextMenu.Size = New Size(153, 70)

        ' CloseMenuItem
        Me.CloseMenuItem.Name = "CloseMenuItem"
        Me.CloseMenuItem.Size = New Size(152, 22)
        Me.CloseMenuItem.Text = "Close the program"
        AddHandler CloseMenuItem.Click, AddressOf CloseMenuItem_Click

        TrayIconContextMenu.ResumeLayout(False)
        TrayIcon.ContextMenuStrip = TrayIconContextMenu
    End Sub

    Private Sub OnApplicationExit(ByVal sender As Object, ByVal e As EventArgs)
        TrayIcon.Visible = False
    End Sub

    Private Sub CloseMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        If MessageBox.Show("Do you really want to close 'Caffeinated Computer'?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Application.Exit()
            Power.EnableConstantDisplayAndPower(False)
        End If
    End Sub

End Class
