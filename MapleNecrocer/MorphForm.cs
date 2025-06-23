﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapleNecrocer;

public partial class MorphForm : Form
{
    public MorphForm()
    {
        InitializeComponent();
        Instance = this;
    }
    public static MorphForm Instance;
    public DataGridViewEx MorphListGrid;

    void CellClick(BaseDataGridView DataGrid, DataGridViewCellEventArgs e)
    {
        if (MapleChair.IsUse || TamingMob.IsUse)
            return;
        var MorphImg = DataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        if (MorphImg == "")
            return;
        Morph.Delete();
        Morph.Create(MorphImg);
        Morph.IsUse = true;
    }
    private void MorphForm_Shown(object sender, EventArgs e)
    {
        this.FormClosing += (s, e1) =>
        {
            this.Hide();
            e1.Cancel = true;
        };

        MorphListGrid = new(90, 164, 0, 0, 220, 400, true, tabControl1.TabPages[0]);
        MorphListGrid.Dock = DockStyle.Fill;
        MorphListGrid.SearchGrid.Dock = DockStyle.Fill;
        MorphListGrid.RowTemplate.Height = 80;
        MorphListGrid.Columns[1].Width = 80;
        ((DataGridViewImageColumn)MorphListGrid.Columns[1]).ImageLayout = DataGridViewImageCellLayout.Zoom;

        var Graphic = MorphListGrid.CreateGraphics();
        var Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
        Graphic.DrawString("Loading...", Font, Brushes.Black, 10, 50);

        MorphListGrid.CellClick += (s, e) =>
        {
            CellClick(MorphListGrid, e);
        };

        MorphListGrid.SearchGrid.CellClick += (s, e) =>
        {
            CellClick(MorphListGrid.SearchGrid, e);
        };

        string MorphName = "";
        Bitmap Bmp = null;
        foreach (var Iter in Wz.GetNode("Item/Consume/0221.img").Nodes)
        {
            if (!Iter.HasNode("spec/morph")) continue;
            if (Wz.HasNode("String/Consume.img/" + Iter.Text.IntID()))
                MorphName = Wz.GetNode("String/Consume.img/" + Iter.Text.IntID()).GetStr("name");
            else
                MorphName = "";
            string MorphID = Iter.GetNode("spec/morph").ToInt().ToString().PadLeft(4, '0');

            if (Wz.HasNode("Morph/" + MorphID + ".img"))
            {
                if (Wz.HasNode("Morph/" + MorphID + ".img/walk/0"))
                    Bmp = Wz.GetNode("Morph/" + MorphID + ".img/walk/0").ExtractPng();
            }
            MorphListGrid.Rows.Add(MorphID + ".img", Bmp, Iter.Text + "  " + "-" + "  " + MorphName);
        }

        for (int i = 0; i < MorphListGrid.Rows.Count; i++)
        {
            MorphListGrid.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            MorphListGrid.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.TopLeft;
        }
        MorphListGrid.Sort(MorphListGrid.Columns[0], ListSortDirection.Ascending);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Morph.Delete();
        Morph.IsUse = false;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        MorphListGrid.Search(textBox1.Text);
    }

    private void MorphForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Alt)
            e.Handled = true;
        if (!textBox1.Focused)
            ActiveControl = null;
    }
}
