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
            string path = @"imla_klavuzu_basit.txt";
            string[] lines = File.ReadAllLines(path, Encoding.Default);

            for(int i=0; i<lines.Length; i++)
                if(!Square.Dict.ContainsKey(lines[i]))
                    Square.Dict.Add(lines[i], i);

            string letters;
            Console.Write("HARFLER: ");
            while ((letters = Console.ReadLine().ToUpper()) != "")
            {
                Square.AllSquares = new Square[4, 4];
                Square.FoundWords = new List<string>();

                for (int i = 0; i < 16; i++)
                    Square.AllSquares[i % 4, i / 4] = new Square(i % 4, i / 4, letters[i]);

                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        Square.AllSquares[i, j].StartSearch();

                Console.ReadLine();
                Console.Clear();
                Console.Write("HARFLER: ");
            }
        }
    }

    public class Square
    {
        public int X;
        public int Y;
        public char Content;

        public Square(int x, int y, char content)
        {
            X = x;
            Y = y;
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
                if (Y == 0)
                    return null;
                return AllSquares[X,Y - 1];
            }
        }
        public Square NE
        {
            get
            {
                if (Y == 0 || X == 3)
                    return null;
                return AllSquares[X + 1, Y - 1];
            }
        }
        public Square E
        {
            get
            {
                if (X == 3)
                    return null;
                return AllSquares[X + 1, Y];
            }
        }
        public Square SE
        {
            get
            {
                if (Y == 3 || X == 3)
                    return null;
                return AllSquares[X + 1, Y + 1];
            }
        }
        public Square S
        {
            get
            {
                if (Y == 3)
                    return null;
                return AllSquares[X, Y + 1];
            }
        }
        public Square SW
        {
            get
            {
                if (Y == 3 || X == 0)
                    return null;
                return AllSquares[X - 1, Y + 1];
            }
        }
        public Square W
        {
            get
            {
                if (X == 0)
                    return null;
                return AllSquares[X - 1, Y];
            }
        }
        public Square NW
        {
            get
            {
                if (Y == 0 || X == 0)
                    return null;
                return AllSquares[X - 1, Y - 1];
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
            string word = VisitedSquares.ToString();
            if (Dict.ContainsKey(word))
            {
                if (!FoundWords.Contains(word) && word.Length>3)
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
