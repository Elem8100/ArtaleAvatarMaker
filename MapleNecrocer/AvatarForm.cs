﻿
using Aspose.PSD.FileFormats.Psd;
using Aspose.PSD.FileFormats.Psd;
using Aspose.PSD.FileFormats.Psd.Layers;
using DevComponents.DotNetBar;
using Manina.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using WzComparerR2.WzLib;
using static System.Windows.Forms.AxHost;
using BlendMode = MonoGame.SpriteEngine.BlendMode;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MapleNecrocer;
public partial class AvatarForm : Form
{
    public AvatarForm()
    {
        InitializeComponent();
        AvatarFormDraw = new();
        AvatarFormDraw.Width = 260;
        AvatarFormDraw.Height = 200;
        AvatarFormDraw.Left = 819;
        AvatarFormDraw.Top = 2;
        AvatarFormDraw.Parent = this;
        AvatarFormDraw.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

        FrameListDraw = new();
        FrameListDraw.Width = 512;
        FrameListDraw.Height = 512;
        FrameListDraw.Left = 0;
        FrameListDraw.Top = 0;
        FrameListDraw.Parent = panel2;
        Instance = this;
        (tabControl1.TabPages[5] as TabPage).Enabled = false;
    }
    private string[] AllFrames = {
        "walk1.0", "walk1.1", "walk1.2", "walk1.3",
        "stand1.0", "stand1.1", "stand1.2",
        "walk2.0", "walk2.1", "walk2.2", "walk2.3",
        "stand2.0", "stand2.1", "stand2.2",
        "alert.0", "alert.1", "alert.2",
        "heal.0", "heal.1", "heal.2",
        "rope.0", "rope.1", "rope.1",
        "ladder.0", "ladder.1",
        "fly.0", "fly.1",
        "proneStab.0", "proneStab.1",
        "jump.0",
        "sit.0",
        "prone.0",

        "shootF.0", "shootF.1", "shootF.2",
        "shoot1.0", "shoot1.1", "shoot1.2",
        "shoot2.0", "shoot2.1", "shoot2.2", "shoot2.3", "shoot2.4",

        "swingOF.0", "swingOF.1", "swingOF.2", "swingOF.3",
        "swingO1.0", "swingO1.1", "swingO1.2",
        "swingO2.0", "swingO2.1", "swingO2.2",
        "swingO3.0", "swingO3.1", "swingO3.2",

        "swingTF.0", "swingTF.1", "swingTF.2", "swingTF.3",
        "swingT1.0", "swingT1.1", "swingT1.2",
        "swingT2.0", "swingT2.1", "swingT2.2",
        "swingT3.0", "swingT3.1", "swingT3.2",

        "swingPF.0", "swingPF.1", "swingPF.2", "swingPF.3",
        "swingP1.0", "swingP1.1", "swingP1.2",
        "swingP2.0", "swingP2.1", "swingP2.2",

        "stabOF.0", "stabOF.1", "stabOF.2",
        "stabTF.0", "stabTF.1", "stabTF.2", "stabTF.3",

        "stabT1.0", "stabT1.1", "stabT1.2",
        "stabO1.0", "stabO1.1", "stabO2.0", "stabO2.1",
        "stabT2.0", "stabT2.1", "stabT2.2",
    };
    private List<Point> sheetPoint;

    private static bool LoadedFrameList;
    public static bool SelectedFrame;
    public static int SelectedFrameNum;
    public static string SelectedAction;
    public static int AdjustX = 128, AdjustY = 128, AdjustW = 256, AdjustH = 256;
    public static bool ChangeExpressionListBox;
    public static AvatarForm Instance;
    public static AvatarFormDraw AvatarFormDraw;
    public static FrameListDraw FrameListDraw;
    public static bool useCustomBound = false;
    public static Rectangle AvatarBound;
    public static Rectangle CurrentSpriteBound;

    static bool ShowToolTip = true;
    ImageListView[] ImageGrids = new ImageListView[21];
    ImageListView AvatarListView;
    public DataGridViewEx Inventory;
    public DataGridViewEx SearchGrid;
    private List<Rectangle> FrameBound = new();
    public static bool debugDraw = false;
    public Dictionary<string, (int RedPointX, int RedPointY, int DrawPosX, int DrawPosY)> OriginData = new();
    List<string> FileNameList = new();
    public List<string> FrameNameList = new();

    public List<ImageForm> ImageFormList = new();
    void AddInventory()
    {
        if (Inventory.Columns.Count == 4)
        {
            Inventory.Columns.RemoveAt(3);
        }
        DataGridViewButtonColumn dgvButton = new DataGridViewButtonColumn();
        dgvButton.Width = 29;
        dgvButton.UseColumnTextForButtonValue = true;
        dgvButton.Text = "X";
        dgvButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dgvButton.CellTemplate.Style.Padding = new System.Windows.Forms.Padding(2, 10, 2, 10);
        Inventory.Columns.Add(dgvButton);

        Inventory.Rows.Clear();

        Player.EqpList.Sort();
        for (int i = 0; i < Player.EqpList.Count; i++)
        {
            var ID = Player.EqpList[i];

            string Dir = Equip.GetDir(ID);
            string Name = "";
            if (Wz.HasNode("String/Eqp.img"))
                Name = Wz.GetNodeA("String/Eqp.img/Eqp").GetStr(Dir + ID.IntID() + "/name");
            else if (Wz.HasNode("String/Item.img/Eqp"))
                Name = Wz.GetNodeA("String/Item.img/Eqp").GetStr(Dir + ID.IntID() + "/name");
            var Entry = Wz.GetNodeA("Character/" + Dir + ID + ".img");
            PartName PartName = Equip.GetPart(ID);

            Bitmap Bmp;
            switch (PartName)
            {
                case PartName.Head:
                    Bmp = Entry.GetBmp("front/head");
                    break;
                case PartName.Body:
                    Bmp = Entry.GetBmp("stand1/0/body");
                    break;
                case PartName.Hair:
                    Bmp = Entry.GetBmp("default/hairOverHead");
                    break;
                case PartName.Face:
                    Bmp = Entry.GetBmp("default/face");
                    break;
                default:
                    Bmp = Entry.GetBmp("info/icon");
                    break;
            }
            Inventory.Rows.Add(ID, Bmp, Name);

            SelectedFrame = false;
            timer1.Enabled = true;
        }

        for (int i = 0; i < Inventory.Rows.Count; i++)
        {
            Inventory.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Inventory.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.TopLeft;
        }
        if (Inventory.Columns.Count > 2)
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 1000, 0);
            Inventory.Rows[0].Cells[3].Style = dataGridViewCellStyle2;
            Inventory.Rows[1].Cells[3].Style = dataGridViewCellStyle2;
        }
    }

    void AddEqps(string EqpID)
    {
        string NewID = EqpID;
        PartName NewPart = Equip.GetPart(EqpID);
        if (NewPart == PartName.SitTamingMob)
            return;

        for (int i = Player.EqpList.Count - 1; i >= 0; i--)
        {
            var OldPart = Equip.GetPart(Player.EqpList[i]);

            if (NewPart == OldPart)
            {
                SetEffect.Remove(Player.EqpList[i]);
                ItemEffect.Remove(Player.EqpList[i]);
                Player.EqpList.RemoveAt(i);
            }
            if ((NewPart == PartName.Weapon) && (OldPart == PartName.CashWeapon))
                Player.EqpList.RemoveAt(i);
            if ((NewPart == PartName.CashWeapon) && (OldPart == PartName.Weapon))
                Player.EqpList.RemoveAt(i);
            if ((NewPart == PartName.Coat) || (NewPart == PartName.Pants))
            {
                if (OldPart == PartName.Longcoat)
                    Player.EqpList.RemoveAt(i);
            }
            if (NewPart == PartName.Longcoat)
            {
                if ((OldPart == PartName.Coat) || (OldPart == PartName.Pants))
                    Player.EqpList.RemoveAt(i);
            }
        }

        Player.EqpList.Add(NewID);

        if (ItemEffect.AllList.Contains(EqpID))
            ItemEffect.Create(EqpID, EffectType.Equip);

        if (SetEffect.AllList.ContainsKey(EqpID))
            SetEffect.Create(EqpID);

    }
    void ResetDyeGrid()
    {
        DyeGrid.Rows.Clear();
        DyeGrid.Columns.Clear();
        for (int i = 0; i <= 16; i++)
        {
            var Icon = new DataGridViewImageColumn();
            DyeGrid.Columns.AddRange(Icon);
        }

        for (int i = 0; i < Inventory.RowCount; i++)
        {
            if (Inventory.Rows[i].Cells[1].Value is Bitmap)
            {
                var Bmp = (Bitmap)Inventory.Rows[i].Cells[1].Value;
                DyeGrid.Rows.Add(Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp, Bmp);
            }
        }

        for (int Col = 0; Col <= 16; Col++)
        {
            for (int Row = 0; Row < DyeGrid.RowCount; Row++)
            {
                var Bmp = (Bitmap)DyeGrid.Rows[Row].Cells[Col].Value;
                if (Col <= 15)
                    ImageFilter.HSL(ref Bmp, Col * 25);
                else
                    ImageFilter.HSL(ref Bmp, 0, -200);
                DyeGrid.Rows[Row].Cells[Col].Value = Bmp;
            }
            DyeGrid.Columns[Col].Width = 45;
        }
    }

    void ResetDyeGrid2()
    {
        DyeGrid2.Rows.Clear();
        DyeGrid2.Columns.Clear();
        if (DyeGrid2.Columns.Count == 0)
        {
            foreach (DataGridViewColumn dgvc in Inventory.Columns)
            {
                DyeGrid2.Columns.Add(dgvc.Clone() as DataGridViewColumn);
            }
        }

        DataGridViewRow row = new DataGridViewRow();

        for (int i = 0; i < Inventory.Rows.Count; i++)
        {
            row = (DataGridViewRow)Inventory.Rows[i].Clone();
            int intColIndex = 0;
            foreach (DataGridViewCell cell in Inventory.Rows[i].Cells)
            {
                row.Cells[intColIndex].Value = cell.Value;
                intColIndex++;
            }
            DyeGrid2.Rows.Add(row);
        }

        DyeGrid2.AllowUserToAddRows = false;
        DyeGrid2.Refresh();
        DyeGrid2.Columns[3].Visible = false;
    }

    private void AvatarForm_Load(object sender, EventArgs e)
    {

        this.FormClosing += (s, e1) =>
        {
            this.Hide();
            e1.Cancel = true;

            ChangeExpressionListBox = false;
        };

        var license = new Aspose.PSD.License();
        license.SetLicense(@"License-pro.txt");
        //Artale 紅點位置資料
        int OffsetY = 0;

        OriginData.Add("walk1.0", (128, 130 + OffsetY, 128, 130 + OffsetY));
        OriginData.Add("walk1.1", (378, 130 + OffsetY, 378, 130 + OffsetY));
        OriginData.Add("walk1.2", (628, 130 + OffsetY, 628, 130 + OffsetY));
        OriginData.Add("walk1.3", (878, 132 + OffsetY, 878, 132 - 2 + OffsetY));
        //
        OriginData.Add("stabO1.0", (1386, 133 + OffsetY, 1386 - 1, 133 - 3 + OffsetY));
        OriginData.Add("stabO1.1", (1639, 135 + OffsetY, 1639 + 8, 135 - 5 + OffsetY));
        //
        OriginData.Add("stabO2.0", (2137, 130 + OffsetY, 2137 - 1, 130 + OffsetY));
        OriginData.Add("stabO2.1", (2390, 134 + OffsetY, 2390 + 10, 134 - 4 + OffsetY));
        //
        OriginData.Add("walk2.0", (128, 380 + OffsetY, 128, 380 + OffsetY));
        OriginData.Add("walk2.1", (378, 380 + OffsetY, 378, 380 + OffsetY));
        OriginData.Add("walk2.2", (628, 380 + OffsetY, 628, 380 + OffsetY));
        OriginData.Add("walk2.3", (878, 382 + OffsetY, 878, 382 - 2 + OffsetY));
        //
        OriginData.Add("stabT1.0", (1389, 381 + OffsetY, 1389 + 3, 381 - 1 + OffsetY));
        OriginData.Add("stabT1.1", (1626, 382 + OffsetY, 1626 + 18, 382 - 2 + OffsetY));
        OriginData.Add("stabT1.2", (1886, 386 + OffsetY, 1886 + 23, 386 - 6 + OffsetY));
        //
        OriginData.Add("stand1.0", (126, 629 + OffsetY, 126 + 2, 629 + 1 + OffsetY));
        OriginData.Add("stand1.1", (377, 630 + OffsetY, 377 + 1, 630 + OffsetY));
        OriginData.Add("stand1.2", (628, 629 + OffsetY, 628, 629 + 1 + OffsetY));
        //
        OriginData.Add("stabT2.0", (1387, 632 + OffsetY, 1387 - 4, 632 - 2 + OffsetY));
        OriginData.Add("stabT2.1", (1630, 634 + OffsetY, 1630 + 4, 634 - 4 + OffsetY));
        OriginData.Add("stabT2.2", (1889, 636 + OffsetY, 1889 + 13, 636 - 6 + OffsetY));
        //
        OriginData.Add("stand2.0", (126, 879 + OffsetY, 126 + 2, 879 + 1 + OffsetY));
        OriginData.Add("stand2.1", (377, 880 + OffsetY, 377 + 1, 880 + OffsetY));
        OriginData.Add("stand2.2", (628, 879 + OffsetY, 628, 879 + 1 + OffsetY));
        //
        OriginData.Add("proneStab.0", (1396, 897 + OffsetY, 1396 + 1, 897 - 17 + OffsetY));
        OriginData.Add("proneStab.1", (1646, 897 + OffsetY, 1646 + 1, 897 - 17 + OffsetY));
        //
        OriginData.Add("alert.0", (139, 1132 + OffsetY, 139 - 3, 1132 - 2 + OffsetY));
        OriginData.Add("alert.1", (389, 1131 + OffsetY, 389 - 3, 1131 - 1 + OffsetY));
        OriginData.Add("alert.2", (639, 1130 + OffsetY, 639 - 3, 1130 + OffsetY));
        //
        OriginData.Add("stabTF.2", (1891, 1120 + OffsetY, 1891 + 12, 1120 + 26 + OffsetY));
        //
        OriginData.Add("swingO1.0", (132, 1384 + OffsetY, 132 - 5, 1384 - 4 + OffsetY));
        OriginData.Add("swingO1.1", (384, 1380 + OffsetY, 384 + 2, 1380 + OffsetY));

        ///******************************************
        OriginData.Add("swingO1.2", (621, 1383 + OffsetY, 621 + 23, 1383 - 3 + OffsetY));
        //
        OriginData.Add("fly.0", (1384, 1376 + OffsetY, 1384 - 2, 1376 + 4 + OffsetY));
        OriginData.Add("fly.1", (1638, 1376 + OffsetY, 1638 - 2, 1376 + 4 + OffsetY));
        //
        OriginData.Add("jump.0", (2132, 1382 + OffsetY, 2132 - 1, 1382 - 2 + OffsetY));
        //
        OriginData.Add("swingO2.0", (138, 1630 + OffsetY, 138, 1630 + OffsetY));
        OriginData.Add("swingO2.1", (385, 1632 + OffsetY, 385 + 2, 1632 - 2 + OffsetY));
        OriginData.Add("swingO2.2", (633, 1634 + OffsetY, 633 + 5, 1634 - 4 + OffsetY));
        //
        OriginData.Add("shoot1.0", (1390, 1631 + OffsetY, 1390 - 2, 1631 - 1 + OffsetY));
        OriginData.Add("shoot1.1", (1640, 1631 + OffsetY, 1640 - 2, 1631 - 1 + OffsetY));
        OriginData.Add("shoot1.2", (1890, 1631 + OffsetY, 1890 - 2, 1631 - 1 + OffsetY));
        //
        OriginData.Add("shootF.0", (2391, 1631 + OffsetY, 2391 - 3, 1631 - 1 + OffsetY));
        OriginData.Add("shootF.1", (2638, 1632 + OffsetY, 2638 - 3, 1632 - 2 + OffsetY));
        //
        OriginData.Add("swingO3.0", (131, 1884 + OffsetY, 131 + 2, 1884 - 4 + OffsetY));
        OriginData.Add("swingO3.1", (384, 1883 + OffsetY, 384 + 18, 1883 - 3 + OffsetY));
        OriginData.Add("swingO3.2", (628, 1884 + OffsetY, 628 + 24, 1884 - 4 + OffsetY));
        //
        OriginData.Add("shoot2.0", (1387, 1882 + OffsetY, 1387, 1882 - 2 + OffsetY));
        OriginData.Add("shoot2.1", (1637, 1882 + OffsetY, 1637, 1882 - 2 + OffsetY));
        OriginData.Add("shoot2.2", (1887, 1882 + OffsetY, 1887, 1882 - 2 + OffsetY));
        OriginData.Add("shoot2.3", (2137, 1882 + OffsetY, 2137, 1882 - 2 + OffsetY));
        OriginData.Add("shoot2.4", (2387, 1882 + OffsetY, 2387, 1882 - 2 + OffsetY));
        //
        OriginData.Add("swingOF.0", (135, 2133 + OffsetY, 135 + 1, 2133 - 3 + OffsetY));
        OriginData.Add("swingOF.1", (384, 2125 + OffsetY, 384 + 7, 2125 + 11 + OffsetY));
        OriginData.Add("swingOF.2", (638, 2129 + OffsetY, 638 + 21, 2129 + 5 + OffsetY));
        OriginData.Add("swingOF.3", (880, 2138 + OffsetY, 880 + 33, 2138 - 8 + OffsetY));
        //
        OriginData.Add("swingT1.0", (126, 2379 + OffsetY, 126 + 2, 2379 + 1 + OffsetY));
        OriginData.Add("swingT1.1", (384, 2382 + OffsetY, 384 + 7, 2382 - 2 + OffsetY));
        OriginData.Add("swingT1.2", (623, 2383 + OffsetY, 623 + 17, 2383 - 3 + OffsetY));
        //
        OriginData.Add("swingP1.0", (1377, 2380 + OffsetY, 1377 + 1, 2380 + OffsetY));
        OriginData.Add("swingP1.1", (1634, 2383 + OffsetY, 1634 + 7, 2383 - 3 + OffsetY));
        OriginData.Add("swingP1.2", (1873, 2384 + OffsetY, 1873 + 17, 2384 - 4 + OffsetY));
        //
        OriginData.Add("swingT2.0", (137, 2632 + OffsetY, 137 + 2, 2632 - 2 + OffsetY));
        OriginData.Add("swingT2.1", (383, 2632 + OffsetY, 383 + 6, 2632 - 2 + OffsetY));
        OriginData.Add("swingT2.2", (624, 2634 + OffsetY, 624 + 15, 2634 - 4 + OffsetY));
        //
        OriginData.Add("swingP2.0", (1386, 2630 + OffsetY, 1386 + 1, 2630 + OffsetY));
        OriginData.Add("swingP2.1", (1634, 2632 + OffsetY, 1632 + 5, 2632 - 2 + OffsetY));
        OriginData.Add("swingP2.2", (1874, 2634 + OffsetY, 1874 + 11, 2634 - 4 + OffsetY));
        //
        OriginData.Add("swingT3.0", (134, 2882 + OffsetY, 134 - 5, 2882 - 2 + OffsetY));
        OriginData.Add("swingT3.1", (380, 2884 + OffsetY, 380 + 4, 2884 - 4 + OffsetY));
        OriginData.Add("swingT3.2", (624, 2883 + OffsetY, 624 + 7, 2883 - 3 + OffsetY));
        //
        OriginData.Add("swingPF.0", (1373, 2885 + OffsetY, 1373 + 6, 2885 - 5 + OffsetY));
        OriginData.Add("swingPF.1", (1625, 2886 + OffsetY, 1625 + 6, 2886 - 6 + OffsetY));
        OriginData.Add("swingPF.2", (1883, 2870 + OffsetY, 1883 + 12, 2870 + 26 + OffsetY));
        OriginData.Add("swingPF.3", (2142, 2884 + OffsetY, 2142 + 30, 2884 - 4 + OffsetY));
        //

        OriginData.Add("sit.0", (2642, 2886 + OffsetY, 2642 - 4, 2886 - 3 + OffsetY));
        //
        OriginData.Add("swingTF.0", (142, 3129 + OffsetY, 142 - 1, 3129 + 1 + OffsetY));
        OriginData.Add("swingTF.1", (388, 3128 + OffsetY, 388 + 3, 3128 + 2 + OffsetY));
        OriginData.Add("swingTF.2", (631, 3132 + OffsetY, 631 + 11, 3132 - 2 + OffsetY));
        OriginData.Add("swingTF.3", (875, 3135 + OffsetY, 875 + 18, 3135 - 5 + OffsetY));
        //
        OriginData.Add("stabOF.0", (1393, 3137 + OffsetY, 1393 - 10, 3137 - 7 + OffsetY));
        OriginData.Add("stabOF.1", (1633, 3131 + OffsetY, 1633 + 12, 3131 + 1 + OffsetY));
        OriginData.Add("stabOF.2", (1875, 3135 + OffsetY, 1875 + 31, 3135 - 5 + OffsetY));
        //
        OriginData.Add("ladder.0", (130, 3379 + OffsetY, 130 - 6, 3379 + 1 + OffsetY));
        OriginData.Add("ladder.1", (379, 3379 + OffsetY, 379 - 8, 3379 - 1 + OffsetY));
        //
        OriginData.Add("rope.0", (1380, 3376 + OffsetY, 1380 - 8, 3376 + 4 + OffsetY));
        OriginData.Add("rope.1", (1629, 3382 + OffsetY, 1629 - 7, 3382 - 2 + OffsetY));





        for (int i = 1; i <= 20; i++)
        {
            ImageGrids[i] = new ImageListView();
            ImageGrids[i].Parent = tabControl1.TabPages[0];
            ImageGrids[i].Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            ImageGrids[i].Dock = DockStyle.Fill;
            ImageGrids[i].BackColor = SystemColors.Window;
            ImageGrids[i].Colors.BackColor = SystemColors.ButtonFace;
            ImageGrids[i].Colors.SelectedBorderColor = Color.Red;

            ImageGrids[i].BorderStyle = BorderStyle.Fixed3D;
            ImageGrids[i].ThumbnailSize = new System.Drawing.Size(32, 32);
            ImageGrids[i].ItemClick += (o, e) =>
            {
                MapleChair.IsUse = false;
                AvatarForm.SelectedFrame = true;
                AvatarForm.SelectedAction = Game.Player.StandType;
                AvatarForm.SelectedFrameNum = 0;

                AddEqps(e.Item.FileName);
                AddInventory();
                Game.Player.RemoveSprites();
                for (int i = 0; i < Player.EqpList.Count; i++)
                {
                    Game.Player.Spawn(Player.EqpList[i]);
                }
                ResetDye2();




            };
            ImageGrids[i].ItemHover += (o, e) =>
            {
                if (ShowToolTip)
                {
                    if (e.Item == null) return;
                    Wz_Node Node = Wz.GetNodeByID(e.Item.FileName, WzType.Character);
                    MainForm.Instance.QuickView(Node);

                }
            };

        }

        AvatarListView = new ImageListView();
        AvatarListView.Parent = tabControl1.TabPages[1];
        AvatarListView.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
        AvatarListView.Dock = DockStyle.Fill;
        AvatarListView.BackColor = SystemColors.Window;
        AvatarListView.Colors.BackColor = SystemColors.ButtonFace;
        AvatarListView.Colors.SelectedBorderColor = Color.Red;
        AvatarListView.Colors.HoverColor1 = SystemColors.ButtonFace;
        AvatarListView.Colors.HoverColor2 = SystemColors.ButtonFace;
        AvatarListView.BorderStyle = BorderStyle.Fixed3D;
        AvatarListView.ThumbnailSize = new System.Drawing.Size(100, 100);
        AvatarListView.ItemClick += (o, e) =>
        {
            ResetDye2();
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    foreach (var Iter in ItemEffect.UseList.Keys)
                        ItemEffect.UseList[Iter].Dead();
                    ItemEffect.UseList.Clear();

                    foreach (var Iter in SetEffect.UseList.Keys)
                        SetEffect.UseList[Iter].Dead();
                    SetEffect.UseList.Clear();

                    Player.EqpList.Clear();
                    Game.Player.ShowHair = false;
                    Game.Player.DressCap = false;
                    Game.Player.RemoveSprites();

                    string[] Split = e.Item.FileName.Split("-");
                    var EqpList = Split.ToList();
                    EqpList.Sort();
                    for (int i = 1; i < EqpList.Count; i++)
                    {
                        AddEqps(EqpList[i]);
                        Game.Player.Spawn(EqpList[i]);
                    }
                    AddInventory();

                    tabControl1.Enabled = false;
                    timer1.Enabled = true;
                    label9.Visible = true;

                    break;
                case 5:
                    string IDs = e.Item.FileName;
                    PlayerEx.Spawn(IDs);
                    break;
            }
        };

        Inventory = new(75, 174, 818, 206, 300, 600, true, this);
        Inventory.RowTemplate.Height = 45;
        Inventory.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        Inventory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        Inventory.DefaultCellStyle.Font = new Font("Tahoma", 15, GraphicsUnit.Pixel);
        Inventory.Anchor = (AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
        Inventory.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
        Inventory.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
        Inventory.CellContentClick += (s, e) =>
        {
            if (e.ColumnIndex != 3)
                return;
            int Row = e.RowIndex;
            var Part = Equip.GetPart(Player.EqpList[Row]);
            string DeleteID = Player.EqpList[Row];
            Player.EqpList.RemoveAt(Row);

            if (ItemEffect.AllList.Contains(DeleteID))
                ItemEffect.Remove(DeleteID);
            if (SetEffect.AllList.ContainsKey(DeleteID))
                SetEffect.Remove(DeleteID);

            var ID = Inventory.Rows[e.RowIndex].Cells[0].Value.ToString();
            Inventory.Rows.RemoveAt(e.RowIndex);
            switch (Part)
            {
                case PartName.Hair:
                    Game.Player.ShowHair = false;
                    break;
                case PartName.Cap:
                    Game.Player.DressCap = false;
                    break;
            }
            Player.EqpList.Sort();
            Game.Player.RemoveSprites();
            for (int i = 0; i < Player.EqpList.Count; i++)
            {
                Game.Player.Spawn(Player.EqpList[i]);
            }

            if (tabControl1.SelectedIndex == 2)
                ResetDyeGrid();
            if (tabControl1.SelectedIndex == 3)
                ResetDyeGrid2();
            ResetDye2();

            SelectedFrame = false;
            timer1.Enabled = true;
        };

        Inventory.CellMouseEnter += (s, e) =>
        {
            if (ShowToolTip)
            {
                Inventory.Refresh();
                string _ID = Inventory.Rows[e.RowIndex].Cells[0].Value.ToString();
                Wz_Node Node = Wz.GetNodeByID(_ID, WzType.Character);
                MainForm.Instance.QuickView(Node);

            }
        };


        // Inventory.SetToolTipEvent(WzType.Character, this);
        AddEqps("00002000");
        AddInventory();
        ResetDyeGrid();
        ResetDyeGrid2();
        foreach (var Iter in Wz.GetNodes("Character/00012000.img/front"))
        {
            if (Iter.Text != "head")
            {
                EarListBox.Items.Add(Iter.Text);
            }
        }

        comboBox1.SelectedIndex = 0;

        if (EarListBox.Items.Count == 0)
        {
            EarListBox.Visible = false;
            label5.Visible = false;
        }

    }


    List<string> PartList = new();
    private void button1_Click(object sender, EventArgs e)
    {
        MapleChair.IsUse = false;
        tabControl1.SelectedIndex = 0;
        string CharacterDir = "";
        string ButtonName = ((System.Windows.Forms.Button)sender).Name.Trim(' ');

        switch (ButtonName)
        {
            case "Head":
            case "Body":
                CharacterDir = "";
                break;
            case "Weapon_1":
            case "Weapon_2":
                CharacterDir = "Weapon";
                break;
            case "Cap_1":
            case "Cap_2":
                CharacterDir = "Cap";
                break;
            case "Hair_1":
            case "Hair_2":
                CharacterDir = "Hair";
                break;
            case "Face_1":
            case "Face_2":
                CharacterDir = "Face";
                break;
            case "FaceAcc":
            case "Glass":
            case "Earring":
                CharacterDir = "Accessory";
                break;
            default:
                CharacterDir = ButtonName;
                break;
        }
        int PartIndex = ((System.Windows.Forms.Button)sender).Tag.ToString().ToInt();
        for (int i = 1; i <= 20; i++)
            ImageGrids[i].Visible = false;
        ImageGrids[PartIndex].Visible = true;

        if (!PartList.Contains(ButtonName))
        {

            var Graphic = ImageGrids[PartIndex].CreateGraphics();
            var Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            Graphic.DrawString("Loading...", Font, Brushes.Black, 150, 150);

            Wz_Node.WzNodeCollection Dir = null;
            string Path = "";
            if (CharacterDir == "")
            {
                Dir = MainForm.TreeNode.Nodes["Character"].Nodes;
                Path = "Character/";
            }
            else
            {
                Dir = MainForm.TreeNode.Nodes["Character"].Nodes[CharacterDir].Nodes;
                Path = "Character/" + CharacterDir + "/";
            }

            int Num = 0;
            bool InRange(int Low, int High) => (Num >= Low) && (Num <= High);

            Win32.SendMessage(ImageGrids[PartIndex].Handle, false);
            foreach (var img in Dir)
            {
                if (!Char.IsNumber(img.Text[0]))
                    continue;
                switch (ButtonName)
                {
                    case "Head":
                        if (img.Text.LeftStr(4) == "0000")
                            continue;
                        break;
                    case "Body":
                        if (img.Text.LeftStr(4) == "0001")
                            continue;
                        break;
                }

                foreach (var Iter in Wz.GetNodeA(Path + img.Text).Nodes)
                {
                    string Left4() => Iter.ImgName().LeftStr(4);
                    Num = Iter.ImgID().ToInt() / 1000;
                    switch (ButtonName)
                    {
                        case "Weapon_1":
                            if (Left4() == "0170")
                                continue;
                            break;
                        case "Weapon_2":
                            if (Left4() != "0170")
                                continue;
                            break;
                        case "Cap_1":
                            if (!InRange(1000, 1003))
                                continue;
                            break;
                        case "Cap_2":
                            if (!InRange(1004, 1006))
                                continue;
                            break;
                        case "Hair_1":
                            if (!InRange(30, 56))
                                continue;
                            break;
                        case "Hair_2":
                            if (!InRange(57, 85))
                                continue;
                            break;
                        case "Face_1":
                            if (!InRange(20, 23))
                                continue;
                            break;
                        case "Face_2":
                            if (!InRange(24, 65))
                                continue;
                            break;
                        case "FaceAcc":
                            if (Left4() != "0101")
                                continue;
                            break;
                        case "Glass":
                            if (Left4() != "0102")
                                continue;
                            break;
                        case "Earring":
                            if (Left4() != "0103")
                                continue;
                            break;
                    }

                    switch (ButtonName)
                    {
                        case "Head":
                            if (Iter.Text == "front")
                                ImageGrids[PartIndex].Items.Add(img.ImgID(), Iter.GetBmp("head"));
                            break;
                        case "Body":
                            if (Iter.Text == "stand1")
                                ImageGrids[PartIndex].Items.Add(img.ImgID(), Iter.GetBmp("0/body"));
                            break;
                        case "Face_1":
                        case "Face_2":
                            if (Iter.Nodes["face"] != null)
                                ImageGrids[PartIndex].Items.Add(Iter.ImgID(), Iter.GetBmp("face"));
                            break;
                        case "Hair_1":
                        case "Hair_2":
                            if (Iter.Nodes["hairOverHead"] != null)
                                ImageGrids[PartIndex].Items.Add(Iter.ImgID(), Iter.GetBmp("hairOverHead"));
                            break;
                        default:
                            if (Iter.Nodes["icon"] != null)
                                ImageGrids[PartIndex].Items.Add(Iter.ImgID(), Iter.GetBmp("icon"));
                            break;
                    }
                }

            }
            Win32.SendMessage(ImageGrids[PartIndex].Handle, true);
            ImageGrids[PartIndex].Refresh();
            PartList.Add(ButtonName);
        }

    }

    private void SaveCharButton_Click(object sender, EventArgs e)
    {
        RenderTarget2D SaveTexture = null;
        int WX = (int)(Game.Player.X - EngineFunc.SpriteEngine.Camera.X - 55);
        int WY = (int)(Game.Player.Y - EngineFunc.SpriteEngine.Camera.Y - 90);
        EngineFunc.Canvas.DrawTarget(ref SaveTexture, 100, 100, () =>
        {
            EngineFunc.Canvas.DrawCropArea(AvatarFormDraw.AvatarPanelTexture, 0, 0, new Rectangle(WX, WY, WX + 100, WY + 100), 0, 0, 1, 1, 0, false, false, 255, 255, 255, 255, false);
        });

        string PngName = "";
        for (int i = 0; i < Player.EqpList.Count; i++)
            PngName = PngName + Player.EqpList[i] + "-";

        string path = Path.Combine(Environment.CurrentDirectory, "Images", PngName + ".png");
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        Stream stream = File.OpenWrite(path);
        SaveTexture.SaveAsPng(stream, 100, 100);
        stream.Dispose();
        SaveTexture.Dispose();

        Bitmap Png = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\" + PngName + ".png");
        AvatarListView.Items.Add(PngName, Png);
        Png.Dispose();
    }

    static bool Loaded;
    static bool SearchGridLoaded;

    void DumpEqpString(Wz_Node Node)
    {
        if (Node.Text == "name")
        {
            if (Node.ParentNode.Text.Length == 5)
                SearchGrid.Rows.Add("000" + Node.ParentNode.Text, " " + Node.ToStr());
            else
                SearchGrid.Rows.Add("0" + Node.ParentNode.Text, " " + Node.ToStr());
        }
        foreach (var Iter in Node.Nodes)
        {
            if ((Node.Text != "Android") && (Node.Text != "ArcaneForce") && (Node.Text != "Bits") && (Node.Text != "Dragon") &&
             (Node.Text != "Mechanic") && (Node.Text != "PetEquip") && (Node.Text != "Skillskin") && (Node.Text != "Taming") &&
             (Node.Text != "MonsterBattle") && (Node.Text.LeftStr(3) != "135") && (Node.Text.LeftStr(3) != "150") &&
             (Node.Text.LeftStr(3) != "151") && (Node.Text.LeftStr(3) != "160") && (Node.Text.LeftStr(3) != "169") &&
             (Node.Text.LeftStr(3) != "111") && (Node.Text.LeftStr(3) != "112") && (Node.Text.LeftStr(3) != "114"))
                DumpEqpString(Iter);
        }
    }

    void CellClick(BaseDataGridView DataGrid, DataGridViewCellEventArgs e)
    {
        var ID = DataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        label1.Text = ID;
        label2.Text = DataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
        string Dir = Equip.GetDir(ID);
        string Name = "";
        // if (Wz.IsDataWz)
        //   Name = Wz.GetStr("String/Eqp.img/Eqp/" + Dir + ID.IntID() + "/name");
        // else
        //   Name = Wz.GetStr("String/Item.img/Eqp/" + Dir + ID.IntID() + "/name");
        var Entry = Wz.GetNodeA("Character/" + Dir + ID + ".img");
        PartName PartName = Equip.GetPart(ID);
        Bitmap Bmp = null;
        if (Bmp != null)
            Bmp.Dispose();
        switch (PartName)
        {
            case PartName.Hair:
                Bmp = Entry.GetBmp("default/hairOverHead");
                break;
            case PartName.Face:
                Bmp = Entry.GetBmp("default/face");
                break;
            default:
                Bmp = Entry.GetBmp("info/icon");
                break;
        }
        pictureBox1.Image = Bmp;

    }
    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MapleChair.IsUse = false;

        void LoadAvatarPics()
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, "Images");
            string[] Files = Directory.Exists(imagePath) ? Directory.GetFiles(imagePath) : Array.Empty<string>();
            foreach (var i in Files)
            {
                string Name = Path.GetFileName(i);
                Name = Name.Replace(".png", "");
                Bitmap Png = new Bitmap(i);
                AvatarListView.Items.Add(Name, Png);
                Png.Dispose();
            }
        }


        switch (tabControl1.SelectedIndex)
        {
            case 0:


                SelectedFrame = false;
                break;
            case 1:
                if (!Loaded)
                {
                    LoadAvatarPics();
                    Loaded = true;
                }
                AvatarListView.Parent = tabControl1.TabPages[1];
                SelectedFrame = false;
                timer1.Enabled = true;
                break;
            case 2:
                ResetDyeGrid();

                SelectedFrame = false;
                break;

            case 3:
                ResetDyeGrid2();
                SelectedFrame = false;
                break;
            case 4:
                if (!SearchGridLoaded)
                {
                    SearchGrid = new(60, 184, 114, 109, 315, 400, false, tabControl1.TabPages[4]);
                    SearchGrid.CellClick += (s, e) =>
                    {
                        CellClick(SearchGrid, e);
                    };

                    SearchGrid.SearchGrid.CellClick += (s, e) =>
                    {
                        CellClick(SearchGrid.SearchGrid, e);
                    };


                    SearchGrid.CellMouseEnter += (s, e) =>
                    {
                        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                        {
                            SearchGrid[0, e.RowIndex].Style.BackColor = Color.LightCyan;
                            SearchGrid[1, e.RowIndex].Style.BackColor = Color.LightCyan;
                        }
                        if (ShowToolTip)
                        {
                            string ID = SearchGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                            Wz_Node Node = Wz.GetNodeByID(ID, WzType.Character);
                            MainForm.Instance.QuickView(Node);
                            // MainForm.Instance.ToolTipView.Location = new Point(448, 395);
                        }
                    };

                    SearchGrid.CellMouseLeave += (s, e) =>
                    {
                        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                        {
                            SearchGrid[0, e.RowIndex].Style.BackColor = Color.White;
                            SearchGrid[1, e.RowIndex].Style.BackColor = Color.White;
                        }
                    };

                    SearchGrid.SearchGrid.CellMouseEnter += (s, e) =>
                    {
                        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                        {
                            SearchGrid.SearchGrid[0, e.RowIndex].Style.BackColor = Color.LightCyan;
                            SearchGrid.SearchGrid[1, e.RowIndex].Style.BackColor = Color.LightCyan;
                        }
                        if (ShowToolTip)
                        {
                            string ID = SearchGrid.SearchGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                            Wz_Node Node = Wz.GetNodeByID(ID, WzType.Character);
                            MainForm.Instance.QuickView(Node);
                            // MainForm.Instance.ToolTipView.Location = new Point(448, 395);
                        }
                    };

                    SearchGrid.SearchGrid.CellMouseLeave += (s, e) =>
                    {
                        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                        {
                            SearchGrid.SearchGrid[0, e.RowIndex].Style.BackColor = Color.White;
                            SearchGrid.SearchGrid[1, e.RowIndex].Style.BackColor = Color.White;
                        }
                    };

                    var Graphic = SearchGrid.CreateGraphics();
                    var Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                    Graphic.DrawString("Loading...", Font, Brushes.Black, 10, 50);

                    string ID = null;
                    string name = null;
                    Win32.SendMessage(SearchGrid.Handle, false);

                    if (!Wz.HasHardCodedStrings)
                    {
                        if (Wz.HasNode("String/Eqp.img"))
                            DumpEqpString(Wz.GetNodeA("String/Eqp.img/Eqp"));
                        else
                            DumpEqpString(Wz.GetNodeA("String/Item.img/Eqp"));
                    }
                    Win32.SendMessage(SearchGrid.Handle, true);
                    SearchGrid.Refresh();
                    SearchGridLoaded = true;
                }
                // MainForm.Instance.ToolTipView.TopLevel = false;
                // MainForm.Instance.ToolTipView.IsMdiContainer = false;
                // MainForm.Instance.ToolTipView.Parent = this;
                SelectedFrame = false;
                break;

            case 5:
                if (!Loaded)
                {
                    LoadAvatarPics();
                    Loaded = true;
                }
                AvatarListView.Parent = tabControl1.TabPages[5];
                SelectedFrame = false;
                break;

            case 6:
                if (!LoadedFrameList)
                {
                    foreach (var i in AllFrames)
                        FrameListBox.Items.Add(i);
                    LoadedFrameList = true;
                }
                UpdateAvatarBound();
                SelectedFrame = true;
                if (FrameListBox.SelectedIndex < 0)
                    FrameListBox.SetSelected(0, true);
                else
                    FrameListBox.SetSelected(FrameListBox.SelectedIndex, true);

                break;
        }

    }

    private void UpdateAvatarBound()
    {
        // 重新计算包围盒
        SelectedFrame = true;


        FrameBound.Clear();
        AvatarBound = Rectangle.Empty;
        foreach (var i in AllFrames)
        {
            var sprite = i.Split('.');
            SelectedFrameNum = sprite[1].ToInt();
            SelectedAction = Game.Player.StandType;// sprite[0];
            Game.Player.DoMove(0);

            Rectangle bound = Rectangle.Empty;
            foreach (var parts in Game.Player.PartSpriteList)
            {
                if (parts.Alpha > 0)
                    bound = Rectangle.Union(bound, new Rectangle((int)parts.Offset.X - 100, (int)parts.Offset.Y - 100, parts.ImageWidth + 200, parts.ImageHeight + 200));
            }
            AvatarBound = Rectangle.Union(AvatarBound, bound);
            FrameBound.Add(bound);
        }

        SelectedFrame = false;
    }

    private void DyeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {

        int Col = e.ColumnIndex;
        string ID = Inventory.Rows[e.RowIndex].Cells[0].Value.ToString();
        string Dir = Equip.GetDir(ID);
        Wz_Node Entry;
        if (ItemEffect.AllList.Contains(ID))
            Entry = Wz.GetNodeA("Effect/ItemEff.img/" + ID.IntID());
        else
            Entry = Wz.GetNodeA("Character/" + Dir + ID + ".img");

        if (Col <= 15)
            Wz.DumpData(Entry, Wz.EquipData, Wz.EquipImageLib, true, Col * 25);
        else
            Wz.DumpData(Entry, Wz.EquipData, Wz.EquipImageLib, true, 0, -200);
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        SearchGrid.Search(textBox1.Text);
    }

    private void UseButton_Click(object sender, EventArgs e)
    {
        if (label1.Text == "")
            return;
        MapleChair.IsUse = false;
        AvatarForm.SelectedFrame = true;
        AvatarForm.SelectedAction = Game.Player.StandType;
        AvatarForm.SelectedFrameNum = 0;

        AddEqps(label1.Text);
        AddInventory();
        Game.Player.RemoveSprites();
        for (int i = 0; i < Player.EqpList.Count; i++)
        {
            Game.Player.Spawn(Player.EqpList[i]);
        }
        ResetDye2();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeExpressionListBox = true;
    }

    private void ShowToolTil_CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        ShowToolTip = !ShowToolTip;

    }


    private void AvatarForm_FormClosing(object sender, FormClosingEventArgs e)
    {

        SelectedFrame = false;
    }

    private void EarListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Game.Player.EarType = EarListBox.Text;
    }

    private void FrameListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedFrame = true;
        string SelectedItem = FrameListBox.SelectedItem.ToString();
        var Split = SelectedItem.Split('.');
        SelectedFrameNum = Split[1].ToInt();
        SelectedAction = Split[0];

        CurrentSpriteBound = FrameBound[FrameListBox.SelectedIndex];

        //if(FrameListBox.SelectedIndex > 0)
        //{
        //    ScrollBarX.Value = 0;
        //}
    }

    private Rectangle GetClipBoundindBox()
    {
        Rectangle result;
        if (customAABB_checkBox.Checked)
        {
            int OffsetX = FrameListDraw.Width / 2;
            int OffsetY = FrameListDraw.Height / 2 + Game.Player.Height / 2;

            Rectangle bound = new Rectangle(AdjustX, AdjustY, AdjustW, AdjustH);
            int posX = (int)(Game.Player.X - EngineFunc.SpriteEngine.Camera.X + bound.X - OffsetX);
            int posY = (int)(Game.Player.Y - EngineFunc.SpriteEngine.Camera.Y + bound.Y - OffsetY);
            result = new Rectangle(posX, posY, bound.Width, bound.Height);
        }
        else
        {
            int posX = (int)(Game.Player.X - EngineFunc.SpriteEngine.Camera.X + AvatarBound.X);
            int posY = (int)(Game.Player.Y - EngineFunc.SpriteEngine.Camera.Y + AvatarBound.Y);
            result = new Rectangle(posX, posY, Math.Min(512, AvatarBound.Width), Math.Min(512, AvatarBound.Height));
        }
        return result;
    }

    private void SaveTexture(RenderTarget2D texture, string filepath)
    {
        // 修复预乘alpha的问题
        Microsoft.Xna.Framework.Color[] pixels = new Microsoft.Xna.Framework.Color[texture.Width * texture.Height];
        texture.GetData(pixels);
        for (int i = 0; i < pixels.Length; i++)
        {
            Microsoft.Xna.Framework.Color pixel = pixels[i];
            if (pixel.A > 0 && pixel.A < 255)
            {
                float alpha = pixel.A / 255f;
                pixels[i] = new Microsoft.Xna.Framework.Color(
                    (byte)(pixel.R / alpha),
                    (byte)(pixel.G / alpha),
                    (byte)(pixel.B / alpha),
                    pixel.A
                );
            }
        }
        Texture2D fixedTexture = new Texture2D(EngineFunc.Canvas.GraphicsDevice, texture.Width, texture.Height);
        fixedTexture.SetData(pixels);

        // 保存文件
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));
        Stream stream = File.OpenWrite(filepath);
        fixedTexture.SaveAsPng(stream, fixedTexture.Width, fixedTexture.Height);

        stream.Dispose();
        fixedTexture.Dispose();

    }

    private void ExportSprite(object sender, EventArgs e)
    {
        Rectangle bound = GetClipBoundindBox();

        RenderTarget2D texture = new RenderTarget2D(EngineFunc.Canvas.GraphicsDevice, bound.Width, bound.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture);
        EngineFunc.Canvas.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
        EngineFunc.Canvas.DrawCropArea(
            FrameListDraw.AvatarPanelTexture,
            0, 0, bound,
            0, 0, 1, 1, 0,
            false, false,
            255, 255, 255, 255,
            false, BlendMode.NonPremultiplied2);

        if (debugDraw)
        {
            EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture);
            EngineFunc.Canvas.DrawRectangle(0, 0, bound.Width - 1, bound.Height - 1, Microsoft.Xna.Framework.Color.Blue);
            if (useCustomBound)
            {
                int cx = FrameListDraw.Width / 2 - AdjustX;
                int cy = FrameListDraw.Height / 2 + Game.Player.Height / 2 - AdjustY;
                EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(0, cy), new Microsoft.Xna.Framework.Point(bound.Width, cy), 1, Microsoft.Xna.Framework.Color.Green);
                EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(cx, 0), new Microsoft.Xna.Framework.Point(cx, bound.Height), 1, Microsoft.Xna.Framework.Color.Green);
            }
            else
            {
                int cx = -AvatarBound.X;
                int cy = -AvatarBound.Y;
                EngineFunc.Canvas.DrawRectangle(cx + CurrentSpriteBound.X, cy + CurrentSpriteBound.Y, CurrentSpriteBound.Width - 1, CurrentSpriteBound.Height - 1, Microsoft.Xna.Framework.Color.Red);
                EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(0, cy), new Microsoft.Xna.Framework.Point(bound.Width, cy), 1, Microsoft.Xna.Framework.Color.Green);
                EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(cx, 0), new Microsoft.Xna.Framework.Point(cx, bound.Height), 1, Microsoft.Xna.Framework.Color.Green);
            }
            EngineFunc.Canvas.DrawString("Arial13", $"{FrameListBox.SelectedItem}", 2, 0, Microsoft.Xna.Framework.Color.Red);
        }

        string filePath = Path.Combine(Environment.CurrentDirectory, "Export", $"{FrameListBox.SelectedItem}.png");
        SaveTexture(texture, filePath);
        texture.Dispose();
    }

    private void _ExportAllSprite(string FolderName)
    {
        tabControl1.Enabled = false;
        SelectedFrame = true;
        ChangeExpressionListBox = true;

        Rectangle bound = GetClipBoundindBox();
        RenderTarget2D texture = new RenderTarget2D(EngineFunc.Canvas.GraphicsDevice, bound.Width, bound.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture);


        float tx = TamingMob.Navel.X;
        float ty = TamingMob.Navel.Y;
        int cx_ = MapleChair.BodyRelMove.X;
        int cy_ = MapleChair.BodyRelMove.Y;

        for (int i = 0; i < AllFrames.Length; i++)
        {
            string frameName = AllFrames[i];
            string[] sprite = frameName.Split('.');
            SelectedFrameNum = sprite[1].ToInt();
            SelectedAction = sprite[0];

            int _SelectedFrameNum = SelectedFrameNum;
            string _SelectedAction = SelectedAction; ;

            if (Morph.IsUse && Morph.Instance != null)
            {
                switch (SelectedAction)
                {
                    case "stand1":
                    case "stand2":
                        Morph.Instance.State = "stand";
                        break;

                    case "walk1":
                    case "walk2":
                        Morph.Instance.State = "walk";
                        break;

                    case "ladder":
                        Morph.Instance.State = "ladder";
                        break;
                    case "rope":
                        Morph.Instance.State = "rope";
                        break;
                    case "fly":
                        Morph.Instance.State = "fly";
                        break;

                    case "jump":
                        Morph.Instance.State = "jump";
                        break;

                    case "prone":
                        Morph.Instance.State = "prone";
                        break;
                    default:
                        Morph.Instance.State = "stand";
                        break;
                }
                Morph.Instance.Frame = SelectedFrameNum;
                Morph.Instance.DoMove(1);

            }

           
            if (TamingMob.IsUse)
            {
                AvatarForm.SelectedAction ="sit";
                foreach (var Iter in TamingMob.PartList)
                {
                     switch (_SelectedAction)
                     {
                         case "stand1":
                         case "stand2":
                             Iter.State = "stand1";
                             break;
                         case "walk1":
                         case "walk2":
                             Iter.State = "walk1";
                             break;

                         case "ladder":
                             Iter.State = "ladder";
                             AvatarForm.SelectedAction = "ladder";
                             break;
                         case "rope":
                             Iter.State = "rope";
                             AvatarForm.SelectedAction = "rope";
                             break;
                         case "fly":
                             Iter.State = "fly";
                             break;

                         case "jump":
                             Iter.State = "jump";
                             break;

                         case "prone":
                             Iter.State = "prone";
                             break;
                         case "sit":
                             Iter.State = "tired";
                             break;
                         default:
                             Iter.State = "stand1";
                             break;
                     }
                    Iter.Frame = _SelectedFrameNum;
                    Iter.DoMove(1);
                }
            }
           

            if (!TamingMob.IsUse)
            {
                if (AvatarForm.SelectedAction == "sit")
                {
                    MapleChair.BodyRelMove.X = cx_;
                    MapleChair.BodyRelMove.Y = cy_;
                    TamingMob.Navel.X = tx;
                    TamingMob.Navel.Y = ty;
                }
                else
                {
                    MapleChair.BodyRelMove.X = 0;
                    MapleChair.BodyRelMove.Y = 0;
                    TamingMob.Navel.X = 0;
                    TamingMob.Navel.Y = 0;
                }
            }

            // 必须domove 3次，不然序号会慢一帧
            Game.Player.DoMove(0);
            Game.Player.DoMove(0);
            Game.Player.DoMove(0);


            if (SetEffect.Instance != null)
            {
                SetEffect.Instance.DoMove(0);
            }
            if (ItemEffect.Instance != null)
            {
                ItemEffect.Instance.DoMove(0);
                //  ItemEffect.Instance.DoMove(0);
                //  ItemEffect.Instance.DoMove(0);
            }

            EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(FrameListDraw.AvatarPanelTexture);
            EngineFunc.Canvas.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            EngineFunc.SpriteEngine.DrawEx("Player", "ItemEffect", "SetEffect", "LabelRingTag", "MedalTag", "NickNameTag",
                "ChatRingBalloon", "MapleChair", "TamingMob", "Morph");

            EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture);
            EngineFunc.Canvas.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            EngineFunc.Canvas.DrawCropArea(
                FrameListDraw.AvatarPanelTexture,
                0, 0, bound,
                0, 0, 1, 1, 0,
                false, false,
                255, 255, 255, 255,
                false, BlendMode.NonPremultiplied2);

            if (debugDraw)
            {
                EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture);
                EngineFunc.Canvas.DrawRectangle(0, 0, bound.Width - 1, bound.Height - 1, Microsoft.Xna.Framework.Color.Blue);
                if (useCustomBound)
                {
                    int cx = FrameListDraw.Width / 2 - AdjustX;
                    int cy = FrameListDraw.Height / 2 + Game.Player.Height / 2 - AdjustY;
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(0, cy), new Microsoft.Xna.Framework.Point(bound.Width, cy), 1, Microsoft.Xna.Framework.Color.Green);
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(cx, 0), new Microsoft.Xna.Framework.Point(cx, bound.Height), 1, Microsoft.Xna.Framework.Color.Green);
                }
                else
                {
                    int cx = -AvatarBound.X;
                    int cy = -AvatarBound.Y;
                    EngineFunc.Canvas.DrawRectangle(cx + FrameBound[i].X, cy + FrameBound[i].Y, FrameBound[i].Width - 1, FrameBound[i].Height - 1, Microsoft.Xna.Framework.Color.Red);
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(0, cy), new Microsoft.Xna.Framework.Point(bound.Width, cy), 1, Microsoft.Xna.Framework.Color.Green);
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(cx, 0), new Microsoft.Xna.Framework.Point(cx, bound.Height), 1, Microsoft.Xna.Framework.Color.Green);
                }
                EngineFunc.Canvas.DrawString("Arial13", $"{i}_{frameName}", 2, 0, Microsoft.Xna.Framework.Color.Red);
            }

            //   string filePath = Path.Combine(Environment.CurrentDirectory, "Export", $"{i}_{frameName}.png");
            string filePath = Path.Combine(Environment.CurrentDirectory, FolderName, $"{frameName}.png");
            SaveTexture(texture, filePath);
        }
        EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(null);
        tabControl1.Enabled = true;
        texture.Dispose();

        string SelectedItem = FrameListBox.SelectedItem.ToString();
        var Split = SelectedItem.Split('.');
        SelectedFrameNum = Split[1].ToInt();
        SelectedAction = Split[0];

    }

    private void ExportAllSprite(object sender, EventArgs e)
    {
        _ExportAllSprite("Export");
    }

    private void ExportSpriteSheet(object sender, EventArgs e)
    {
        if (sheetPoint == null)
        {
            sheetPoint = new List<Point> {
                // textire1 
                new Point(0,0), new Point(1,0), new Point(2,0), new Point(3,0),     new Point(5,0), new Point(6,0), new Point(7,0),
                new Point(0,1), new Point(1,1), new Point(2,1), new Point(3,1),     new Point(5,1), new Point(6,1), new Point(7,1),
                new Point(0,2), new Point(1,2), new Point(2,2),                     new Point(5,2), new Point(6,2), new Point(7,2),
                new Point(0,3), new Point(1,3), new Point(2,3),                     new Point(5,3), new Point(6,3),
                new Point(0,4), new Point(1,4),                                     new Point(5,4), new Point(6,4),
                new Point(0,5),                 new Point(2,5),                     new Point(5,5),
                new Point(0,6), new Point(1,6), new Point(2,6),                     new Point(5,6), new Point(6,6), new Point(7,6),
                new Point(0,7), new Point(1,7), new Point(2,7), new Point(3,7), new Point(4,7), 

                // textire2
                new Point(0,0), new Point(1,0), new Point(2,0), new Point(3,0),     new Point(5,0), new Point(6,0), new Point(7,0),
                new Point(0,1), new Point(1,1), new Point(2,1),                     new Point(5,1), new Point(6,1), new Point(7,1),
                new Point(0,2), new Point(1,2), new Point(2,2), new Point(3,2),     new Point(5,2), new Point(6,2), new Point(7,2),
                new Point(0,3), new Point(1,3), new Point(2,3),                     new Point(5,3), new Point(6,3), new Point(7,3),
                new Point(0,4), new Point(1,4), new Point(2,4), new Point(3,4),     new Point(5,4), new Point(6,4), new Point(7,4),
                new Point(0,5), new Point(1,5), new Point(2,5),                     new Point(5,5), new Point(6,5), new Point(7,5),
                new Point(0,6), new Point(1,6), new Point(2,6), new Point(3,6),     new Point(5,6), new Point(6,6), new Point(7,6),
                new Point(0,7), new Point(1,7), new Point(2,7), new Point(3,7),     new Point(5,7), new Point(6,7), new Point(7,7),
            };
        }

        tabControl1.Enabled = false;
        SelectedFrame = true;
        ChangeExpressionListBox = true;

        Rectangle clipBound = GetClipBoundindBox();
        int SpriteSize = 8 * Math.Max(clipBound.Width + 2, clipBound.Height + 2);
        int textureSize = 128;
        while (textureSize < SpriteSize && textureSize < 4096)
            textureSize *= 2;

        RenderTarget2D texture1 = new RenderTarget2D(EngineFunc.Canvas.GraphicsDevice, textureSize, textureSize, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        RenderTarget2D texture2 = new RenderTarget2D(EngineFunc.Canvas.GraphicsDevice, textureSize, textureSize, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture1);
        EngineFunc.Canvas.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
        EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture2);
        EngineFunc.Canvas.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);

        SpriteSize = textureSize / 8;
        Rectangle spriteBound = new Rectangle(
            (SpriteSize - clipBound.Width + 2) / 2,
            (SpriteSize - clipBound.Height + 2) / 2,
            clipBound.Width + 2,
            clipBound.Height + 2);
        RenderTarget2D texture;
        for (int index = 0; index < AllFrames.Length; index++)
        {
            texture = index < 43 ? texture1 : texture2;

            int posX = SpriteSize * sheetPoint[index].X + spriteBound.X;
            int posY = SpriteSize * sheetPoint[index].Y + spriteBound.Y;

            string frameName = AllFrames[index];
            string[] sprite = frameName.Split('.');
            SelectedFrameNum = sprite[1].ToInt();
            SelectedAction = sprite[0];


            // 必须domove 3次，不然序号会慢一帧
            Game.Player.DoMove(0);
            Game.Player.DoMove(0);
            Game.Player.DoMove(0);

            EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(FrameListDraw.AvatarPanelTexture);
            EngineFunc.Canvas.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            EngineFunc.SpriteEngine.DrawEx("Player", "ItemEffect", "SetEffect", "LabelRingTag", "MedalTag", "NickNameTag",
                "ChatRingBalloon", "MapleChair", "TamingMob", "Morph");

            EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(texture);
            EngineFunc.Canvas.DrawCropArea(
                FrameListDraw.AvatarPanelTexture,
                posX, posY, clipBound,
                0, 0, 1, 1, 0,
                false, false,
                255, 255, 255, 255,
                false, BlendMode.NonPremultiplied2);

            if (debugDraw)
            {
                int ox = SpriteSize * sheetPoint[index].X;
                int oy = SpriteSize * sheetPoint[index].Y;
                EngineFunc.Canvas.DrawRectangle(ox, oy, SpriteSize - 1, SpriteSize - 1, Microsoft.Xna.Framework.Color.Black);
                EngineFunc.Canvas.DrawRectangle(ox + spriteBound.X, oy + spriteBound.Y, spriteBound.Width - 1, spriteBound.Height - 1, Microsoft.Xna.Framework.Color.Blue);

                if (useCustomBound)
                {
                    int cx = posX + FrameListDraw.Width / 2 - AdjustX;
                    int cy = posY + FrameListDraw.Height / 2 - AdjustY + Game.Player.Height / 2;
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(ox, cy), new Microsoft.Xna.Framework.Point(ox + SpriteSize, cy), 1, Microsoft.Xna.Framework.Color.Green);
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(cx, oy), new Microsoft.Xna.Framework.Point(cx, oy + SpriteSize), 1, Microsoft.Xna.Framework.Color.Green);
                }
                else
                {
                    int cx = ox + spriteBound.X - AvatarBound.X;
                    int cy = oy + spriteBound.Y - AvatarBound.Y;
                    EngineFunc.Canvas.DrawRectangle(cx + FrameBound[index].X, cy + FrameBound[index].Y, FrameBound[index].Width - 1, FrameBound[index].Height - 1, Microsoft.Xna.Framework.Color.Red);
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(ox, cy), new Microsoft.Xna.Framework.Point(ox + SpriteSize, cy), 1, Microsoft.Xna.Framework.Color.Green);
                    EngineFunc.Canvas.DrawLine(new Microsoft.Xna.Framework.Point(cx, oy), new Microsoft.Xna.Framework.Point(cx, oy + SpriteSize), 1, Microsoft.Xna.Framework.Color.Green);
                }

                EngineFunc.Canvas.DrawString("Arial13", $"{index}_{frameName}", ox + 2, oy, Microsoft.Xna.Framework.Color.Red);
            }
        }

        SaveTexture(texture1, Path.Combine(Environment.CurrentDirectory, "Export", $"_SpriteSheet1.png"));
        SaveTexture(texture2, Path.Combine(Environment.CurrentDirectory, "Export", $"_SpriteSheet2.png"));

        EngineFunc.Canvas.GraphicsDevice.SetRenderTarget(null);
        tabControl1.Enabled = true;
        texture1.Dispose();
        texture2.Dispose();

        string SelectedItem = FrameListBox.SelectedItem.ToString();
        var Split = SelectedItem.Split('.');
        SelectedFrameNum = Split[1].ToInt();
        SelectedAction = Split[0];
    }

    private void customAABB_checkBox_CheckedChanged(object sender, EventArgs e)
    {
        useCustomBound = customAABB_checkBox.Checked;
        groupBox1.Enabled = useCustomBound;
    }

    private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
        switch (((HScrollBar)sender).Name)
        {
            case "ScrollBarX":
                AdjX.Text = ScrollBarX.Value.ToString();
                AdjustX = ScrollBarX.Value;
                break;
            case "ScrollBarY":
                AdjY.Text = ScrollBarY.Value.ToString();
                AdjustY = AdjY.Text.ToInt();
                break;
            case "ScrollBarW":
                AdjW.Text = ScrollBarW.Value.ToString();
                AdjustW = AdjW.Text.ToInt();
                break;
            case "ScrollBarH":
                AdjH.Text = ScrollBarH.Value.ToString();
                AdjustH = AdjH.Text.ToInt();
                break;

        }
    }


    int RowIndex;

    private void DyeGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        HueTrackBar.Value = 0;
        SatTrackBar.Value = 0;
        LightnessTrackBar.Value = 0;
        LabelHue.Text = "0";
        LabelSat.Text = "0";
        LabelLightness.Text = "0";
        DyePicture.Image = (Bitmap)DyeGrid2.Rows[e.RowIndex].Cells[1].Value;
        RowIndex = e.RowIndex;
    }


    void SetDye2()
    {
        if (DyePicture.Image == null)
            return;
        Bitmap Bmp = (Bitmap)DyeGrid2.Rows[RowIndex].Cells[1].Value;
        Bitmap Image = null;
        ImageFilter.HSL(ref Bmp, HueTrackBar.Value, SatTrackBar.Value, LightnessTrackBar.Value);

        DyePicture.Image = Bmp;
        LabelHue.Text = HueTrackBar.Value.ToString();
        LabelSat.Text = SatTrackBar.Value.ToString();
        LabelLightness.Text = LightnessTrackBar.Value.ToString();
    }

    void ResetDye2()
    {
        DyePicture.Image = null;
        HueTrackBar.Value = 0;
        SatTrackBar.Value = 0;
        LightnessTrackBar.Value = 0;
        LabelHue.Text = "0";
        LabelSat.Text = "0";
        LabelLightness.Text = "0";
    }

    private void HueTrackBar_Scroll(object sender, EventArgs e)
    {
        SetDye2();
    }

    private void SatTrackBar_Scroll(object sender, EventArgs e)
    {
        SetDye2();
    }

    private void LightnessTrackBar_Scroll(object sender, EventArgs e)
    {
        SetDye2();
    }

    private void button22_Click(object sender, EventArgs e)
    {
        string ID = DyeGrid2.Rows[RowIndex].Cells[0].Value.ToString();
        string Dir = Equip.GetDir(ID);
        Wz_Node Entry;
        if (ItemEffect.AllList.Contains(ID))
            Entry = Wz.GetNodeA("Effect/ItemEff.img/" + ID.IntID());
        else
            Entry = Wz.GetNodeA("Character/" + Dir + ID + ".img");

        Wz.DumpData(Entry, Wz.EquipData, Wz.EquipImageLib, true, HueTrackBar.Value, SatTrackBar.Value, LightnessTrackBar.Value);
    }

    private void tabPage1_MouseLeave(object sender, EventArgs e)
    {

    }

    private void timer1_Tick(object sender, EventArgs e)
    {

        if (Game.Player.ResetAction == false)
        {
            UpdateAvatarBound();
            tabControl1.Enabled = true;
            timer1.Enabled = false;
            label9.Visible = false;

            if (tabControl1.SelectedIndex == 6)
            {
                FrameListBox_SelectedIndexChanged(sender, e);
            }
        }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        debugDraw = checkBox1.Checked;
    }
    static int SaveCount;
    private void SavePsdButton_Click(object sender, EventArgs e)
    {
        AdjustX = 60;//90;
        AdjustY = 88;//128;
        AdjustW = 336;//256;
        AdjustH = 296;//256;
        customAABB_checkBox.Checked = true;
        SavePsdButton.Text = "處理中,請稍後...";
        SavePsdButton.Enabled = false;
        _ExportAllSprite("Temp");

        new PreViewForm();
        foreach (var key in OriginData.Keys)
        {
            var ImageForm = new ImageForm();
            ImageForm.RedPointX = OriginData[key].RedPointX;
            ImageForm.RedPointY = OriginData[key].RedPointY;
            ImageForm.NewRedPointX = OriginData[key].RedPointX;
            ImageForm.NewRedPointY = OriginData[key].RedPointY;
            ImageForm.DrawPosX = OriginData[key].DrawPosX;
            ImageForm.DrawPosY = OriginData[key].DrawPosY;
            ImageForm.NewDrawPosX = OriginData[key].DrawPosX;
            ImageForm.NewDrawPosY = OriginData[key].DrawPosY;
            System.Drawing.Image Image = System.Drawing.Image.FromFile(System.Environment.CurrentDirectory + "\\Temp\\" + key + ".png");
            ImageForm.SetBitmap((Bitmap)Image);
            ImageForm.FrameName = key;
            ImageForm.Parent = PreViewForm.Instance;
            ImageForm.Left = OriginData[key].DrawPosX - 188;//158;
            ImageForm.Top = OriginData[key].DrawPosY - 180;//140;
            ImageForm.CaptionRectangle = new System.Drawing.Rectangle(0, 0, 250, 250);
            ImageFormList.Add(ImageForm);
            Image.Dispose();
        }
        PreViewForm.Instance.Show();
        SavePsdButton.Text = "儲存psd";

        customAABB_checkBox.Checked = false;
        AdjustX = ScrollBarX.Value;
        AdjustY = ScrollBarY.Value; ;
        AdjustW = ScrollBarW.Value;
        AdjustH = ScrollBarH.Value;

    }
}

