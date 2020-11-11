using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Window
    {
        private int xWindowSize = 1280;
        private int yWindowSize = 960;

        private float paddleSpeed = 10f;
        private float ballSpeed;

        public Window()
        {
            Paddle paddle = new Paddle(xWindowSize/2, yWindowSize-30, KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT);
            Ball ball = new Ball();

            Raylib.InitWindow(xWindowSize, yWindowSize, "Bricks");
            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyDown(paddle.leftKey))
                {
                    paddle.xPos -= paddleSpeed;
                }

                else if (Raylib.IsKeyDown(paddle.rightKey))
                {
                    paddle.xPos += paddleSpeed;
                }

                if (paddle.xPos == 0+20)
                {
                    paddle.xPos += paddleSpeed;
                }

                else if (paddle.xPos >= xWindowSize-paddle.xRec-20)
                {
                    paddle.xPos -= paddleSpeed;
                }

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);

                paddle.Draw();

                Raylib.EndDrawing();
            }
        }

        private void CheckPaddlePos()
        {
            
        }
    }
}
