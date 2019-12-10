using BackpackTeleport.Character.Enemy;
using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float aimAngle;
    [SerializeField] private GameObject cameraMount;
    [SerializeField] private GameObject arrow;
    [SerializeField] private AttackDirection attack;

    Vector2 attackDir;

    private void Awake()
    {
        attack.onColliderHit = i => Damage(i);
    }

    public void Attack(Player player, Animator anim, Vector2 vel)
    {
        anim.SetTrigger("Attack");

        Vector2 arrowPos = Camera.main.WorldToScreenPoint(player.transform.position);
        arrowPos = (Vector2)Input.mousePosition - arrowPos;

        aimAngle = Mathf.Atan2(arrowPos.y, arrowPos.x) * Mathf.Rad2Deg;
        Debug.Log(aimAngle);

        arrowPos = Quaternion.AngleAxis(aimAngle, Vector3.forward) * (Vector2.right * 1.5f);
        arrow.transform.position = (Vector2)player.transform.position + arrowPos;
        arrow.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

        Utils.ShakeCameraPosition(Camera.main.transform, 0.6f, 0.08f, 15, 0f, false);

        attackDir = Utils.GetVectorFromAngle(aimAngle);

        Vector2 attackDirRounded = new Vector2(Mathf.RoundToInt(attackDir.x), Mathf.RoundToInt(attackDir.y));
        player.HandleMovementDirection(attackDirRounded);
    }

    private void Damage(GameObject other)
    {
        other.GetComponent<IDamageable>().TakeDamage(1f, attackDir * 2f);
    }
}
