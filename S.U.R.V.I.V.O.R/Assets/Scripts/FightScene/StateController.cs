using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static Dictionary<FightState, bool> AvailablePhase = new Dictionary<FightState, bool>();

    public void SwitchMovePhase()
    {
        if(CanChangePhase() && AvailablePhase[FightState.MovePhase])
        {
            if(SceneController.State == FightState.MovePhase)
            {
                SceneController.State = FightState.Sleeping;
                Debug.Log("Cancel Move Phase");
            }
            else
            {
                SceneController.State = FightState.MovePhase;
                Debug.Log("Move Phase");
            }
        }
    }

    public void SwitchShootPhase()
    {
        if(CanChangePhase() && AvailablePhase[FightState.ShootPhase])
        {
            if(SceneController.State == FightState.ShootPhase)
            {
                SceneController.State = FightState.Sleeping;
                Debug.Log("Cancel Shoot Phase");
            }
            else
            {
                SceneController.State = FightState.ShootPhase;
                Debug.Log("Shoot Phase");
            }
        }
    }

    public void SwitchFightPhase()
    {
        if(CanChangePhase() && AvailablePhase[FightState.FightPhase])
        {
            if(SceneController.State == FightState.FightPhase)
            {
                SceneController.State = FightState.Sleeping;
                Debug.Log("Cancel Fight Phase");
            }
            else
            {
                SceneController.State = FightState.FightPhase;
                Debug.Log("Fight Phase");
            }
        }
    }

    public void EndPhaseOn()
    {
        if(CanChangePhase())
        {
            SceneController.State = FightState.EndTurnPhase;
            Debug.Log("End Phase");
        }
    }

    public static void MakeAvailablePhases()
    {
        AvailablePhase[FightState.MovePhase] = true;
        AvailablePhase[FightState.ShootPhase] = true;
        AvailablePhase[FightState.FightPhase] = true;
    }

    public static bool CanChangePhase()
    {
        return SceneController.State == FightState.Sleeping
            || SceneController.State == FightState.ShootPhase
            || SceneController.State == FightState.MovePhase
            || SceneController.State == FightState.FightPhase;
    }
}
