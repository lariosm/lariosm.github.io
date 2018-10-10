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
}
