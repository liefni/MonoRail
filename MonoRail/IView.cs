using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    public interface IView : IRectangle
    {
        Level Level { get; set; }

        GameObject ObjectToFollow { get; set; }
        int FollowBorderTop { get; set; }
        int FollowBorderRight { get; set; }
        int FollowBorderBottom { get; set; }
        int FollowBorderLeft { get; set; }

        void SetFollowBorders(int top, int right, int bottom, int left);


        int ScreenX { get; set; }
        int ScreenY { get; set; }
        int ScreenWidth { get; set; }
        int ScreenHeight { get; set; }

        void Update();
        void Draw();
    }
}
