using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using System.ComponentModel;

namespace GridView_PriorityRowCellStyle
{
    public class CustomGridView : GridView
    {
        PriorityRowCellStyleEventArgs priorityStyleEventArgs = new PriorityRowCellStyleEventArgs(null, new AppearanceObject(), false);
        public CustomGridView() : base() { }
        protected internal virtual void SetGridControlAccessMetod(GridControl newControl) { SetGridControl(newControl); }

        private static readonly object _priorityRowCellStyle = new object();

        public override void Assign(BaseView v, bool copyEvents)
        {
            if (v == null) return;
            BeginUpdate();
            try
            {
                CustomGridView gv = v as CustomGridView;
                if (gv != null)
                    if (copyEvents)
                        Events.AddHandler(_priorityRowCellStyle, gv.Events[_priorityRowCellStyle]);
            }
            finally { EndUpdate(); }
        }

        [Description("Enables the appearance settings of individual cells to be changed, or merged with Format Conditon"), Category("Appearance")]
        public event PriorityRowCellStyleEventHandler PriorityRowCellStyle
        {
            add { this.Events.AddHandler(_priorityRowCellStyle, value); }
            remove { this.Events.RemoveHandler(_priorityRowCellStyle, value); }
        }
        protected internal virtual AppearanceObject RaisePriorityRowCellStyle(GridCellInfo cellInfo, AppearanceObject appearance, bool overrydeFormatCondition)
        {
            priorityStyleEventArgs.OverrydeFormatCondition = false;
            PriorityRowCellStyleEventHandler handler = (PriorityRowCellStyleEventHandler)Events[_priorityRowCellStyle];
            if (handler == null) return appearance;
            priorityStyleEventArgs.Appearance = appearance.Clone() as AppearanceObject;
            priorityStyleEventArgs.SetCellInfo(cellInfo);
            handler(this, priorityStyleEventArgs);
            return priorityStyleEventArgs.Appearance;
        }

        protected override string ViewName { get { return "CustomGridView"; } }
        protected internal virtual AppearanceObject GetPriorityRowCellStyle(GridCellInfo cellInfo, AppearanceObject appearance)
        {
            return RaisePriorityRowCellStyle(cellInfo , appearance, false);
        }
        protected internal virtual bool OverrideFormatCondition { get { return priorityStyleEventArgs.OverrydeFormatCondition; } }
    }

    public delegate void PriorityRowCellStyleEventHandler(object sender, PriorityRowCellStyleEventArgs e);
    public class PriorityRowCellStyleEventArgs : EventArgs
    {
        bool overrydeFormatCondition;
        GridCellInfo cellInfo;
        AppearanceObject appearance;
        public PriorityRowCellStyleEventArgs(GridCellInfo cellInfo, AppearanceObject appearance, bool overrydeFormatCondition)
            : base()
        {
            this.overrydeFormatCondition = overrydeFormatCondition;
            this.cellInfo = cellInfo;
            this.appearance = appearance;
        }
        public bool OverrydeFormatCondition
        {
            get { return overrydeFormatCondition; }
            set { overrydeFormatCondition = value; }
        }
        public GridCellInfo CellInfo { get { return cellInfo; } }
        protected internal void SetCellInfo(GridCellInfo cellInfo) { this.cellInfo = cellInfo; }
        public AppearanceObject Appearance
        {
            get { return appearance; }
            set { appearance = value; }
        }

    }
    public class CustomGridControl : GridControl
    {
        public CustomGridControl() : base() { }
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridInfoRegistrator());
        }
        protected override BaseView CreateDefaultView() { return CreateView("CustomGridView"); }

    }
    public class CustomGridInfoRegistrator : GridInfoRegistrator
    {
        public CustomGridInfoRegistrator() : base() { }
        public override string ViewName { get { return "CustomGridView"; } }
        public override BaseView CreateView(GridControl grid)
        {
            CustomGridView view = new CustomGridView();
            view.SetGridControlAccessMetod(grid);
            return view;
        }


        public override DevExpress.XtraGrid.Views.Base.ViewInfo.BaseViewInfo CreateViewInfo(BaseView view) { return new CustomGridViewInfo(view as GridView); }
    }
    public class CustomGridViewInfo : GridViewInfo
    {
        public CustomGridViewInfo(GridView gridView) : base(gridView) { }
        protected override void UpdateCellAppearanceCore(GridCellInfo cell, bool allowCache = true, bool allowCondition = true, AppearanceObjectEx cellCondition = null)
        {
            base.UpdateCellAppearanceCore(cell, allowCache, allowCondition, cellCondition);
            if (cell.IsDataCell)
                if ((cell.State & GridRowCellState.FocusedCell) == 0 || !View.IsEditing)
                {
                    AppearanceObjectEx condition = null;
                    AppearanceObject priorityStyle = ((CustomGridView)View).GetPriorityRowCellStyle(cell, cell.Appearance);
                    if (priorityStyle != cell.Appearance)
                    {
                        if (!((CustomGridView)View).OverrideFormatCondition)
                        {
                            condition = cell.RowInfo.ConditionInfo.GetCellAppearance(cell.Column);
                            if (condition != null)
                                priorityStyle = MergeAppearences(priorityStyle, condition, cell.Appearance);
                        }
                        cell.Appearance = priorityStyle;
                    }
                }
        }

        protected virtual AppearanceObject MergeAppearences(AppearanceObject target, AppearanceObject source, AppearanceObject current)
        {
            if (source.Options.UseBackColor)
            {
                if (!source.BackColor.IsEmpty && source.BackColor == current.BackColor)
                    target.BackColor = source.BackColor;
                if (!source.BackColor2.IsEmpty && source.BackColor2 == current.BackColor2)
                    target.BackColor2 = source.BackColor2;
            }
            if (source.Options.UseBorderColor && source.BorderColor == current.BorderColor)
                target.BorderColor = source.BorderColor;
            if (source.Options.UseFont && source.Font == current.Font)
                target.Font = source.Font;
            if (source.Options.UseForeColor && source.ForeColor == current.ForeColor)
                target.ForeColor = source.ForeColor;
            if (source.Options.UseImage && source.Image == current.Image)
                target.Image = source.Image;
            if (source.Options.UseTextOptions && source.TextOptions == current.TextOptions)
                target.TextOptions.Assign(source.TextOptions);
            if (source.GradientMode == current.GradientMode)
                target.GradientMode = source.GradientMode;
            return target;
        }
    }
}
