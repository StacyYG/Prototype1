using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGrowControl : MonoBehaviour
{
    [SerializeField] private GameObject unitBranch;
    [SerializeField] private GameObject leavesParticle;
    private const int GrowBranchInterval = 9; // How long till the tree grows a new branch
    private const int StopGrowTime = 30; // How long till the tree stops growing
    private const float HorizontalRange = 1.5f; // How far can a branch be from the trunk

    public bool isGrowing = true;
    private GameObject _trunk;
    private List<Bar> _bars;

    public void Start ()
    {
        _bars = new List<Bar>();
        CreateNewTree();
        StartCoroutine(StopGrow(StopGrowTime));
    }

    void Update ()
    {
        foreach (var bar in _bars)
        {
            bar.Update();
        }
    }
    
    public void CreateNewTree()
    {
        // Create a trunk
        _trunk = Instantiate(unitBranch, transform.position, Quaternion.identity, transform);
        var trunk = new Trunk(_trunk, this);
        _bars.Add(trunk);
        
        if (Services.treeCount == 0) // The first branch of the first tree stays the same
        {
            InstantiateBranch(TreeTop(), Vector3.right);
        }
        else
        {
            GrowRandomBranch();
        }

        Services.treeCount++;
    }

    private Vector3 TreeTop() // This is where new branches grow
    {
        var rootPosition = transform.position;
        return new Vector3(rootPosition.x, rootPosition.y + _trunk.transform.localScale.y - 0.2f, 0f);
    }
    
    private void GrowRandomBranch()
    {
        // The branch starts randomly, within a range from the trunk
        var random = Random.Range(-HorizontalRange, HorizontalRange);
        var branchPosition = TreeTop() + new Vector3(random,0);
        
        // If starts from the left of the trunk, it points to the left, vise versa
        InstantiateBranch(branchPosition, random > 0 ? Vector3.right : Vector3.left);
        
        // Add the branch to the big picture
        Services.TreeRange.Update(branchPosition);
    }

    private void InstantiateBranch(Vector3 position, Vector3 direction)
    {
        var branch = Instantiate(unitBranch, position, Quaternion.identity, transform);
        Instantiate(leavesParticle, position, Quaternion.identity, branch.transform); // Add leaves
        branch.transform.up = direction;
        branch.tag = "branch";
        var branchBar = new Branch(branch, this);
        _bars.Add(branchBar);
        
        if (isGrowing) // Call for the next branch growth
        {
            StartCoroutine(GrowNewBranch(GrowBranchInterval));
        }
    }

    private IEnumerator StopGrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isGrowing = false;
    }

    private IEnumerator GrowNewBranch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GrowRandomBranch();
    }
}
