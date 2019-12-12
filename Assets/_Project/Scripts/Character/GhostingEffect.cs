using BackpackTeleport.Character;
using UnityEngine;
using DG.Tweening;

public class GhostingEffect : MonoBehaviour
{
	[Header("Ghosting Effect Setup")]
	[SerializeField] private Transform ghostParent;
	[SerializeField] private Color trailColor;
	[SerializeField] private Color fadeColor;
	[SerializeField] private float ghostInterval;
	[SerializeField] private float fadeTime;


	// Components
	private PlayerMovement characterMovement;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		characterMovement = FindObjectOfType<PlayerMovement>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void ShowGhost()
	{
		Sequence s = DOTween.Sequence();

		for (int i = 0; i < ghostParent.childCount; i++)
		{
			Transform currentGhost = ghostParent.GetChild(i);
			s.AppendCallback(() => currentGhost.position = characterMovement.transform.position);
			s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = characterMovement.GetComponent<SpriteRenderer>().sprite);
			s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
			s.AppendCallback(() => FadeSprite(currentGhost));
			s.AppendInterval(ghostInterval);
		}
	}

	public void FadeSprite(Transform current)
	{
		current.GetComponent<SpriteRenderer>().material.DOKill();
		current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
	}
}
