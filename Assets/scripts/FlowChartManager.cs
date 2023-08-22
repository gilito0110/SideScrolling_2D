using UnityEngine;
using System;
using Fungus;

public class FlowChartManager : MonoBehaviour
{    
    public static Flowchart flowchartManager;
    public Flowchart flowchart;
    public string onCollosionEnter;

    private void Awake()
    {
        flowchartManager = GameObject.Find("對話管理器").GetComponent<Flowchart>();
    }

    public static bool TalkBlock 
    {
        get { return flowchartManager.GetBooleanVariable("對話中"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Block targetBlock = flowchart.FindBlock(onCollosionEnter);
            flowchart.ExecuteBlock(targetBlock);
        }
    }


}
