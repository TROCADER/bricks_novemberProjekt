using System.Numerics;
using System.Collections.Generic;
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

        // Randomiser för att göra så att bollen studsar en aning okontrollerat
        private Random random = new Random();

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
            // Använder sig av en 2-dimentionell array för alla bricks
            // y-led och x-led
            Brick[,] allBricks = new Brick[8, 5];

            for (int y = 0; y < allBricks.GetLength(1); y++)
            {
                for (int x = 0; x < allBricks.GetLength(0); x++)
                {
                    allBricks[x, y] = new Brick(20 + x * 120, 20 + y * 50);
                }
            }

            // Så länge fönstret är öppet så kommer programmet att loopas varje ny bild/frame
            while (!Raylib.WindowShouldClose())
            {
                // Uppdaterar bollen
                ball.Update();

                // Styrning av paddlen
                // Ändrar x postion i paddelns C#-klass
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

                else if (paddle.rectangle.x >= Raylib.GetScreenWidth() - paddle.rectangle.width - 10)
                {
                    paddle.rectangle.x -= paddleSpeed;
                }

                for (int y = 0; y < allBricks.GetLength(1); y++)
                {
                    for (int x = 0; x < allBricks.GetLength(0); x++)
                    {
                        if (Raylib.CheckCollisionRecs(ball.rectangle, allBricks[x, y].rectangle))
                        {
                            if (allBricks[x, y].destroyed == false)
                            {
                                ball.yMov = -ball.yMov;
                            }

                            allBricks[x, y].destroyed = true;
                        }
                    }
                }

                if (Raylib.CheckCollisionRecs(ball.rectangle, paddle.rectangle))
                {
                    ball.yMov = -ball.yMov;
                }

                // Börjar rita ut spelet
                Raylib.BeginDrawing();

                // Sätter bakgrunden till svart då paddlen, bollen samt varje tegelsten/brick kommer vara vit
                Raylib.ClearBackground(Color.BLACK);

                // Ritar ut alla objekt
                paddle.Draw();
                ball.Draw();

                // Ritar ut alla bricks/tegelstenar
                // Nest:ad for-loop för att rita ut i både x- och y-led
                for (int y = 0; y < allBricks.GetLength(1); y++)
                {
                    for (int x = 0; x < allBricks.GetLength(0); x++)
                    {
                        if (allBricks[x, y].destroyed != true)
                        {
                            allBricks[x, y].Draw();
                        }
                    }
                }

                // Om bollen har nuddat den nedre kanten av fönstret så är den klassad som död och annan spellogik ska inträffa
                if (ball.isDead == true)
                {
                    // Skriver ut "Game Over" för att signalera att spelet oär över då spelaren har förlorat
                    // Jag kunde spilttat upp  av texten till en separat Vector2, men eftersom att den (förmodligen) inte kommer användas mer så var det onödigt och bestämde mig för en lång rad kod istället :)
                    string gameOverText = "Game Over";
                    Raylib.DrawText(gameOverText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), gameOverText, 100, default).X / 2, Raylib.GetScreenHeight() / 2 -(int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), gameOverText, 100, default).Y / 2, 100, Color.WHITE);
                }

                Raylib.EndDrawing();
            }
        }
    }
}
