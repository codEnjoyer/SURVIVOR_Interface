using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInterfaceSet : MonoBehaviour
{
    public FightCharacter CurrentCharacter { get; set; }
    [SerializeField]
    public SpecialCell gunSlot;
    [SerializeField]
    private SpecialCell magazineSlot;
    [SerializeField]
    private SpecialCell springSlot;
    [SerializeField]
    private SpecialCell shutterSlot;
    [SerializeField]
    private SpecialCell scopeSlot;
    [SerializeField]
    private SpecialCell handleSlot;
    [SerializeField]
    private SpecialCell tacticalSlot;
    [SerializeField]
    private SpecialCell supressorSlot;
}
