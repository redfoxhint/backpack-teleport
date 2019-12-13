using BackpackTeleport.Character;
using UnityEngine;
using DG.Tweening;
using System;

[System.Serializable]
public class GhostingEffect
{
	// Private Variables
	[SerializeField] private GameObject owner;
	private GameObject ghostParent;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private Color trailColor;
	[SerializeField] private Color fadeColor;
	[SerializeField] private float ghostInterval;
	[SerializeField] private float fadeTime;

	public GhostingEffect(GameObject owner, Color trailColor, Color fadeColor, float ghostInterval, float fadeTime)
	{
		this.owner = owner;
		spriteRenderer = owner.GetComponent<SpriteRenderer>();

		this.trailColor = trailColor;
		this.fadeColor = fadeColor;
		this.ghostInterval = ghostInterval;
		this.fadeTime = fadeTime;

		Init();
	}
	private void Init()
	{
		ghostParent = new GameObject("GhostParent");
		ghostParent.SetActive(false);
		ghostParent.transform.parent = GameObject.Find("_Dynamic").transform;
		ghostParent.transform.position = Vector3.zero;

		for (int i = 0; i < 5; i++)
		{
			GameObject ghostCopy = new GameObject("GhostCopy");
			ghostCopy.transform.parent = ghostParent.transform;
			ghostCopy.transform.localPosition = Vector3.zero;

			SpriteRenderer spr = ghostCopy.AddComponent<SpriteRenderer>();
			spr.sortingLayerName = "Player";
			spr.sortingOrder = 1;
		}
	}

	public void ShowGhost()
	{
		ghostParent.SetActive(true);

		Sequence s = DOTween.Sequence();

		for (int i = 0; i < ghostParent.transform.childCount; i++)
		{
			Transform currentGhost = ghostParent.transform.GetChild(i);
			SpriteRenderer ghostRenderer = currentGhost.GetComponent<SpriteRenderer>();

			s.AppendCallback(() => currentGhost.position = owner.transform.position);
			s.AppendCallback(() => ghostRenderer.sprite = spriteRenderer.sprite);
			s.Append(ghostRenderer.material.DOColor(trailColor, 0));
			s.AppendCallback(() => FadeSprite(ghostRenderer));
			s.AppendInterval(ghostInterval);
			s.onComplete += delegate { OnEffectComplete(); };
		}
	}

	private void FadeSprite(SpriteRenderer current)
	{
		current.material.DOKill();
		current.material.DOColor(fadeColor, fadeTime);
	}

	private void OnEffectComplete()
	{
		ghostParent.SetActive(false);
	}
}
