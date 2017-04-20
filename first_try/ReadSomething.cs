using System;
using System.Collections.Generic;
using System.IO;

namespace first_try
{
    public class ReadSomething
    {
        public static List<int> ReadListIntegers(string inputFile)
        {
            var result = new List<int>();
            var input = new StreamReader(@inputFile);
            var countOfIntegers = Convert.ToInt32(input.ReadLine());
            if (countOfIntegers > 0)
            {
                var buffer = input.ReadLine().Split(' ');

                for (var i = 0; i < countOfIntegers; ++i)
                    result.Add(Convert.ToInt32(buffer[i]));
            }
            input.Close();
            return result;
        }

        public static Edge ReadEdge(StreamReader input)
        {
            var buffer = input.ReadLine().Split(' ');       
            var from = Convert.ToInt32(buffer[0]);
            var to = Convert.ToInt32(buffer[1]);
            var id = Convert.ToInt32(buffer[2]);
            return new Edge(from, to, id);
        }

        public static List<Edge> ReadListEdges(string inputFile)
        {
            var input = new StreamReader(@inputFile);
            var countOfEdges = Convert.ToInt32(input.ReadLine());
            var result = new List<Edge>();
            for (var i = 0; i < countOfEdges; ++i)        
                result.Add(ReadEdge(input));
            input.Close();
            return result;
        }
    }
}