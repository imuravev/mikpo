using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mikpo_3.Classes
{

    /// <summary>
    /// Модель кассы
    /// </summary>
    public class CashBox
    {
        /// <summary>
        /// Очередь объектов
        /// </summary>
        private ConcurrentQueue<QObject> objects;

        private Random r=null;

        private double willFree;

        private bool isAlive = true;

        private Clock clock = null;

        public CashBox(Clock clock)
        {
            objects = new ConcurrentQueue<QObject>();
            r = new Random();
            this.clock = clock;
            willFree = 0;
        }


        /// <summary>
        /// Минимальное время на обработку заявки
        /// </summary>
        public double MinTimeOnQuery
        {
            get;
            set;
        }

        /// <summary>
        /// Макс время на обработку заявки
        /// </summary>
        public double MaxTimeOnQuery
        {
            get;
            set;
        }

        /// <summary>
        /// Длина очереди
        /// </summary>
        public int CurrentQueueLen
        {
            get
            {
                return objects.Count;
            }
        }


        /// <summary>
        /// Добавить объект в очередь
        /// </summary>
        /// <param name="obj"></param>
        public void AddToQueue(QObject obj)
        {
            obj.StartServe = willFree;
            obj.TimeOut = willFree = (willFree + this.MinTimeOnQuery + (this.MaxTimeOnQuery - this.MinTimeOnQuery) * r.NextDouble());
       
            this.objects.Enqueue(obj);
        }


        /// <summary>
        /// Удалить объект из очереди
        /// </summary>
        private QObject RemoveFromQueue()
        {
            QObject obj;
            obj.Id = -1;
            obj.TimeIn = obj.TimeOut  = obj.StartServe = -1;
     
            if (this.CurrentQueueLen > 0)
            {
                while(obj.Id==-1)
                    this.objects.TryDequeue(out obj);
            }

            return obj;
        }



        Thread thread;

        public void Start()
        {
             thread = new Thread(new ThreadStart(new Action(() => {
                while (this.isAlive)
                {
                    if (this.objects.Count > 0)
                    {
                        QObject obj = this.objects.First();
                        if (obj.Id >= 0 && obj.StartServe <= clock.CurrentTime)
                            this.RemoveFromQueue();
                        else
                        {
                            if (this.clock.isEnd && obj.StartServe > this.clock.CurrentTime  )
                                isAlive = false;
                        }
                    }
                    else
                        if (this.clock.isEnd) isAlive = false;
                    Thread.Sleep(0);
                }
            })));
            thread.Start();
        }


        public void Close()
        {
            this.isAlive = false;
        }


        public void Join()
        {
            thread.Join();
        }






    }
}
