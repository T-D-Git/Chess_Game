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
    abstract public class Piece
    {
        public Texture2D texture;

        public Vector2 position;

        public bool Alive;

        public Rectangle rectangle;        
        
        public Vector2 startPos = new Vector2();

        public bool legalMove = false;

        public string chessPiece;

        public Vector2 endPos = new Vector2();

        public bool isWhite;

        public string name;





        public Piece(Texture2D texture_, Rectangle rectangle_, bool Alive_, bool isWhite_, Vector2 position_, string name_)
        {
            position = position_;
            rectangle = rectangle_;
            texture = texture_;
            Alive = Alive_;
            isWhite = isWhite_;
            name = name_;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, null, null, 0, null, null, 0);
        }       

        public virtual Rectangle updateRec()
        { 
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public virtual Vector2 getsPosition(int x, int y, bool isWhite)
        {           
            float x_ = ((x * 100) + 50 - (texture.Width / 2));
            float y_ = ((y * 100) + 50 - (texture.Height / 2));
            Vector2 position_ = new Vector2(x_, y_);
            return position_;
            
        }

        

        
    }
}
