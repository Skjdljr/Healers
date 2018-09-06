using HM_Utils;

public class Mage : CharacterData
{
    //Class specific 
    int mana { get; set; }
    float spellDistruption { get; set; }

    // Use this for initialization
    protected override void Start()
    {
        SetBaseStats("Mage", 50, 50, 100, 8.0f, 2.5f, CLASS_SPECIFIC_TYPE.MAGE);
        SetResistances(.3f, .2f, .3f, .2f, .5f, .1f);
        mana = 100;
        spellDistruption = .25f;
    }

    // Update is called once per frame
    public override void Update()
    {

    }
}
