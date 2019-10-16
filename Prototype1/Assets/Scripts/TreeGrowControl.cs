using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowControl : MonoBehaviour
{
    [SerializeField] private GameObject branch;
    [SerializeField] private int growNewBranchInterval = 9;
    [SerializeField] private int stopGrowTime = 30;
    [SerializeField] private float horizontalRange = 4;
    public Transform right;
    public Transform left;
	
    private Vector2 branchPosition;
    private float randomNumber;
    private Transform treeTop;
    public bool isGrowing = true;
	

	
	

    // Use this for initialization
    void Start ()
    {
        treeTop = transform.GetChild(0).gameObject.transform.GetChild(0);
        Debug.Log(treeTop);
		
		
        Instantiate(branch, treeTop.position, right.rotation, transform);
        InvokeRepeating("GrowNewBranch",growNewBranchInterval,growNewBranchInterval);
        Invoke("StopGrowBranch", stopGrowTime);

    }

    // Update is called once per frame
    void Update ()
    {
        


    }
    private void GrowNewBranch()
    {
        randomNumber = Random.Range(-horizontalRange, horizontalRange);
        branchPosition = treeTop.position + new Vector3(randomNumber,0);
		
        if (randomNumber > 0)
        {
            Instantiate(branch, branchPosition, right.rotation, transform);
        }
        else
        {
            Instantiate(branch, branchPosition, left.rotation, transform);
        }
    }

    void StopGrowBranch()
    {
        CancelInvoke("GrowNewBranch");
        isGrowing = false;
    }
	
}
