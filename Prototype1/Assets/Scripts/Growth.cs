using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Growth
{
    public GameObject _thisGameObject;
    public TreeGrowControl _treeGrowControl;
    
    public Growth(GameObject gameObject, TreeGrowControl treeGrowControl)
    {
        _thisGameObject = gameObject;
        _treeGrowControl = treeGrowControl;
    }

    public abstract void Start();
    public abstract void Update();
    
    private float EaseOutQuart(float start, float end, float percent)
    {
        percent--;
        end -= start;
        return -end * (percent * percent * percent * percent - 1) + start;
    }

//    private float Grow(float startValue, float endValue, float growTime)
//    {
//        return EaseOutQuart(startValue,endValue,)
//    }
}

public class Trunk : Growth
{
    private float trunkGrowSpeed = 0.3f;
    private Vector3 startSize = new Vector3(0.5f, 2.5f, 1f);
    public Trunk (GameObject gameObject, TreeGrowControl treeGrowControl) : base(gameObject, treeGrowControl){}
    public override void Start()
    {
        _thisGameObject.transform.localScale = startSize;
        _thisGameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void Update()
    {
        if (_treeGrowControl.isGrowing)
        {
            _thisGameObject.transform.localScale += new Vector3(0, trunkGrowSpeed * Time.deltaTime, 0);
        }
    }
}

public class Branch : Growth
{
    private float branchGrowMultiplierX = 0.01f;
    private float branchGrowMultiplierY = 0.1f;
    private Vector3 startSize = new Vector3(0.3f,1f,1f);
    public Branch (GameObject gameObject, TreeGrowControl treeGrowControl) : base(gameObject, treeGrowControl){}
    public override void Start()
    {
        _thisGameObject.transform.localScale = startSize;
    }

    public override void Update()
    {
        if (_treeGrowControl.isGrowing)
        {
            _thisGameObject.transform.localScale += new Vector3(branchGrowMultiplierX * Time.deltaTime, branchGrowMultiplierY * Time.deltaTime, 0);
        }
    }
    
}
