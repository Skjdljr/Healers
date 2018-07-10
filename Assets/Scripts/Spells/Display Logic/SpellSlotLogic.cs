using UnityEngine;
using HM_Utils;
using UnityEngine.UI;
using Assets.Scripts.Spells.Components;

public class SpellSlotLogic : MonoBehaviour
{
    //Used to display the cd
    public Slider coolDownSlider;

    //TEST!
    private BaseComponent testComponent;

    //Used to hide the cd
    private Canvas fillCanvas;

    private SpellBase spellData = null;

    private SimpleTimer coolDownTimer;

    //To disable the button when on CD
    Button butt;

    public bool onCooldown { get; set; }

    public delegate void SpellSlotClicked(SpellSlotLogic spell);

    public event SpellSlotClicked OnSpellSlotClicked;

    // Use this for initialization
    void Start()
    {
        butt = GetComponent<Button>();
        fillCanvas = coolDownSlider.GetComponent<Canvas>();
        onCooldown = false;
        coolDownTimer = new SimpleTimer();
    }

    void Update()
    {
        if (coolDownTimer != null && onCooldown)
        {
            coolDownSlider.value = 1 - (float)coolDownTimer.GetElapsedTimeSecs() / spellData.coolDown;

            if (coolDownTimer.GetElapsedTimeSecs() >= spellData.coolDown)
            {
                coolDownTimer.Stop();
                onCooldown = false;

                butt.enabled = true;
                fillCanvas.enabled = false;
                coolDownSlider.value = 1;
            }
        }
    }

    public void SetSpell(SpellBase spell)
    {
        spellData = spell;
        Sprite s = Utils.LoadSprite(spellData.imagePath, SPITE_LOCATIONS.SPELLS);
        Image img = GetComponent<Image>();
        img.sprite = s;

        //if(spellData.spellType == SPELL_TYPE.HEALOVERTIME)
        //{
        //    testComponent = new HealOverTime(spellData.interval)
        //    {
        //        healAmount = spellData.power
        //    };
        //}
    }

    public SpellBase GetSpellData()
    {
        return spellData;
    }

    public void Use(BaseCharacter target)
    {
        if (!coolDownTimer.IsRunning())
        {
            print("Spell " + spellData.displayName + " used on target " + target.data.displayName);
            
            //add the component to the list, have the components loop over and call fire?


            if (spellData != null)
            {
                //other actions- Merge ability to a databag/abstract class and have spell base
                //class per spell, have each handle its own functionality. Does not handle combinations well, EASY TO READ/DEBUG
                //component based system. COMPLEX but all functionality 

                //Todo: add a spell used/casted to character or have the spell have a reference or be friend of the character
                //so we can just modifiy its health/whatever
                
                target.AddSpellEffect(spellData);



                coolDownTimer.Start();
                onCooldown = true;
                fillCanvas.enabled = true;
                butt.enabled = false;
            }
        }
    }

    public void ButtonClicked()
    {
        OnSpellSlotClicked(this);
    }
}