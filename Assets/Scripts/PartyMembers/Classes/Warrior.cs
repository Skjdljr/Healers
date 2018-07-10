using HM_Utils;

public class Warrior : CharacterData
{
    //Class specific 
    float rage { get; set; }
    float maxRage { get; set; }
    float blockChance { get; set; }

    // Use this for initialization
    protected override void Start()
    {
        SetBaseStats("Warrior", 100, 150, 5, 5.0f, 5.0f, CLASS_SPECIFIC_TYPE.WARRIOR);
        SetResistances(.1f, .1f, .1f, .1f, .6f, 0f);
        rage = 0;
        maxRage = 100;
        blockChance = .15f;
    }

    // Update is called once per frame
    public override void Update()
    {

    }
}
