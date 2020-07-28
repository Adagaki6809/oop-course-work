using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public class Bonus: Unalive
    {
        private Vector2 Pos = Vector2.Zero;
        private static int TimeOut = 2000;

        public Bonus(float posX, float posY)
        {
            PositionX = posX;
            PositionY = posY;
        }

        public static void Update()
        {
            if (TimeOut < 0)
                TimeOut = 2000;
            TimeOut--;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D BonusModel)
        {
            spriteBatch.Draw(BonusModel, new Vector2(PositionX, PositionY), Color.White);
        }

        public bool OnScreen
        {
            get { return PositionY < 600; }
        }

        public float PositionX
        {
            get { return Pos.X; }
            set { Pos.X = value; }
        }

        public float PositionY
        {
            get { return Pos.Y; }
            set { Pos.Y = value; }
        }

        public static bool Ready
        {
            get { return TimeOut == 0; }
        }
    }
}
