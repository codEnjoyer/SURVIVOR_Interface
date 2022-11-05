// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class testChancgeState : MonoBehaviour
// {
//     [SerializeField] private Size size1;
//     [SerializeField] private Size size2;
//     private InventoryState state1;
//     private InventoryState state2;
//     [SerializeField] private ItemGrid currentItemGrid;
//     void Start()
//     {
//         state1 = new InventoryState(size1);
//         state2 = new InventoryState(size2);
//         currentItemGrid.ChangeState(state1);
//     }
//
//     public void ChangeState()
//     {
//         if (currentItemGrid.curInventoryState == state1)
//             currentItemGrid.ChangeState(state2);
//         else
//         {
//             currentItemGrid.ChangeState(state1);
//         }
//     }
//
//     void Update()
//     {
//         
//     }
// }
