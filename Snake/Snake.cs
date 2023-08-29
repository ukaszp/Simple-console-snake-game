using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakev0
{
    abstract class Snake
    {
        protected bool gameOverBool = false;
        protected string nick = "";
        protected int headx, heady, lenght = 0;
        protected bool goup = false;
        protected bool godown = false;
        protected bool goright = false;
        protected bool goleft = false;
        public List<SnakeSegments> segments;

        public bool Goup { get => goup; protected set => goup = value; }
        public bool Godown { get => godown; protected set => godown = value; }
        public bool Goright { get => goright; protected set => goright = value; }
        public bool Goleft { get => goleft; protected set => goleft = value; }
        public string Nick { get=> nick; set=>nick=value; }
        public int Headx { get => headx; set => headx = value; }
        public int Heady { get => heady; set => heady = value; }
        public int Lenght { get => lenght; set => lenght = value; }
        public bool GameOverBool { get => gameOverBool; set => gameOverBool = value; }

        public abstract void DrawSnake();
        protected virtual void RemoveSnake()
        {
            if (!(segments.Count == 0))
            {
                Console.SetCursorPosition(segments.ElementAt(0).x, segments.ElementAt(0).y);
                Console.Write("  ");
            }
            else
            {
                Console.SetCursorPosition(headx, heady);
                Console.Write("  ");
            }
        }
        public virtual int PrevStepX()
        {
            if (Goup)
            {
                return headx;
            }
            if (Godown)
            {
                return headx;
            }
            if (Goright)
            {
                return headx - 2;
            }
            if (Goleft)
            {
                return headx + 2;
            }
            return 0;
        }
        public virtual int PrevStepY()
        {
            if (Goup)
            {
                return heady + 1;
            }
            if (Godown)
            {
                return heady - 1;
            }
            if (Goright)
            {
                return heady;
            }
            if (Goleft)
            {
                return heady;
            }
            return 0;
        }
        public virtual void GoUp()
        {
            Goup = true;
            Godown = false;
            Goleft = false;
            Goright = false;
            RemoveSnake();
            heady -= 1;
            DrawSnake();

        }
        public virtual void GoDown()
        {

            Godown = true;
            Goup = false;
            Goleft = false;
            Goright = false;
            RemoveSnake();
            heady += 1;
            DrawSnake();
        }
        public virtual void GoLeft()
        {
            Goleft = true;
            Godown = false;
            Goup = false;
            Goright = false;
            {
                RemoveSnake();
                headx -= 2;
                DrawSnake();
            }

        }

        public virtual void GoRight()
        {
            Goright = true;
            Godown = false;
            Goup = false;
            Goleft = false;
            RemoveSnake();
            headx += 2;
            DrawSnake();

        }
        public virtual void MoveSnake()
        {

            for (var i = 0; i < lenght; i++)
            {
                if (i == lenght - 1)
                {
                    segments.ElementAt(i).x = PrevStepX();
                    segments.ElementAt(i).y = PrevStepY();
                }
                else
                {
                    segments.ElementAt(i).x = segments.ElementAt(i + 1).x;
                    segments.ElementAt(i).y = segments.ElementAt(i + 1).y;
                }
            }

        }
        public virtual bool HitMs()
        {
            if (segments.Count > 0)
            {
                foreach (SnakeSegments seg in segments)
                {
                    if (headx == seg.x && heady == seg.y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
