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
        public Vector2 tempPos = new Vector2();


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
                if (isHovered == false)
                {
                    tempPos.X = (mouseState.X - (mouseState.X % 100)) + 50 - (texture.Width / 2);
                    tempPos.Y = (mouseState.Y - (mouseState.Y % 100)) + 50 - (texture.Height / 2);
                }
                if (rectangle.Contains(mousePoint) || isHovered)
                {
                    isHovered = true;
                }
                if (isClicked && isHovered)
                {
                    position = new Vector2(mouseState.X - (texture.Width / 2), mouseState.Y - (texture.Height / 2));
                }
            }
            else
            {
                if (isHovered == true)
                {
                    if (mouseState.X > -1 && mouseState.X <801 && mouseState.Y >-1 && mouseState.Y <801)
                    {
                        position.X = (mouseState.X - (mouseState.X % 100)) + 50 - (texture.Width / 2);
                        position.Y = (mouseState.Y - (mouseState.Y % 100)) + 50 - (texture.Height / 2);                      
                    }
                    else
                    {
                        position = tempPos;
                    }                    
                }
                isHovered = false;

            }
        }
    

        public Rectangle updateRec()
        { 
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

    }
}
