using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public enum MaterialType
    {
        Scrap,
        Cloth
    }

    [SerializeField] private MaterialType materialtype;

    public MaterialType Materialtype => materialtype;
}
