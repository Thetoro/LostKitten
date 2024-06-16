using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// En esta clase se definen los datos compartidos entre estados y los estados.
/// </summary>
public class BatController : Controller
{

    [SerializeField]
    private float attackRange;  //Dato compartido por varios estados: Attack y Chase.

    [SerializeField]
    private SpriteRenderer batVisual;

    private Transform batTarget;  //Datos compartido por varios estados: Patrol y Chase.



    //Se definen los estados que va a tener el enemigo:
    private BatPatrolState patrolState;
    private BatChaseState chaseState;
    private BatAttackState attackState;


    #region getters & setters
    public BatPatrolState PatrolState { get => patrolState;}
    public BatChaseState ChaseState { get => chaseState;}
    public BatAttackState AttackState { get => attackState;}
    public Transform BatTarget { get => batTarget; set => batTarget = value; }
    public float AttackRange { get => attackRange;}
    public SpriteRenderer BatVisual { get => batVisual;}
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
        patrolState = GetComponent<BatPatrolState>();
        chaseState = GetComponent<BatChaseState>();
        attackState = GetComponent<BatAttackState>();
    }

    protected override void Update()
    {
        base.Update();
    }
}
