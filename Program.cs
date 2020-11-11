using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            int xWindowSize = 800;
            int yWindowSize = 600;

            float paddleSpeed;
            float ballSpeed;

            Raylib.InitWindow(xWindowSize, yWindowSize, "Bricks");

            while (!Raylib.WindowShouldClose())
            {
                

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);

                Raylib.EndDrawing();
            }
        }
    }
}
