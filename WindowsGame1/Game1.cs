using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Bang bang;
        //Bang AnimatedBang;
        Vector2 pos1 = Vector2.Zero, pos2= Vector2.Zero, pos3 = Vector2.Zero;
        float speed1 = 1f, speed2 = 7f, speed3 = 3f;
        Space alive = new Alive();
        Space unalive = new Unalive();
        Menu menu;
        GameState gameState = GameState.Menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //bang = new Bang();
            //AnimatedBang = new Bang(5);
            float Width = graphics.PreferredBackBufferWidth = 800;
            float Height = graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;
            SpaceShip.PositionX = 352;
            SpaceShip.PositionY = 504;    
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menu = new Menu();
            MenuItem newG = new MenuItem("New Game");
            MenuItem restartG = new MenuItem("Restart");
            MenuItem resumeG = new MenuItem("Resume");
            MenuItem exitG = new MenuItem("Exit");
            resumeG.Active = false;
            restartG.Active = false;
            newG.Click += new EventHandler(newG_Click);
            resumeG.Click += new EventHandler(resumeG_Click);
            exitG.Click += new EventHandler(exitG_Click);
            menu.Items.Add(newG);
            menu.Items.Add(resumeG);
            menu.Items.Add(exitG);
           // bang.bangPosition = new Vector2(300, 300);
           // AnimatedBang.bangPosition = new Vector2(300, 300);
            base.Initialize();
        }

        void newG_Click(object sender, EventArgs e)
        {
            alive.Reset();
            menu.Items[1].Active = true;
            gameState = GameState.Game;
        }

        void resumeG_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
        }

        void exitG_Click(object sender, EventArgs e)
        {
            this.Exit();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        Texture2D Background, Frontground, SpaceShipModel;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Background = Content.Load<Texture2D>("kosmos1");
            Frontground = Content.Load<Texture2D>("425-sozdanie-kosmicheskoy-strelyalki-v-hge-chast-1");
            SpaceShipModel = Content.Load<Texture2D>("SpaceShip");
            menu.LoadContent(Content);
            //bang.LoadContent(Content, "bang");
            //AnimatedBang.LoadContent(Content, "bang");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (gameState == GameState.Game)
            {
                //скорость фона
                pos1.Y += speed1;
                if (pos1.Y > 1920)
                    pos1.Y = 0;
                //скорость первого слоя звезд
                pos2.Y += speed2;
                if (pos2.Y > 600)
                    pos2.Y = 0;
                //скорость второго слоя звезд
                pos3.Y += speed3;
                if (pos3.Y > 600)
                    pos3.Y = 0;
                //скорость пули

                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && (SpaceShip.PositionX > 2))
                    SpaceShip.PositionX -= speed2;
                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && (SpaceShip.PositionX < graphics.PreferredBackBufferWidth - 82))
                    SpaceShip.PositionX += speed2;
                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) && (SpaceShip.PositionY > 2))
                    SpaceShip.PositionY -= speed2;
                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) && (SpaceShip.PositionY < graphics.PreferredBackBufferWidth - 300))
                    SpaceShip.PositionY += speed2;
                //выход в меню
                if (SpaceShip.IsAlive <= 0)
                {
                    gameState = GameState.Menu;
                }
                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                    gameState = GameState.Menu;
                //AnimatedBang.Update(gameTime);
                alive.Update(gameTime);
                unalive.Update(gameTime);
            }
            else menu.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(Background, pos1, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(Background, pos1, null, Color.White, 0, new Vector2(0, 1920), 1, SpriteEffects.None, 0);
            spriteBatch.Draw(Frontground, pos2, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(Frontground, pos2, null, Color.White, 0, new Vector2(0, 600), 1, SpriteEffects.None, 0);
            spriteBatch.End();

            if (gameState == GameState.Game)
            {
                spriteBatch.Begin();
                //подложка(черный фон)
                spriteBatch.Draw(Background, pos1, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Background, pos1, null, Color.White, 0, new Vector2(0, 1920), 1, SpriteEffects.None, 0);
                //первый слой звезд
                spriteBatch.Draw(Frontground, pos2, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Frontground, pos2, null, Color.White, 0, new Vector2(0, 600), 1, SpriteEffects.None, 0);
                //второй слой звезд
                spriteBatch.Draw(Frontground, pos3, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Frontground, pos3, null, Color.White, 0, new Vector2(0, 600), 1, SpriteEffects.None, 0);
                //кораблик
                spriteBatch.Draw(SpaceShipModel, new Vector2(SpaceShip.PositionX, SpaceShip.PositionY), Color.White);
                //bang.Draw(spriteBatch);
                KeyboardState state = Keyboard.GetState();
                //if(state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.K)) 
                    //AnimatedBang.Draw(spriteBatch);
                alive.Draw(spriteBatch, Content);
                unalive.Draw(spriteBatch, Content);

                spriteBatch.End();
            }
            else menu.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        enum GameState
        {
            Game,
            Menu
        }
    }
}
