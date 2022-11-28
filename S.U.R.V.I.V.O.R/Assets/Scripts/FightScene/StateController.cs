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
            if(FightSceneController.State == FightState.MovePhase)
            {
                FightSceneController.State = FightState.Sleeping;
                Debug.Log("Cancel Move Phase");
            }
            else
            {
                FightSceneController.State = FightState.MovePhase;
                Debug.Log("Move Phase");
            }
        }
    }

    public void SwitchShootPhase()
    {
        if(CanChangePhase() && AvailablePhase[FightState.ShootPhase])
        {
            if(FightSceneController.State == FightState.ShootPhase)
            {
                FightSceneController.State = FightState.Sleeping;
                Debug.Log("Cancel Shoot Phase");
            }
            else
            {
                FightSceneController.State = FightState.ShootPhase;
                Debug.Log("Shoot Phase");
            }
        }
    }

    public void SwitchFightPhase()
    {
        if(CanChangePhase() && AvailablePhase[FightState.FightPhase])
        {
            if(FightSceneController.State == FightState.FightPhase)
            {
                FightSceneController.State = FightState.Sleeping;
                Debug.Log("Cancel Fight Phase");
            }
            else
            {
                FightSceneController.State = FightState.FightPhase;
                Debug.Log("Fight Phase");
            }
        }
    }

    public void EndPhaseOn()
    {
        if(CanChangePhase())
        {
            FightSceneController.State = FightState.EndTurnPhase;
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
        return FightSceneController.State == FightState.Sleeping
            || FightSceneController.State == FightState.ShootPhase
            || FightSceneController.State == FightState.MovePhase
            || FightSceneController.State == FightState.FightPhase;
    }
}
