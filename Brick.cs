using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Brick
    {
        public int xRec = 100;
        public int yRec = 30;

        public float xPos = 0;
        public float yPos = 0;

        public Brick(float xPos, float yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)xPos, (int)yPos, xRec, yRec, Color.WHITE);
        }
    }
}
