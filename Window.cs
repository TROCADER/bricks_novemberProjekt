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
            // Temp-kod, planerar att byta ut till bättre kod
            Brick brick1_1 = new Brick(20, 20);
            Brick brick1_2 = new Brick(120+20, 20);
            Brick brick1_3 = new Brick(240+20, 20);
            Brick brick1_4 = new Brick(360+20, 20);
            Brick brick1_5 = new Brick(480+20, 20);
            Brick brick1_6 = new Brick(600+20, 20);
            Brick brick1_7 = new Brick(720+20, 20);
            Brick brick1_8 = new Brick(840+20, 20);

            Brick brick2_1 = new Brick(20, 70);
            Brick brick2_2 = new Brick(120+20, 70);
            Brick brick2_3 = new Brick(240+20, 70);
            Brick brick2_4 = new Brick(360+20, 70);
            Brick brick2_5 = new Brick(480+20, 70);
            Brick brick2_6 = new Brick(600+20, 70);
            Brick brick2_7 = new Brick(720+20, 70);
            Brick brick2_8 = new Brick(840+20, 70);

            Brick brick3_1 = new Brick(20, 120);
            Brick brick3_2 = new Brick(120+20, 120);
            Brick brick3_3 = new Brick(240+20, 120);
            Brick brick3_4 = new Brick(360+20, 120);
            Brick brick3_5 = new Brick(480+20, 120);
            Brick brick3_6 = new Brick(600+20, 120);
            Brick brick3_7 = new Brick(720+20, 120);
            Brick brick3_8 = new Brick(840+20, 120);

            Brick brick4_1 = new Brick(20, 170);
            Brick brick4_2 = new Brick(120+20, 170);
            Brick brick4_3 = new Brick(240+20, 170);
            Brick brick4_4 = new Brick(360+20, 170);
            Brick brick4_5 = new Brick(480+20, 170);
            Brick brick4_6 = new Brick(600+20, 170);
            Brick brick4_7 = new Brick(720+20, 170);
            Brick brick4_8 = new Brick(840+20, 170);

            Brick brick5_1 = new Brick(20, 220);
            Brick brick5_2 = new Brick(120+20, 220);
            Brick brick5_3 = new Brick(240+20, 220);
            Brick brick5_4 = new Brick(360+20, 220);
            Brick brick5_5 = new Brick(480+20, 220);
            Brick brick5_6 = new Brick(600+20, 220);
            Brick brick5_7 = new Brick(720+20, 220);
            Brick brick5_8 = new Brick(840+20, 220);


            // Så länge fönstret är öppet så kommer programmet att loopas varje ny bild/frame
            while (!Raylib.WindowShouldClose())
            {
                ball.UpdatePos();
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
                if (paddle.rectangle.x == 0+20)
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

                // Temp-kod, planerar att byta ut till bättre kod
                brick1_1.Draw();
                brick1_2.Draw();
                brick1_3.Draw();
                brick1_4.Draw();
                brick1_5.Draw();
                brick1_6.Draw();
                brick1_7.Draw();
                brick1_8.Draw();

                brick2_1.Draw();
                brick2_2.Draw();
                brick2_3.Draw();
                brick2_4.Draw();
                brick2_5.Draw();
                brick2_6.Draw();
                brick2_7.Draw();
                brick2_8.Draw();

                brick3_1.Draw();
                brick3_2.Draw();
                brick3_3.Draw();
                brick3_4.Draw();
                brick3_5.Draw();
                brick3_6.Draw();
                brick3_7.Draw();
                brick3_8.Draw();

                brick4_1.Draw();
                brick4_2.Draw();
                brick4_3.Draw();
                brick4_4.Draw();
                brick4_5.Draw();
                brick4_6.Draw();
                brick4_7.Draw();
                brick4_8.Draw();

                brick5_1.Draw();
                brick5_2.Draw();
                brick5_3.Draw();
                brick5_4.Draw();
                brick5_5.Draw();
                brick5_6.Draw();
                brick5_7.Draw();
                brick5_8.Draw();

                Raylib.EndDrawing();
            }
        }
    }
}
