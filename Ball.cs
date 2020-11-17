using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Ball
    {
        private int xRec = 25;
        private int yRec = 25;

        private float xPos = 0;
        private float yPos = 0;

        public Ball()
        {
            this.xPos = Raylib.GetScreenWidth()/2-(xRec/2);
            this.yPos = Raylib.GetScreenHeight()/2-(yRec/2);
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)xPos, (int)yPos, xRec, yRec, Color.WHITE);
        }
    }
}
