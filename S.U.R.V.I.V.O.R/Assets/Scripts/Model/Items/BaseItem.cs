using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Interface;
using Model.Entities.Characters;
using Model.SaveSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Model.Items
{
    [RequireComponent(typeof(Saved))]
    public class BaseItem : MonoBehaviour, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler, ISaved<ItemSave>
    {
        [FormerlySerializedAs("itemData")] [SerializeField]
        private BaseItemData data;

        public int OnGridPositionX { get; set; }
        public int OnGridPositionY { get; set; }

        public InventoryGrid InventoryGrid => transform.GetComponentInParent<InventoryGrid>();

        public Character ItemOwner { get; set; }

        public Vector3 OnAwakeRectTransformSize { get; set; }

        public Vector3 OnAwakeRectTransformScale { get; set; }

        public bool IsRotated { get; private set; }
        public int Height => !IsRotated ? data.Size.Height : data.Size.Width;
        public int Width => !IsRotated ? data.Size.Width : data.Size.Height;
        public BaseItemData Data => data;

        public void Awake()
        {
            if (data == null || data.Icon == null)
                return;

            gameObject.AddComponent<Image>().sprite = data.Icon;
            gameObject.GetComponent<Image>().raycastTarget = false;

            var rt = gameObject.GetComponent<RectTransform>();
            var scaleFactor = Game.Instance.MainCanvas.scaleFactor;
            var size = new Vector2(((data.Size.Width * InventoryGrid.TileSize) - data.Size.Width - 1) * scaleFactor,
                ((data.Size.Height * InventoryGrid.TileSize) - data.Size.Height - 1) * scaleFactor);
            rt.sizeDelta = size;
            OnAwakeRectTransformScale = rt.localScale;
            OnAwakeRectTransformSize = rt.sizeDelta;
        }

        public void Rotate()
        {
            IsRotated = !IsRotated;
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.rotation = Quaternion.Euler(0, 0, IsRotated ? 90 : 0);
        }

        public void Destroy()
        {
            InventoryGrid?.PickUpItem(this);
            Destroy(gameObject);
        }

        #region TooltipRegion

        private bool mouseEnter;
        private ISaved<ItemSave> savedImplementation;
        const float Seconds = 0.5f;

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseEnter = true;
            StartCoroutine(ShowTooltipCoroutine());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseEnter = false;
            Tooltip.Instance.HideTooltip();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            mouseEnter = false;
            Tooltip.Instance.HideTooltip();
        }


        IEnumerator ShowTooltipCoroutine()
        {
            yield return new WaitForSeconds(Seconds);
            if (mouseEnter && !ContextMenuController.Instance.IsActive)
                Tooltip.Instance.ShowTooltip(data.ItemName);
        }

        #endregion

        public ItemSave CreateSave()
        {
            var itemSave = new ItemSave()
            {
                resourcesPath = GetComponent<Saved>().ResourcesPath,
                positionInInventory = new Vector2Int(OnGridPositionX, OnGridPositionY),
                isRotated = IsRotated,
            };

            var allComponents = GetComponents<Component>()
                .Where(component => !component.Equals(this));
            var componentSaves = new List<ComponentSave>();
            foreach (var component in allComponents)
            {
                var type = component.GetType();
                var method = type.GetMethod("HiddenCreateSave",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (method == null) continue;
                var componentSave = (ComponentSave) method.Invoke(component, Array.Empty<object>());
                if (componentSave == null) continue;

                componentSaves.Add(componentSave);
                componentSave.itemSave = itemSave;
            }

            itemSave.componentSaves = componentSaves.ToArray();
            return itemSave;
        }

        public void Restore(ItemSave save)
        {
            OnGridPositionX = save.positionInInventory.x;
            OnGridPositionY = save.positionInInventory.y;

            IsRotated = IsRotated;

            var allComponents = GetComponents<Component>()
                .Where(component => !component.Equals(this));

            foreach (var component in allComponents)
            {
                var type = component.GetType();
                var method = type.GetMethod("HiddenRestore",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (method == null) continue;
                method.Invoke(component, new object[] {save});
            }
        }
    }

    [DataContract]
    [KnownType("GetKnownTypes")]
    public class ItemSave
    {
        [DataMember] public string resourcesPath;
        [DataMember] public Vector2Int positionInInventory;
        [DataMember] public bool isRotated;
        [DataMember] public ComponentSave[] componentSaves;


        #region Save
        private static Type[] knownTypes;
        private static Type[] GetKnownTypes()
        {
            if (knownTypes == null)
            {
                var type = typeof(ComponentSave);
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                    .ToArray();
                knownTypes = types;
            }

            return knownTypes;
        }
        #endregion
    }

    [DataContract]
    public abstract class ComponentSave
    {
        [DataMember] public ItemSave itemSave;
    }
}