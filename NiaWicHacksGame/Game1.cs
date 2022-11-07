using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NiaWicHacksGame
{
    public class Game1 : Game
    {

        enum PlayerState
        {
            MoveLeft,
            IdleLeft,
            KickLeft,
            HurtLeft,
            CrouchLeft,
            MoveRight,
            IdleRight,
            KickRight,
            HurtRight,
            CrouchRight
        }



        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Dino texture fields
        private Texture2D dinoTextureGreen;
        private Texture2D dinoTextureRed;
        private Texture2D dinoTextureYellow;
        private Texture2D dinoTextureBlue;
        private Vector2 dinoPosition;

        // Sprite sheet data
        private int numSpritesInSheet;
        private int widthOfSingleSprite;

        // Animation data
        private int dinosCurrentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;

        private Background background1;

        private PlayerState dinoState;

        private bool isMoving;

        public Vector2 DinoPosition()
        {
            return dinoPosition;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            dinoState = new PlayerState();

            background1 = new Background(dinoPosition, Content.Load<Texture2D>("Background1Pos1"), Content.Load<Texture2D>("Background1Pos2"), Content.Load<Texture2D>("Background1Pos3"), Content.Load<Texture2D>("Background1Pos4"), Content.Load<Texture2D>("Background1Pos5"), isMoving);

            isMoving = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // ================================================================
            // Loading in textures for the different dinos
            // ================================================================

            // Load the sprite sheet and fill in sprite data for the green dino
            dinoTextureGreen = Content.Load<Texture2D>("DinoSprites - vita");
            numSpritesInSheet = 24;
            widthOfSingleSprite = dinoTextureGreen.Width / numSpritesInSheet;

            // Load the sprite sheet and fill in sprite data for the red dino
            dinoTextureRed = Content.Load<Texture2D>("DinoSprites - mort");
            numSpritesInSheet = 24;
            widthOfSingleSprite = dinoTextureRed.Width / numSpritesInSheet;

            // Load the sprite sheet and fill in sprite data for the yellow dino
            dinoTextureYellow = Content.Load<Texture2D>("DinoSprites - tard");
            numSpritesInSheet = 24;
            widthOfSingleSprite = dinoTextureYellow.Width / numSpritesInSheet;

            // Load the sprite sheet and fill in sprite data for the blue dino
            dinoTextureBlue = Content.Load<Texture2D>("DinoSprites - doux");
            numSpritesInSheet = 24;
            widthOfSingleSprite = dinoTextureBlue.Width / numSpritesInSheet;

            // ================================================================
            // End of loading in textures for the different dinos
            // ================================================================

            dinoPosition = new Vector2(200, 200);

            // Set up animation data, too
            fps = 5.0;
            secondsPerFrame = 1.0 / fps;
            timeCounter = 0;
            dinosCurrentFrame = 1;








        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            switch (dinoState)
            {
                // If the dino is facing left, check if the left or right key is down
                // (ie, it should start moving in one direction or the other)
                case PlayerState.IdleLeft:
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        isMoving = true;
                        dinoState = PlayerState.MoveLeft;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        isMoving = true;
                        dinoState = PlayerState.MoveRight;
                    }
                    break;
                // If the dino is facing right, check if the left or right key is down
                // (ie, it should start moving in one direction or the other)
                case PlayerState.IdleRight:
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        isMoving = true;
                        dinoState = PlayerState.MoveLeft;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        isMoving = true;
                        dinoState = PlayerState.MoveRight;
                    }
                    break;
                // If the dino is walking left, check if the left key is no longer being pressed,
                // and move the sprite left if not
                case PlayerState.MoveLeft:
                    if (!Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        isMoving = false;
                        dinoState = PlayerState.IdleLeft;
                    }
                    else
                    {
                        isMoving = true;
                        dinoPosition.X -= 5;
                    }
                    break;
                // If the dino is walking right, check if the right key is no longer being pressed,
                // and move the sprite right if not
                case PlayerState.MoveRight:
                    if (!Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        isMoving = false;
                        dinoState = PlayerState.IdleRight;
                    }
                    else
                    {
                        isMoving = true;
                        dinoPosition.X += 5;
                    }
                    break;
                // The dino sprite is set facing to the right by default
                default:
                    dinoState = PlayerState.IdleRight;
                    break;

            }








            




            // Always update the animation
            UpdateAnimation(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Helper for updating the selected dino's animation based on time
        /// </summary>
        /// <param name="gameTime">Info about time from MonoGame</param>
        private void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is how the last GAME frame took
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active,
                // ensuring we go back to 1 eventually
                dinosCurrentFrame++;
                if (dinosCurrentFrame >= 4) // hardcoded here because I KNOW what my spritesheet looks like
                {
                    dinosCurrentFrame = 1;
                }

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Making it so that the drawn in assets don't become blurred when enlarged
            _spriteBatch.Begin
                (SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                RasterizerState.CullCounterClockwise);

            background1.Draw(_spriteBatch, SpriteEffects.None);

            // ================================================================
            // Drawing in the dino in it's current state
            // ================================================================

            // If the dino is hurt and facing left, animate it hurt facing left
            if (dinoState.Equals(PlayerState.HurtLeft))
            {
                DrawDinoHurt(SpriteEffects.FlipHorizontally);
            }
            // If the dino is hurt and facing right, animate it hurt facing right
            else if (dinoState.Equals(PlayerState.HurtRight))
            {
                DrawDinoHurt(SpriteEffects.None);
            }
            // If the dino sprite is walking left, animate it walking flipped
            else if (dinoState.Equals(PlayerState.MoveLeft))
            {
                DrawDinoMoving(SpriteEffects.FlipHorizontally);
            }
            // If the dino sprite is walking right, animate it walking
            else if (dinoState.Equals(PlayerState.MoveRight))
            {
                DrawDinoMoving(SpriteEffects.None);
            }
            // If the dino sprite is standing left, animate it standing flipped
            else if (dinoState.Equals(PlayerState.IdleLeft))
            {
                DrawDinoIdle(SpriteEffects.FlipHorizontally);
            }
            // If the dino sprite is standing right, animate it standing
            else
            {
                DrawDinoIdle(SpriteEffects.None);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws the current dino with an idle animation
        /// </summary>
        /// <param name="flip">Should he be flipped horizontally?</param>
        private void DrawDinoIdle(SpriteEffects flip)
        {
             //This version of draw can flip (mirror) the
             //image horizontally depending on our parameter
            _spriteBatch.Draw(dinoTextureRed,
            new Rectangle((int)dinoPosition.X, 345, widthOfSingleSprite * 5, dinoTextureGreen.Height * 5),
            new Rectangle(dinosCurrentFrame * widthOfSingleSprite, 0, widthOfSingleSprite, dinoTextureGreen.Height),
            Color.White,
            0f,
            Vector2.Zero,
            flip,
            .826f);
        }

        /// <summary>
        /// Draws the current dino with a moving animation
        /// </summary>
        /// <param name="flip">Should he be flipped horizontally?</param>
        private void DrawDinoMoving(SpriteEffects flip)
        {
            //This version of draw can flip (mirror) the
            //image horizontally depending on our parameter
            _spriteBatch.Draw(dinoTextureGreen,
            new Rectangle((int)dinoPosition.X, 345, widthOfSingleSprite * 5, dinoTextureGreen.Height * 5),
            new Rectangle((dinosCurrentFrame + 6) * widthOfSingleSprite, 0, widthOfSingleSprite, dinoTextureGreen.Height),
            Color.White,
            0f,
            Vector2.Zero,
            flip,
            .826f);
        }

        /// <summary>
        /// Draws the current dino with a moving animation
        /// </summary>
        /// <param name="flip">Should he be flipped horizontally?</param>
        private void DrawDinoHurt(SpriteEffects flip)
        {
            //This version of draw can flip (mirror) the
            //image horizontally depending on our parameter
            _spriteBatch.Draw(dinoTextureGreen,
            new Rectangle((int)dinoPosition.X, 345, widthOfSingleSprite * 5, dinoTextureGreen.Height * 5),
            new Rectangle((dinosCurrentFrame + 12) * widthOfSingleSprite, 0, widthOfSingleSprite, dinoTextureGreen.Height),
            Color.White,
            0f,
            Vector2.Zero,
            flip,
            .826f);
        }









    }
}
