using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Game
    {
        // Ställer in storleken av fönstret ()
        private int xGameSize = 980;
        private int yGameSize = 720;

        // Ställer in hastigheten av paddlen och bollen
        private float paddleSpeed = 10f;

        private bool isRestart = false;
        private bool isStart = true;

        public Game()
        {
            // Initierar fönstret samt begränsar FPS'en till 60 på grund av varierande FPS
            // --> programmet körs snabbare eller långsammare beroende på datorn kapabilitet
            // --> kan leda till en försämrad spelupplevelse, samt svårare att koda efter
            Raylib.InitWindow(xGameSize, yGameSize, "Bricks");
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
                // Börjar rita ut spelet
                // Placerad här för att den måste användas i flera olika if-statements
                // --> både när spelet körs samt när man förlorat
                Raylib.BeginDrawing();

                // Startscreen för spelet
                // Visas bara 1 gång (när spelet öppnas)
                if (isStart == true)
                {
                    Start();
                }

                // Återställer alla positioner samt bool:s till hur de var från början, för att kunna starta om spelet
                if (isRestart == true)
                {
                    Restart(paddle, ball, allBricks);
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

                    CheckCollisionBrick(allBricks, ball);

                    // Kollision mellan paddel och boll
                    // Även till för att se till att bollen studsar på paddeln
                    if (Raylib.CheckCollisionRecs(ball.rectangle, paddle.rectangle))
                    {
                        ball.yMov = -ball.yMov;
                    }

                    DrawGame(paddle, ball, allBricks);
                }

                // Om bollen har nuddat den nedre kanten av fönstret så är den klassad som död och annan spellogik ska inträffa
                // Game Over-state
                else if (ball.isDead == true && isStart == false && Brick.bricksCounted != allBricks.GetLength(1) * allBricks.GetLength(0))
                {
                    GameOver();
                }

                // Om man har
                if (Brick.bricksCounted == allBricks.GetLength(1) * allBricks.GetLength(0) && isStart == false)
                {
                    ball.isDead = true;
                    Win();
                }

                Raylib.EndDrawing();
            }
        }

        private void Start()
        {
            Raylib.ClearBackground(Color.BLACK);

            string text = "Bricks";
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 150, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 150, default).Y / 2, 150, Color.WHITE);

            string startGameText = "Press SPACE to start";
            Raylib.DrawText(startGameText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), startGameText, 50, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), startGameText, 50, default).Y / 2, 50, Color.WHITE);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                isStart = false;
            }
        }

        private void Restart(Paddle paddle, Ball ball, Brick[,] allBricks)
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

            Brick.bricksCounted = 0;
        }

        private void GameOver()
        {
            // Skriver ut "Game Over" för att signalera att spelet oär över då spelaren har förlorat
            Raylib.ClearBackground(Color.BLACK);

            string text = "Game Over";
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 150, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 150, default).Y / 2, 150, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            string restartText = "Press SPACE to restart";
            Raylib.DrawText(restartText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), restartText, 50, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), restartText, 50, default).Y / 2, 50, Color.WHITE);

            // Startar om spelet
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                isRestart = true;
            }
        }

        private void Win()
        {
            // Skriver ut "Game Over" för att signalera att spelet oär över då spelaren har förlorat
            Raylib.ClearBackground(Color.BLACK);

            string text = "Victory!";
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 150, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, 150, default).Y / 2, 150, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            string restartText = "Press SPACE to restart";
            Raylib.DrawText(restartText, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), restartText, 50, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), restartText, 50, default).Y / 2, 50, Color.WHITE);

            // Startar om spelet
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                isRestart = true;
            }
        }

        private void DrawGame(Paddle paddle, Ball ball, Brick[,] allBricks)
        {
            // Sätter bakgrunden till svart då paddlen, bollen samt varje tegelsten/brick kommer vara vit
            // Svart-vitt spel för nostagli
            Raylib.ClearBackground(Color.BLACK);

            // Ritar ut alla objekt
            // Kallar på en metod som ligger i objektets klass
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

        private void CheckCollisionBrick(Brick[,] allBricks, Ball ball)
        {
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
                            Brick.bricksCounted++;
                        }

                        allBricks[x, y].destroyed = true;
                    }
                }
            }
        }
    }
}
