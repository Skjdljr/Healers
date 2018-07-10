using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Spells.Components
{
    public abstract class BaseComponent : MonoBehaviour
    {
        protected List<BaseCharacter> targets
        {
            get
            {
                return targets;
            }
            set
            {
                if (value != null)
                {
                    targets.AddRange(value);
                }
            }
        }

        protected BaseComponent()
        {
            targets = new List<BaseCharacter>();
        }
    }
}

/*example components
    *damage modifier (block/dodge ...)
    * pre-reqComponent (undead ... can cast, choose targets
    * noTarget
    * 
*/
