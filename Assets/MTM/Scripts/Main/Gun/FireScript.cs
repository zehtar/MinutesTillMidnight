using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
	public List<string> activeEffects;
	
  	public bool canfire = true;
	//public GameObject bullet; // префаб нашей пули
	public Transform gunPoint; // Точка отсчета
	public GameObject gunPointPrefab;//префаб точки выстрела
	public List<Transform> gunPointList = new List<Transform>();
	public int shotPerShot = 1;

	public StatsSaved st;//обьект с базовыми статами

	public float fireRate = 1;
	public float curTimeout; // скорострельность
	public float baseCurTimeOut;
	public float spread = 10f;
	public WeaponPassivesActive wp;
	public FireButton fb;
	public FireJpi fbJoi;

	public int bulletHave;
	public int bulletMax;
	public Text reloadText;
	public GameObject reloadLine;
	public float shotCounter;
	public bool isReloading;

	public Animator ringAnimator; // Ссылка на Animator компонента с кольцами
	public GameObject muzzleFlashPrefab;
	
	public BladeEffect bladeEffect;
	public EffectsStatsController effectsStatsController;
	
	public void Start()
	{
		//gunPointList.Add(gunPoint.gameObject);
		//activeEffects.Add("LightingChain");
		st = FindObjectOfType<StatsSaved>();
		wp = FindObjectOfType<WeaponPassivesActive>();
		bladeEffect =  FindObjectOfType<BladeEffect>();
		effectsStatsController =  FindObjectOfType<EffectsStatsController>();
		
		spread = st.spread;
		baseCurTimeOut = st.gunfireRate;
		curTimeout = baseCurTimeOut;
		shotPerShot = st.gunshotPerShot;
		ShotCountCheck();
		bulletMax = st.bulletsInMagazine;
		bulletHave = bulletMax;
	}

	public void ShotCountCheck()
	{
		if(shotPerShot > gunPointList.Count)
		{
			for(int i = gunPointList.Count;i<shotPerShot;i++)
			{
				GameObject thisGunPoint = Instantiate(gunPointPrefab , gunPoint.position , Quaternion.identity);
				thisGunPoint.transform.SetParent(gunPoint);
				gunPointList.Add(thisGunPoint.transform);
			}	
		}
		GunPointRotate();
	}

	public void BulletTrue()
	{	
		shotCounter++;
		if (effectsStatsController.fireboll && shotCounter % 5 == 0)
		{
			Instantiate(effectsStatsController.fireBollPrefab, gunPoint.position,  Quaternion.Euler(0, 0, Random.Range(0, 360f)));
		}

		for(int i = 0; i < gunPointList.Count;i++)
		{
			GameObject bullet = BulletPoulManager.SharedInstance.GetPooledObject(activeEffects); 
  			if (bullet != null) 
			{
    			bullet.transform.position = gunPointList[i].transform.position;
    			bullet.transform.rotation = gunPointList[i].transform.rotation;
    			bullet.SetActive(true);
  			}
		}
		bulletHave = bulletHave - 1;
	}

	public void BigBulletTrue()
	{	
		GameObject bullet = BulletPoulManager.SharedInstance.GetBigPooledObject(activeEffects); 
  		if (bullet != null) 
		{
    		bullet.transform.position = gunPoint.transform.position;
    		bullet.transform.rotation = gunPoint.transform.rotation;
    		bullet.SetActive(true);
  		}
	}

	public void GunPointRotate()
	{
		GunPointUnRotate();
		int midIndex = gunPointList.Count / 2; // Индекс центральной точки
		for (int i = 0; i < gunPointList.Count; i++)
		{
			float angle;
			if(gunPointList.Count == 1)
			{
				angle = Random.Range(-spread / 2, spread / 2);
			}
			else if (gunPointList.Count % 2 == 1) // Нечётное количество точек
			{
				angle = (i - midIndex) * spread; // Центральная точка = 0, остальные симметрично
			}
			else // Чётное количество точек
			{
				angle = (i - midIndex + 0.5f) * spread; // Сдвиг на половину для чётных
			}

			gunPointList[i].localRotation = Quaternion.Euler(0, 0, angle); // Устанавливаем локальный угол
		}
	}


	public void GunPointUnRotate()
	{
		for(int i = 0 ; i < gunPointList.Count;i++)
		{
			gunPointList[i].localRotation = Quaternion.identity;
		}
	}

	void Update()
	{
		if (bulletMax <= 0)
		{
			bulletMax = 1;
		}

		if(wp.bigAmmo && shotCounter > 3)
		{
			ringAnimator.SetTrigger("Pulse");
        	GameObject flash = Instantiate(muzzleFlashPrefab, gunPoint.position, gunPoint.rotation);
			ShotCountCheck();
			BigBulletTrue();
			shotCounter = 0;
		}

		if(bulletHave <= 0 && !isReloading)
		{
			isReloading = true;
			reloadLine.gameObject.SetActive(true);
			reloadLine.GetComponent<ReloadMove>().ReloadStart();
			curTimeout = baseCurTimeOut;	
			shotCounter = 0;
		}

		if(fireRate <= 0 && canfire)
		{
			if(fb.isHolding || fbJoi.isHolding)
			{
				FireButton();
			}

			if(st.controlType == ControlType.PC && Input.GetMouseButton(0))
			{
				FireButton();	
			}
		}
		else
		{
			fireRate -= Time.deltaTime;
		}

		if (bulletHave < 0)
		{
			bulletHave = 0;
		}
		reloadText.text = bulletHave.ToString() + " / " + bulletMax.ToString();
	}

	public void FireButton()
	{
		if(bulletHave > 0)
		{
			AudioManager.Instance.PlayShoot();
			FindObjectOfType<CameraMovement>().Shake(0.3f, 2f); 
        	GameObject flash = Instantiate(muzzleFlashPrefab, gunPoint.position, gunPoint.rotation);
			ShotCountCheck();
			BulletTrue();
			fireRate = curTimeout;		
			if(wp.fireSpeeder)
			{				
				curTimeout = baseCurTimeOut - (shotCounter / 150);
			}
			if(curTimeout <= 0)
			{
				curTimeout = 0.01f;
			}			
		}	
	}

	public void puShotPerShot(int addShot = 1)
	{
		shotPerShot += addShot;
	}

	public void puFireSpeedBuff(float percent)
	{
		baseCurTimeOut *= 1 - (percent / 100f);
	}

	public void puBulletMaxBuff(int i)
	{
		bulletMax += i;
	}

	public void puSpreadBuff(float percent)
	{
		spread *= 1 - (percent / 100f);
	}

}
