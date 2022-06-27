using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour {
	public int AmmoCount; // Патронов в обоймах
	public int CurAmmo; // Кол-во патроеов
	public int Ammo; // Кол-во патронов в 1ой обойме
	public AudioClip Fire; // Звук выстрела
	public float ShootSpeed; // Скорострельность
	public float ReloadSpeed; // Скорость Перезарядки
	public AudioClip Reload; // Звук перезарядки
	public float ReloadTimer = 0.0f; // Время перезарядки
	public float ShootTimer = 0.0f; // Время выстрела
	public Transform bullet; // Патрон
	
	void Update () 
	{
		if(Input.GetMouseButton(0)& CurAmmo>0 &ReloadTimer<=0 &ShootTimer<=0) 
		{ 
			Transform BulletInstance = (Transform) Instantiate(bullet, GameObject.Find("Spawn").transform.position, Quaternion.identity); 
			BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 5000); 
			CurAmmo = CurAmmo - 1; 
			GetComponent<AudioSource>().PlayOneShot (Fire); 
			ShootTimer = ShootSpeed; 
		} 
		if(ShootTimer>0) 
		{ 
			ShootTimer -= Time.deltaTime; 
		} 
     
		if(Input.GetKeyDown(KeyCode.R)) 
		{ 
			ReloadTimer = ReloadSpeed;   
			CurAmmo = Ammo; 
			GetComponent<AudioSource>().PlayOneShot(Reload); 
		{ 
		if(ShootTimer>0) 
		{ 
			ShootTimer -= Time.deltaTime; 
		} 
	} 
} 
   
	if(ReloadTimer>0) 
	{ 
	ReloadTimer -= Time.deltaTime; 
	} 
}
}