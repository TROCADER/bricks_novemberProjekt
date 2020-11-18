using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Window
    {
        // Ställer in storleken av fönstret ()
        private int xWindowSize = 960;
        private int yWindowSize = 720;

        // Ställer in hastigheten av paddlen och bollen
        private float paddleSpeed = 10f;
        private float ballSpeed;

        public Window()
        {
            // Initierar fönstret samt begränsar FPS'en till 60 på grund av varierande FPS
            // --> programmet körs snabbare eller långsammare beroende på datorn kapabilitet
            // --> kan leda till en försämrad spelupplevelse, samt svårare att koda efter
            Raylib.InitWindow(xWindowSize, yWindowSize, "Bricks");
            Raylib.SetTargetFPS(60);

            Paddle paddle = new Paddle(KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT);
            Ball ball = new Ball();

            Brick brick1 = new Brick(20, 20);

            // Så länge fönstret är öppet så kommer programmet att loopas varje ny bild/frame
            while (!Raylib.WindowShouldClose())
            {
                // Styrning av paddlen
                // Ändrar x postion i paddelns klass
                if (Raylib.IsKeyDown(paddle.leftKey))
                {
                    paddle.xPos -= paddleSpeed;
                }

                else if (Raylib.IsKeyDown(paddle.rightKey))
                {
                    paddle.xPos += paddleSpeed;
                }
                
                // Begränsar så att paddeln inte kan åka utanför bilden
                // Kanske byter ut så att paddlen istället teleporterar till motsatta sidan
                if (paddle.xPos == 0+20)
                {
                    paddle.xPos += paddleSpeed;
                }

                else if (paddle.xPos >= xWindowSize-paddle.xRec-20)
                {
                    paddle.xPos -= paddleSpeed;
                }

                Raylib.BeginDrawing();

                // Sätter bakgrunden till svart då paddlen, bollen samt varje tegelsten/brick kommer vara vit
                Raylib.ClearBackground(Color.BLACK);

                paddle.Draw();
                ball.Draw();
                brick1.Draw();

                Raylib.EndDrawing();
            }
        }
    }
}
