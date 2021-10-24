using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class Menu
    {
        public List<MenuItem> Items { get; set; }
        SpriteFont font;
        int currentItem, i;
        KeyboardState oldState;

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            if (SpaceShip.IsAlive <= 0)
            {
                if (i == 0)
                    currentItem = 0;
                Items[1].Active = false;
                i++;
            }
            if (SpaceShip.IsAlive == 3)
                i = 0;
            if (state.IsKeyDown(Keys.Enter))
                Items[currentItem].OnClick();
            int d = 0;
            if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                d = -1;
            if (state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                d = 1;
            currentItem += d;

            bool ok = false;
            while (!ok)
            {
                if (currentItem < 0)
                    currentItem = Items.Count - 1;
                else if (currentItem > Items.Count - 1)
                    currentItem = 0;
                else if (Items[currentItem].Active == false)
                    currentItem += d;
                else ok = true;
            }
            oldState = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            int y = 100;
            foreach (MenuItem item in Items)
            {
                Color color = Color.White;
                if (item.Active == false)
                    color = Color.Gray;
                if (item == Items[currentItem])
                    color = Color.Brown;

                spriteBatch.DrawString(font, item.Name, new Vector2(100, y), color);
                y += 40;
            }
            spriteBatch.End();
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("GameFont");
        }
    }
}
