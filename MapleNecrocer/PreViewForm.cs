using Aspose.PSD.FileFormats.Psd.Layers;
using Aspose.PSD.FileFormats.Psd;
using Aspose.PSD;

namespace MapleNecrocer;

public partial class PreViewForm : Form
{
    public PreViewForm()
    {
        InitializeComponent();
        Instance = this;
    }
    public static PreViewForm Instance;
    
    private void PreViewForm_Load(object sender, EventArgs e)
    {
        button1.Parent = pictureBox1;
        button2.Parent = pictureBox1;
    }
    static int SaveCount;
    PsdImage MainPSD;
    PsdImage Back;
    void SavePSD()
    {
        Bitmap Bmp = new(System.Environment.CurrentDirectory + "\\Temp\\" + "walk1.0.png");
        int Width = Bmp.Width;
        int Height = Bmp.Height;
        Bmp.Dispose();

        DirectoryInfo Folder = new DirectoryInfo(System.Environment.CurrentDirectory + "\\Temp\\");
        foreach (FileInfo File in Folder.GetFiles())
        {
            if (File.Extension == ".png")
            {
                var PsdImg = new PsdImage(Width, Height);
                var PngStream = new FileStream(File.FullName, FileMode.Open);
                Layer Layer = null;
                Layer = new Layer(PngStream);
                PsdImg.AddLayer(Layer);
                var FileName = System.IO.Path.GetFileNameWithoutExtension(File.FullName);
                PsdImg.Save(System.Environment.CurrentDirectory + "\\Temp\\" + FileName + ".psd", true);

                PngStream.Close();
                PngStream.Dispose();
                PsdImg.Dispose();
            }
        }


        MainPSD = (PsdImage)Aspose.PSD.Image.Load(System.Environment.CurrentDirectory + "\\Avatar_CapeMod.psd");
        Back = (PsdImage)Aspose.PSD.Image.Load(System.Environment.CurrentDirectory + "\\back2.psd");

        var graphic = new Aspose.PSD.Graphics(MainPSD.Layers[0]);
        graphic.Clear(new Aspose.PSD.Color());

        var graphic2 = new Aspose.PSD.Graphics((MainPSD.Layers[1] as LayerGroup).Layers[3]);
        graphic2.Clear(new Aspose.PSD.Color());

        (MainPSD.Layers[1] as LayerGroup).Layers[3].Resize(2750, 3500);
        (MainPSD.Layers[1] as LayerGroup).Layers[3].Left = 0;
        (MainPSD.Layers[1] as LayerGroup).Layers[3].Top = 0;
        (MainPSD.Layers[1] as LayerGroup).Layers[3].Right = 2750;
        (MainPSD.Layers[1] as LayerGroup).Layers[3].Bottom = 3500;

        foreach (var Iter in AvatarForm.Instance.ImageFormList)
        {
            var PsdImg = Aspose.PSD.RasterImage.Load(System.Environment.CurrentDirectory + "\\Temp\\" + Iter.FrameName + ".psd");
            if (Morph.IsUse)
              graphic.DrawImage(Back, new Aspose.PSD.Point(Iter.NewDrawPosX - 94, Iter.NewDrawPosY - 135));
            graphic.DrawImage(PsdImg, new Aspose.PSD.Point(Iter.NewDrawPosX - 188, Iter.NewDrawPosY - 180));
            (MainPSD.Layers[1] as LayerGroup).Layers[3].SetPixel(Iter.NewRedPointX, Iter.NewRedPointY, Aspose.PSD.Color.Red);
            PsdImg.Dispose();
        }

        SaveCount += 1;
        MainPSD.Save(System.Environment.CurrentDirectory + "\\PSD\\" + "NewAvatar" + SaveCount.ToString() + ".psd", true);
        MessageBox.Show("儲存NewAvatar" + SaveCount.ToString() + ".psd 完成" + "\n" + "存放在PSD資料夾裡面");

        MainPSD.Dispose();
        Back.Dispose();
        AvatarForm.Instance.ImageFormList.Clear();
        foreach (var Iter in AvatarForm.Instance.ImageFormList)
            Iter.Close();
        this.Close();
        AvatarForm.Instance.SavePsdButton.Enabled = true;
    }

    private void PreViewForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (MainPSD != null)
            MainPSD.Dispose();
        if (Back != null)
            MainPSD.Dispose();
        AvatarForm.Instance.ImageFormList.Clear();
        foreach (var Iter in AvatarForm.Instance.ImageFormList)
            Iter.Close();
        AvatarForm.Instance.SavePsdButton.Enabled = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        button1.Text = "儲存中, 請稍後...";
        button1.Refresh();  
        SavePSD();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        button2.Text = "儲存中, 請稍後...";
        button2.Refresh();
        SavePSD();
    }
}
