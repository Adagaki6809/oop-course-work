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
    public abstract class Space
    {
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Reset()
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
        }
    }
}
