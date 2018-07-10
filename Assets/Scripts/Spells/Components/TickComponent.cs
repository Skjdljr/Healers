using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HM_Utils;

namespace Assets.Scripts.Spells.Components
{
    public abstract class TickComponent : BaseComponent
    {
        private SimpleTimer timer;
        private double updateInterval;

        public TickComponent(double interval)
        {
            updateInterval = interval;
            timer = new SimpleTimer();
            timer.Start();
        }

        public void Update()
        {
            if(timer.GetElapsedTime() > updateInterval)
            {
                ExecuteTick();
            }
        }

        //For every implementation do something with the execute....
        protected abstract void ExecuteTick();
    }

}
