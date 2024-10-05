using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : SolidBullet
{
	[SerializeField] float radiusExplosive = 5;
	[SerializeField] GameObject explosionPref;
	public override void HitEnemy(EnemyBehaviour enemy)
	{
		List<EnemyBehaviour> enemies = FindAllEnemies(enemy.transform.position);
		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].GetHit(damage);
		}
		if (explosionPref) Instantiate(explosionPref, enemy.transform.position, Quaternion.identity);
		DestroyMyself();
	}
	private List<EnemyBehaviour> FindAllEnemies(Vector3 position)
	{
		List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();
		Collider[] closestColliders = Physics.OverlapSphere(transform.position, radiusExplosive);
		for (int i = 0; i < closestColliders.Length; i++)
		{
			if (EnemyBehaviour.CheckIsEnemy(closestColliders[i].gameObject, out EnemyBehaviour enemy)) enemies.Add(enemy);
		}
		return enemies;
	}
}