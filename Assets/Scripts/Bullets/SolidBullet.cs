using UnityEngine;

public class SolidBullet : MonoBehaviour
{
	[SerializeField] protected float damage = 0.5f;
	[SerializeField] protected float speed = 5f;
	protected float timeTrail = 0;
	protected virtual void Awake()
	{
		if (TryGetComponent(out TrailRenderer trailComponent)) timeTrail = trailComponent.time;
	}
	protected virtual void FixedUpdate()
	{
		transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.Self);
	}
	protected virtual void OnTriggerEnter(Collider other)
	{
		if (EnemyBehaviour.CheckIsEnemy(other.gameObject, out EnemyBehaviour enemy)) HitEnemy(enemy);
	}
	public virtual void HitEnemy(EnemyBehaviour enemy)
	{
		enemy.GetHit(damage);
		DestroyMyself();
	}
	protected void DestroyMyself()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
		Destroy(transform.gameObject, timeTrail);
		enabled = false;
	}
}