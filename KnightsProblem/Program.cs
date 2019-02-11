using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTracking
{
    public class Position
    {
        public int posX { get; set; }
        public int posY { get; set; }

        public static Position operator+ (Position p1, Position p2)
        {
            Position p = new Position() { posX = p1.posX + p2.posX, posY = p1.posY + p2.posY };
            return p;
        }
    }

    class KnightsProblem
    {
        /*Start from (0, 0), boundaries are (0, *), (*, 0), (7, *), (*, 7)*/
        static List<Position> _moves;
        static Position _start;
        static int[, ] chessBoard;
         
        static public void initiateGame()
        {
            // List all the 8 possible moves at each spot
            _moves = new List<Position>();
            _moves.Add(new Position() { posX = 1, posY = 2 });
            _moves.Add(new Position() { posX = 1, posY = -2});
            _moves.Add(new Position() { posX = -1, posY = 2 });
            _moves.Add(new Position() { posX = -1, posY = -2 });
            _moves.Add(new Position() { posX = 2, posY = 1 });
            _moves.Add(new Position() { posX = -2, posY = 1 });
            _moves.Add(new Position() { posX = 2, posY = -1 });
            _moves.Add(new Position() { posX = -2, posY = -1 });

            _start = new Position() { posX = 0, posY = 0 };

            chessBoard = new int[8, 8];
        }

        static public bool isOut(Position pos)
        {
            if (pos.posX < 0 || pos.posX > 7) return true;
            if (pos.posY < 0 || pos.posY > 7) return true;
            return false;
        }
        
        static void Main(string[] args)
        {
            initiateGame();
            List<Position> choices = new List<Position>();
            Position cur = _start;
            while (chessBoard.Cast<int>().Sum() < 64)
            {
                Console.WriteLine("Current position is: " + cur.posX + " , " + cur.posY);
                foreach (Position move in _moves)
                {
                    if (!isOut(cur + move))
                    {
                        cur = cur + move;
                        choices.Add(move);
                        chessBoard[cur.posX, cur.posY] = 1;
                        break;
                    }
                }
                if (choices.Count() > 100)
                {
                    Console.WriteLine("Mission failed.");
                    return;
                }
            }

            Console.WriteLine("Mission completed.");
        }
    }
}
