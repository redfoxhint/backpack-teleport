﻿using BackpackTeleport.Character;
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
	[SerializeField] private int ghostAmount;

	public GhostingEffect(GameObject owner, Color trailColor, Color fadeColor, float ghostInterval, float fadeTime, int ghostAmount)
	{
		this.owner = owner;
		spriteRenderer = owner.GetComponent<SpriteRenderer>();

		this.trailColor = trailColor;
		this.fadeColor = fadeColor;
		this.ghostInterval = ghostInterval;
		this.fadeTime = fadeTime;
		this.ghostAmount = ghostAmount;

		if(this.ghostAmount < 3)
		{
			this.ghostAmount = 3;
		}

		Init();
	}
	private void Init()
	{
		ghostParent = new GameObject("GhostParent");
		ghostParent.SetActive(false);
		ghostParent.transform.parent = GameObject.Find("_Dynamic").transform;
		ghostParent.transform.position = Vector3.zero;

		for (int i = 0; i < ghostAmount; i++)
		{
			GameObject ghostCopy = new GameObject("GhostCopy");
			ghostCopy.transform.parent = ghostParent.transform;
			ghostCopy.transform.localPosition = Vector3.zero;

			SpriteRenderer spr = ghostCopy.AddComponent<SpriteRenderer>();
			Material newMat = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));
			spr.material = newMat;
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
			s.Append(ghostRenderer.DOColor(trailColor, 0));
			//s.Append(ghostRenderer.material.DOColor(trailColor, 0));
			s.AppendCallback(() => FadeSprite(ghostRenderer));
			s.AppendInterval(ghostInterval);
			s.onComplete += delegate { OnEffectComplete(); };
		}
	}

	private void FadeSprite(SpriteRenderer current)
	{
		current.material.DOKill();
		current.DOColor(fadeColor, fadeTime);
	}

	private void OnEffectComplete()
	{
		GameObject.Destroy(ghostParent);
		//ghostParent.SetActive(false);
	}

	public static GhostingEffect CreateEffect(GameObject _owner, Color _trailColor, Color _fadeColor, float _ghostInterval, float _fadeTime, int _ghostAmount)
	{
		return new GhostingEffect(_owner, _trailColor, _fadeColor, _ghostInterval, _fadeTime, _ghostAmount);
	}
}
