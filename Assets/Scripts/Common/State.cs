using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{

    //Todo estado tendr�Etres fases: Entrada, actualizaci�n y salida.
    public abstract void OnEnterState(Controller controller);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
}
