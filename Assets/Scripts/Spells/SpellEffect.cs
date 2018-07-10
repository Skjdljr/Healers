using HM_Utils;
using UnityEngine;
using UnityEngine.UI;

public class SpellEffect : MonoBehaviour
{
    private SimpleTimer timer;
    private SpellBase spellData;
    private BaseCharacter target;

    private float intervalDt;
    public Image effectImage = null;

    public void SetEffect(SpellBase spell, BaseCharacter _target)
    {
        if (spell != null && _target != null)
        {
            target = _target;
            spellData = spell;
            effectImage.sprite = Utils.LoadSprite(spellData.imagePath, SPITE_LOCATIONS.SPELLS);

            timer = new SimpleTimer();
            timer.Start();
        }
        else
        {
            print("SpellEffect failed to create spell: " + spell.displayName + " _target: " + _target.data.displayName);
        }
    }

    private void Start()
    {
    }

    public virtual void DoSomething()
    {
        //Break the spell effects out into there own subclasses. Each one handles there own DoSomething
        switch (spellData.spellType)
        {
            case SPELL_TYPE.DEBUFF:
            case SPELL_TYPE.BUFF:
                //target.AddComponent();
                break;
            case SPELL_TYPE.HEALOVERTIME:
                {
                    target.Heal(spellData);
                    //Create heal over time
                    break;
                }
            default:
                {

                    target.Heal(spellData);
                    break;
                }

        }
    }

    private void KillEffect()
    {
        timer.Stop();
        Debug.Log("End of spell effect " + name);

        transform.SetParent(null);
        Destroy(this);
        //Destroy(this.GetComponent<GameObject>());
    }

    public void Update()
    {
        if (timer != null)
        {
            intervalDt += Time.deltaTime;
            if (spellData.interval > 0.0f)
            {
                if (intervalDt >= spellData.interval)
                {
                    intervalDt = 0;
                    DoSomething();
                }
            }
            else
            {
                //This might not be what we want
                DoSomething();
                KillEffect();
                Debug.Log("Spell effect has no interval! " + spellData.displayName);
            }

            if ((float)timer.GetElapsedTimeSecs() >= spellData.duration)
            {
                KillEffect();
            }
        }
    }
}