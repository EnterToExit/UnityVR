using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GunShoot : MonoBehaviour {
	//public int AmmoCount; // Патронов в обоймах
	//public int CurAmmo; // Кол-во патронов
	//public int Ammo; // Кол-во патронов в 1ой обойме
	public AudioClip Fire; // Звук выстрела
	public float ShootSpeed; // Скорострельность
	public float ReloadSpeed; // Скорость Перезарядки
	public AudioClip reload; // Звук перезарядки
	public float ReloadTimer = 0.0f; // Время перезарядки
	public float ShootTimer = 0.0f; // Время выстрела
	public Transform bullet; // Патрон
	//public AudioSource source;
	public int maxammo = 30;
	private int currentammo;
	//public Text textMy;

	void Reload()
	{
		currentammo = maxammo;
		GetComponent<AudioSource>().PlayOneShot(reload);
		
	}

	void Update()
	{
		if (Input.GetMouseButton(0) & maxammo > 0 & ReloadTimer <= 0 & ShootTimer <= 0)
		{
			Transform BulletInstance = (Transform)Instantiate(bullet, GameObject.Find("Spawn").transform.position, Quaternion.identity);
			BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 5000f);
			maxammo = maxammo - 1;
			GetComponent<AudioSource>().PlayOneShot(Fire);
			ShootTimer = ShootSpeed;
		}
		if (ShootTimer > 0)
		{
			ShootTimer -= Time.deltaTime;
		}

		if (Vector3.Angle(transform.up, Vector3.up) > 100 && currentammo < maxammo)
			Reload();
		//textMy.text = currentammo.ToString();

		if (ShootTimer > 0)
		{
			ShootTimer -= Time.deltaTime;
		}



		if (ReloadTimer > 0)
		{
			ReloadTimer -= Time.deltaTime;
		}
	}
}