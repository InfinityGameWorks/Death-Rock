using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunBarrel; // Titik keluar tembakan
    public GameObject bulletPrefab; // Prefab peluru
    public float bulletSpeed = 10f; // Kecepatan peluru
    public float fireRate = 0.5f; // Waktu antara setiap tembakan

    private float nextFireTime; // Waktu kapan bisa menembak selanjutnya

    private void Update()
    {
        // Menembak jika tombol fire ditekan dan cooldown tembakan sudah berlalu
        
    }


    public void Shoot(InputAction.CallbackContext context)
    {
            if (context.performed)
            {
                Debug.Log("Shoottt");
                // Membuat peluru dari prefab
                GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);

                // Memberikan kecepatan pada peluru
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.velocity = gunBarrel.forward * bulletSpeed;

                // Menghancurkan peluru setelah beberapa detik
                Destroy(bullet, 2f);
            }
        if (Time.time >= nextFireTime)
        {
            
            nextFireTime = Time.time + 1f / fireRate; // Mengatur waktu untuk tembakan selanjutnya
        }
    }
}
