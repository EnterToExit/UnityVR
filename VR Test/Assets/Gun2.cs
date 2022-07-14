using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(XRGrabInteractable))]
public class Gun2 : MonoBehaviour
{
    public int AmmoCount; // �������� � �������
    public int CurAmmo; // ���-�� ��������
    public int Ammo; // ���-�� �������� � 1�� ������
    public AudioClip Fire; // ���� ��������
    public float ShootSpeed; // ����������������
    public float ReloadSpeed; // �������� �����������
    public AudioClip Reload; // ���� �����������
    public float ReloadTimer = 0.0f; // ����� �����������
    public float ShootTimer = 0.0f; // ����� ��������
    public Transform bullet; // ������

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
