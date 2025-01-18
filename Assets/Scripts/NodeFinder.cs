//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NodeFinder
//{
//    public static T FindComponentInChildWithTag(this GameObject parent, string tag) where T : Component
//    {
//        Transform t = parent.transform;
//        foreach (Transform tr in t)
//        {
//            if (tr.tag == tag)
//            {
//                return tr.GetComponent();
//            }
//        }
//        return null;
//    }
//}