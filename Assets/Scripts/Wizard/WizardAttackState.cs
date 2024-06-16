using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttackState : State
{
    private WizardController main;
    private Animator anim;

    [SerializeField]
    private float timeBetweenAttacks;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private GameObject fireBallPrefab;

    [SerializeField]
    private Transform fireBallSpawn;

    private float timer;

    public override void OnEnterState(Controller controller)
    {
        main = controller as WizardController;

        timer = timeBetweenAttacks; //Para que ataque nada más alcanzar al jugador.

        //main.WizardVisual.color = Color.red; //Para ver visualmente cuando cambia de un estado a otro.

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
        if (Vector3.Distance(transform.position, main.WizardTarget.position) > main.AttackRange)
        {
            main.ChangeState(main.ChaseState);
        }
    }

    private void LaunchAttack()
    {
        anim.SetTrigger("atacar");
    }

    public override void OnExitState()
    {

    }

    public void LanzarBola()
    { 
        Instantiate(fireBallPrefab, fireBallSpawn.position, transform.rotation);
    }
}
