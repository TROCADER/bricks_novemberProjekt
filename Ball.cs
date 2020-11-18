using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Ball
    {
        // Ställer in storleken på bollen eftersom att dn är en kvadrat (normalt sett)
        // Har använt 2 variabler istället för 1 eftersom att (om man vill) så ska man kunna spela med en rektangulär boll (inte en boll vid det laget)
        private int xRec = 25;
        private int yRec = 25;

        private float xPos = 0;
        private float yPos = 0;

        // Ställer in paddels position
        // Hämtar in hur stor skärmen är och därefter positionerar enligt den informationen
        public Ball()
        {
            this.xPos = Raylib.GetScreenWidth()/2-(xRec/2);
            this.yPos = Raylib.GetScreenHeight()/2-(yRec/2);
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)xPos, (int)yPos, xRec, yRec, Color.WHITE);
        }
    }
}
