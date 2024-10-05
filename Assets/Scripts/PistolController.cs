using UnityEngine;

public class PistolController : MonoBehaviour
{
    [SerializeField] float reloadTime = 1f;
    [SerializeField] GameObject bulletPref;
    [SerializeField] Transform bulletSpawnPoint;

    private float lastReloadTime = 0;
	private Vector3 offsetSpawn;
	private void Awake()
	{
        if (bulletSpawnPoint) offsetSpawn = bulletSpawnPoint.localPosition;
	}
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			if (!CheckReloadComplete()) return;
			if (bulletPref == null) throw new System.ArgumentException("SELECT BULLET");

            Instantiate(bulletPref, transform.position + offsetSpawn, transform.rotation);
        }
    }
    private bool CheckReloadComplete()
    {
        if (Time.time - lastReloadTime > reloadTime)
        {
            lastReloadTime = Time.time;
			return true;
        }
        return false;
    }
    public void SelectNewBullet(GameObject bulletPref) => this.bulletPref = bulletPref;
}
