using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnData : MonoBehaviour
{
    // time inbetween spawns
    public bool isAvailable = true;
    public float timer;

    // enemies to be flagged for spawning
    public bool chair = false;
    public bool couch = false;
    public bool oven = false;
    public bool lamp = false;
    public bool rug = false;
    public bool teakettle = false;
    public bool tv = false;
    //public bool hand = false;

    //private bool[] spawnList;

    // Start is called before the first frame update
    void Start()
    {
        //spawnList = new bool[] {chair, couch, oven, lamp, rug, teakettle, tv}; //outdated spawn method


    }

    // Update is called once per frame
    void Update()
    {
        if (!isAvailable)
        {
            StartCoroutine(cooldown(timer));
        }
    }

    IEnumerator cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        isAvailable = true;
    }

}
