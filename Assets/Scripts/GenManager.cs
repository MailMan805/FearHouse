using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GenManager : MonoBehaviour
{
    // the array of rooms
    public GameObject[] rooms;
    // a changing list that represents all open nodes
    public GameObject[] availableNodes;
    // a changing list that represents all rooms
    List<GameObject> rootParts = new List<GameObject>();
    // variable representing location in rootParts<> index
    private int kidNamedFinger = 0;
    // the count of tokens spent to generate rooms
    public int tokens = 1;
    // LayerMask for overlap check
    public LayerMask Overlap;

    // Start is called before the first frame update
    void Start()
    {
        // targets the Overlap LayerMask for overlap detection
        rooms = Resources.LoadAll<GameObject>("HaydenRooms");
        GameObject genRoom = Instantiate(rooms[0]) as GameObject;
        Vector3 spawn = new Vector3(0, 0, 0);
        genRoom.transform.position = spawn;
        rootParts.Add(genRoom);
        GameObject[] genColliders = GameObject.FindGameObjectsWithTag("Bounds");
        List<GameObject> newColliders = new List<GameObject>();
        foreach (GameObject collider in genColliders)
        {
            if (collider.GetComponent<ColliderData>().newCollider)
            {
                newColliders.Add(collider);
                collider.GetComponent<ColliderData>().newCollider = false;
                Debug.Log("added new collider " + collider);
            }
        }
        foreach (GameObject collider in newColliders)
        {
            collider.GetComponent<ColliderData>().rootIndex = kidNamedFinger;
        }
        kidNamedFinger++; //increases rootIndex

        availableNodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject node in availableNodes)
        {
            if (node.GetComponent<NodeData>().newNode)
            {
                node.GetComponent<NodeData>().newNode = false;
                //Debug.Log("starting node no longer new " + node);
            }
        }
        Debug.Log(availableNodes);
    }

    // generates rooms using tokens of a quantity relative to room size using nodes as positions for new rooms
    void GenerateRooms()
    {
            
        GameObject genRoom = Instantiate(rooms[Random.Range(1, rooms.Length)]) as GameObject;
        if (tokens - genRoom.GetComponent<RoomData>().tokenCost >= 0)
        {
            tokens -= genRoom.GetComponent<RoomData>().tokenCost;
            GameObject selectedNode = availableNodes[Random.Range(0, availableNodes.Length)];
            //Debug.Log(selectedNode.transform.position);
            genRoom.transform.position = selectedNode.transform.position;

            GameObject[] genNodes = GameObject.FindGameObjectsWithTag("Node");
            List<GameObject> offsetNodes = new List<GameObject>();
            foreach (GameObject node in genNodes)
            {
                if (node.GetComponent<NodeData>().newNode)
                {
                    offsetNodes.Add(node);
                    node.GetComponent<NodeData>().newNode = false;
                    Debug.Log("added new node " + node);
                }
            }
            GameObject[] genColliders = GameObject.FindGameObjectsWithTag("Bounds");
            List<GameObject> newColliders = new List<GameObject>();
            foreach (GameObject collider in genColliders)
            {
                if (collider.GetComponent<ColliderData>().newCollider)
                {
                    newColliders.Add(collider);
                    collider.GetComponent<ColliderData>().newCollider = false;
                    Debug.Log("added new collider " + collider);
                }
            }
            bool colliding = false;
            if (selectedNode.GetComponent<NodeData>().east)
            {
                Debug.Log("if east worked!");
                foreach (GameObject node in offsetNodes)
                {
                    if (node.GetComponent<NodeData>().west)
                    {
                        Debug.Log("for each west worked!");
                        Vector3 deltaPosition = genRoom.transform.position - node.transform.position;
                        Debug.Log(deltaPosition);
                        genRoom.transform.position += deltaPosition;
                        Debug.Log(genRoom.transform.position);
                        foreach (GameObject collider in newColliders)
                        {
                            Collider[] overlapDetector = Physics.OverlapBox(collider.transform.position, collider.transform.localScale / 2, Quaternion.identity, Overlap);
                            if (overlapDetector.Length > 0)
                            {
                                colliding = true;
                            }
                        }
                        
                        if (colliding) // deletes rooms if any Overlap boxes overlap
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        } else
                        {
                            // insert root index
                            rootParts.Add(genRoom);
                            foreach (GameObject collider in newColliders)
                            {
                                collider.GetComponent<ColliderData>().rootIndex = kidNamedFinger;
                            }
                            kidNamedFinger++; //increases rootIndex
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
                            {
                                if (availableNodes[i] != null)
                                {
                                    for (int j = i + 1; j < availableNodes.Length - 1; j++)
                                    {
                                        if (availableNodes[i] != null && availableNodes[j] != null && availableNodes[i].transform.position == availableNodes[j].transform.position)
                                        {
                                            Debug.Log("Destroyed used Nodes");
                                            Destroy(availableNodes[i]);
                                            Destroy(availableNodes[j]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (selectedNode.GetComponent<NodeData>().west)
            {
                foreach (GameObject node in offsetNodes)
                {
                    if (node.GetComponent<NodeData>().east)
                    {
                        Debug.Log("for each east worked!");
                        Vector3 deltaPosition = genRoom.transform.position - node.transform.position;
                        Debug.Log(deltaPosition);
                        genRoom.transform.position += deltaPosition;
                        Debug.Log(genRoom.transform.position);
                        foreach (GameObject collider in newColliders)
                        {
                            Collider[] overlapDetector = Physics.OverlapBox(collider.transform.position, collider.transform.localScale / 2, Quaternion.identity, Overlap);
                            if (overlapDetector.Length > 0)
                            {
                                colliding = true;
                            }
                        }
                        if (colliding) // deletes rooms if any Overlap boxes overlap
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        }
                        else
                        {
                            // insert root index
                            rootParts.Add(genRoom);
                            foreach (GameObject collider in newColliders)
                            {
                                collider.GetComponent<ColliderData>().rootIndex = kidNamedFinger;
                            }
                            kidNamedFinger++; //increases rootIndex
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
                            {
                                if (availableNodes[i] != null)
                                {
                                    for (int j = i + 1; j < availableNodes.Length - 1; j++)
                                    {
                                        if (availableNodes[i] != null && availableNodes[j] != null && availableNodes[i].transform.position == availableNodes[j].transform.position)
                                        {
                                            Debug.Log("Destroyed used Nodes");
                                            Destroy(availableNodes[i]);
                                            Destroy(availableNodes[j]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (selectedNode.GetComponent<NodeData>().north)
            {
                foreach (GameObject node in offsetNodes)
                {
                    if (node.GetComponent<NodeData>().south)
                    {
                        Debug.Log("for each south worked!");
                        Vector3 deltaPosition = genRoom.transform.position - node.transform.position;
                        Debug.Log(deltaPosition);
                        genRoom.transform.position += deltaPosition;
                        Debug.Log(genRoom.transform.position);
                        foreach (GameObject collider in newColliders)
                        {
                            Collider[] overlapDetector = Physics.OverlapBox(collider.transform.position, collider.transform.localScale / 2, Quaternion.identity, Overlap);
                            if (overlapDetector.Length > 0)
                            {
                                colliding = true;
                            }
                        }
                        if (colliding) // deletes rooms if any Overlap boxes overlap
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        }
                        else
                        {
                            // insert root index
                            rootParts.Add(genRoom);
                            foreach (GameObject collider in newColliders)
                            {
                                collider.GetComponent<ColliderData>().rootIndex = kidNamedFinger;
                            }
                            kidNamedFinger++; //increases rootIndex
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
                            {
                                if (availableNodes[i] != null)
                                {
                                    for (int j = i + 1; j < availableNodes.Length - 1; j++)
                                    {
                                        if (availableNodes[i] != null && availableNodes[j] != null && availableNodes[i].transform.position == availableNodes[j].transform.position)
                                        {
                                            Debug.Log("Destroyed used Nodes");
                                            Destroy(availableNodes[i]);
                                            Destroy(availableNodes[j]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (selectedNode.GetComponent<NodeData>().south)
            {
                foreach (GameObject node in offsetNodes)
                {
                    if (node.GetComponent<NodeData>().north)
                    {
                        Debug.Log("for each north worked!");
                        Vector3 deltaPosition = genRoom.transform.position - node.transform.position;
                        Debug.Log(deltaPosition);
                        genRoom.transform.position += deltaPosition;
                        Debug.Log(genRoom.transform.position);
                        foreach (GameObject collider in newColliders)
                        {
                            Collider[] overlapDetector = Physics.OverlapBox(collider.transform.position, collider.transform.localScale / 2, Quaternion.identity, Overlap);
                            if (overlapDetector.Length > 0)
                            {
                                colliding = true;
                            }
                        }
                        if (colliding) // deletes rooms if any Overlap boxes overlap
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        }
                        else
                        {
                            // insert root index
                            rootParts.Add(genRoom);
                            foreach (GameObject collider in newColliders)
                            {
                                collider.GetComponent<ColliderData>().rootIndex = kidNamedFinger;
                            }
                            kidNamedFinger++; //increases rootIndex
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
                            {
                                if (availableNodes[i] != null)
                                {
                                    for (int j = i + 1; j < availableNodes.Length - 1; j++)
                                    {
                                        if (availableNodes[i] != null && availableNodes[j] != null && availableNodes[i].transform.position == availableNodes[j].transform.position)
                                        {
                                            Debug.Log("Destroyed used Nodes");
                                            Destroy(availableNodes[i]);
                                            Destroy(availableNodes[j]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


        } else
        {
            Destroy(genRoom);
        }

    }

    private void cleaner()
    {
        GameObject[] staticRoots = new GameObject[rootParts.Count];
        GameObject[] colliders = GameObject.FindGameObjectsWithTag("Bounds");
        
        for (int i = 0;i < staticRoots.Length;i++)
        {
            staticRoots[i] = rootParts[i];
        }
        
        for(int i = 1;i < colliders.Length;i++)
        {
            if (colliders[i] != null)
            {
                Collider[] overlapDetector = Physics.OverlapBox(colliders[i].transform.position, colliders[i].transform.localScale / 2, Quaternion.identity, Overlap);
                if (overlapDetector.Length > 0 && colliders[i].GetComponent<ColliderData>().rootIndex != 0)
                {
                    Debug.Log("deleted room at index" + colliders[i].GetComponent<ColliderData>().rootIndex);
                    Destroy(staticRoots[colliders[i].GetComponent<ColliderData>().rootIndex]);
                }
                else
                {
                    Debug.Log("Succeed deez nuts");
                }
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (tokens > 0)
        {
            Debug.Log("Assigned new availableNodes array");
            availableNodes = GameObject.FindGameObjectsWithTag("Node");
            GenerateRooms();

            // double checks overlaping nodes
            availableNodes = GameObject.FindGameObjectsWithTag("Node");
            for (int i = 0; i < availableNodes.Length; i++)
            {
                if (availableNodes[i] != null)
                {
                    for (int j = i + 1; j < availableNodes.Length - 1; j++)
                    {
                        if (availableNodes[i] != null && availableNodes[j] != null && availableNodes[i].transform.position == availableNodes[j].transform.position)
                        {
                            Debug.Log("Destroyed used Nodes");
                            Destroy(availableNodes[i]);
                            Destroy(availableNodes[j]);
                        }
                    }
                }
            } // cleaner is obsolete
            //if (tokens == 0)
            //{
            //    availableNodes = GameObject.FindGameObjectsWithTag("Node");
            //    foreach (GameObject node in availableNodes)
            //    {
            //        Destroy(node);
            //    }
            //    GameObject[] colliders = GameObject.FindGameObjectsWithTag("Bounds");
            //    foreach (GameObject collider in colliders)
            //    {
            //        Destroy(collider);
            //    }
            //}
        }
       


        // if round end : deleteRooms();
    }

}
