using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.IO;
using System.Collections;

namespace Cinar.Test
{
    public class ImlaKlavuzu
    {
        public static void Run()
        {
            string path = @"K:\Work\CSharp\Cinar\_library\imla_klavuzu_basit.txt";
            string[] lines = File.ReadAllLines(path, Encoding.Default);

            for(int i=0; i<lines.Length; i++)
                if(!Square.Dict.ContainsKey(lines[i]))
                    Square.Dict.Add(lines[i], i);

            string letters = "DSÜGMNRİNİAMLİKI";
            for (int i = 0; i < 16; i++)
                Square.AllSquares[i % 4, i / 4] = new Square(i % 4, i / 4, letters[i]);

            for(int i=0; i<4; i++)
                for(int j = 0; j<4; j++)
                    Square.AllSquares[i,j].StartSearch();

        }
    }

    public class Square
    {
        public Point Position;
        public char Content;

        public Square(int x, int y, char content)
        {
            Position = new Point(x,y);
            Content = content;
        }

        public static SortedDictionary<string, int> Dict = new SortedDictionary<string, int>();
        public static Square[,] AllSquares = new Square[4,4];
        public static ListSquare VisitedSquares;
        public static List<string> FoundWords = new List<string>();

        public Square N
        {
            get
            {
                if (Position.Y == 0)
                    return null;
                return AllSquares[Position.X, Position.Y - 1];
            }
        }
        public Square NE
        {
            get
            {
                if (Position.Y == 0 || Position.X == 3)
                    return null;
                return AllSquares[Position.X + 1, Position.Y - 1];
            }
        }
        public Square E
        {
            get
            {
                if (Position.X == 3)
                    return null;
                return AllSquares[Position.X + 1, Position.Y];
            }
        }
        public Square SE
        {
            get
            {
                if (Position.Y == 3 || Position.X == 3)
                    return null;
                return AllSquares[Position.X + 1, Position.Y + 1];
            }
        }
        public Square S
        {
            get
            {
                if (Position.Y == 3)
                    return null;
                return AllSquares[Position.X, Position.Y + 1];
            }
        }
        public Square SW
        {
            get
            {
                if (Position.Y == 3 || Position.X == 0)
                    return null;
                return AllSquares[Position.X - 1, Position.Y + 1];
            }
        }
        public Square W
        {
            get
            {
                if (Position.X == 0)
                    return null;
                return AllSquares[Position.X - 1, Position.Y];
            }
        }
        public Square NW
        {
            get
            {
                if (Position.Y == 0 || Position.X == 0)
                    return null;
                return AllSquares[Position.X - 1, Position.Y - 1];
            }
        }

        public override string ToString()
        {
            return Content.ToString();
        }

        public void StartSearch()
        {
            VisitedSquares = new ListSquare();

            this.Search();
        }

        public void Search()
        {
            VisitedSquares.Add(this);

            if (VisitedSquares.Count > 4)
                if(!checkWord())
                    return;

            foreach (var square in new []{E, N, NE, SE, S, SW, W, NW})
                if (square != null && !VisitedSquares.Contains(square))
                {
                    square.Search();
                    VisitedSquares.RemoveAt(VisitedSquares.Count-1);
                }
        }

        private bool checkWord()
        {
            string word = VisitedSquares.Select(s => s.Content.ToString()).StringJoin();
            if (Dict.Keys.Any(s => s == word))
            {
                if (!FoundWords.Contains(word))
                {
                    FoundWords.Add(word);
                    Console.WriteLine(word);
                }
            }

            return Dict.Keys.Any(s => s.StartsWith(word));
        }
    }

    public class ListSquare : List<Square>
    {
        public override string ToString()
        {
            return this.Select(s=>s.Content.ToString()).StringJoin();
        }
    }
}
