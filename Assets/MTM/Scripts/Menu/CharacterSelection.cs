using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Image selectionCircle;
    public Sprite selectedSprite;
    public Sprite defaultSprite;

    public Text speedText;
    public Text healthText;
    public Text powerCDText;
    public Text collectRangeText;
    public Text powerDamageText;
    public Text powerDurationText;
    public Text powerSizeText;
    
    public StatsSaved statsSaved;
    public CharacterStats characterStats; // Привязываем через инспектор

    private static CharacterSelection selectedCharacter;
    
    public InformationPlayerUpgrade informationPlayerUpgrade;
    public void Awake()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
        informationPlayerUpgrade = FindAnyObjectByType<InformationPlayerUpgrade>();
    }

    public void SelectCharacter()
    {
        if (selectedCharacter != null && selectedCharacter != this)
        {
            selectedCharacter.DeselectCharacter();
        }

        selectionCircle.sprite = selectedSprite;
        selectedCharacter = this;

        // Обновляем параметры в StatsSaved
        statsSaved.characterName = characterStats.characterName;
        statsSaved.playerhealth = characterStats.health;
        statsSaved.playerspeed = characterStats.speed;
        statsSaved.playerlevel = characterStats.startLevel;
        statsSaved.collectRange = characterStats.collectRange;
        statsSaved.regenerationRate = characterStats.regenerationRate;
        // скилы
        statsSaved.powersDamage = characterStats.powersDamage;
        statsSaved.powersSize =  characterStats.powersSize;
        statsSaved.powersDuration  = characterStats.powersDuration;

        UpdateCharacterStatsUI();
        informationPlayerUpgrade.SelectInfoPanel();
    }

    public void DeselectCharacter()
    {
        selectionCircle.sprite = defaultSprite;
    }

    void UpdateCharacterStatsUI()
    {
        if (speedText.text != statsSaved.playerspeed.ToString())
        {
            speedText.text = "" + statsSaved.playerspeed;
            speedText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (healthText.text != statsSaved.playerhealth.ToString())
        {
            healthText.text = "" + statsSaved.playerhealth;
            healthText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (powerCDText.text != characterStats.powerCD.ToString())
        {
            powerCDText.text = "" + characterStats.powerCD;
            powerCDText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (collectRangeText.text != statsSaved.collectRange.ToString())
        {
            collectRangeText.text = "" + statsSaved.collectRange;
            collectRangeText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (powerDamageText.text != statsSaved.powersDamage.ToString())
        {
            powerDamageText.text = "" + statsSaved.powersDamage;
            powerDamageText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (powerDurationText.text != statsSaved.powersDuration.ToString())
        {
            powerDurationText.text = "" + statsSaved.powersDuration;
            powerDurationText.GetComponent<Animator>().SetTrigger("updateText");
        }
        
        if (powerSizeText.text != statsSaved.powersSize.ToString())
        {
            powerSizeText.text = "" + statsSaved.powersSize;
            powerSizeText.GetComponent<Animator>().SetTrigger("updateText");
        }
    }
}
