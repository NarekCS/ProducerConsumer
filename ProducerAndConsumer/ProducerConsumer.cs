using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerAndConsumer
{
    class ProducerConsumer
    {
        Queue<int> q = new Queue<int>();       
        Random r = new Random();
        const int something = 100;
        int consumersNumber; 
        int producersNumber; 
        public ProducerConsumer(int prodNum, int conNum)
        {
            producersNumber = prodNum;
            consumersNumber = conNum;
        }
        static readonly object obj = new object();
        
        public void Produce()
        {            
            for (int i = 0; i < producersNumber; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        lock (obj)
                        {
                            while (q.Count == 100)
                                Monitor.Wait(obj);
                                                     
                            Thread.Sleep(GetRandNumber(10));
                            q.Enqueue(something);
                            Console.WriteLine("+");
                            Console.WriteLine($"Queue count = {q.Count}");                             
                        }
                        lock (obj)
                            if (q.Count == 20)
                                Monitor.PulseAll(obj);
                    }                    
                }).Start();               
            }            
        }
        
        public void Consume()
        {          
            for (int i = 0; i < consumersNumber; i++)
            {
                 new Thread(() =>
                {
                    while (true)
                    {
                        lock (obj)
                        {
                            while (q.Count == 0)
                                Monitor.Wait(obj);
                          
                            Thread.Sleep(GetRandNumber(10));
                            q.Dequeue();
                            Console.WriteLine("-");
                            Console.WriteLine($"Queue count = {q.Count}");                            
                        }
                        lock (obj)
                             if (q.Count == 80)
                                 Monitor.PulseAll(obj);
                    }                   
                }).Start();               
            }
        }    

        int GetRandNumber(int max)
        {           
            return r.Next(max*20);
        }    
    }
}
