Imports Microsoft.VisualBasic
Imports System
Namespace GridView_PriorityRowCellStyle
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim styleFormatCondition1 As New DevExpress.XtraGrid.StyleFormatCondition()
			Me.gridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.customGridControl1 = New GridView_PriorityRowCellStyle.CustomGridControl()
			Me.customGridView1 = New GridView_PriorityRowCellStyle.CustomGridView()
			Me.gridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
			CType(Me.customGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.customGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridColumn2
			' 
			Me.gridColumn2.Caption = "gridColumn2"
			Me.gridColumn2.Name = "gridColumn2"
			Me.gridColumn2.Visible = True
			Me.gridColumn2.VisibleIndex = 1
			' 
			' customGridControl1
			' 
			Me.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customGridControl1.Location = New System.Drawing.Point(0, 0)
			Me.customGridControl1.MainView = Me.customGridView1
			Me.customGridControl1.Name = "customGridControl1"
			Me.customGridControl1.Size = New System.Drawing.Size(624, 345)
			Me.customGridControl1.TabIndex = 0
			Me.customGridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.customGridView1})
			' 
			' customGridView1
			' 
			Me.customGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.gridColumn1, Me.gridColumn2})
			styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Red
			styleFormatCondition1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
			styleFormatCondition1.Appearance.Options.UseBackColor = True
			styleFormatCondition1.Column = Me.gridColumn2
			styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual
			styleFormatCondition1.Value1 = "0"
			Me.customGridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() { styleFormatCondition1})
			Me.customGridView1.GridControl = Me.customGridControl1
			Me.customGridView1.Name = "customGridView1"
'			Me.customGridView1.PriorityRowCellStyle += New GridView_PriorityRowCellStyle.PriorityRowCellStyleEventHandler(Me.customGridView1_PriorityRowCellStyle);
			' 
			' gridColumn1
			' 
			Me.gridColumn1.Caption = "gridColumn1"
			Me.gridColumn1.Name = "gridColumn1"
			Me.gridColumn1.Visible = True
			Me.gridColumn1.VisibleIndex = 0
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(624, 345)
			Me.Controls.Add(Me.customGridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.customGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.customGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private customGridControl1 As CustomGridControl
		Private WithEvents customGridView1 As CustomGridView
		Private gridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
		Private gridColumn2 As DevExpress.XtraGrid.Columns.GridColumn

	End Class
End Namespace

