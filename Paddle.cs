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

        public Paddle(float xPos, float yPos, KeyboardKey upKey, KeyboardKey downKey)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.leftKey = upKey;
            this.rightKey = downKey;
        }

        // Enkel metod för att rita ut paddlen
        public void Draw()
        {
            Raylib.DrawRectangle((int)xPos, (int)yPos, xRec, yRec, Color.WHITE);
        }
    }
}
