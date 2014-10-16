using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mikpo_3.Classes
{
    public class QueuingSystem
    {

        /// <summary>
        /// Выбор в какую очередь вставать
        /// </summary>
        /// <param name="prevQNum">номер очереди, выбранной в прошлый раз</param>
        /// <returns></returns>
        private int getQueueNum(int prevQNum)
        {
            prevQNum++;
            if (prevQNum == this.ThreadCount) prevQNum = 0;
            return prevQNum;
        }

        /// <summary>
        /// Генератор запросов
        /// </summary>
        private QueryGenerator generator; 

        /// <summary>
        /// Класс, для записи в лог.
        /// </summary>
        private LogFile Log;

        /// <summary>
        /// Число потоков
        /// </summary>
        public int ThreadCount
        {
            get;
            set;
        }

        /// <summary>
        /// Время работы
        /// </summary>
        public double WorkTime
        {
            get;
            set;
        }

        /// <summary>
        /// Текущее время системы
        /// </summary>
        public Clock clock;


        /// <summary>
        /// Максимальное время на обработку запроса
        /// </summary>
        public double MaxTimeOnQuery
        {
            get;
            set;
        }
        /// <summary>
        /// Минимальнео время на обработку запроса
        /// </summary>
        public double MinTimeOnQuery
        {
            get;
            set;
        }

        /// <summary>
        /// Вкл/Выкл запись в лог
        /// </summary>
        public bool LogEnable
        {
            get;
            set;
        }


        /// <summary>
        /// Путь к лог.файлу
        /// </summary>
        public string LogFile
        {
            get;
            set;
        }

        /// <summary>
        /// Интенсивность генерирующихся заявок 
        /// </summary>
        public double Intension
        {
            get;
            set;
        }



        /// <summary>
        /// Конструктор
        /// </summary>
        public QueuingSystem()
        {
        }


        public QueuingSystem(string inifile)
        {
            this.LoadParams(inifile);
        }


        public int CountNonServedObjects
        {
            get;
            private set;
        }

        /// <summary>
        /// Загрузка параметров из Ini файла
        /// </summary>
        /// <param name="PathToIni"></param>
        public void LoadParams(string PathToIni)
        {


            IniFile ini = new IniFile(PathToIni);

            this.LogEnable = ini.IniReadBoolValue("Log", "Enable");
            this.LogFile = ini.IniReadValue("Log", "Path");


            this.ThreadCount = ini.IniReadIntValue("Settings", "Threads");
            this.MinTimeOnQuery = ini.IniReadDoubleValue("Settings", "MinTimeOnQuery");
            this.MaxTimeOnQuery = ini.IniReadDoubleValue("Settings", "MaxTimeOnQuery");
            this.Intension = ini.IniReadDoubleValue("Settings", "Intension");
            this.WorkTime = ini.IniReadDoubleValue("Settings", "WorkTime");
        }


        /// <summary>
        /// Запуск системы обслуживания
        /// </summary>
        public void Start()
        {
            this.clock = new Clock(this.WorkTime);
            this.generator = new QueryGenerator(this.Intension);
            if (this.LogEnable)
            {
                Log = new LogFile(this.LogFile);
                Log.Add("НАЧАЛО МОДЕЛИРОВАНИЯ");
                Log.Add(String.Format("Параметры: \r\n Threads ={0} \r\n MinTimeOnQuery={1} \r\n MaxTimeOnQuery={2} \r\n Intension={3}  \r\n WorkTime={4}",
                    this.ThreadCount,this.MinTimeOnQuery, this.MaxTimeOnQuery, this.Intension,this.WorkTime));
                Log.Add("*******************");

            }
            CashBox[] cashes = new CashBox[this.ThreadCount];
            for (int i = 0; i < cashes.Length; i++)
            {
                cashes[i] = new CashBox(this.clock);
                cashes[i].MinTimeOnQuery = this.MinTimeOnQuery;
                cashes[i].MaxTimeOnQuery = this.MaxTimeOnQuery;
                cashes[i].Start();
            }
            int qNum = 0;  
            while (!clock.isEnd)
            {
                QObject obj = generator.Next();
                this.clock.CurrentTime =obj.TimeIn;
                if (this.LogEnable) Log.Add(String.Format("Объект {2} прибыл в {0} и встал в очередь {1}", obj.TimeIn, qNum, obj.Id));
                cashes[qNum].AddToQueue(obj);
                qNum = this.getQueueNum(qNum);
            }
            for (int i = 0; i < cashes.Length; i++)
            {
                cashes[i].Join();
                this.CountNonServedObjects += cashes[i].CurrentQueueLen;
            }

            
            

        }

   



    }
}
