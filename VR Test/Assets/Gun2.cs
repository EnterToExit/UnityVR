using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(XRGrabInteractable))]
public class Gun2 : MonoBehaviour
{
    public int AmmoCount; // Патронов в обоймах
    public int CurAmmo; // Кол-во патронов
    public int Ammo; // Кол-во патронов в 1ой обойме
    public AudioClip Fire; // Звук выстрела
    public float ShootSpeed; // Скорострельность
    public float ReloadSpeed; // Скорость Перезарядки
    public AudioClip Reload; // Звук перезарядки
    public float ReloadTimer = 0.0f; // Время перезарядки
    public float ShootTimer = 0.0f; // Время выстрела
    public Transform bullet; // Патрон

    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null)
            return;

        _grabInteractable.activated.AddListener(GunActivated);
        _grabInteractable.deactivated.AddListener(GunDectivated);
    }

    private void OnDisable()
    {
        if (_grabInteractable == null)
            return;

        _grabInteractable.activated.RemoveListener(GunActivated);
        _grabInteractable.deactivated.RemoveListener(GunDectivated);
    }

    private void GunActivated(ActivateEventArgs activateEventArgs)
    {
        Debug.Log("Start shoot");
        if (Input.GetMouseButton(0) & CurAmmo > 0 & ReloadTimer <= 0 & ShootTimer <= 0)
        {
            Transform BulletInstance = (Transform)Instantiate(bullet, GameObject.Find("Spawn").transform.position, Quaternion.identity);
            BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
            CurAmmo = CurAmmo - 1;
            GetComponent<AudioSource>().PlayOneShot(Fire);
            ShootTimer = ShootSpeed;
        }
        if (ShootTimer > 0)
        {
            ShootTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadTimer = ReloadSpeed;
            CurAmmo = Ammo;
            GetComponent<AudioSource>().PlayOneShot(Reload);
            {
                if (ShootTimer > 0)
                {
                    ShootTimer -= Time.deltaTime;
                }
            }
        }

        if (ReloadTimer > 0)
        {
            ReloadTimer -= Time.deltaTime;
        }
    }




    private void GunDectivated(DeactivateEventArgs activateEventArgs)
    {
        Debug.Log("Stop shoot");

        
    }

    
}
