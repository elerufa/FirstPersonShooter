using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject impactPrefab; // Effetto per muri e pavimento
    public GameObject bloodPrefab;  // Effetto per i nemici

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
        {
            Debug.Log("Colpito: " + hit.transform.name);
            Vector3 spawnPos = hit.point + (hit.normal * 0.01f);
            Quaternion spawnRotation = Quaternion.LookRotation(hit.normal);

            if (hit.transform.CompareTag("Enemy"))
            {
                // Istanzia il sangue
                GameObject blood = Instantiate(bloodPrefab, spawnPos, spawnRotation);
                Destroy(blood, 2f);

                //nemico di subisce 1 danno
                hit.transform.GetComponent<Enemy>().TakeDamage(1);
            }
            else
            {
                GameObject impact = Instantiate(impactPrefab, spawnPos, spawnRotation);
                Destroy(impact, 2f);
            }
        }
    }
}

