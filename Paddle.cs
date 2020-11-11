using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Paddle
    {
        public int xRec = 150;
        public int yRec = 20;

        public float xPos = 0;
        public float yPos = 0;

        public KeyboardKey leftKey;
        public KeyboardKey rightKey;

        public Paddle(float xPos, float yPos, KeyboardKey upKey, KeyboardKey downKey)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.leftKey = upKey;
            this.rightKey = downKey;
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)xPos, (int)yPos, xRec, yRec, Color.WHITE);
        }
    }
}
