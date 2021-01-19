using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Ball
    {
        public Sound bounceSound = Raylib.LoadSound("resources/bounce.mp3");

        // Ställer in hastigheten på bollen
        public float xMov = 7f;
        public float yMov = 7f;

        public bool isDead = false;

        //Ändrade om till en Raylib Rectangle då det visade sig vara mer praktiskt
        public Rectangle rectangle = new Rectangle(Raylib.GetScreenWidth() / 2 - (25 / 2), Raylib.GetScreenHeight() / 2 - (25 / 2), 25, 25);

        private Random random = new Random();

        public void Draw()
        {
            Raylib.DrawRectangleRec(rectangle, Color.WHITE);
        }

        public void Update()
        {
            rectangle.x += xMov;
            rectangle.y += yMov;

            if (rectangle.x > Raylib.GetScreenWidth() - rectangle.width || rectangle.x < 0)
            {
                xMov = -xMov;
            }

            if (rectangle.y > Raylib.GetScreenHeight() - rectangle.height || rectangle.y < 0)
            {
                yMov = -yMov;
            }

            if (rectangle.y > Raylib.GetScreenHeight() - rectangle.height)
            {
                isDead = true;
            }
        }

        // Används för att återställa position vid restart av spelet
        public void Reset()
        {
            rectangle.x = Raylib.GetScreenWidth() / 2 - (25 / 2);
            rectangle.y = Raylib.GetScreenHeight() / 2 - (25 / 2);
        }

        public void RandomBall()
        {
            // Gör att bollen åker ej i en konstant bana
            // --> åker slumpartat
            // --> för ett roligare och svårare spel
            yMov = -yMov;

            if (xMov < 0)
            {
                xMov = -random.Next(4, 11);
            }

            else if (xMov > 0)
            {
                xMov = random.Next(4, 11);
            }
        }
    }
}
