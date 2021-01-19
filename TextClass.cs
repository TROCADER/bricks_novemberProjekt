using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class TextClass
    {
        // Tre storleksfonter jag använder mig av
        // Har de definerade utanför metoderna så att jag kan använda de i alla metoder
        private int fontSize = 0;
        private int fontSize1 = 150;
        private int fontSize2 = 50;
        private int fontSize3 = 30;
        private int fontSize4 = 20;

        private string text = "";

        public void StartText()
        {
            text = "Bricks";
            fontSize = fontSize1;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            text = "Press SPACE to start";
            fontSize = fontSize2;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            text = "Move the paddle using left and right arrowkeys";
            fontSize = fontSize3;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) + 200 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            text = "Destroy all the Bricks and bounce the ball on the paddle";
            fontSize = fontSize4;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) + 300 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);
        }

        public void GameOverText()
        {
            text = "Game Over";
            fontSize = fontSize1;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            text = "Press SPACE to restart";
            fontSize = fontSize2;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            // Använder mig av 2 strings eftersom att vid kombination med en variabel så mätte den inte korrekt, utan enbart mätte tills variabeln tog plats
            // --> texten blev inte centrerad
            text = "You broke a total of: " + Game.bricksBroken + " bricks!";
            fontSize = fontSize3;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) - 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);
        }

        public void WinText()
        {
            text = "Victory!";
            fontSize = fontSize1;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            text = "Press SPACE to restart";
            fontSize = fontSize2;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            text = "You broke a total of: " + Game.bricksBroken + " bricks!";
            fontSize = fontSize3;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) - 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);
        }
    }
}
