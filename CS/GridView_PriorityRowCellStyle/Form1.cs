using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;

namespace GridView_PriorityRowCellStyle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ArrayList myUsers = new ArrayList();
        private void Form1_Load(object sender, EventArgs e)
        {
            myUsers.Add(new User("Artur", 100000));
            myUsers.Add(new User("Bill", 1000));
            myUsers.Add(new User("Charli", -100));
            myUsers.Add(new User("Den", 234));
            myUsers.Add(new User("Elf", -2313));
            customGridControl1.DataSource = myUsers;
            gridColumn1.FieldName = "Name";
            gridColumn2.FieldName = "Budget";
        }

        private void customGridView1_PriorityRowCellStyle(object sender, PriorityRowCellStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.Azure  ;
            e.Appearance.BackColor2 = Color.GreenYellow;
            e.Appearance.Options.UseBackColor = true;
            e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
           // e.OverrydeFormatCondition = true;
        }
    }
    class User
    {
        string name;
        decimal budget;
        public User(string name, decimal budget)
        {
            this.name = name;
            this.budget = budget;
        }
        public string Name { get { return name; } }
        public decimal Budget
        { get { return budget; } }
    }
}