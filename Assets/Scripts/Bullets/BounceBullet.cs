using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : SolidBullet
{
	[SerializeField] float radiusBounce = 5;
	[SerializeField] int bounceCount = 3;
	public override void HitEnemy(EnemyBehaviour enemy)
	{
		enemy.GetHit(damage);
		if (!TryBounce(enemy)) DestroyMyself();
	}
	private bool TryBounce(EnemyBehaviour lastTarget)
	{
		if (bounceCount == 0) return false;
		bounceCount--;
		if (TryFindNextTarget(lastTarget, out GameObject enemy))
		{
			transform.LookAt(enemy.transform.position);
			return true;
		}
		return false;
	}
	private bool TryFindNextTarget(EnemyBehaviour lastEnemy, out GameObject enemy)
	{
		enemy = null;
		Collider[] closestColliders = Physics.OverlapSphere(transform.position, radiusBounce);
		List<EnemyBehaviour> closestEnemies = new List<EnemyBehaviour>();
		for (int i = 0; i < closestColliders.Length; i++)
		{
			if (EnemyBehaviour.CheckIsEnemy(closestColliders[i].gameObject, out EnemyBehaviour nextEnemy) && nextEnemy != lastEnemy) closestEnemies.Add(nextEnemy);
		}
		if (closestEnemies.Count > 0) enemy = closestEnemies[Random.Range(0, closestEnemies.Count)].gameObject;
		return enemy != null;
	}
}
