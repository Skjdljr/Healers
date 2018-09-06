using HM_Utils;

public class Rogue : CharacterData
{
    //Class specific 
    float dodgeChance { get; set; }
    int energy { get; set; }

    // Use this for initialization
    protected override void Start()
    {
        SetBaseStats("Rogue", 100, 65, 1, 2.0f, 0.5f, CLASS_SPECIFIC_TYPE.ROGUE);
        SetResistances(.1f, .3f, .1f, .7f, .1f, .3f);
        dodgeChance = .25f;
        energy = 100;
    }

    // Update is called once per frame
    public override void Update()
    {

    }
}
