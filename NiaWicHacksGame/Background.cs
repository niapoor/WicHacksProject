using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace NiaWicHacksGame
{
    class Background : Game1
    {

        private Vector2 dinoPos;

        private Texture2D backgroundPos1;
        private Texture2D backgroundPos2;
        private Texture2D backgroundPos3;
        private Texture2D backgroundPos4;   
        private Texture2D backgroundPos5;

        private Vector2 vector1;
        private Vector2 vector2;
        private Vector2 vector3;
        private Vector2 vector4;
        private Vector2 vector5;

        private bool inMotion;


        public Background(Vector2 dinoPos,
            Texture2D backgroundPos1, Texture2D backgroundPos2,
            Texture2D backgroundPos3, Texture2D backgroundPos4,
            Texture2D backgroundPos5, bool inMotion)
        {
            this.dinoPos = dinoPos;

            this.backgroundPos1 = backgroundPos1;
            this.backgroundPos2 = backgroundPos2;
            this.backgroundPos3 = backgroundPos3;
            this.backgroundPos4 = backgroundPos4;
            this.backgroundPos5 = backgroundPos5;

            vector1 = new Vector2(0, 0);
            vector2 = new Vector2(0, 0);
            vector3 = new Vector2(0, 0);
            vector4 = new Vector2(0, 0);
            vector5 = new Vector2(0, 0);

            this.inMotion = inMotion;

        }


        public void Draw(SpriteBatch sb, SpriteEffects flip)
        {


            if (inMotion)
            {
                vector1.X += 1;
                vector2.X += 2;
                vector3.X += 3;
                vector4.X += 4;
                vector5.X += 5;
            }



            sb.Draw(backgroundPos1,
                new Rectangle((int)vector1.X, -60, backgroundPos1.Width / 2, backgroundPos1.Height / 2),
                new Rectangle(0, 0, backgroundPos1.Width, backgroundPos1.Height),
                Color.White,
                0f,
                Vector2.Zero,
                flip,
                .826f
                );

            sb.Draw(backgroundPos2,
                new Rectangle((int)vector2.X, -60, backgroundPos1.Width / 2, backgroundPos2.Height / 2),
                new Rectangle(0, 0, backgroundPos2.Width, backgroundPos2.Height),
                Color.White,
                0f,
                Vector2.Zero,
                flip,
                .826f
                );

            sb.Draw(backgroundPos3,
                new Rectangle((int)vector3.X, -60, backgroundPos3.Width / 2, backgroundPos3.Height / 2),
                new Rectangle(0, 0, backgroundPos3.Width, backgroundPos3.Height),
                Color.White,
                0f,
                Vector2.Zero,
                flip,
                .826f
                );

            sb.Draw(backgroundPos4,
                new Rectangle((int)vector4.X, -60, backgroundPos4.Width / 2, backgroundPos4.Height / 2),
                new Rectangle(0, 0, backgroundPos4.Width, backgroundPos4.Height),
                Color.White,
                0f,
                Vector2.Zero,
                flip,
                .826f
                );

            sb.Draw(backgroundPos5,
                new Rectangle((int)vector5.X, -60, backgroundPos5.Width / 2, backgroundPos5.Height / 2),
                new Rectangle(0, 0, backgroundPos5.Width, backgroundPos5.Height),
                Color.White,
                0f,
                Vector2.Zero,
                flip,
                .826f
                );




        }







    }
}
