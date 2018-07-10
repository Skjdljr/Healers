using UnityEditor;
using UnityEngine;

//namespace Assets.Scripts.Utils
namespace HM_Utils
{
    public enum PLAYER_CLASS { DRUID, PALADIN, PRIEST, SHAMAN }
    public enum SPELL_EFFECT_TYPE { HEALTH, ARMOR, RESOURCE, RESISTANCE, ACTION_SPEED }
    public enum SPELL_TYPE { HEAL, HEALOVERTIME, BUFF, DEBUFF, DAMAGE, AOE_DAMAGE, CROWD_CONTROL }
    public enum SPELL_SCHOOL { BASE, PYHSICAL, POISION, LIGHTNING, FIRE, ICE, WILL }
    public enum CLASS_SPECIFIC_TYPE { ALL, WARRIOR, MAGE, ROGUE }
    public enum PLAYER_STATE { IDLE, CASTING, STUNNED, SILENCED }
   
    public enum SPITE_LOCATIONS{BACKGROUNDS = 0, BORDERS, ENEMIES, GAME_ICON, PARTY_MEMBERS, PLAYER_CHARACTER, RANDOMS, SPELLS }
    
    static class Utils
    {
        //Array of asset paths that our Sprite_Location enum directly indexes into.
        private static string[] AssetPaths = new string[] { 
            "Assets/GameSprites/Backgrounds/",      //Backgrounds 0
            "Assets/GameSprites/Borders/",          //Borders 1
            "Assets/GameSprites/Enemies/",          //Enemies 2
            "Assets/GameSprites/GameIcon/",         //GameIcon 3
            "Assets/GameSprites/PartyMembers/",     //PartyMembers 4
            "Assets/GameSprites/PlayerCharacter/",  //PlayerCharacter 5
            "Assets/GameSprites/Randoms/",          //Randoms 6
            "Assets/GameSprites/Spells/"            //Spells 7
            };

        public static Sprite LoadSprite(string SpriteTextureName, SPITE_LOCATIONS assetLocation)
        {
            string path = CreateSpritePath(SpriteTextureName, assetLocation);
            Debug.unityLogger.Log("path of sprite to load " + path);
            
            return (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
        }

        private static string CreateSpritePath(string textureName, SPITE_LOCATIONS location, string textureExtension = ".png")
        {
            return AssetPaths[(int)location] + textureName + textureExtension;
        }

        public static string ConvertPlayerClassToString(PLAYER_CLASS pc)
        {
            var type = string.Empty;
            switch (pc)
            {
                case PLAYER_CLASS.DRUID:
                    type = "Druid";
                    break;
                case PLAYER_CLASS.PALADIN:
                    type = "Paladin";
                    break;
                case PLAYER_CLASS.PRIEST:
                    type = "Priest";
                    break;
                case PLAYER_CLASS.SHAMAN:
                    type = "Shaman";
                    break;
            }

            return type;
        }
    }
}
