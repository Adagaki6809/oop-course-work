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
using System.Threading;
using System.Windows.Forms;

namespace WindowsGame1
{
    public class Alive: Space
    {
        private int Score = 0, Health;    // очки, жизни корабля
        KeyboardState prev = Keyboard.GetState();
        private List<Bullet> bullets;               // коллекция пуль игрока
        private List<Bullet> enemybullets;          // коллекция пуль врагов
        private List<Enemy> enemies;                // коллекция первого вида врагов
        private List<Enemy2> enemies2;              // коллекция второго вида врагов
        private List<Enemy3> enemies3;              // коллекция третьего вида врагов
        Bang bang;
        private int i = 0,j=0;                      // счетчики
        private int ResistTime;
        private int x, y; // координаты нового врага
        private float bangX = -1, bangY = -1;
        private bool flag;
        // конструктор
        public Alive()
        {
            bullets = new List<Bullet>();       
            enemybullets = new List<Bullet>();  
            enemies = new List<Enemy>();        
            enemies2 = new List<Enemy2>();      
            enemies3 = new List<Enemy3>();
            bang = new Bang(5);
        }

        public override void Reset()
        {
            enemybullets.Clear();

            bullets.Clear();

            enemies.Clear();

            enemies2.Clear();

            enemies3.Clear();

            SpaceShip.IsAlive = 3;
            Score = 0;
            SpaceShip.ResistTime = 0;
            SpaceShip.PositionX = 352;
            SpaceShip.PositionY = 504;
        }

        // обновление позиций пуль, кораблей, значений жизней, очков и т.д.
        public override void Update(GameTime gameTime)
        {
            SpaceShip.Update();
            flag=bang.Update(gameTime);
            ResistTime = SpaceShip.ResistTime;
            Health = SpaceShip.IsAlive;

            #region bullet
            KeyboardState state = Keyboard.GetState();
            if (prev.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Space) && state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                bullets.Add(new Bullet(SpaceShip.PositionX + 37, SpaceShip.PositionY+1));
            }
            foreach (Bullet bullet in bullets)
                bullet.PositionY-=3f;

            prev = state;
            i = 0;
            while (i < bullets.Count)
            {
                if (bullets[i].OnScreen == false)
                    bullets.RemoveAt(i);
                else i++;
            }
            #endregion

            #region enemy2bullet

            for (i = enemies2.Count - 1; i >= 0; i--)
            {
                enemies2[i].Update(gameTime);
                if(enemies2[i].Ready)
                    enemybullets.Add(new Bullet(enemies2[i].PositionX + 41, enemies2[i].PositionY + 86));
                //if (enemybullets.Count < enemies2.Count)
            }

            foreach (Bullet bullet in enemybullets)
                bullet.PositionY+=3f;

            i = 0;
            while (i < enemybullets.Count)
            {
                if (enemybullets[i].OnScreen == false)
                    enemybullets.RemoveAt(i);
                else i++;
            }

            #endregion

            #region enemy3bullet

            for (i = enemies3.Count - 1; i >= 0; i--)
            {
                enemies3[i].Update(gameTime);
                if (enemies3[i].Ready)
                    enemybullets.Add(new Bullet(enemies3[i].PositionX + 64, enemies3[i].PositionY + 98));
                //if (enemybullets.Count < enemies2.Count)
            }

            foreach (Bullet bullet in enemybullets)
                bullet.PositionY += 3f;

            i = 0;
            while (i < enemybullets.Count)
            {
                if (enemybullets[i].OnScreen == false)
                    enemybullets.RemoveAt(i);
                else i++;
            }

            #endregion

            #region enemybullets in spaceship
            try
            {
                //обработка столкновений вражеских пуль корабля игрока
                for (i = enemybullets.Count - 1; i >= 0; i--)
                {
                    if ((enemybullets.Count > 0) && (i <= (enemybullets.Count - 1)) && (SpaceShip.ResistTime<=0))
                    {
                        if ((enemybullets[i].PositionX >= SpaceShip.PositionX) && (enemybullets[i].PositionX <= (SpaceShip.PositionX + 76)) && (enemybullets[i].PositionY >= SpaceShip.PositionY) && (enemybullets[i].PositionY <= (SpaceShip.PositionY + 92)))
                        {
                            enemybullets.RemoveAt(i);
                            SpaceShip.IsAlive-=1;
                            SpaceShip.ResistTime = 200;
                            SpaceShip.PositionX = 352;
                            SpaceShip.PositionY = 504;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                //Exception exc=new Exception();
                MessageBox.Show("sad", e.Message, MessageBoxButtons.OK);
            }
            #endregion

            #region enemy1
            foreach (Enemy enemy in enemies)
                enemy.PositionY += 5f;
            i = 0;
            while (i < enemies.Count)
            {
                if (enemies[i].OnScreen==false)
                    enemies.RemoveAt(i);
                else i++;
            }
            Random rand = new Random();
            while (enemies.Count < 3)
            {
                x=rand.Next(0, 600);
                y=rand.Next(-500, -196);
                if (CanCreate(x,y))
                    //enemies.Add(new Enemy(rand.Next(0, 600), rand.Next(-5000, -116)));
                    enemies.Add(new Enemy(x, y));
            }
           
            
             
            //снижение жизней и респавн в начальной точке, если нашего корабля коснулся враг
            for (i = enemies.Count - 1; i >= 0; i--)
            {
                if ((enemies.Count > 0) && (i <= (enemies.Count - 1)) && (SpaceShip.ResistTime<=0))
                {
                    if (((enemies[i].PositionX+54) >= SpaceShip.PositionX) && (enemies[i].PositionX <= (SpaceShip.PositionX+76)) && ((enemies[i].PositionY + 43) >= SpaceShip.PositionY) && (enemies[i].PositionY <= SpaceShip.PositionY + 92))
                    {
                        enemies.RemoveAt(i);
                        SpaceShip.IsAlive -= 1;
                        SpaceShip.ResistTime = 200;
                        SpaceShip.PositionX = 352;
                        SpaceShip.PositionY = 504;
                    }
                }
            }

            //обработка столкновений пуль и вражеских кораблей
            for (i = bullets.Count-1; i >=0 ; i--)
            {
                for (j = enemies.Count-1; j >=0 ; j--)
                {
                    if ((bullets.Count > 0) && (enemies.Count > 0) && (i <= (bullets.Count - 1)) && (j <= (enemies.Count - 1)) && (SpaceShip.ResistTime <= 0))
                    {
                        if ((bullets[i].PositionX >= enemies[j].PositionX) && (bullets[i].PositionX <= (enemies[j].PositionX + 54)) && (bullets[i].PositionY >= enemies[j].PositionY) && (bullets[i].PositionY <= (enemies[j].PositionY + 43)))
                        {
                            enemies[j].EnemyHealth -= 5;
                            bullets.RemoveAt(i);
                            if (enemies[j].EnemyHealth <= 0)
                            {
                                bangX=enemies[j].PositionX-20;
                                bangY= enemies[j].PositionY-20;
                                enemies.RemoveAt(j);
                                Score += 5;
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region enemy2
            foreach (Enemy2 enemy2 in enemies2)
                enemy2.PositionY += 2f;
            i = 0;
            while (i < enemies2.Count)
            {
                if (enemies2[i].OnScreen == false)
                    enemies2.RemoveAt(i);
                else i++;
            }
            Random rand2 = new Random();
            /*
            if (enemies2.Count < 50)
            {
                enemies2.Add(new Enemy2(rand2.Next(0, 600), rand2.Next(-5000, -130)));
            }
            */
            if (enemies2.Count < 2)
            {
                x = rand2.Next(0, 600);
                y = rand2.Next(-500, -196);
                if (CanCreate(x, y))
                    enemies2.Add(new Enemy2(x, y));
            }
            //снижение жизней и респавн в начальной точке, если нашего корабля коснулся враг
            for (i = enemies2.Count - 1; i >= 0; i--)
            {
                if ((enemies2.Count > 0) && (i <= (enemies2.Count - 1)) && (SpaceShip.ResistTime <= 0))
                {
                    if (((enemies2[i].PositionX + 88) >= SpaceShip.PositionX) && (enemies2[i].PositionX <= (SpaceShip.PositionX + 76)) && ((enemies2[i].PositionY + 86) >= SpaceShip.PositionY) && ((enemies2[i].PositionY) <= SpaceShip.PositionY + 92))
                    {
                        enemies2.RemoveAt(i);
                        SpaceShip.IsAlive-=1;
                        SpaceShip.ResistTime = 200;
                        SpaceShip.PositionX = 352;
                        SpaceShip.PositionY = 504;
                    }
                }
            }

            //обработка столкновений пуль и вражеских кораблей
            for (i = bullets.Count - 1; i >= 0; i--)
            {
                for (j = enemies2.Count - 1; j >= 0; j--)
                {
                    if ((bullets.Count > 0) && (enemies2.Count > 0) && (i <= (bullets.Count - 1)) && (j <= (enemies2.Count - 1)) && (SpaceShip.ResistTime <= 0))
                    {
                        if ((bullets[i].PositionX >= enemies2[j].PositionX) && (bullets[i].PositionX <= (enemies2[j].PositionX + 88)) && (bullets[i].PositionY >= enemies2[j].PositionY) && (bullets[i].PositionY <= (enemies2[j].PositionY + 86)))
                        {
                            enemies2[j].EnemyHealth -= 5;
                            bullets.RemoveAt(i);
                            if (enemies2[j].EnemyHealth <= 0)
                            {
                                bangX = enemies2[j].PositionX - 15;
                                bangY = enemies2[j].PositionY + 10;
                                enemies2.RemoveAt(j);
                                Score += 10;
                            }
                        }
                    }
                }
            }
            #endregion
                 
            #region enemy3
            foreach (Enemy3 enemy3 in enemies3)
                enemy3.PositionY += 2f;
            i = 0;
            while (i < enemies3.Count)
            {
                if (enemies3[i].OnScreen == false)
                    enemies3.RemoveAt(i);
                else i++;
            }
            Random rand3 = new Random();
            /*
            if (enemies3.Count < 10)
            {
                enemies3.Add(new Enemy3(rand3.Next(0, 600), rand3.Next(-5000, -130)));
            }*/
            while (enemies3.Count < 1)
            {
                x = rand3.Next(0, 600);
                y = rand3.Next(-500, -196);
                if (CanCreate(x, y))
                    //enemies.Add(new Enemy(rand.Next(0, 600), rand.Next(-5000, -116)));
                    enemies3.Add(new Enemy3(x, y));
            }
            //снижение жизней и респавн в начальной точке, если нашего корабля коснулся враг
            for (i = enemies3.Count - 1; i >= 0; i--)
            {
                if ((enemies3.Count > 0) && (i <= (enemies3.Count - 1)) && (SpaceShip.ResistTime <= 0))
                {   
                    if (((enemies3[i].PositionX + 132) >= SpaceShip.PositionX) && (enemies3[i].PositionX <= (SpaceShip.PositionX + 76)) && ((enemies3[i].PositionY + 196) >= SpaceShip.PositionY) && (enemies3[i].PositionY <= (SpaceShip.PositionY + 92)))
                    {
                        enemies3.RemoveAt(i);
                        SpaceShip.IsAlive-=1;
                        SpaceShip.ResistTime = 200;
                        SpaceShip.PositionX = 352;
                        SpaceShip.PositionY = 504;
                    }
                }
            }

            //обработка столкновений пуль и вражеских кораблей
            for (i = bullets.Count - 1; i >= 0; i--)
            {
                for (j = enemies3.Count - 1; j >= 0; j--)
                {
                    if ((bullets.Count > 0) && (enemies3.Count > 0) && (i <= (bullets.Count - 1)) && (j <= (enemies3.Count - 1)) && (SpaceShip.ResistTime <= 0))
                    {
                        if ((bullets[i].PositionX >= enemies3[j].PositionX) && (bullets[i].PositionX <= (enemies3[j].PositionX + 132)) && (bullets[i].PositionY >= enemies3[j].PositionY) && (bullets[i].PositionY <= (enemies3[j].PositionY + 196)))
                        {
                            enemies3[j].EnemyHealth -= 5;
                            bullets.RemoveAt(i);
                            if (enemies3[j].EnemyHealth <= 0)
                            {
                                bangX = enemies3[j].PositionX + 15;
                                bangY = enemies3[j].PositionY + 40;
                                enemies3.RemoveAt(j);
                                Score += 15;
                            }
                        }
                    }
                }
            }
            #endregion

            #region кривой респ
            /*
            //проверка на столкновение первого вида врагов между собой
            for (i = enemies.Count - 1; i >= 0; i--)
            {
                for (j = enemies.Count - 1; j >= 0; j--)
                {
                    if ((enemies.Count > 1) && (i <= (enemies.Count - 1)) && (j <= (enemies.Count - 1)) && (i != j))
                    {
                        if (((enemies[i].PositionX + 143) >= enemies[j].PositionX) && (enemies[i].PositionX <= (enemies[j].PositionX + 143)) && ((enemies[i].PositionY + 116) >= enemies[j].PositionY) && (enemies[i].PositionY <= (enemies[j].PositionY + 116)))
                        {
                            enemies.RemoveAt(i);
                            if (j < enemies.Count)
                                enemies.RemoveAt(j);
                        }
                    }
                }
            }
            //проверка на столкновение второго вида врагов между собой
            for (i = enemies2.Count - 1; i >= 0; i--)
            {
                for (j = enemies2.Count - 1; j >= 0; j--)
                {
                    if ((enemies2.Count > 1) && (i <= (enemies2.Count - 1)) && (j <= (enemies2.Count - 1)) && (i != j))
                    {
                        if (((enemies2[i].PositionX + 143) >= enemies2[j].PositionX) && (enemies2[i].PositionX <= (enemies2[j].PositionX + 143)) && ((enemies2[i].PositionY + 116) >= enemies2[j].PositionY) && (enemies2[i].PositionY <= (enemies2[j].PositionY + 116)))
                        {
                            enemies2.RemoveAt(i);
                            if (j < enemies2.Count)
                                enemies2.RemoveAt(j);
                        }
                    }
                }
            }
            //проверка на столкновение третьего вида врагов между собой
            for (i = enemies3.Count - 1; i >= 0; i--)
            {
                for (j = enemies3.Count - 1; j >= 0; j--)
                {
                    if ((enemies3.Count > 1) && (i <= (enemies3.Count - 1)) && (j <= (enemies3.Count - 1)) && (i != j))
                    {
                        if (((enemies3[i].PositionX + 143) >= enemies3[j].PositionX) && (enemies3[i].PositionX <= (enemies3[j].PositionX + 143)) && ((enemies3[i].PositionY + 116) >= enemies3[j].PositionY) && (enemies3[i].PositionY <= (enemies3[j].PositionY + 116)))
                        {
                            enemies3.RemoveAt(i);
                            if (j < enemies3.Count)
                                enemies3.RemoveAt(j);
                        }
                    }
                }
            }
            
            //проверка на столкновение первого и второго вида врагов между собой
            for (i = enemies.Count - 1; i >= 0; i--)
            {
                for (j = enemies2.Count - 1; j >= 0; j--)
                {
                    if ((enemies.Count > 1) && (i <= (enemies.Count - 1)) && (j <= (enemies2.Count - 1)) && (i != j))
                    {
                        if (((enemies[i].PositionX + 143) >= enemies2[j].PositionX) && (enemies[i].PositionX <= (enemies2[j].PositionX + 143)) && ((enemies[i].PositionY + 116) >= enemies2[j].PositionY) && (enemies[i].PositionY <= (enemies2[j].PositionY + 116)))
                        {
                            enemies.RemoveAt(i);
                            if (j < enemies2.Count)
                                enemies2.RemoveAt(j);
                        }
                    }
                }
            }
            
            //проверка на столкновение первого и третьего вида врагов между собой
            for (i = enemies.Count - 1; i >= 0; i--)
            {
                for (j = enemies.Count - 1; j >= 0; j--)
                {
                    if ((enemies.Count > 1) && (i <= (enemies.Count - 1)) && (j <= (enemies3.Count - 1)) && (i != j))
                    {
                        if (((enemies[i].PositionX + 143) >= enemies3[j].PositionX) && (enemies[i].PositionX <= (enemies3[j].PositionX + 143)) && ((enemies[i].PositionY + 116) >= enemies3[j].PositionY) && (enemies[i].PositionY <= (enemies3[j].PositionY + 116)))
                        {
                            enemies.RemoveAt(i);
                            if (j < enemies3.Count)
                                enemies3.RemoveAt(j);
                        }
                    }
                }
            }
            
            //проверка на столкновение второго и третьего вида врагов между собой
            for (i = enemies2.Count - 1; i >= 0; i--)
            {
                for (j = enemies3.Count - 1; j >= 0; j--)
                {
                    if ((enemies2.Count > 1) && ((enemies3.Count > 1)) && (i <= (enemies2.Count - 1)) && (j <= (enemies3.Count - 1)) && (i!=j))
                    {
                        if (((enemies2[i].PositionX + 40) >= enemies3[j].PositionX) && (enemies2[i].PositionX <= (enemies3[j].PositionX + 143)) && ((enemies2[i].PositionY + 30) >= enemies3[j].PositionY) && (enemies2[i].PositionY <= (enemies3[j].PositionY + 116)))
                        {
                            enemies2.RemoveAt(i);
                            if (j < enemies3.Count)
                                enemies3.RemoveAt(j);
                        }
                    }
                }
            }
            
            */
            #endregion
        }

        // отрисовка корабля игрока, пуль, вражеских кораблей, жизней, очков
        public override void Draw(SpriteBatch spriteBatch,ContentManager content)
        {
            ContentManager Content = content;
            Texture2D BulletModel = Content.Load<Texture2D>("bullet");
            Texture2D EnemyBulletModel = Content.Load<Texture2D>("laser");
            Texture2D EnemyModel = Content.Load<Texture2D>("enemy1");
            Texture2D Enemy2Model = Content.Load<Texture2D>("enemy2");
            Texture2D Enemy3Model = Content.Load<Texture2D>("enemy3");
            SpriteFont font = Content.Load<SpriteFont>("GameFont");
            //Texture2D bangTexture = Content.Load<Texture2D>("bang");
            bang.LoadContent(content, "bang");

            //отрисовка пуль игрока
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch, BulletModel);
            }
            //отрисовка пуль врагов
            foreach (Bullet bullet in enemybullets)
            {
                bullet.Draw(spriteBatch, EnemyBulletModel);
            }
            //отрисовка первого вида врагов
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch, EnemyModel);
            }
            //отрисовка второго вида врагов
            foreach (Enemy2 enemy2 in enemies2)
            {
                enemy2.Draw(spriteBatch, Enemy2Model);
            }
            //отрисовка третьего вида врагов
            foreach (Enemy3 enemy3 in enemies3)
            {
                enemy3.Draw(spriteBatch, Enemy3Model);
            }
            KeyboardState state = Keyboard.GetState();
            if ((bangX!=-1) && (bangY!=-1) && flag)
                bang.Draw(spriteBatch,new Vector2 (bangX,bangY));
            if (!flag)
            {
                bangX = -1;
                bangY = -1;
            }
            //отрисовка жизней и очков
            spriteBatch.DrawString(font, "Score: " + Score.ToString(), new Vector2(20, 20), Color.Yellow);
            spriteBatch.DrawString(font, "Lifes: " + Health.ToString(), new Vector2(20, 50), Color.Yellow);
            spriteBatch.DrawString(font, "Resist time: " + ResistTime.ToString(), new Vector2(20, 80), Color.Yellow);
        }

        // проверка на возможность создания врага в данных координатах
        public bool CanCreate(int x,int y)
        {
            int i=0;
            bool CanCreate=true;
            //проверка на создание нового врага поверх первого вида врагов
            for (i = enemies.Count - 1; i >= 0; i--)
            {
                if ((enemies.Count >= 1) && (i <= (enemies.Count - 1)))
                    {
                        if ((x >= (enemies[i].PositionX-132)) && (x <= (enemies[i].PositionX + 132)) && (y >= (enemies[i].PositionY-196)) && (y <= (enemies[i].PositionY + 196)))
                            return false;
                    }
            }
            //проверка на создание нового врага поверх второго вида врагов
            for (i = enemies2.Count - 1; i >= 0; i--)
            {
                if ((enemies2.Count >= 1) && (i <= (enemies2.Count - 1)))
                {
                    if ((x >= (enemies2[i].PositionX - 132)) && (x <= (enemies2[i].PositionX + 132)) && (y >= (enemies2[i].PositionY - 196)) && (y <= (enemies2[i].PositionY + 196)))//135
                        return false;
                }
            }
            //проверка на создание нового врага поверх третьего вида врагов
            for (i = enemies3.Count - 1; i >= 0; i--)
            {
                if ((enemies3.Count >= 1) && (i <= (enemies3.Count - 1)))
                {
                    if ((x >= (enemies3[i].PositionX - 132)) && (x <= (enemies3[i].PositionX + 132)) && (y >= (enemies3[i].PositionY - 196)) && (y <= (enemies3[i].PositionY + 196)))
                        return false;
                }
            }
            return CanCreate;
        }
    }
}
