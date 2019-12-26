Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.Drawing
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports System.ComponentModel

Namespace GridView_PriorityRowCellStyle
	Public Class CustomGridView
		Inherits GridView
		Private priorityStyleEventArgs As New PriorityRowCellStyleEventArgs(Nothing, New AppearanceObject(), False)
		Public Sub New()
			MyBase.New()
		End Sub
		Protected Friend Overridable Sub SetGridControlAccessMetod(ByVal newControl As GridControl)
			SetGridControl(newControl)
		End Sub

		Private Shared ReadOnly _priorityRowCellStyle As Object = New Object()

		Public Overrides Sub Assign(ByVal v As BaseView, ByVal copyEvents As Boolean)
			If v Is Nothing Then
				Return
			End If
			BeginUpdate()
			Try
				Dim gv As CustomGridView = TryCast(v, CustomGridView)
				If gv IsNot Nothing Then
					If copyEvents Then
						Events.AddHandler(_priorityRowCellStyle, gv.Events(_priorityRowCellStyle))
					End If
				End If
			Finally
				EndUpdate()
			End Try
		End Sub

		<Description("Enables the appearance settings of individual cells to be changed, or merged with Format Conditon"), Category("Appearance")> _
		Public Custom Event PriorityRowCellStyle As PriorityRowCellStyleEventHandler
			AddHandler(ByVal value As PriorityRowCellStyleEventHandler)
				Me.Events.AddHandler(_priorityRowCellStyle, value)
			End AddHandler
			RemoveHandler(ByVal value As PriorityRowCellStyleEventHandler)
				Me.Events.RemoveHandler(_priorityRowCellStyle, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As PriorityRowCellStyleEventArgs)
			End RaiseEvent
		End Event
		Protected Friend Overridable Function RaisePriorityRowCellStyle(ByVal cellInfo As GridCellInfo, ByVal appearance As AppearanceObject, ByVal overrydeFormatCondition As Boolean) As AppearanceObject
			priorityStyleEventArgs.OverrydeFormatCondition = False
			Dim handler As PriorityRowCellStyleEventHandler = CType(Events(_priorityRowCellStyle), PriorityRowCellStyleEventHandler)
			If handler Is Nothing Then
				Return appearance
			End If
			priorityStyleEventArgs.Appearance = TryCast(appearance.Clone(), AppearanceObject)
			priorityStyleEventArgs.SetCellInfo(cellInfo)
			handler(Me, priorityStyleEventArgs)
			Return priorityStyleEventArgs.Appearance
		End Function

		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "CustomGridView"
			End Get
		End Property
		Protected Friend Overridable Function GetPriorityRowCellStyle(ByVal cellInfo As GridCellInfo, ByVal appearance As AppearanceObject) As AppearanceObject
			Return RaisePriorityRowCellStyle(cellInfo, appearance, False)
		End Function
		Protected Friend Overridable ReadOnly Property OverrideFormatCondition() As Boolean
			Get
				Return priorityStyleEventArgs.OverrydeFormatCondition
			End Get
		End Property
	End Class

	Public Delegate Sub PriorityRowCellStyleEventHandler(ByVal sender As Object, ByVal e As PriorityRowCellStyleEventArgs)
	Public Class PriorityRowCellStyleEventArgs
		Inherits EventArgs
		Private overrydeFormatCondition_Renamed As Boolean
		Private cellInfo_Renamed As GridCellInfo
		Private appearance_Renamed As AppearanceObject
		Public Sub New(ByVal cellInfo As GridCellInfo, ByVal appearance As AppearanceObject, ByVal overrydeFormatCondition As Boolean)
			MyBase.New()
			Me.overrydeFormatCondition_Renamed = overrydeFormatCondition
			Me.cellInfo_Renamed = cellInfo
			Me.appearance_Renamed = appearance
		End Sub
		Public Property OverrydeFormatCondition() As Boolean
			Get
				Return overrydeFormatCondition_Renamed
			End Get
			Set(ByVal value As Boolean)
				overrydeFormatCondition_Renamed = value
			End Set
		End Property
		Public ReadOnly Property CellInfo() As GridCellInfo
			Get
				Return cellInfo_Renamed
			End Get
		End Property
		Protected Friend Sub SetCellInfo(ByVal cellInfo As GridCellInfo)
			Me.cellInfo_Renamed = cellInfo
		End Sub
		Public Property Appearance() As AppearanceObject
			Get
				Return appearance_Renamed
			End Get
			Set(ByVal value As AppearanceObject)
				appearance_Renamed = value
			End Set
		End Property

	End Class
	Public Class CustomGridControl
		Inherits GridControl
		Public Sub New()
			MyBase.New()
		End Sub
		Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
			MyBase.RegisterAvailableViewsCore(collection)
			collection.Add(New CustomGridInfoRegistrator())
		End Sub
		Protected Overrides Function CreateDefaultView() As BaseView
			Return CreateView("CustomGridView")
		End Function

	End Class
	Public Class CustomGridInfoRegistrator
		Inherits GridInfoRegistrator
		Public Sub New()
			MyBase.New()
		End Sub
		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return "CustomGridView"
			End Get
		End Property
		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
			Dim view As New CustomGridView()
			view.SetGridControlAccessMetod(grid)
			Return view
		End Function


		Public Overrides Function CreateViewInfo(ByVal view As BaseView) As DevExpress.XtraGrid.Views.Base.ViewInfo.BaseViewInfo
			Return New CustomGridViewInfo(TryCast(view, GridView))
		End Function
	End Class
	Public Class CustomGridViewInfo
		Inherits GridViewInfo
        Public Sub New(ByVal gridView As GridView)
            MyBase.New(gridView)
        End Sub
        Protected Overrides Sub UpdateCellAppearanceCore(cell As GridCellInfo, Optional allowCache As Boolean = True, Optional allowCondition As Boolean = True, Optional cellCondition As AppearanceObjectEx = Nothing)
            MyBase.UpdateCellAppearanceCore(cell, allowCache, allowCondition, cellCondition)
            If cell.IsDataCell Then
                If (cell.State And GridRowCellState.FocusedCell) = 0 OrElse (Not View.IsEditing) Then
                    Dim condition As AppearanceObjectEx = Nothing
                    Dim priorityStyle As AppearanceObject = (CType(View, CustomGridView)).GetPriorityRowCellStyle(cell, cell.Appearance)
                    If priorityStyle IsNot cell.Appearance Then
                        If Not (CType(View, CustomGridView)).OverrideFormatCondition Then
                            condition = cell.RowInfo.ConditionInfo.GetCellAppearance(cell.Column)
                            If condition IsNot Nothing Then
                                priorityStyle = MergeAppearences(priorityStyle, condition, cell.Appearance)
                            End If
                        End If
                        cell.Appearance = priorityStyle
                    End If
                End If
            End If
        End Sub

        Protected Overridable Function MergeAppearences(ByVal target As AppearanceObject, ByVal source As AppearanceObject, ByVal current As AppearanceObject) As AppearanceObject
			If source.Options.UseBackColor Then
				If (Not source.BackColor.IsEmpty) AndAlso source.BackColor = current.BackColor Then
					target.BackColor = source.BackColor
				End If
				If (Not source.BackColor2.IsEmpty) AndAlso source.BackColor2 = current.BackColor2 Then
					target.BackColor2 = source.BackColor2
				End If
			End If
			If source.Options.UseBorderColor AndAlso source.BorderColor = current.BorderColor Then
				target.BorderColor = source.BorderColor
			End If
			If source.Options.UseFont AndAlso source.Font Is current.Font Then
				target.Font = source.Font
			End If
			If source.Options.UseForeColor AndAlso source.ForeColor = current.ForeColor Then
				target.ForeColor = source.ForeColor
			End If
            If source.Options.UseImage And source.Image Is current.Image Then
                target.Image = source.Image
            End If
			If source.Options.UseTextOptions AndAlso source.TextOptions Is current.TextOptions Then
				target.TextOptions.Assign(source.TextOptions)
			End If
			If source.GradientMode = current.GradientMode Then
				target.GradientMode = source.GradientMode
			End If
			Return target
		End Function
	End Class
End Namespace
