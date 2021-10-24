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
    class Bullet
    {
        private Vector2 Pos = Vector2.Zero;//координаты пули
        
        public Bullet() {  } //конструктор по  умолчанию

        public Bullet(float posX, float posY)
        {
            PositionX = posX;
            PositionY = posY;
        }
        public void Update()
        {
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D BulletModel)
        {
            spriteBatch.Draw(BulletModel, new Vector2(PositionX, PositionY), Color.White);
        }
        public bool OnScreen
        {
            get { return ((PositionY >= 0) && (PositionY < 600)); }
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
    }
}
