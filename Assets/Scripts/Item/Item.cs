using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PairIndex
{
    public int First;
    public int Second;

    public PairIndex(int first, int second)
    {
        First = first;
        Second = second;
    }

    public override string ToString()
    {
        return "Fist: " + First + "  Second: " + Second;
    }

    
}
public class Item : MonoBehaviour
{
    private int  index = 0;
    public int Index => index;
    private LineRenderer line;

    private PairIndex pairIndex;
    public PairIndex PairIndex => pairIndex;
    


    public void SetPair(int first, int second)
    {
        pairIndex = new PairIndex(first, second);
   
       
    }
    private void Start()
    {
        
        line = transform.GetChild(1).GetComponent<LineRenderer>();
    }
    [ContextMenu("Turn On")]
    public void TurnOnPOutLine()
    {
        line.enabled = true;
    }
    [ContextMenu("Turn Off")]
    public void TurnOffPOutLine()
    {
        line.enabled = false;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
}
