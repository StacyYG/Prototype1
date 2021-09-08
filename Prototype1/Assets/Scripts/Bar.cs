using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bar
{
    protected readonly GameObject thisGameObject;
    protected readonly TreeGrowControl treeGrowControl;
    protected Vector3 startSize;

    protected Bar(GameObject gameObject, TreeGrowControl treeGrowControl)
    {
        thisGameObject = gameObject;
        this.treeGrowControl = treeGrowControl;
    }
    
    public abstract void Update();
}

public class Trunk : Bar
{
    private const float TrunkGrowSpeed = 0.3f;

    public Trunk(GameObject gameObject, TreeGrowControl treeGrowControl) : base(gameObject, treeGrowControl)
    {
        startSize = new Vector3(0.5f, 2.5f, 1f);
        thisGameObject.transform.localScale = startSize;
        thisGameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    public override void Update()
    {
        if (treeGrowControl.isGrowing)
        {
            thisGameObject.transform.localScale += new Vector3(0, TrunkGrowSpeed * Time.deltaTime, 0);
        }
    }
}

public class Branch : Bar
{
    private const float GrowSpeedX = 0.01f;
    private const float GrowSpeedY = 0.1f;

    public Branch(GameObject gameObject, TreeGrowControl treeGrowControl) : base(gameObject, treeGrowControl)
    {
        startSize = new Vector3(0.3f,1f,1f);
        thisGameObject.transform.localScale = startSize;
    }

    public override void Update()
    {
        if (treeGrowControl.isGrowing)
        {
            thisGameObject.transform.localScale += new Vector3(GrowSpeedX * Time.deltaTime, GrowSpeedY * Time.deltaTime, 0);
        }
    }
    
}
