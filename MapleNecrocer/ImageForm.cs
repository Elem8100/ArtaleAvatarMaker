using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleNecrocer;


public class ImageForm : AlphaForm
{
    public ImageForm()
    {
        TopLevel = false;

    }

    bool MouseDown;
    Point LastLocation;
    public int RedPointX, RedPointY;
    public int NewRedPointX, NewRedPointY;
    public int DrawPosX, DrawPosY;
    public int NewDrawPosX, NewDrawPosY;
    public string FrameName;

    protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
    {
        MouseDown = false;
        int sx = this.Left - PreViewForm.Instance.AutoScrollPosition.X+188;
        int sy = this.Top - PreViewForm.Instance.AutoScrollPosition.Y+180;

        NewRedPointX = RedPointX + (sx - DrawPosX);
        NewRedPointY = RedPointY + (sy - DrawPosY);
        // NewDrawPosX = DrawPosX - (this.Left - DrawPosX);
        // NewDrawPosY = DrawPosY - (this.Top - DrawPosY);
        NewDrawPosX = sx;
        NewDrawPosY = sy;
       
       
    }

    protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
    {
        MouseDown = true;
        LastLocation = e.Location;
    }

    protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
    {
        if (MouseDown)
        {
            Location = new Point((Location.X - LastLocation.X) + e.X, (Location.Y - LastLocation.Y) + e.Y);
        }
    }

}


