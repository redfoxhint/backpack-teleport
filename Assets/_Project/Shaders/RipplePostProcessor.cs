﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
	public Material rippleMaterial;
	public float MaxAmount = 50f;

	[Range(0, 1)]
	public float Friction = .9f;

	private float Amount = 0f;

	void Update()
	{
		this.rippleMaterial.SetFloat("_Amount", this.Amount);
		this.Amount *= this.Friction;
	}

	public void RippleAt(Vector3 pos)
	{
		this.Amount = this.MaxAmount;
		this.rippleMaterial.SetFloat("_CenterX", pos.x);
		this.rippleMaterial.SetFloat("_CenterY", pos.y);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, this.rippleMaterial);
	}
}
