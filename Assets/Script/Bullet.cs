using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Kerusakan yang disebabkan oleh peluru

    private void OnTriggerEnter(Collider other)
    {
        // Deteksi saat peluru menyentuh objek lain dengan collider
        if (other.CompareTag("Enemy"))
        {
            // Hancurkan peluru setelah menyentuh musuh
            Destroy(gameObject);
        }
    }
}
