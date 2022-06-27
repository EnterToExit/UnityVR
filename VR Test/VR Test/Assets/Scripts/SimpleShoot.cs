using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleShoot : MonoBehaviour
{
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        
    }

    void Update()
    {
        // Левая кнопка мыши
        if (Input.GetButtonDown("Fire1"))
        {
            // Вызывает анимацию на оружии, имеющем соответствующие события анимации, чтобы сделать вытрел
            gunAnimator.SetTrigger("Fire");
        }
    }

    
    // Эта функция создает поведение пули
    void Shoot()
    {
        
        if (muzzleFlashPrefab)
        {
            // Создание эффекта
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // Уничтожение эффекта
            Destroy(tempFlash, destroyTimer);
        }

        // Отменяется, если нет префаба пули
        if (!bulletPrefab)
        { return; }

        // Создаём пулю и добавеяем к ней силу в направлении ствола
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        if (source != null)
        {
            
            if (source.transform.IsChildOf(transform))
            {
                source.Play();
            }
            else
            {
                AudioSource newAS = Instantiate(source);
                if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                {
                    newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                    newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);
                    
                    newAS.PlayOneShot(GunShotClip);
                   
                    Destroy(newAS.gameObject, 4);
                }
            }
        }
    }

    // Эта функция создает кожух в прорези выброса
    void CasingRelease()
    {
        // Отменяет функцию, если прорезь для выброса не установлена или отсутствует гильза 
        if (!casingExitLocation || !casingPrefab)
        { return; }

        // Создание корпуса
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        // Выталкивание снаряда
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);

        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        // Уничтожить кожух через X секунд
        Destroy(tempCasing, destroyTimer);
    }
    

}
