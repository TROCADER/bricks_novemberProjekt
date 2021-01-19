using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Paddle
    {
        // De två tangenterna för att kunna styra paddlen
        public KeyboardKey leftKey;
        public KeyboardKey rightKey;

        // Initierar en rektangel med mått baserade på skärmens mått
        public Rectangle rectangle = new Rectangle(Raylib.GetScreenHeight() / 2 + (150 / 2), Raylib.GetScreenHeight() - 30, 150, 20);

        // Ställer in paddels position
        // Hämtar in hur stor skärmen är och därefter positionerar enligt den informationen
        public Paddle(KeyboardKey upKey, KeyboardKey downKey)
        {
            this.leftKey = upKey;
            this.rightKey = downKey;
        }

        // Enkel metod för att rita ut paddlen
        public void Draw()
        {
            Raylib.DrawRectangleRec(rectangle, Color.WHITE);
        }

        // Används för att återställa position vid restart av spelet
        public void Reset()
        {
            rectangle.x = Raylib.GetScreenHeight() / 2 + (150 / 2);
            rectangle.y = Raylib.GetScreenHeight() - 30;
        }

        public void Update()
        {
            // Styrning av paddlen
            // Ändrar x postion i paddelns C#-klass
            if (Raylib.IsKeyDown(leftKey))
            {
                rectangle.x -= 10f;
            }

            else if (Raylib.IsKeyDown(rightKey))
            {
                rectangle.x += 10f;
            }

            // Begränsar så att paddeln inte kan åka utanför bilden
            // Kanske byter ut så att paddlen istället teleporterar till motsatta sidan
            if (rectangle.x <= 10)
            {
                rectangle.x += 10f;
            }

            else if (rectangle.x >= Raylib.GetScreenWidth() - rectangle.width - 10)
            {
                rectangle.x -= 10f;
            }
        }
    }
}
