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
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    chessBoard[i, j] = -1; 
                }
            }
        }

        static public bool isSafe(Position pos)
        {
            if (pos.posX < 0 || pos.posX > 7) return false;
            if (pos.posY < 0 || pos.posY > 7) return false;
            if (chessBoard[pos.posX, pos.posY] != -1) return false;
            return true;
        }

        static public bool helper(Position cur, int moveCount)
        {
            if (moveCount == 63) return true;
            Position next;

            foreach(Position move in _moves)
            {
                next = cur + move;
                if (isSafe(next))
                {
                    moveCount++;
                    chessBoard[next.posX, next.posY] = moveCount;
                    Console.WriteLine("Tring " + move.posX + "' " + move.posY + "  From " + cur.posX + "' " + cur.posY + "   ----" + moveCount);
                    if (helper(next, moveCount))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                Console.Write(chessBoard[i, j]);
                            }
                            Console.Write("\n");
                        }
                        return true;
                    }
                    else
                    {
                        moveCount--;
                        chessBoard[next.posX, next.posY] = -1;
                    }
                }
            }
            return false;
        }
        
        static void Main(string[] args)
        {
            initiateGame();
            chessBoard[0, 0] = 0;
            int moveCount = 0;
            Position cur = _start;
            helper(cur, moveCount);
            Console.WriteLine("Mission completed.");
        }
    }
}
