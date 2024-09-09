using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 250; 
    public int currenAmmo = 10; 
    public int totalAmmo; 
    public float reload = 1.5f;
    private bool isReloading = false;

    public Camera fpsCam;
    public GameObject impact;
    public Animator animator;

    public Text ammoText; 
    public Text maxAmmoText;
    private float fireTimeToRate = 0f;

    public int ammoPerReload = 15;

    public AudioSource shootsSound;
    public AudioSource reloadSound;
   



    void Start()
    {

        if (currenAmmo == 0)
        {
            Reload();
        }

        totalAmmo = maxAmmo; 
        UpdateAmmoText(); 
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currenAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("fire1") && Time.time >= fireTimeToRate)
        {
            fireTimeToRate = Time.time + 1f / fireRate;
            Shoot();
            UpdateAmmoText(); 
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading..");
       
        
            reloadSound.Play();
        
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reload - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        int ammoToAdd = Mathf.Min(ammoPerReload, totalAmmo - currenAmmo);
        totalAmmo -= ammoToAdd;
        currenAmmo += ammoToAdd;

        UpdateAmmoText(); 
        isReloading = false;
    }

    void Shoot()
    {
        animator.SetBool("Fire", true);
    
            shootsSound.Play();
       
        currenAmmo--;
        UpdateAmmoText();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name); 
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                
                target.TakeDamage(damage);
                animator.SetBool("Fire", true);

            }

            if (impact != null)
            {
                GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

                Destroy(impactGO, 2f);
            }
        }
    }

    public void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currenAmmo} / {totalAmmo}";
            maxAmmoText.text = $"{totalAmmo}";

        }
    }
}
