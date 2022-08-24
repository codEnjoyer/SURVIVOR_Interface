using UnityEngine;

namespace Player
{
    // public class GroupMovementLogic : MonoBehaviour
    // {
    //     public Button Button;
    //     public GameObject Trigger;
    //     public NearestNodeFinder NearestNodeFinder;
    //     private bool isWaitingCord;
    //     private bool isWaitingTarget;
    //     private GameObject TargetNode;
    //     void Start()
    //     {
    //         NearestNodeFinder.Trigger = Trigger;
    //         Button.onClick.AddListener(OnButtonClick);
    //         InputAggregator.OnTurnEndEvent += OnTurnEnd;
    //     }
    //
    //     void Update()
    //     {
    //         if (Input.GetMouseButtonDown(0) && isWaitingCord)
    //         {
    //             Vector3 clickPosition;
    //             if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo,200f))
    //             {
    //                 clickPosition = hitInfo.point;
    //                 Trigger.transform.position = clickPosition;
    //                 isWaitingCord = false;
    //                 isWaitingTarget = true;
    //                 NearestNodeFinder.isNeedToFindNode = true;
    //             }
    //         }
    //         
    //         var nearestNode = NearestNodeFinder.GetNearestNode();
    //         
    //         if (isWaitingTarget && nearestNode != null)
    //         {
    //             isWaitingTarget = false;
    //             TargetNode = nearestNode;
    //             TargetNode.gameObject.transform.position = Vector3.zero;
    //             Debug.Log(TargetNode);
    //         }
    //     }
    //
    //     private void OnButtonClick()
    //     {
    //         isWaitingCord = true;
    //         Button.enabled = false;
    //     }
    //
    //     private void OnTurnEnd()
    //     {
    //         Button.enabled = true;
    //     }
    //}
}
