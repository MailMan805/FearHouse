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

    public bool validSpawn (string name)
    {
        if (name.Equals("chair"))
        {
            return chair;
        } else if (name.Equals("couch"))
        {
            return couch;
        } else if (name.Equals("oven"))
        {
            return oven;
        } else if (name.Equals("lamp"))
        {
            return lamp;
        } else if (name.Equals("rug"))
        {
            return rug;
        } else if (name.Equals("teakettle"))
        {
            return teakettle;
        } else if (name.Equals("tv"))
        {
            return tv;
        } else
        {
            return false;
        } 
    }

    IEnumerator cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        isAvailable = true;
    }

}
