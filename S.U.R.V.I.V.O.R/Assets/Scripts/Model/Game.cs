using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Interface;
using Model.Items;
using Model.Player;
using Model.SaveSystem;
using UnityEngine;

namespace Model
{ 
    public class Game : MonoBehaviour, ISaved<GameSave>
    {
        public static Game Instance { get; private set; }

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
        }


        public void Resume()
        {
            OnPause = false;
            Selector.Instance.Activate();
            MinimapController.Instance.isActive = true;
            CameraController.Instance.isActive = true;
        }

        public void Pause()
        {
            OnPause = true;
            Tooltip.Instance.HideTooltip();
            Selector.Instance.DeActivate();
            MinimapController.Instance.isActive = false;
            CameraController.Instance.isActive = false;
            if (InventoryController.Instance.SelectedItem != null)
            {
                LocationInventory.Instance.LocationInventoryGrid.InsertItem(InventoryController.Instance.SelectedItem);
                InventoryController.Instance.SelectedItem = null;
            }
        }

        public GameSave CreateSave()
        {
            return new GameSave()
            {
                turnNumber = TurnNumber,
                groupSaves = groups.Select(g => g.CreateSave()).ToArray(),
                chosenGroupIndex = ChosenGroupIndex,
                locationInventory = LocationInventory.Instance.LocationInventoryGrid
                    .GetItems().Select(x => x.CreateSave()).ToArray(),
                cameraPosition = CameraController.Instance.transform.position,
                zoomHeight = CameraController.Instance.zoomHeight
            };
        }

        public void Restore(GameSave gameSave)
        {
            groups = new List<Group>();
            foreach (var groupSave in gameSave.groupSaves)
            {
                var group = Instantiate(Resources.Load<Group>(groupSave.resourcesPath));
                groups.Add(group);
                group.Restore(groupSave);
            }

            ChosenGroupIndex = gameSave.chosenGroupIndex;
            TurnNumber = gameSave.turnNumber;

            var inventory = LocationInventory.Instance.LocationInventoryGrid;
            foreach (var itemSave in gameSave.locationInventory) 
            {
                var item = Instantiate(Resources.Load<BaseItem>(itemSave.resourcesPath));
                item.Restore(itemSave);
                inventory.PlaceItem(item, item.OnGridPositionX, item.OnGridPositionY);
            }

            CameraController.Instance.MoveCamera(gameSave.cameraPosition);
            CameraController.Instance.zoomHeight = gameSave.zoomHeight;
        }

        public void Clear()
        {
            foreach (var group in groups)
                Destroy(group.gameObject);

            groups = new List<Group>();
            foreach (var item in FindObjectsOfType<BaseItem>(true))
                Destroy(item.gameObject);
        }
    }

    [DataContract]
    public class GameSave
    {
        [DataMember] public int turnNumber;
        [DataMember] public GroupSave[] groupSaves;
        [DataMember] public int chosenGroupIndex;
        [DataMember] public ItemSave[] locationInventory;
        [DataMember] public Vector3 cameraPosition;
        [DataMember] public float zoomHeight;
    }
}