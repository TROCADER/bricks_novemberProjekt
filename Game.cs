using System;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class Game
    {
        // Ställer in storleken av fönstret
        private int xGameSize = 980;
        private int yGameSize = 720;

        // Ställer in hastigheten av paddlen och bollen
        private float paddleSpeed = 10f;

        private bool isRestart = false;
        private bool isStart = true;

        private int bricksBroken = 0;

        // Tre storleksfonter jag använder mig av
        // Har de definerade utanför metoderna så att jag kan använda de i alla metoder
        private int fontSize = 0;
        private int fontSize1 = 150;
        private int fontSize2 = 50;
        private int fontSize3 = 30;
        private int fontSize4 = 20;

        // Temporär bool för att se till att ljudet bara körs 1 gång
        // Används i ett if-statement
        private bool audioBool = false;

        private string text = "";

        private Random random = new Random();

        public Game()
        {
            // Initierar och laddar in all audio för spelet
            Raylib.InitAudioDevice();

            Sound brickSound = Raylib.LoadSound("resources/brick.mp3");
            Sound bounceSound = Raylib.LoadSound("resources/bounce.mp3");
            Sound coinSound = Raylib.LoadSound("resources/coin.mp3");
            Sound gameOverSound = Raylib.LoadSound("resources/game_over.mp3");
            Sound winSound = Raylib.LoadSound("resources/victory.mp3");

            // Sätter volymen för ljuden eftersom att de var för höga
            // --> inte få ont i öronen när man spelar
            // --> kom fram till att 0.5 (antar är 50%) var inte för högt eller lågt
            // --> förutom för vinst-ljudet som fortfarande för för högt
            // --> har längre ljudnivå än de övriga ljuden
            Raylib.SetSoundVolume(brickSound, (float)0.5f);
            Raylib.SetSoundVolume(bounceSound, (float)0.5f);
            Raylib.SetSoundVolume(coinSound, (float)0.5f);
            Raylib.SetSoundVolume(gameOverSound, (float)0.5f);
            Raylib.SetSoundVolume(winSound, (float)0.25f);

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
                    Start(coinSound);
                }

                // Återställer alla positioner samt bool:s till hur de var från början, för att kunna starta om spelet
                if (isRestart == true)
                {
                    Restart(paddle, ball, allBricks, coinSound);
                }

                // Spelet körs sålänge som spelaren fortfanrade är vid liv
                // Detta är för att inte kodlogik ska köras även när spelaren har dött
                // --> minskar på processeringskraft (även om det är väldigt små mängder i detta fall)
                if (ball.isDead == false && isStart == false)
                {
                    // Uppdaterar bollen
                    ball.Update();

                    PaddleControl(paddle);

                    CheckCollisionBrick(allBricks, ball, brickSound);

                    // Kollision mellan paddel och boll
                    // Även till för att se till att bollen studsar på paddeln
                    if (Raylib.CheckCollisionRecs(ball.rectangle, paddle.rectangle))
                    {
                        RandomBall(ball);

                        Raylib.PlaySound(bounceSound);
                    }

                    DrawGame(paddle, ball, allBricks);
                }

                // Om bollen har nuddat den nedre kanten av fönstret så är den klassad som död och kod relaterad till förlust-scenario kommer köras
                // Game Over-state
                else if (ball.isDead == true && isStart == false && Brick.bricksCounted != allBricks.GetLength(1) * allBricks.GetLength(0))
                {
                    GameOver(gameOverSound);
                }

                // Om man har tagit sönder alla bricks så kommer spelet att registrera det som en vinst och kod relaterad till vinst-scenario kommer köras
                // Win-state
                if (Brick.bricksCounted == allBricks.GetLength(1) * allBricks.GetLength(0) && isStart == false)
                {
                    ball.isDead = true;
                    Win(winSound);
                }

                Raylib.EndDrawing();
            }
        }

        private void Start(Sound coinSound)
        {
            if (audioBool == false)
            {
                Raylib.PlaySound(coinSound);
                audioBool = true;
            }

            Raylib.ClearBackground(Color.BLACK);

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

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                audioBool = false;
                isStart = false;
            }
        }

        private void Restart(Paddle paddle, Ball ball, Brick[,] allBricks, Sound coinSound)
        {
            // Kod som kallar på paddeln och bollens reset metod som återställer deras positioner
            paddle.Reset();
            ball.Reset();

            // Återställer destroyed-boolen i varje brick (x samt y led i arrayen)
            // --> gör att de kommer ritas ut samt funka i spelet igen
            for (int y = 0; y < allBricks.GetLength(1); y++)
            {
                for (int x = 0; x < allBricks.GetLength(0); x++)
                {
                    allBricks[x, y].destroyed = false;
                }
            }

            // Återställer bools som ser till att spelet inte restartar igen samt att bollen återfår sin funktionalitet
            isRestart = false;
            ball.isDead = false;

            // Återställer den statiska räknaren och hur många bricks som spelaren har tagit sönder
            Brick.bricksCounted = 0;
            bricksBroken = 0;

            Raylib.PlaySound(coinSound);
        }

        private void GameOver(Sound gameOverSound)
        {
            if (audioBool == false)
            {
                Raylib.PlaySound(gameOverSound);
                audioBool = true;
            }

            // Skriver ut "Game Over" för att signalera att spelet oär över då spelaren har förlorat
            Raylib.ClearBackground(Color.BLACK);

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
            text = "You broke a total of: " + bricksBroken + " bricks!";
            fontSize = fontSize3;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) - 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Startar om spelet
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                isRestart = true;
                audioBool = false;
            }
        }

        private void Win(Sound winSound)
        {
            if (audioBool == false)
            {
                Raylib.PlaySound(winSound);
                audioBool = true;
            }

            // Skriver ut "Game Over" för att signalera att spelet oär över då spelaren har förlorat
            Raylib.ClearBackground(Color.BLACK);

            text = "Victory!";
            fontSize = fontSize1;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, Raylib.GetScreenHeight() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            text = "Press SPACE to restart";
            fontSize = fontSize2;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) + 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Meddelar spelaren om att hen kan starta om spelet
            text = "You have broken a total of: " + bricksBroken + " bricks!";
            fontSize = fontSize3;
            Raylib.DrawText(text, Raylib.GetScreenWidth() / 2 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).X / 2, (Raylib.GetScreenHeight() / 2) - 100 - (int)Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, default).Y / 2, fontSize, Color.WHITE);

            // Startar om spelet
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                isRestart = true;
                audioBool = false;
            }
        }

        private void DrawGame(Paddle paddle, Ball ball, Brick[,] allBricks)
        {
            // Sätter bakgrunden till svart då paddlen, bollen samt varje tegelsten/brick kommer vara vit
            // Svart-vitt spel för arcade-game nostagli
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

        private void CheckCollisionBrick(Brick[,] allBricks, Ball ball, Sound brickSound)
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
                            RandomBall(ball);
                            Brick.bricksCounted++;
                            bricksBroken++;

                            Raylib.PlaySound(brickSound);
                        }

                        allBricks[x, y].destroyed = true;
                    }
                }
            }
        }

        private void PaddleControl(Paddle paddle)
        {
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
        }

        private void RandomBall(Ball ball)
        {
            // Gör att bollen åker ej i en konstant bana
            // --> åker slumpartat
            // --> för ett roligare och svårare spel
            ball.yMov = -ball.yMov;

            if (ball.xMov < 0)
            {
                ball.xMov = -random.Next(4, 11);
            }

            else if (ball.xMov > 0)
            {
                ball.xMov = random.Next(4, 11);
            }
        }
    }
}
