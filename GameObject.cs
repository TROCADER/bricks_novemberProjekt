using System;
using System.Collections.Generic;
using Raylib_cs;

namespace bricks_novemberProjekt
{
    public class GameObject
    {
        public Rectangle rectangle = new Rectangle();

        public static List<GameObject> gameObjects = new List<GameObject>();

        public GameObject()
        {
            gameObjects.Add(this);
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(rectangle, Color.WHITE);
        }
    }
}
