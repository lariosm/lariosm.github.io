using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatedJavaCode
{
    public class Node<T>
    {
        public T Data;
        public Node<T> Next;


        public Node(T data, Node<T> next)
        {
            Data = data;
            Next = next;
        }
    }

    public interface IQueueInterface<T>
    {
        T Push(T element);

        T Pop();

        bool IsEmpty();
    }

    public class QueueUnderflowException : Exception
    {
        public QueueUnderflowException() : base()
        {
            //No code here
        }

        public QueueUnderflowException(string message) : base(message)
        {
            //No code here
        }

    }

    public class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> Front;
        private Node<T> Rear;

        public LinkedQueue()
        {
            Front = null;
            Rear = null;
        }

        public T Push(T element)
        {
            if(element == null)
            {
                throw new NullReferenceException();
            }
            
            if(IsEmpty())
            {
                Node<T> tmp = new Node<T>(element, null);
                Rear = Front = tmp;
            }
            else
            {
                Node<T> tmp = new Node<T>(element, null);
                Rear.Next = tmp;
                Rear = tmp;
            }
            return element;
        }

        public T Pop()
        {
            T tmp = default(T);
            if(IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if(Front == Rear)
            {
                tmp = Front.Data;
                Front = null;
                Rear = null;
            }
            else
            {
                tmp = Front.Data;
                Front = Front.Next;
            }
            return tmp;
        }

        public bool IsEmpty()
        {
            if(Front == null && Rear == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class MainClass
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
