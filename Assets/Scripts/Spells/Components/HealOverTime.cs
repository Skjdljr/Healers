using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Spells.Components
{
    public class HealOverTime : TickComponent
    {
        public HealOverTime(float interval) : base(interval)
        {
        }

        public float healAmount  = 0;

        protected override void ExecuteTick()
        {
            foreach (var target in targets)
            {
                target.Heal(healAmount);
            }
        }
    }
}
