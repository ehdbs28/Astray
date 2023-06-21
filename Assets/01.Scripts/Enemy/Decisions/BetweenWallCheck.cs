using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenWallCheck : AIDecision
{
    [SerializeField]
    private LayerMask _whatIsWall;

    private Transform _rayPos;

    public override void SetUp(Transform root)
    {
        base.SetUp(root);
        _rayPos = root.Find("RayPos");
    }

    public override bool MakeADecision()
    {
        Vector3 rayDir = (_controller.ActionData.Player.transform.position - transform.position).normalized;
        float rayDis = Vector3.Distance(_controller.ActionData.Player.transform.position, transform.position);

        bool isHit = Physics.Raycast(_rayPos.position, rayDir, rayDis, _whatIsWall);

        return !isHit;
    }
}
