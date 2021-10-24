using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    class Bang
    {
        public Texture2D bangTexture;
        public Vector2 bangPosition;
        int FrameCount; //Количество всех фреймов в изображении (5) 
        int frame;//какой фрейм нарисован в данный момент 
        float TimeForFrame;//Сколько времени нужно показывать один фрейм (скорость) 
        float TotalTime;//сколько времени прошло с показа предыдущего фрейма 
        float time=0;

        public Bang()
        {
        }
 
        public Bang(int speedAnimation) 
        {
            frame = 0; 
            TimeForFrame = (float)1 / speedAnimation; 
            TotalTime = 0; 
        }

        public void LoadContent(ContentManager Content, String texture)
        {
            bangTexture = Content.Load<Texture2D>(texture);
        } 

        public bool Update(GameTime gameTime)
        {
            FrameCount = bangTexture.Width / bangTexture.Height;
            TotalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (TotalTime > TimeForFrame)
            {
                frame++;
                frame = frame % (FrameCount - 1);
                TotalTime -= TimeForFrame;
                //return true;
            }
            if (time < 5.0 * TimeForFrame)
                return true;
            if (time >= 5 * TimeForFrame)
            {
                time = 0;
                //frame = 1;
                return false;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 bangPosition)
        {
            int frameWidth = bangTexture.Width / FrameCount;
            //Rectangle rectanglе = new Rectangle(frameWidth * frame, 0, frameWidth, bangTexture.Height);
            spriteBatch.Draw(bangTexture, bangPosition,new Rectangle(frameWidth * frame, 0, frameWidth, bangTexture.Height), Color.White);
        } 

        public void Draw1(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(bangTexture, bangPosition, Color.White); 
        }
    }
}
