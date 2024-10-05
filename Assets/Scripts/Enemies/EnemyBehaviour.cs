using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	private Action<float> hitAction;
	public void GetHit(float damage) => hitAction?.Invoke(damage);
	public static bool CheckIsEnemy(GameObject someObject, out EnemyBehaviour enemyBehaviour) => someObject.TryGetComponent(out enemyBehaviour);
	public void SubscribeActionHit(Action<float> action) => hitAction += action;
	public void UnsubscribeActionHit(Action<float> action) => hitAction -= action;
}
