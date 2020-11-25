using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Window
    {
        // Ställer in storleken av fönstret ()
        private int xWindowSize = 980;
        private int yWindowSize = 720;

        // Ställer in hastigheten av paddlen och bollen
        private float paddleSpeed = 10f;

        public Window()
        {
            // Initierar fönstret samt begränsar FPS'en till 60 på grund av varierande FPS
            // --> programmet körs snabbare eller långsammare beroende på datorn kapabilitet
            // --> kan leda till en försämrad spelupplevelse, samt svårare att koda efter
            Raylib.InitWindow(xWindowSize, yWindowSize, "Bricks");
            Raylib.SetTargetFPS(60);

            Paddle paddle = new Paddle(KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT);
            Ball ball = new Ball();

            // Initierar alla tegelstenar
            // Hittade hur man gör för att initiera genom en loop, men efter tester så kunde jag enbart göra 1 loop, crash på andra loopen
            // https://stackoverflow.com/questions/29653186/c-sharp-instantiate-multiple-objects
            
            // Positionering av all bricks/tegelstenar
            // (120*i)+20, 20
            // (120*i)+20, 70
            // (120*i)+20, 120
            // (120*i)+20, 170
            // (120*i)+20, 220

            Brick[] bricks = new Brick[8];
            for (int i = 0; i < bricks.Length; i++)
            {
                bricks[i] = new Brick((120*i)+20, 20);
            }

            Brick[] bricks2 = new Brick[8];
            for (int i = 0; i < bricks2.Length; i++)
            {
                bricks2[i] = new Brick((120*i)+20, 70);
            }

            Brick[] bricks3 = new Brick[8];
            for (int i = 0; i < bricks3.Length; i++)
            {
                bricks3[i] = new Brick((120*i)+20, 120);
            }

            Brick[] bricks4 = new Brick[8];
            for (int i = 0; i < bricks4.Length; i++)
            {
                bricks4[i] = new Brick((120*i)+20, 170);
            }

            Brick[] bricks5 = new Brick[8];
            for (int i = 0; i < bricks5.Length; i++)
            {
                bricks5[i] = new Brick((120*i)+20, 220);
            }

            // Så länge fönstret är öppet så kommer programmet att loopas varje ny bild/frame
            while (!Raylib.WindowShouldClose())
            {
                ball.Update();
                // Styrning av paddlen
                // Ändrar x postion i paddelns klass
                if (Raylib.IsKeyDown(paddle.leftKey))
                {
                    paddle.rectangle.x -= paddleSpeed;
                }

                else if (Raylib.IsKeyDown(paddle.rightKey))
                {
                    paddle.rectangle.x += paddleSpeed;
                }
                
                // Begränsar så att paddeln inte kan åka utanför bilden
                // Kanske byter ut så att paddlen istället teleporterar till motsatta sidan
                if (paddle.rectangle.x <= 10)
                {
                    paddle.rectangle.x += paddleSpeed;
                }

                else if (paddle.rectangle.x >= Raylib.GetScreenWidth()-paddle.rectangle.width-10)
                {
                    paddle.rectangle.x -= paddleSpeed;
                }

                Raylib.BeginDrawing();

                // Sätter bakgrunden till svart då paddlen, bollen samt varje tegelsten/brick kommer vara vit
                Raylib.ClearBackground(Color.BLACK);

                // Ritar ut alla objekt
                paddle.Draw();
                ball.Draw();

                // Ritar ut alla bricks/tegelstenar
                // Eftersom att alla arrayer med bricks/tegelstenar har samma antal funkar det att ha alla i samma loop
                // Annars skulle jag vara tvungen att ha fler loopar
                for (int i = 0; i < bricks.Length; i++)
                {
                    bricks[i].Draw();
                    bricks2[i].Draw();
                    bricks3[i].Draw();
                    bricks4[i].Draw();
                    bricks5[i].Draw();
                }

                Raylib.EndDrawing();
            }
        }
    }
}
