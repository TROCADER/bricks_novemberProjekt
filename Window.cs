using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Window
    {
        private int xWindowSize = 1280;
        private int yWindowSize = 960;

        private float paddleSpeed;
        private float ballSpeed;

        public Window()
        {

            Paddle paddle = new Paddle(10, yWindowSize-30, KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT);
            Ball ball = new Ball();

            Raylib.InitWindow(xWindowSize, yWindowSize, "Bricks");
            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                

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
