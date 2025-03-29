using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnData : MonoBehaviour
{
    // time inbetween spawns


    // enemies to be flagged for spawning
    public bool chair = false;
    public bool couch = false;
    public bool oven = false;
    public bool lamp = false;
    public bool rug = false;
    public bool tv = false;
    //public bool hand = false;

    private bool[] spawnList;

    // Start is called before the first frame update
    void Start()
    {
        spawnList = new bool[] {chair, couch, oven, lamp, rug, tv};

    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
