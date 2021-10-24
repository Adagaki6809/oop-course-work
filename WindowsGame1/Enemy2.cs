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
    public class Enemy2 : Alive
    {
        private Vector2 Pos = Vector2.Zero;//координаты врага
        private int Health;
        private static Random rand=new Random();
        private int TimeOut = rand.Next(10, 100) + 50;
        public Enemy2() { }

        public Enemy2(float posX, float posY)
        {
            PositionX = posX;
            PositionY = posY;
            EnemyHealth = 15;
        }
        public bool OnScreen
        {
            get { return PositionY < 600; }
        }
        public override void Update(GameTime gameTime)
        {
            TimeOut--;
            if (TimeOut < 0)
                TimeOut = rand.Next(10, 20) + 50;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D Enemy2Model)
        {
            spriteBatch.Draw(Enemy2Model, new Vector2(PositionX, PositionY), Color.White);
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
        public int EnemyHealth
        {
            get { return Health; }
            set { Health = value; }
        }

        public bool Ready
        {
            get { return TimeOut==0; }
        }
        
    }
}
