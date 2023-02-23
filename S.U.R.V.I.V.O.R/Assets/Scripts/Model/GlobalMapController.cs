using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Interface;
using Model.Player;
using Model.SaveSystem;
using UnityEngine;

namespace Model
{ 
    public class GlobalMapController : MonoBehaviour, ISaved<GlobalMapData>
    {
        public static GlobalMapController Instance { get; private set; }
        public static GlobalMapData Data { get; set; }
        public bool OnPause { get; private set; }

        public int TurnNumber { get; private set; } = 1;
        
        [SerializeField] private List<Group> groups;
        [field: SerializeField] public Canvas MainCanvas { get; private set; }

        [SerializeField] [Min(0)] private int chosenGroupIndex;

        public int ChosenGroupIndex
        {
            get => chosenGroupIndex;
            set
            {
                if (value < 0 || value >= groups.Count)
                    throw new InvalidOperationException();
                ChosenGroupChange?.Invoke(groups[chosenGroupIndex], groups[value]);
                chosenGroupIndex = value;
            }
        }

        public Group ChosenGroup => groups[chosenGroupIndex];
        public IEnumerable<Group> Groups => groups;
        public event Action<Group, Group> ChosenGroupChange;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                Init();
            }
        }

        private void Init()
        {
            TurnController.Instance.AddListener(() => TurnNumber++);
            if (Data != null)
            {
                StartCoroutine(RestoreGameCoroutine());
            }
                
        }


        public void Resume()
        {
            OnPause = false;
            Selector.Instance.gameObject.SetActive(true);
            MinimapController.Instance.isActive = true;
            CameraController.Instance.isActive = true;
            InventoryController.Instance.gameObject.SetActive(true);
        }

        public void Pause()
        {
            OnPause = true;
            Tooltip.Instance.HideTooltip();
            Selector.Instance.gameObject.SetActive(false);
            MinimapController.Instance.isActive = false;
            CameraController.Instance.isActive = false;
            InventoryController.Instance.gameObject.SetActive(false);
        }

        public GlobalMapData CreateData()
        {
            return new GlobalMapData()
            {
                turnNumber = TurnNumber,
                groupSaves = groups.Select(g => g.CreateData()).ToList(),
                chosenGroupIndex = ChosenGroupIndex,
                locationInventory = LocationInventory.Instance.LocationInventoryGrid
                    .GetItems().Select(x => x.CreateData()).ToArray(),
                cameraPosition = CameraController.Instance.transform.position,
                zoomHeight = CameraController.Instance.zoomHeight
            };
        }

        public void Restore(GlobalMapData globalMapData)
        {
            groups = new List<Group>();
            foreach (var groupSave in globalMapData.groupSaves)
            {
                var group = Instantiate(Resources.Load<Group>(groupSave.resourcesPath));
                groups.Add(group);
                group.Restore(groupSave);
            }

            ChosenGroupIndex = globalMapData.chosenGroupIndex;
            TurnNumber = globalMapData.turnNumber;

            var inventory = LocationInventory.Instance.LocationInventoryGrid;
            foreach (var itemSave in globalMapData.locationInventory) 
            {
                var item = Instantiate(Resources.Load<BaseItem>(itemSave.resourcesPath));
                item.Restore(itemSave);
                inventory.PlaceItem(item, item.OnGridPositionX, item.OnGridPositionY);
            }

            CameraController.Instance.MoveCamera(globalMapData.cameraPosition);
            CameraController.Instance.zoomHeight = globalMapData.zoomHeight;
            InterfaceController.Instance.Init();
        }

        public void Clear()
        {
            foreach (var group in groups)
                Destroy(group.gameObject);

            groups = new List<Group>();
            foreach (var item in FindObjectsOfType<BaseItem>(true))
                Destroy(item.gameObject);
        }
        
        IEnumerator RestoreGameCoroutine()
        {
            yield return null;
            Clear();
            yield return null;
            Restore(Data);
        }
    }

    [DataContract]
    public class GlobalMapData
    {
        [DataMember] public int turnNumber;
        [DataMember] public List<GroupData> groupSaves;
        [DataMember] public int chosenGroupIndex;
        [DataMember] public ItemData[] locationInventory;
        [DataMember] public Vector3 cameraPosition;
        [DataMember] public float zoomHeight;
    }
}