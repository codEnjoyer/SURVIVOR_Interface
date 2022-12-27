using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineData : MonoBehaviour
{
    [SerializeField] private Caliber caliber;

    public Caliber Caliber => caliber;
}
