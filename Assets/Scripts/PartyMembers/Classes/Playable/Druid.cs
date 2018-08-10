using HM_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PartyMembers.Classes.Playable
{
    public class Druid : CharacterData
    {
        //Class specific stuff?

        // Use this for initialization
        protected override void Start()
        {
            SetBaseStats("Druid", 100, 50, 100, 1.0f, 2.5f, CLASS_SPECIFIC_TYPE.DRUID);
            SetResistances(.3f, .2f, .3f, .2f, .5f, .1f);
        }
    }
}
