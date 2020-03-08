using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGrowControl : MonoBehaviour
{
    private int _growNewBranchInterval = 9;
    private int _stopGrowTime = 30;
    private float _horizontalRange = 4;

    private Vector2 _branchPosition;
    public bool isGrowing = true;

    private GameObject _trunk;
    public List<GameObject> branches;
    private List<Growth> _growths;
    private Vector3 _treeTopPosition;
    
    // Use this for initialization
    public void Awake ()
    {
        branches = new List<GameObject>();
        _growths = new List<Growth>();
        CreateNewTree();
        StartCoroutine(WaitAndStopGrow(_stopGrowTime));
    }

    public void CreateNewTree()
    {
        var rootPosition = transform.position;
        _trunk = Instantiate(Resources.Load<GameObject>("Prefabs/unitBranch"), rootPosition, Quaternion.identity,
            transform);
        var trunkGrowth = new Trunk(_trunk, this);
        trunkGrowth.Start();
        _growths.Add(trunkGrowth);
        branches.Add(_trunk);
        _treeTopPosition = new Vector3(rootPosition.x, rootPosition.y + _trunk.transform.localScale.y * 2.5f - 0.2f, 0f);
        if (Services.Trees.Count == 0)
        {
            InstantiateBranch(_treeTopPosition, Vector3.right);
        }
        else
        {
            GrowRandomNewBranch();
        }
        Services.Trees.Add(this);
    }

    // Update is called once per frame
    void Update ()
    {
        var rootPosition = transform.position;
        _treeTopPosition = new Vector3(rootPosition.x, rootPosition.y + _trunk.transform.localScale.y * 2.5f - 0.2f, 0f);
        foreach (var growth in _growths)
        {
            growth.Update();
        }
        
    }
    private void GrowRandomNewBranch()
    {
        var random = Random.Range(-_horizontalRange, _horizontalRange);
        _branchPosition = _treeTopPosition + new Vector3(random,0);
        if (random > 0)
        {
            InstantiateBranch(_branchPosition, Vector3.right);
        }
        else
        {
            InstantiateBranch(_branchPosition, Vector3.left);
        }
        
    }

    private void InstantiateBranch(Vector3 position, Vector3 direction)
    {
        var branch = Instantiate(Resources.Load<GameObject>("Prefabs/unitBranch"), position, Quaternion.identity,
            transform);
        var leaves = Instantiate(Resources.Load<GameObject>("Prefabs/leavesParticle"), position, Quaternion.identity,
            branch.transform);
        branch.transform.up = direction;
        branch.tag = "branch";
        var branchGrowth = new Branch(branch, this);
        branchGrowth.Start();
        branches.Add(branch);
        _growths.Add(branchGrowth);
        
        if (isGrowing)
        {
            StartCoroutine(WaitAndGrowNewBranch(_growNewBranchInterval));
        }

    }

    private IEnumerator WaitAndStopGrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isGrowing = false;
    }

    private IEnumerator WaitAndGrowNewBranch(float interval)
    {
        yield return new WaitForSeconds(interval);
        GrowRandomNewBranch();
    }
}
