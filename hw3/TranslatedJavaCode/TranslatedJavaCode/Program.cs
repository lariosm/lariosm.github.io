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
}
