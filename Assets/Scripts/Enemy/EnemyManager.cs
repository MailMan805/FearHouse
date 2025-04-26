using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    // Spawn caps for enemies and the total cap chair, couch, oven, lamp, rug, tv
    public int enemyCap; //Max number of enemies allowed in a level
    public int enemyCount;
    public int chairCap; //Max number of chairs
    public int chairCount;
    public int couchCap; //Max number of couchs
    public int couchCount;
    public int ovenCap; //Max number of ovens
    public int ovenCount;
    public int lampCap; //Max number of lamps
    public int lampCount;
    public int rugCap; //Max number of rugs
    public int rugCount;
    public int teakettleCap; //Max number of teakettles
    public int teakettleCount;
    public int tvCap; //Max number of tvs
    public int tvCount;

    public Boolean increaseCount(string name)
    {
        if (enemyCount < enemyCap)
        {
            if (name.Equals("chair"))
            {
                if (chairCount < chairCap)
                {
                    enemyCount++;
                    chairCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (name.Equals("couch"))
            {
                if (couchCount < couchCap)
                {
                    enemyCount++;
                    couchCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (name.Equals("oven"))
            {
                if (ovenCount < ovenCap)
                {
                    enemyCount++;
                    ovenCount++;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            if (name.Equals("lamp"))
            {
                if (lampCount < lampCap)
                {
                    enemyCount++;
                    lampCount++;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            if (name.Equals("rug"))
            {
                if (rugCount < rugCap)
                {
                    enemyCount++;
                    rugCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (name.Equals("teakettle"))
            {
                if (teakettleCount < teakettleCap)
                {
                    enemyCount++;
                    teakettleCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (name.Equals("tv"))
            {
                if (tvCount < tvCap)
                {
                    enemyCount++;
                    tvCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(findPresent());
    }

    IEnumerator findPresent()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemyList.Length;
        yield return new WaitForSeconds(10);
    }
}
