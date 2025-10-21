using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_mono
{
    class Piece
    {
        public Texture2D texture;

        public Vector2 position;

        public bool Alive;

        Rectangle rectangle;

        public MouseState mouseState;

        public Point mousePoint;

        public bool isHovered = false;

        public bool isClicked;
        

        public Piece(Texture2D newTexture, Rectangle newRectangle, bool Alive_)
        {
            texture = newTexture;
            rectangle = newRectangle;
            Alive = Alive_;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, null, null, 0, null, null, 0);
        }

        public void Update(GameTime gametime)
        {
            mouseState = Mouse.GetState();
            mousePoint = new Point(mouseState.X, mouseState.Y);
            rectangle = updateRec();

            if (isClicked = mouseState.LeftButton == ButtonState.Pressed)
            {
                if (rectangle.Contains(mousePoint) || isHovered)
                {
                    isHovered = true;
                }
            }
            else
            {
                if (isHovered == true)
                {
                    position.X = (mouseState.X - (mouseState.X % 100)) + 50 - (texture.Width / 2);
                    position.Y = (mouseState.Y - (mouseState.Y % 100)) + 50 - (texture.Height / 2);
                }

                isHovered = false;
            }
            if (isClicked && isHovered)
            {
                position = new Vector2(mouseState.X - (texture.Width / 2), mouseState.Y - (texture.Height / 2));
            }
            
        }
    

        public Rectangle updateRec()
        { 
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

    }
}
