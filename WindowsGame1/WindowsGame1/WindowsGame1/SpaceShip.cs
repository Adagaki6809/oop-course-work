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
    public class SpaceShip: Alive
    {
        private static Vector2 Pos = Vector2.Zero;
        private static int health=3;
        private static int time = 0;

        public static float PositionX
        {
            get { return Pos.X; }
            set { Pos.X = value; }
        }

        public static float PositionY
        {
            get { return Pos.Y; }
            set { Pos.Y = value; }
        }
        public static int IsAlive
        {
            get { return health; }
            set { health=value; }
        }
        public static int ResistTime
        {
            get { return time; }
            set { time=value; }
        }
        public static void Update()
        {
            if ((time<=200) && (time>0))
                time--;
        }
    }
}
