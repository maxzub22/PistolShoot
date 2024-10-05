using System.Collections;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
	[SerializeField] float speeedChangeColor;
	[SerializeField] Color hitColor;
	[SerializeField] Renderer myRenderer;
	private EnemyBehaviour myBehaviour;
	private Color startColor;
	private bool isAnimationActive = false;
	private void Awake()
	{
		startColor = myRenderer.material.color;
		if (TryGetComponent(out myBehaviour))
		{
			myBehaviour.SubscribeActionHit(AnimationHit);
		}
	}
	private void OnDestroy()
	{
		//According to current task, this object will not destroy ever.
		//But if in future these objects will be dynamically spawn and destroyed, and we forgot to unsubscribe from event, it will be memory leak.
		if (myBehaviour) GetComponent<EnemyBehaviour>().UnsubscribeActionHit(AnimationHit);
	}
	public void AnimationHit(float time)
	{
		if (isAnimationActive) return;
		StartCoroutine(AnimationChangeColor(time));
	}
	IEnumerator AnimationChangeColor(float time)
	{
		isAnimationActive = true;
		float timer = 0;
		do
		{
			timer += Time.deltaTime;
			myRenderer.material.color = Color.Lerp(startColor, hitColor, timer / speeedChangeColor);
			yield return null;
		} while (timer < speeedChangeColor);
		timer = 0;
		yield return new WaitForSeconds(time);
		do
		{
			timer += Time.deltaTime;
			myRenderer.material.color = Color.Lerp(hitColor, startColor, timer / speeedChangeColor);
			yield return null;
		} while (timer < speeedChangeColor);
		isAnimationActive = false;
		yield break;
	}
}
