using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Paddle
    {
        // Ställer in storleken på paddlen
        public int xRec = 150;
        public int yRec = 20;

        // Variabler för paddelns position, byts ut av klassen Window som ställer in paddlens position
        public float xPos = 0;
        public float yPos = 0;

        // De två tangenterna för att kunna styra paddlen
        public KeyboardKey leftKey;
        public KeyboardKey rightKey;

        // Initierar en rektangel med mått baserade på skärmens mått
        public Rectangle rectangle = new Rectangle(Raylib.GetScreenHeight()/2+(150/2), Raylib.GetScreenHeight()-30, 150, 20);

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
    }
}
