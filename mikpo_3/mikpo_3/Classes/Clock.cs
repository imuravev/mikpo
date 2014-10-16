using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikpo_3.Classes
{
    public class Clock
    {

        private double currentTime;

        public double CurrentTime
        {
            get
            {
                return this.currentTime;
            }
            set
            {
                if (!isEnd)
                {
                    if (value > this.MaxTime)
                    {
                        isEnd = true;
                        this.currentTime = MaxTime;
                    }
                    else
                        this.currentTime = value;
                }
            }
        }

        private double MaxTime;

        public bool isEnd
        {
            get;
            set;
        }

        public Clock(double maxTime)
        {
            this.isEnd = false;
            this.CurrentTime = 0;
            this.MaxTime = maxTime;
        }

    }
}
