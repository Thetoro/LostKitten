using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : Controller
{
    [SerializeField]
    private float attackRange;  //Dato compartido por varios estados: Attack y Chase.

    [SerializeField]
    private SpriteRenderer slimeVisual;

    private Transform slimeTarget;  //Datos compartido por varios estados: Patrol y Chase.



    //Se definen los estados que va a tener el enemigo:
    private SlimePatrolState patrolState;
    private SlimeChaseState chaseState;
    private SlimeAttackState attackState;


    #region getters & setters
    public SlimePatrolState PatrolState { get => patrolState; }
    public SlimeChaseState ChaseState { get => chaseState; }
    public SlimeAttackState AttackState { get => attackState; }
    public Transform SlimeTarget { get => slimeTarget; set => slimeTarget = value; }
    public float AttackRange { get => attackRange; }
    public SpriteRenderer SlimeVisual { get => slimeVisual; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitStates();

        ChangeState(patrolState);
    }


    //Inicializa los estados pasando como parámetro el controlador al que pertenecen.
    private void InitStates()
    {
        patrolState = GetComponent<SlimePatrolState>();
        chaseState = GetComponent<SlimeChaseState>();
        attackState = GetComponent<SlimeAttackState>();
    }

    protected override void Update()
    {
        base.Update();
    }
}
