Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports DevExpress.XtraGrid.Views.Grid

Namespace GridView_PriorityRowCellStyle
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private myUsers As New ArrayList()
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			myUsers.Add(New User("Artur", 100000))
			myUsers.Add(New User("Bill", 1000))
			myUsers.Add(New User("Charli", -100))
			myUsers.Add(New User("Den", 234))
			myUsers.Add(New User("Elf", -2313))
			customGridControl1.DataSource = myUsers
			gridColumn1.FieldName = "Name"
			gridColumn2.FieldName = "Budget"
		End Sub

		Private Sub customGridView1_PriorityRowCellStyle(ByVal sender As Object, ByVal e As PriorityRowCellStyleEventArgs) Handles customGridView1.PriorityRowCellStyle
			e.Appearance.BackColor = Color.Azure
			e.Appearance.BackColor2 = Color.GreenYellow
			e.Appearance.Options.UseBackColor = True
			e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
		   ' e.OverrydeFormatCondition = true;
		End Sub
	End Class
	Friend Class User
		Private name_Renamed As String
		Private budget_Renamed As Decimal
		Public Sub New(ByVal name As String, ByVal budget As Decimal)
			Me.name_Renamed = name
			Me.budget_Renamed = budget
		End Sub
		Public ReadOnly Property Name() As String
			Get
				Return name_Renamed
			End Get
		End Property
		Public ReadOnly Property Budget() As Decimal
			Get
				Return budget_Renamed
			End Get
		End Property
	End Class
End Namespace