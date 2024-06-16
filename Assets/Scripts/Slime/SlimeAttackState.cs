using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : State
{
    private SlimeController main;
    private Animator anim;

    [SerializeField]
    private float timeBetweenAttacks;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private float damageRadius;

    [SerializeField]
    private LayerMask whatIsDamageable;

    [SerializeField]
    private Transform attackPosition;

    private float timer;

    public override void OnEnterState(Controller controller)
    {
        main = controller as SlimeController;

        timer = timeBetweenAttacks; //Para que ataque nada más alcanzar al jugador.

        //main.SlimeVisual.color = Color.red; //Para ver visualmente cuando cambia de un estado a otro.

        anim = GetComponentInChildren<Animator>();
    }
    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks)
        {
            LaunchAttack();
            timer = 0;
        }
        if (Vector3.Distance(transform.position, main.SlimeTarget.position) > main.AttackRange)
        {
            main.ChangeState(main.ChaseState);
        }
    }

    private void LaunchAttack()
    {
        anim.SetBool("atacando", true);
        Collider2D[] collision = Physics2D.OverlapCircleAll(attackPosition.position, damageRadius, whatIsDamageable);
        foreach (Collider2D item in collision)
        {
            if (item.gameObject.CompareTag("PlayerHitbox"))
            {
                SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
                sistemaVidas.TakeDamage(attackDamage);
            }

        }

    }

    public override void OnExitState()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPosition.position, damageRadius);
    }
}
