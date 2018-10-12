using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatedJavaCode
{
	public class Program
    {
        public static LinkedList<String> GenerateBinaryRepresentationList(int n)
        {
            //Create an empty queue of strings with which to perform the traversal
            LinkedQueue<StringBuilder> q = new LinkedQueue<StringBuilder>();

            //A list for returning the binary values
            LinkedList<String> output = new LinkedList<string>();

            if(n < 1)
            {
                //binary representation of negative values is not supported
                //return an empty list
                return output;
            }

            //Enqueue the first binary number. Use a dynamic string to avoid string concat.
            q.Push(new StringBuilder("1"));

            //BFS
            while(n-- > 0)
            {
                //print the front of queue
                StringBuilder sb = q.Pop();
                output.AddLast(sb.ToString());

                //make a copy
                StringBuilder sbc = new StringBuilder(sb.ToString());

                //left child
                sb.Append('0');
                q.Push(sb);

                //right child
                sbc.Append('1');
                q.Push(sbc);
            }
            return output;
        }

        //Driver program to test above function
        public static void Main(String[] args)
        {
            int n = 10;
            if(args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print to print binary up to, like this:");
                Console.WriteLine("\tjava Main 12");
                return;
            }
            try
            {
                n = int.Parse(args[0]);
            }
            catch(FormatException e)
            {
                Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
                return;
            }
            LinkedList<String> output = GenerateBinaryRepresentationList(n);
            //Print it right justified. Longest string is the last one.
            //Print enought spaces to move it over the correct distance.
            int maxLength = output.Last().Length;

            foreach(string s in output)
            {
                for(var i = 0; i < maxLength - s.Length; ++i)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(s);
            }
        }
    }
}