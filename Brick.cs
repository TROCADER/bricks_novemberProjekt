using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Brick
    {
        public float xPos = 0;
        public float yPos = 0;

        public Rectangle rectangle;

        public Brick(float xPos, float yPos)
        {
            rectangle = new Rectangle(xPos, yPos, 100, 30);
            this.xPos = xPos;
            this.yPos = yPos;
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rectangle, Color.WHITE);
        }
    }
}
