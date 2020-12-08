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

        private bool isRestart = false;
        private bool isStart = true;

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
                // Startscreen för spelet
                // Visas bara 1 gång (när spelet öppnas)
                if (isStart == true)
                {
                    Raylib.ClearBackground(Color.BLACK);
                            
                    string bricksText = "Bricks";
                    Raylib.DrawText(bricksText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), bricksText, 150, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), bricksText, 150, default).Y / 2, 150, Color.WHITE);

                    string startGameText = "Press SPACE to start";
                    Raylib.DrawText(startGameText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), startGameText, 50, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), startGameText, 50, default).Y / 2, 50, Color.WHITE);

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        isStart = false;
                    }
                }

                // Återställer alla positioner samt bool:s till hur de var från början, för att kunna starta om spelet
                if (isRestart == true)
                {
                    paddle.Reset();
                    ball.Reset();

                    for (int y = 0; y < allBricks.GetLength(1); y++)
                    {
                        for (int x = 0; x < allBricks.GetLength(0); x++)
                        {
                            allBricks[x, y].destroyed = false;
                        }
                    }

                    isRestart = false;
                    ball.isDead = false;
                }

                // Spelet körs sålänge som spelaren fortfanrade är vid liv
                // Detta är för att inte kodlogik ska köras även när spelaren har dött
                // --> minskar på processeringskraft (även om det är väldigt små mängder i detta fall)
                if (ball.isDead == false && isStart == false)
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

                    // Går igenom den två-dimentionella arrayen av bricks och kollar kollision
                    // Ifall kollision sker så markeras den som död
                    // Även till för att se till att bollen studsar på en brick
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

                    // Kollision mellan paddel och boll
                    // Även till för att se till att bollen studsar på paddeln
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

                    // Ritar ut alla bricks/tegelstenar som är vid liv/förstörda
                    // Nest:ad for-loop för att rita ut i både x- och y-led
                    for (int y = 0; y < allBricks.GetLength(1); y++)
                    {
                        for (int x = 0; x < allBricks.GetLength(0); x++)
                        {
                            if (allBricks[x, y].destroyed == false)
                            {
                                allBricks[x, y].Draw();
                            }
                        }
                    }
                }

                // Om bollen har nuddat den nedre kanten av fönstret så är den klassad som död och annan spellogik ska inträffa
                // Game Over-state
                else if (ball.isDead == true && isStart == false)
                {
                    // Skriver ut "Game Over" för att signalera att spelet oär över då spelaren har förlorat
                    Raylib.ClearBackground(Color.BLACK);

                    string gameOverText = "Game Over";
                    Raylib.DrawText(gameOverText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), gameOverText, 150, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), gameOverText, 150, default).Y / 2, 150, Color.WHITE);

                    // Meddelar spelaren om att hen kan starta om spelet
                    string restartText = "Press SPACE to restart";
                    Raylib.DrawText(restartText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), restartText, 50, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), restartText, 50, default).Y / 2, 50, Color.WHITE);

                    // Startar om spelet
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        isRestart = true;
                    }
                }

                Raylib.EndDrawing();
            }
        }
    }
}
