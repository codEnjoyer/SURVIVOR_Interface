using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Graph_and_Map;
using Interface;
using Model.Player;
using Model.SaveSystem;
using UnityEngine;

namespace Model
{
    [RequireComponent(typeof(Saved))]
    public class Game : MonoBehaviour, ISaved<GameSave>
    {
        public static Game Instance { get; private set; }

        public bool OnPause { get; private set; }

        public int TurnNumber { get; private set; }

        [SerializeField] private List<Group> groups;
        [field: SerializeField] public Node StartNode { get; private set; }
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
                    .GetItems().Select(x => x.CreateSave()).ToArray()
            };
        }
    }

    [DataContract]
    public class GameSave
    {
        [DataMember] public int turnNumber;
        [DataMember] public GroupSave[] groupSaves;
        [DataMember] public int chosenGroupIndex;
        [DataMember] public ItemSave[] locationInventory;
    }
}