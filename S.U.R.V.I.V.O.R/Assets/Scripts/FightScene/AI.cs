using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AI 
{
    public static GameObject CurrentCharacterObj;
    private static List<GameObject> opponents = new List<GameObject>();

    public static List<GameObject> GetOpponents(IEnumerable<GameObject> characters)
    {
        var opponentsObj = new List<GameObject>();
        foreach(var character in characters.Where(opp => opp.GetComponent<FightCharacter>().Type == CharacterType.Ally))
            opponentsObj.Add(character);
        
        return opponentsObj;
    }

    private static void SortOpponentsListByDistance()
    {
        opponents = opponents
            .OrderBy(opp => Vector3.Distance(CurrentCharacterObj.transform.position, opp.transform.position))
            .ToList();
    }

    public static Decision MakeDecision(IEnumerable<GameObject> opponentsObj)
    {
        if (!StateController.CanChangePhase())
            return new Decision(FightSceneController.State, null);
        opponents = GetOpponents(opponentsObj);
        SortOpponentsListByDistance();
        var character = CurrentCharacterObj.GetComponent<FightCharacter>();

        if(StateController.AvailablePhase[FightState.FightPhase])
        {
            foreach(var oppObj in opponents)
            {
                if(TryGetMelleTarget(oppObj))
                    return new Decision(FightState.FightPhase, oppObj);
            }
        }
        if(StateController.AvailablePhase[FightState.MovePhase])
        {
            foreach(var oppObj in opponents)
            {
                if(TryGoToOpponent(oppObj))
                    return new Decision(FightState.MovePhase);
            }
        }
        return new Decision(FightState.EndTurnPhase);
    }

    private static bool TryGetMelleTarget(GameObject oppObj)
    {
        var currentCharacterPos = CurrentCharacterObj.transform.position;
        var oppObjPos = oppObj.transform.position;
        return NodesNav.TryFindPath(NodesNav.GetNearestNode(currentCharacterPos).GetComponent<FightNode>(),
            NodesNav.GetNearestNodeNearEnemy(oppObj, oppObjPos
            + Vector3.ClampMagnitude(currentCharacterPos - oppObjPos, 0.2f)));
    }

    private static bool TryGoToOpponent(GameObject oppObj)
    {
        var currentCharacterPos = CurrentCharacterObj.transform.position;
        var directionToOpp = Vector3.ClampMagnitude((oppObj.transform.position 
            - currentCharacterPos) * 10,
            NodesNav.innerRadius * 2 * CurrentCharacterObj.GetComponent<FightCharacter>().RemainingEnergy);
        
        var searchPoint = currentCharacterPos + directionToOpp;
        var colliders = Physics.OverlapSphere(searchPoint, NodesNav.innerRadius * 6)
            .Where(c => c.transform.GetComponent<FightNode>() && 
                    NodesNav.AvailableNodes.ContainsKey(c.transform.GetComponent<FightNode>()))
            .OrderBy(c => Vector3.Distance(searchPoint, c.transform.position))
            .ToList();

        foreach(var collider in colliders)
        {
            var isPathFounded = NodesNav.TryFindPath(
                NodesNav.GetNearestNode(currentCharacterPos).GetComponent<FightNode>(),
                collider.transform.gameObject.GetComponent<FightNode>());
            if (isPathFounded)
                return true;
        }
        return false;

    }
}
