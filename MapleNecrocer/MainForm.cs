﻿using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WzComparerR2.PluginBase;
using WzComparerR2;
using WzComparerR2.WzLib;
using WzComparerR2.Common;
using System.Reflection;
using WzComparerR2.CharaSim;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar.Controls;

using Microsoft.Xna.Framework;
using Spine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.CompilerServices;

using System.Text.RegularExpressions;
using WzComparerR2.CharaSim;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.IO;
using DPIUtils;
using System.Diagnostics;

namespace MapleNecrocer;


public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Instance = this;
        openedWz = new List<Wz_Structure>();
        PluginManager.WzFileFinding += new FindWzEventHandler(WzFileFinding);
        if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
        {
            var dgvType = this.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this, true, null);
        }
        RenderForm.TopLevel = false;
        RenderForm.Parent = this;
        RenderForm.Show();



        stringLinker = new StringLinker();

        //ToolTipView.KeyDown += new KeyEventHandler(afrm_KeyDown);


        //  RenderForm.Show();
    }
    public static RenderForm RenderForm = new RenderForm();
    List<Wz_Structure> openedWz;
    public static Wz_Node TreeNode;
    public static MainForm Instance;
    public DataGridViewEx MapListBox;
    public Dictionary<string, string> MapNames = new();
    public StringLinker stringLinker;

    DefaultLevel skillDefaultLevel = DefaultLevel.Level0;
    int skillInterval = 32;
    public void CenterToScreen2()
    {
        this.CenterToScreen();
    }

    public static void OpenWZ(string wzFilePath)
    {
        MainForm.Instance.openWz(wzFilePath);
    }

    string LeftStr(string s, int count)
    {
        if (count > s.Length)
            count = s.Length;
        return s.Substring(0, count);
    }

    public void DumpMapIDs()
    {
        //  if(Wz.HasNode("Map/Map/Map0"))
        if (Wz.HasNode("String/Map.img"))
        {
            foreach (var Iter in Wz.GetNodes("String/Map.img"))
            {
                foreach (var Iter2 in Iter.Nodes)
                {
                    string ID = Iter2.Text.PadLeft(9, '0');
                    var MapName = Iter2.GetStr("mapName");
                    var StreetName = Iter2.GetStr("streetName");
                    Map.MapNameList.AddOrReplace(ID, new MapNameRec(ID, MapName, StreetName));
                    if (!MapNames.ContainsKey(ID))
                    {
                        MapNames.Add(ID, MapName);
                    }
                }
            }

            Win32.SendMessage(MapListBox.Handle, false);
            foreach (var Dir in Wz.GetNodes("Map/Map"))
            {
                if (LeftStr(Dir.Text, 3) != "Map" && Wz.HasHardCodedStrings == false)
                    continue;
                foreach (var img in Dir.Nodes)
                {
                    if (!Char.IsNumber(img.Text[0]))
                        continue;
                    var ID = img.ImgID();
                    if (MapNames.ContainsKey(ID))
                        MapListBox.Rows.Add(ID, MapNames[ID]);
                    else
                        MapListBox.Rows.Add(ID, "");
                }
            }

        }
        else if (Wz.HasHardCodedStrings)
        {
            Win32.SendMessage(MapListBox.Handle, false);
            foreach (var Iter in Wz.GetNodes("Map/Map"))
            {
                string ID = Iter.Text.Replace(".img", "");
                ID = ID.PadLeft(9, '0');
                var MapName = Iter.HasNode("info/mapName") ? Iter.GetStr("info/mapName") : "";
                var StreetName = Iter.HasNode("info/streetName") ? Iter.GetStr("info/mapName") : "";
                Map.MapNameList.AddOrReplace(ID, new MapNameRec(ID, MapName, StreetName));
                if (!MapNames.ContainsKey(ID))
                {
                    MapNames.Add(ID, MapName);
                    MapListBox.Rows.Add(ID, MapNames[ID]);
                }
            }
        }

        // foreach(var i in TreeNode.Nodes["Map"].Nodes["Map"].Nodes)
        //   MapListBox.Items.Add(i.Text);
        Win32.SendMessage(MapListBox.Handle, true);

        MapListBox.Refresh();

    }
    void WzFileFinding(object sender, FindWzEventArgs e)
    {
        string[] fullPath = null;
        if (!string.IsNullOrEmpty(e.FullPath)) //用fullpath作为输入参数
        {
            fullPath = e.FullPath.Split('/', '\\');
            e.WzType = Enum.TryParse<Wz_Type>(fullPath[0], true, out var wzType) ? wzType : Wz_Type.Unknown;
        }

        List<Wz_Node> preSearch = new List<Wz_Node>();
        if (e.WzType != Wz_Type.Unknown) //用wztype作为输入参数
        {
            IEnumerable<Wz_Structure> preSearchWz = e.WzFile?.WzStructure != null ?
                Enumerable.Repeat(e.WzFile.WzStructure, 1) :
                this.openedWz;
            foreach (var wzs in preSearchWz)
            {
                Wz_File baseWz = null;

                bool find = false;
                foreach (Wz_File wz_f in wzs.wz_files)
                {
                    if (wz_f.Header.FileName.RightStr(5) == "NL.wz") continue;
                    if (wz_f.Header.FileName.RightStr(5) == "ES.wz") continue;
                    if (wz_f.Header.FileName.RightStr(5) == "FR.wz") continue;
                    if (wz_f.Header.FileName.RightStr(5) == "DE.wz") continue;
                    if (wz_f.Type == e.WzType)
                    {
                        preSearch.Add(wz_f.Node);
                        find = true;
                        //e.WzFile = wz_f;
                    }
                    if (wz_f.Type == Wz_Type.Base)
                    {
                        baseWz = wz_f;
                    }
                }

                // detect data.wz
                if (baseWz != null && !find)
                {
                    string key = e.WzType.ToString();
                    foreach (Wz_Node node in baseWz.Node.Nodes)
                    {
                        if (node.Text == key && node.Nodes.Count > 0)
                        {
                            preSearch.Add(node);

                        }
                    }
                }
            }
        }

        if (fullPath == null || fullPath.Length <= 1)
        {
            if (e.WzType != Wz_Type.Unknown && preSearch.Count > 0) //返回wzFile
            {
                e.WzNode = preSearch[0];
                e.WzFile = preSearch[0].Value as Wz_File;
            }
            return;
        }

        if (preSearch.Count <= 0)
        {
            return;
        }

        foreach (var wzFileNode in preSearch)
        {
            var searchNode = wzFileNode;
            for (int i = 1; i < fullPath.Length && searchNode != null; i++)
            {
                searchNode = searchNode.Nodes[fullPath[i]];
                var img = searchNode.GetValueEx<Wz_Image>(null);
                if (img != null)
                {
                    searchNode = img.TryExtract() ? img.Node : null;
                }
            }

            if (searchNode != null)
            {
                e.WzNode = searchNode;
                e.WzFile = wzFileNode.Value as Wz_File;
                return;
            }
        }
        //寻找失败
        e.WzNode = null;
    }


    private Node createNode(Wz_Node wzNode)
    {
        if (wzNode == null)
            return null;

        Node parentNode = new Node(wzNode.Text) { Tag = new WeakReference(wzNode) };
        foreach (Wz_Node subNode in wzNode.Nodes)
        {
            Node subTreeNode = createNode(subNode);
            if (subTreeNode != null)
                parentNode.Nodes.Add(subTreeNode);
        }
        return parentNode;
    }

    private void sortWzNode(Wz_Node wzNode)
    {
        this.sortWzNode(wzNode, true);
    }

    private void sortWzNode(Wz_Node wzNode, bool sortByImgID)
    {
        if (wzNode.Nodes.Count > 1)
        {
            if (sortByImgID)
            {
                wzNode.Nodes.SortByImgID();
            }
            else
            {
                wzNode.Nodes.Sort();
            }
        }
        foreach (Wz_Node subNode in wzNode.Nodes)
        {
            sortWzNode(subNode, sortByImgID);
        }
    }

    private void btnItemOpenWz_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog dlg = new OpenFileDialog())
        {
            dlg.Title = "Wz檔";
            dlg.Filter = "Base.wz|*.wz";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                openWz(dlg.FileName);

            }
        }
    }

    public void openWz(string wzFilePath)
    {
        foreach (Wz_Structure wzs in openedWz)
        {
            foreach (Wz_File wz_f in wzs.wz_files)
            {
                if (string.Compare(wz_f.Header.FileName, wzFilePath, true) == 0)
                {
                    MessageBoxEx.Show("已經開啟的wz", "OK");
                    return;
                }
            }
        }

        //var Path = System.IO.Path.GetDirectoryName(wzFilePath);

        Wz_Structure wz = new Wz_Structure();

        try
        {

            if (string.Equals(Path.GetExtension(wzFilePath), ".ms", StringComparison.OrdinalIgnoreCase))
            {
                wz.LoadMsFile(wzFilePath);
            }
            else if (wz.IsKMST1125WzFormat(wzFilePath))
            {
                wz.LoadKMST1125DataWz(wzFilePath);
                if (string.Equals(Path.GetFileName(wzFilePath), "Base.wz", StringComparison.OrdinalIgnoreCase))
                {
                    string packsDir = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(wzFilePath)), "Packs");
                    if (Directory.Exists(packsDir))
                    {
                        foreach (var msFile in Directory.GetFiles(packsDir, "*.ms"))
                        {
                            wz.LoadMsFile(msFile);
                        }
                    }
                }
            }
            else
            {
                wz.Load(wzFilePath, true);
            }

            sortWzNode(wz.WzNode);

            Node node = createNode(wz.WzNode);
            TreeNode = node.AsWzNode();
            // node.Expand();
            // advTree1.Nodes.Add(node);
            this.openedWz.Add(wz);
            // QueryPerformance.End();
        }
        catch (FileNotFoundException)
        {
            MessageBoxEx.Show("檔案沒找到", "OK");
        }
        catch (Exception ex)
        {
            MessageBoxEx.Show(ex.ToString(), "OK");
            wz.Clear();
        }
        finally
        {
            //  advTree1.EndUpdate();
        }

        Wz.IsDataWz = false;
        if (Wz.GetNode("Mob").FullPathToFile.LeftStr(4) == "Data")
            Wz.IsDataWz = true;

        Wz.HasStringWz = true;
        if (Wz.HasNode("Mob/0100100.img/info/name"))
        {
            Wz.HasStringWz = false;
        }

        if (Wz.HasStringWz == false && Wz.HasNode("String") == false)
        {
            Wz.HasHardCodedStrings = true;
        }


        Wz.HasMap9Dir = false;
        if (Wz.HasNode("Map/Map/Map1"))
        {
            Wz.HasMap9Dir = true;
        }
    }

    public void RemoveWz()
    {
        foreach (var Iter in this.openedWz)
            Iter.Clear();
    }

    enum DefaultLevel
    {
        Level0 = 0,
        Level1 = 1,
        LevelMax = 2,
        LevelMaxWithCO = 3,
    }

    public void QuickView(Wz_Node node)
    {
        Wz_Node selectedNode = node;
        if (selectedNode == null)
        {
            return;
        }
        if (!Wz.IsDataWz)
        {
            Wz_File findStringWz()
            {
                foreach (Wz_Structure wz in openedWz)
                {
                    foreach (Wz_File file in wz.wz_files)
                    {
                        if (file.Type == Wz_Type.String)
                        {
                            return file;
                        }
                    }
                }
                return null;
            }

            Wz_File findItemWz()
            {
                foreach (Wz_Structure wz in openedWz)
                {
                    foreach (Wz_File file in wz.wz_files)
                    {
                        if (file.Type == Wz_Type.Item)
                        {
                            return file;
                        }
                    }
                }
                return null;
            }

            Wz_File findEtcWz()
            {
                foreach (Wz_Structure wz in openedWz)
                {
                    foreach (Wz_File file in wz.wz_files)
                    {
                        if (file.Type == Wz_Type.Etc)
                        {
                            return file;
                        }
                    }
                }
                return null;
            }

            Wz_Image image;
            Wz_File wzf = selectedNode.GetNodeWzFile();
            if (wzf == null)
            {
                // labelItemStatus.Text = "The WZ file where the node belongs to has not been found.";
                return;
            }

            if (!this.stringLinker.HasValues)
            {
                this.stringLinker.Load(findStringWz(), findItemWz(), findEtcWz());
            }

            object obj = null;
            string fileName = null;
            switch (wzf.Type)
            {
                case Wz_Type.Character:
                    if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                        return;

                    var gear = Gear.CreateFromNode(image.Node, PluginManager.FindWz);
                    obj = gear;
                    if (gear != null)
                    {
                        fileName = gear.ItemID + ".png";
                    }
                    break;
                case Wz_Type.Item:

                    Wz_Node itemNode = selectedNode;
                    if (Regex.IsMatch(itemNode.FullPathToFile, @"^Item\\(Cash|Consume|Etc|Install|Cash)\\\d{4,6}.img\\\d+$") || Regex.IsMatch(itemNode.FullPathToFile, @"^Item\\Special\\0910.img\\\d+$"))
                    {
                        var item = Item.CreateFromNode(itemNode, PluginManager.FindWz);
                        obj = item;
                        if (item != null)
                        {
                            fileName = item.ItemID + ".png";
                        }
                    }
                    else if (Regex.IsMatch(itemNode.FullPathToFile, @"^Item\\Pet\\\d{7}.img"))
                    {

                        if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                            return;
                        var item = Item.CreateFromNode(image.Node, PluginManager.FindWz);
                        obj = item;
                        if (item != null)
                        {
                            fileName = item.ItemID + ".png";
                        }
                    }

                    break;
                case Wz_Type.Skill:
                    Wz_Node skillNode = selectedNode;
                    //模式路径分析
                    if (Regex.IsMatch(skillNode.FullPathToFile, @"^Skill\d*\\Recipe_\d+.img\\\d+$"))
                    {
                        Recipe recipe = Recipe.CreateFromNode(skillNode);
                        obj = recipe;
                        if (recipe != null)
                        {
                            fileName = "recipe_" + recipe.RecipeID + ".png";
                        }
                    }
                    else if (Regex.IsMatch(skillNode.FullPathToFile, @"^Skill\d*\\\d+.img\\skill\\\d+$"))
                    {
                        WzComparerR2.CharaSim.Skill skill = WzComparerR2.CharaSim.Skill.CreateFromNode(skillNode, PluginManager.FindWz);
                        if (skill != null)
                        {
                            switch (this.skillDefaultLevel)
                            {
                                case DefaultLevel.Level0: skill.Level = 0; break;
                                case DefaultLevel.Level1: skill.Level = 1; break;
                                case DefaultLevel.LevelMax: skill.Level = skill.MaxLevel; break;
                                case DefaultLevel.LevelMaxWithCO: skill.Level = skill.MaxLevel + 2; break;
                            }
                            obj = skill;
                            fileName = "skill_" + skill.SkillID + ".png";
                        }
                    }
                    break;

                case Wz_Type.Mob:
                    if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                        return;
                    var mob = WzComparerR2.CharaSim.Mob.CreateFromNode(image.Node, PluginManager.FindWz);
                    obj = mob;
                    if (mob != null)
                    {
                        fileName = mob.ID + ".png";
                    }
                    break;

                case Wz_Type.Npc:
                    if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                        return;
                    var npc = WzComparerR2.CharaSim.Npc.CreateFromNode(image.Node, PluginManager.FindWz);
                    obj = npc;
                    if (npc != null)
                    {
                        fileName = npc.ID + ".png";
                    }
                    break;

                case Wz_Type.Etc:

                    Wz_Node setItemNode = selectedNode;
                    if (Regex.IsMatch(setItemNode.FullPathToFile, @"^Etc\\SetItemInfo.img\\-?\d+$"))
                    {
                        SetItem setItem;



                    }
                    break;

                case Wz_Type.Map:
                    // if (selectedNode.Text.Length == 13 && selectedNode.Text.RightStr(4) == ".img")
                    {
                        var map = new WzComparerR2.CharaSim.Map();
                        WzComparerR2.CharaSim.Map.ImgNode = selectedNode;
                        obj = map;
                    }
                    break;
            }


        }
        else
        {
            if (!this.stringLinker.HasValues)
            {
                this.stringLinker.Load(Wz.GetNode("String"), Wz.GetNode("Item"), Wz.GetNode("Etc"));
            }

            string[] Split = selectedNode.FullPathToFileEx().Split('/');
            object obj = null;
            string fileName = null;
            Wz_Image image;
            switch (Split[1])
            {
                case "Character":
                    if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                        return;

                    var gear = Gear.CreateFromNode(image.Node, PluginManager.FindWz);
                    obj = gear;
                    if (gear != null)
                    {
                        fileName = gear.ItemID + ".png";
                    }
                    break;
                case "Item":


                    Wz_Node itemNode = selectedNode;
                    if (Regex.IsMatch(itemNode.FullPathToFile.Replace("Data\\", ""), @"^Item\\(Cash|Consume|Etc|Install|Cash)\\\d{4,6}.img\\\d+$") || Regex.IsMatch(itemNode.FullPathToFile, @"^Item\\Special\\0910.img\\\d+$"))
                    {
                        var item = Item.CreateFromNode(itemNode, PluginManager.FindWz);
                        obj = item;
                        if (item != null)
                        {
                            fileName = item.ItemID + ".png";
                        }
                    }
                    else if (Regex.IsMatch(itemNode.FullPathToFile.Replace("Data\\", ""), @"^Item\\Pet\\\d{7}.img"))
                    {

                        if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                            return;
                        var item = Item.CreateFromNode(image.Node, PluginManager.FindWz);
                        obj = item;
                        if (item != null)
                        {
                            fileName = item.ItemID + ".png";
                        }
                    }

                    break;
                case "Skill":
                    Wz_Node skillNode = selectedNode;
                    //模式路径分析
                    if (Regex.IsMatch(skillNode.FullPathToFile.Replace("Data\\", ""), @"^Skill\d*\\Recipe_\d+.img\\\d+$"))
                    {
                        Recipe recipe = Recipe.CreateFromNode(skillNode);
                        obj = recipe;
                        if (recipe != null)
                        {
                            fileName = "recipe_" + recipe.RecipeID + ".png";
                        }
                    }
                    else if (Regex.IsMatch(skillNode.FullPathToFile.Replace("Data\\", ""), @"^Skill\d*\\\d+.img\\skill\\\d+$"))
                    {
                        WzComparerR2.CharaSim.Skill skill = WzComparerR2.CharaSim.Skill.CreateFromNode(skillNode, PluginManager.FindWz);
                        if (skill != null)
                        {
                            switch (this.skillDefaultLevel)
                            {
                                case DefaultLevel.Level0: skill.Level = 0; break;
                                case DefaultLevel.Level1: skill.Level = 1; break;
                                case DefaultLevel.LevelMax: skill.Level = skill.MaxLevel; break;
                                case DefaultLevel.LevelMaxWithCO: skill.Level = skill.MaxLevel + 2; break;
                            }
                            obj = skill;
                            fileName = "skill_" + skill.SkillID + ".png";
                        }
                    }
                    break;

                case "Mob":
                    if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                        return;
                    var mob = WzComparerR2.CharaSim.Mob.CreateFromNode(image.Node, PluginManager.FindWz);
                    obj = mob;
                    if (mob != null)
                    {
                        fileName = mob.ID + ".png";
                    }
                    break;

                case "Npc":
                    if ((image = selectedNode.GetValue<Wz_Image>()) == null || !image.TryExtract())
                        return;
                    var npc = WzComparerR2.CharaSim.Npc.CreateFromNode(image.Node, PluginManager.FindWz);
                    obj = npc;
                    if (npc != null)
                    {
                        fileName = npc.ID + ".png";
                    }
                    break;

                case "Etc":

                    Wz_Node setItemNode = selectedNode;
                    if (Regex.IsMatch(setItemNode.FullPathToFile, @"^Etc\\SetItemInfo.img\\-?\d+$"))
                    {
                        SetItem setItem;


                    }
                    break;
                case "Map":
                    // if (selectedNode.Text.Length == 13 && selectedNode.Text.RightStr(4) == ".img")
                    {
                        var map = new WzComparerR2.CharaSim.Map();
                        WzComparerR2.CharaSim.Map.ImgNode = selectedNode;
                        obj = map;
                    }
                    break;
            }


        }

    }


    void CellClick(BaseDataGridView DataGrid, DataGridViewCellEventArgs e)
    {
        var ID = DataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        var LeftNum = LeftStr(ID, 1);
        //  var Node = Wz.GetNode("Map/Map/Map" + LeftNum + '/' + ID + ".img/info/link");

        var NodePath = (Wz.HasMap9Dir ? "Map/Map/Map" + LeftNum + '/' : "Map/Map/") + ID + ".img";
        var Node = Wz.GetNode(NodePath + "/info/link");
        if (Node == null)
            Map.ID = ID;
        else
            Map.ID = Node.Value.ToString();

        LeftNum = LeftStr(Map.ID, 1);
        //Node = Wz.GetNode("Map/Map/Map" + LeftNum + "/" + Map.ID + ".img/miniMap");
        Node = Wz.GetNode(NodePath + "/miniMap");


    }

    private void MainForm_Load(object sender, EventArgs e)
    {



        Graphics graphics = this.CreateGraphics();
        float dpiX = graphics.DpiX;
        float dpiY = graphics.DpiY;
        DPIUtil.dpiX = dpiX;
        DPIUtil.dpiY = dpiY;
    }

    private void OpenFolderButton_Click(object sender, EventArgs e)
    {

        if (SelectFolderForm.Instance == null)
            new SelectFolderForm().Show();
        else
            SelectFolderForm.Instance.Show();
        OpenFolderButton.Enabled = false;
    }

    private void MapListBox_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    public static void LoadMap()
    {
        Map.LoadMap("000050000");
        int PX = 0, PY = 0;
        foreach (var Portals in MapPortal.PortalList)
        {
            if (Portals.Type == 0)
            {
                PX = Portals.X;
                PY = Portals.Y;
                break;
            }
        }

        Game.Player.X = PX;
        Game.Player.Y = PY;
        Foothold BelowFH = null;
        Vector2 Below = FootholdTree.Instance.FindBelow(new Vector2(PX, PY - 2), ref BelowFH);
        Game.Player.FH = BelowFH;
        Game.Player.FaceDir = FaceDir.None;
        Game.Player.JumpState = JumpState.jsNone;

        if (Pet.Instance != null)
        {
            Pet.Instance.X = Game.Player.X;
            Pet.Instance.Y = Game.Player.Y;
            Pet.Instance.JumpState = JumpState.jsFalling;
        }




        EngineFunc.SpriteEngine.Camera.X = PX - Map.DisplaySize.X / 2;
        EngineFunc.SpriteEngine.Camera.Y = PY - (Map.DisplaySize.Y / 2) - 100;
        if (EngineFunc.SpriteEngine.Camera.X > Map.Right)
            EngineFunc.SpriteEngine.Camera.X = Map.Right;
        if (EngineFunc.SpriteEngine.Camera.X < Map.Left)
            EngineFunc.SpriteEngine.Camera.X = Map.Left;
        if (EngineFunc.SpriteEngine.Camera.Y > Map.Bottom)
            EngineFunc.SpriteEngine.Camera.Y = Map.Bottom;
        if (EngineFunc.SpriteEngine.Camera.Y < Map.Top)
            EngineFunc.SpriteEngine.Camera.Y = Map.Top;

        Map.OffsetY = (Map.DisplaySize.Y - 600) / 2;

        EngineFunc.SpriteEngine.Move(1);

        Game.Player.JumpState = JumpState.jsFalling;
        if (!LoadedEff)
        {
            SetEffect.LoadList();
            ItemEffect.LoadList();
            TamingMob.LoadSaddleList();
            /*
            foreach (var Iter in this.panel1.Controls)
            {
                if (Iter.GetType().Name == "Button")
                {
                    ((System.Windows.Forms.Button)Iter).Enabled = true;
                }
            }

            if (!Wz.HasNode("Character/TamingMob"))
                MountButton.Enabled = false;
            if (!Wz.HasNode("Item/Cash"))
            {
                CashButton.Enabled = false;
                CashEffectButton.Enabled = false;
            }
            if (!Wz.HasNode("Morph"))
                MorphButton.Enabled = false;
            if (!Wz.HasNode("Effect/DamageSkin.img") && !Wz.HasNode("Etc/DamageSkin.img"))
                DamageSkinButton.Enabled = false;
            if (!Wz.HasNode("Character/Accessory/01142000.img"))
                MedalButton.Enabled = false;
            if (!Wz.HasNode("Item/Install/0370.img"))
                TitleButton.Enabled = false;
            if (!Wz.HasNode("Character/Ring/01112100.img"))
                RingButton.Enabled = false;
            if (!Wz.HasNode("Character/Familiar"))
                FamiliarButton.Enabled = false;
            if (!Wz.HasNode("Character/Android"))
                AndroidButton.Enabled = false;
            if (!Wz.HasNode("Character/Totem"))
                TotemEffectButton.Enabled = false;
            if (!Wz.HasNode("Item/Consume/0259.img"))
                SoulEffectButton.Enabled = false;
            if (!Wz.HasNode("Effect/BasicEff.img"))
                DamageSkinButton.Enabled = false;
            if (Wz.HasNode("Etc/DamageSkin.img"))
                DamageSkinButton.Enabled = true;
            if (Wz.IsDataWz)
                ReactorButton.Enabled = false;
            */
            LoadedEff = true;
        }


    }

    static bool LoadedEff;
    private void LoadMapButton_Click(object sender, EventArgs e)
    {
        LoadMap();

    }

    [DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool Repaint);

    public static void SetScreenNormal()
    {
        RenderFormDraw.ScreenMode = ScreenMode.Normal;

        // Map.DisplaySize.X = Split[0].ToInt();
        // Map.DisplaySize.Y = Split[1].ToInt();
        bool Result;
        Result = MoveWindow(MainForm.Instance.Handle, MainForm.Instance.Left, MainForm.Instance.Top, Map.DisplaySize.X + 287, Map.DisplaySize.Y + 152, true);
        //this.Width = Map.DisplaySize.X + 283;
        //this.Height = Map.DisplaySize.Y + 124;

        Result = MoveWindow(RenderForm.Handle, 257, 93, Map.DisplaySize.X, Map.DisplaySize.Y, true);
        // RenderForm.Width = Map.DisplaySize.X;
        //RenderForm.Height = Map.DisplaySize.Y;
        RenderForm.RenderFormDraw.Width = Map.DisplaySize.X;
        RenderForm.RenderFormDraw.Height = Map.DisplaySize.Y;
        RenderForm.RenderFormDraw.Parent = RenderForm;
        EngineFunc.SpriteEngine.VisibleWidth = Map.DisplaySize.X + 200;
        EngineFunc.SpriteEngine.VisibleHeight = Map.DisplaySize.Y + 200;
        Map.ResetPos = true;
        MainForm.Instance.CenterToScreen();
        MainForm.Instance.Refresh();
    }
    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetScreenNormal();
    }

    private void comboBox2_Click(object sender, EventArgs e)
    {
    }

    private void SerachMapBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void MobButton_Click(object sender, EventArgs e)
    {
        void ShowForm(Form Instance, Action NewForm)
        {
            if (Instance == null)
                NewForm();
            else
                Instance.Show();
        }
        switch (((System.Windows.Forms.Button)sender).Name)
        {
            case "ViewButton": ShowForm(ViewForm.Instance, () => new ViewForm().Show()); break;
            case "MobButton": ShowForm(MobForm.Instance, () => new MobForm().Show()); break;
            case "NpcButton": ShowForm(NpcForm.Instance, () => new NpcForm().Show()); break;
            // case "AvatarButton": ShowForm(AvatarForm.Instance, () => new AvatarForm().Show()); break;
            case "ChairButton": ShowForm(ChairForm.Instance, () => new ChairForm().Show()); break;
            case "MountButton": ShowForm(MountForm.Instance, () => new MountForm().Show()); break;
            case "CashEffectButton": ShowForm(CashEffectForm.Instance, () => new CashEffectForm().Show()); break;
            case "MorphButton": ShowForm(MorphForm.Instance, () => new MorphForm().Show()); break;


            case "MedalButton": ShowForm(MedalForm.Instance, () => new MedalForm().Show()); break;
            case "TitleButton": ShowForm(TitleForm.Instance, () => new TitleForm().Show()); break;
            case "RingButton": ShowForm(RingForm.Instance, () => new RingForm().Show()); break;
            case "PetButton": ShowForm(PetForm.Instance, () => new PetForm().Show()); break;



            case "ConsumeButton": ShowForm(ConsumeForm.Instance, () => new ConsumeForm().Show()); break;


            case "TotemEffectButton": ShowForm(TotemEffectForm.Instance, () => new TotemEffectForm().Show()); break;
            case "SoulEffectButton": ShowForm(SoulEffectForm.Instance, () => new SoulEffectForm().Show()); break;




            case "EffectRingButton": ShowForm(EffectRingForm.Instance, () => new EffectRingForm().Show()); break;
            case "ChatRingButton": ShowForm(ChatRingForm.Instance, () => new ChatRingForm().Show()); break;
            case "EffectButton": ShowForm(EffectForm.Instance, () => new EffectForm().Show()); break;
        }
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Alt)
            e.Handled = true;

    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private List<PictureBox> PictureBoxList = new();
    private System.Windows.Forms.ToolTip ToolTip = new();
    private void WorldMapListGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {





    }

    private void FullScreenButton_Click(object sender, EventArgs e)
    {
        RenderFormDraw.ScreenMode = ScreenMode.FullScreen;
        this.Controls.Remove(RenderForm);
        RenderForm.TopLevel = true;
        RenderForm.FormBorderStyle = FormBorderStyle.None;
        RenderForm.Bounds = Screen.PrimaryScreen.Bounds;

        RenderForm.RenderFormDraw.Width = Map.ScreenWidth;
        RenderForm.RenderFormDraw.Height = Map.ScreenHeight;
        RenderForm.RenderFormDraw.Parent = RenderForm;
        EngineFunc.SpriteEngine.VisibleWidth = Map.DisplaySize.X + 200;
        EngineFunc.SpriteEngine.VisibleHeight = Map.DisplaySize.Y + 200;
        Map.ResetPos = true;
        Refresh();

    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        if (AvatarForm.Instance != null)
        {
            AvatarForm.Instance.Width = this.Width - 50;
            AvatarForm.Instance.Height = this.Height - 140;
        }
    }
}

