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
    public class Enemy: Alive
    {
        private Vector2 Pos = Vector2.Zero;//координаты врага
        private int Health;

        public Enemy() { }

        public Enemy(float posX, float posY)
        {
            PositionX = posX;
            PositionY = posY;
            EnemyHealth = 5;
        }
        public bool OnScreen
        {
            get { return PositionY < 600; }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D EnemyModel)
        {
            spriteBatch.Draw(EnemyModel, new Vector2(PositionX, PositionY), Color.White);
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
    }
}
