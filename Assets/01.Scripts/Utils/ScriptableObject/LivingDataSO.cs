using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LivingData")]
public class LivingDataSO : ScriptableObject
{
    public float RotateSpeed;
    public float Gravity;
    public float MoveSpeed;
    public float SprintSpeed;
    public float JumpPower;
    public float MaxHP;
}
