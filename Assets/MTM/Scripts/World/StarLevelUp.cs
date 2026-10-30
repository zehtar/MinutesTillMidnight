using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLevelUp : MonoBehaviour
{
    public int starcount;
    public Sprite fullstar;
    public Sprite emptystar;
    public Image[] stars;
    public StatsSaved st;
    public string nameOfLevelObj;
    public int playerlevel;
    public int gunlevel;

    void Start()
    {
        st = FindObjectOfType<StatsSaved>();
        // level из оттуда сюда
        playerlevel = st.playerlevel;
        gunlevel = st.gunlevel;
    }

    public void Update()
    {
        for(int i = 0; i< stars.Length; i++)
        {
            if(i<starcount) stars[i].sprite = fullstar;
            else stars[i].sprite = emptystar;
            /*
            if(i<numOfHearts) stars[i].enabled = true;
            else stars[i].enabled = false;
            */
        }
    }

    public void LevelUp()
    {
        starcount++;
        switch(nameOfLevelObj)
        {
            case "player" :
            if(starcount <= 5 )
            {
                st.playerhealth++;
                st.playerspeed++;
                st.playerlevel++;
                playerlevel++;
            }
                break;
        
            case "gun" :
            if(starcount <= 5)
            {
                GunLevelUpSwitch();
                st.gunlevel++;
                gunlevel++;
            }
                break;

            default:
                Debug.Log("что то не то в лвл апе " + nameOfLevelObj + "  " + starcount);
                break;
        }
    }

    /*
    st.fireRate -= 0.15f;
    st.shotPerShot++;
    st.bulletdamage -= 0.1f;
    public float bulletspeed = 10;
	public float bulletdamage = 1;
    public int rotateDown = 5;
	public int rotateUp = 5;
    */
    private void GunLevelUpSwitch()
    {
        switch(gunlevel)
        {
            case 0 :
                st.gunfireRate -= 0.2f;
                st.bulletdamage += 0.2f;               
                break;

            case 1 :
                st.bulletdamage += 0.2f;
                st.bulletspeed += 3;
                break;

             default:
                Debug.Log("я дальше не писал лол " + nameOfLevelObj + "  " + gunlevel);
                break;  
        }
        
    }
}
