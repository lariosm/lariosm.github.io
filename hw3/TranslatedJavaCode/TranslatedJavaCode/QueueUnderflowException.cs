using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatedJavaCode
{
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
