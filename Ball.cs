using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Ball
    {
        // Ställer in hastigheten på bollen
        private float xMov = 5f;
        private float yMov = 5f;

        //Ändrade om till en Raylib Rectangle då det visade sig vara mer praktiskt
        public Rectangle rectangle = new Rectangle(Raylib.GetScreenWidth()/2-(25/2), Raylib.GetScreenHeight()/2-(25/2), 25, 25);

        // Ställer in paddels position
        // Hämtar in hur stor skärmen är och därefter positionerar enligt den informationen
        public Ball()
        {
            
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rectangle, Color.WHITE);
        }

        public void UpdatePos()
        {
            rectangle.x += xMov;
            rectangle.y += yMov;

            if (rectangle.x > Raylib.GetScreenWidth()-rectangle.width || rectangle.x < 0)
            {
                xMov = -xMov;
            }

            if (rectangle.y > Raylib.GetScreenHeight()-rectangle.height || rectangle.y < 0)
            {
                yMov = -yMov;
            }
        }
    }
}
