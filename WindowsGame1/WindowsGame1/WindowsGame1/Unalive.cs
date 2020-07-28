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
    public class Unalive: Space
    {
        private List<Bonus> bonuses;               // коллекция бонусов

        // конструктор
        public Unalive()
        {
            bonuses = new List<Bonus>();       
        }

        public override void Update(GameTime gameTime)
        {
            int i;
            Bonus.Update();

            foreach (Bonus bonus in bonuses)
                bonus.PositionY += 3f;

            i = 0;
            while (i < bonuses.Count)
            {
                if (bonuses[i].OnScreen == false)
                    bonuses.RemoveAt(i);
                else i++;
            }
            Random rand = new Random();
            while ((bonuses.Count < 1) && Bonus.Ready)
            {
                bonuses.Add(new Bonus(rand.Next(0, 600), rand.Next(-500, -196)));
            }

            //снижение жизней и респавн в начальной точке, если нашего корабля коснулся враг
            for (i = bonuses.Count - 1; i >= 0; i--)
            {
                if ((bonuses.Count > 0) && (i <= (bonuses.Count - 1)))
                {
                    if (((bonuses[i].PositionX + 28) >= SpaceShip.PositionX) && (bonuses[i].PositionX <= (SpaceShip.PositionX + 76)) && ((bonuses[i].PositionY + 28) >= SpaceShip.PositionY) && (bonuses[i].PositionY <= (SpaceShip.PositionY + 92)))
                    {
                        SpaceShip.IsAlive+=1;
                        bonuses.RemoveAt(i);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch,ContentManager content)
        {
            ContentManager Content = content;
            Texture2D BonusModel = Content.Load<Texture2D>("bonus");
            foreach (Bonus bonus in bonuses)
            {
                bonus.Draw(spriteBatch, BonusModel);
            } 
        }
    }     
    
}
