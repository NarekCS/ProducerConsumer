using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerAndConsumer
{
    class Program
    {       
            static void Main(string[] args)
            {          
               ProducerConsumer pc = new ProducerConsumer(10, 5);
                pc.Produce();
                pc.Consume();     
            }
     }
}

