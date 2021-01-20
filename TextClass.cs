using System;
using Raylib_cs;
using System.Collections.Generic;

namespace bricks_novemberProjekt
{
    public class TextClass
    {    
        // Tre storleksfonter jag använder mig av
        // Har de definerade utanför metoderna så att jag kan använda de i alla metoder
        private List<int> fonts = new List<int>() { 150, 50, 30, 20 };

        public void StartText()
        {
            List<string> text = new List<string>() {
                "Bricks",
                "Press SPACE to start",
                "Move the paddle using left and right arrowkeys",
                "Destroy all the Bricks and bounce the ball on the paddle"
            };
            
            for (int i = 0; i < text.Count; i++)
            {
                Raylib.DrawText(text[i], Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text[i], fonts[i], default).X / 2, Raylib.GetScreenHeight() / 2 + (100 * i) - 50 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text[i], fonts[i], default).Y / 2, fonts[i], Color.WHITE);
            }
        }

        public void GameOverText()
        {
            List<string> text = new List<string>() {
                "Game Over",
                "Press SPACE to restart",
                "You broke a total of: " + Game.bricksBroken + " bricks!"
            };
            
            for (int i = 0; i < text.Count; i++)
            {
                Raylib.DrawText(text[i], Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text[i], fonts[i], default).X / 2, Raylib.GetScreenHeight() / 2 + (100 * i) - 50 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text[i], fonts[i], default).Y / 2, fonts[i], Color.WHITE);
            }
        }

        public void WinText()
        {
            List<string> text = new List<string>() {
                "Victory!",
                "Press SPACE to restart",
                "You broke a total of: " + Game.bricksBroken + " bricks!"
            };
            
            for (int i = 0; i < text.Count; i++)
            {
                Raylib.DrawText(text[i], Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text[i], fonts[i], default).X / 2, Raylib.GetScreenHeight() / 2 + (100 * i) - 50 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text[i], fonts[i], default).Y / 2, fonts[i], Color.WHITE);
            }
        }
    }
}
