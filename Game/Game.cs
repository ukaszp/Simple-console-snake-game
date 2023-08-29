using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakev0

{

    abstract class Game
    {
        protected int speed = 150;
        protected int growingspeed = 200;
        protected bool growingspeedbool = false;
        protected static int arenaWidth = 52;
        protected static int arenaHeight = 25;
        protected ScoreBoard scoreboard;
        protected FirstSnake snake = new FirstSnake(arenaWidth, arenaHeight);
        protected Arena arena = new Arena(arenaHeight, arenaWidth);
        protected Fruit fruit = new Fruit(arenaWidth, arenaHeight);
        public abstract void StartGame();
        protected abstract void HandleInput();
        protected abstract void Logic();
        protected abstract void GameOver();
        protected abstract void ScoreCounter();
        protected abstract void TypeNick();
        protected abstract void MoveLogic();
       
    }
}
