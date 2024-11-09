using UnityEngine;

public class shootingWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    // Update is called once per frame
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation );
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}
