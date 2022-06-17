using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int gold = 0;

    public void IncreaseGold(int value)
    {
        gold += value;
    }
}
