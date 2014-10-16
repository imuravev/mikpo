using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikpo_3.Classes
{
    /// <summary>
    /// Генератор запросов
    /// </summary>
    public class QueryGenerator
    {
        /// <summary>
        /// Интенсивность генерирующихся заявок 
        /// </summary>
        public double Intension
        {
            get;
            set;
        }

        private int objId;

        private double lastTimeIn;

        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private readonly Random r=null;

        /// <summary>
        /// Генерирует новый запрос
        /// </summary>
        /// <returns></returns>
        public QObject Next()
        {
            QObject res;
            res.Id=this.objId++;
            res.TimeIn = lastTimeIn =  lastTimeIn + (-Math.Log(r.NextDouble()) / this.Intension);
            res.TimeOut = 0;
            res.StartServe = 0;
            return res;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="intension"> Интенсивность заявок </param>
        public QueryGenerator(double intension)
        {
            this.objId = 0;
            this.Intension = intension;
            this.lastTimeIn = 0;
            r = new Random();
        }
    }
}
