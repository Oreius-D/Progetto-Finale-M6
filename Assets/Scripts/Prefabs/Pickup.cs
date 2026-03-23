using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // Weapon prefab to give to the player
    [SerializeField] private Transform attachPoint;   // Optional: attachment point (defaults to player if null)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to the player
        if (!other.CompareTag("Player")) return;

        // Determine where the weapon should be attached
        Transform parent = attachPoint != null ? attachPoint : other.transform;

        // Remove already equipped weapon
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        // Instantiate and attach the weapon without inheriting unwanted transforms
        GameObject weaponInstance = Instantiate(weaponPrefab);
        weaponInstance.transform.SetParent(parent, false); // false = keep clean local transforms
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;

        // Remove "(Clone)" from the instance name
        weaponInstance.name = weaponPrefab.name;

        // Destroy the pickup object after being collected
        Destroy(gameObject);
    }
}