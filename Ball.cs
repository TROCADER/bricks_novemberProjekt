using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Ball
    {
        // Ställer in hastigheten på bollen
        public float xMov = 5f;
        public float yMov = 5f;

        public bool isDead = false;

        //Ändrade om till en Raylib Rectangle då det visade sig vara mer praktiskt
        public Rectangle rectangle = new Rectangle(Raylib.GetScreenWidth()/2-(25/2), Raylib.GetScreenHeight()/2-(25/2), 25, 25);

        // Initierar paddlen för kollision
        Paddle paddle = new Paddle(KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT);

        public void Draw()
        {
            Raylib.DrawRectangleRec(rectangle, Color.WHITE);
        }

        public void Update()
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

            if (rectangle.y > Raylib.GetScreenHeight()-rectangle.height)
            {
                isDead = true;
            }
        }

        // Används för att återställa position vid restart av spelet
        public void Reset()
        {
            rectangle.x = Raylib.GetScreenWidth()/2-(25/2);
            rectangle.y = Raylib.GetScreenHeight()/2-(25/2);
        }
    }
}
