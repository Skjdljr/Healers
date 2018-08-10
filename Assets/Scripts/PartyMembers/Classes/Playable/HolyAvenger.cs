using HM_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PartyMembers.Classes.Playable
{
    public class HolyAvenger : CharacterData
    {
        //Class specific stuff?

        // Use this for initialization
        protected override void Start()
        {
            SetBaseStats("Holy Avenger", 100, 50, 100, 1.0f, 2.5f, CLASS_SPECIFIC_TYPE.HOLY_AVENGER);
            SetResistances(.3f, .2f, .3f, .2f, .5f, .1f);
        }
    }
}
