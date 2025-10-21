using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chess_mono
{
    class Bishop : Piece
    {
        public Bishop(Texture2D texture_, Rectangle rectangle_, bool Alive_, bool isWhite_, Vector2 position_, string name_) : base(texture_, rectangle_, Alive_, isWhite_, position_, name_)
        {
            position = position_;
            rectangle = rectangle_;
            texture = texture_;
            Alive = Alive_;
            isWhite = isWhite_;
            name = name_;
        }
        public override Rectangle updateRec()
        {
            return base.updateRec();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }



    }
}
