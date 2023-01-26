﻿using System;
using System.Collections.Generic;
using Graph_and_Map;
using Interface;
using Player;
using UnityEngine;

namespace Model
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        public bool OnPause { get; private set; }

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
    }
}