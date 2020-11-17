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

        public Paddle(KeyboardKey upKey, KeyboardKey downKey)
        {
            this.xPos = Raylib.GetScreenWidth()/2-(xRec/2);
            System.Console.WriteLine();
            this.yPos = Raylib.GetScreenHeight()-30;
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
