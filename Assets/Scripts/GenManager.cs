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
    // the count of tokens spent to generate rooms
    public int tokens = 1;

    // Start is called before the first frame update
    void Start()
    {
        rooms = Resources.LoadAll<GameObject>("Rooms");
        GameObject genRoom = Instantiate(rooms[0]) as GameObject;
        Vector3 spawn = new Vector3(0, 0, 0);
        genRoom.transform.position = spawn;
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
                        if (genRoom.transform.position == new Vector3(0, 0, 0)) // deletes rooms and refunds tokens that could go beyond the start room
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        } else
                        {
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
                            {
                                for (int j = i+1; j < availableNodes.Length-1; j++)
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
                        if (genRoom.transform.position == new Vector3(0, 0, 0)) // deletes rooms and refunds tokens that could go beyond the start room
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        }
                        else
                        {
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
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
                        if (genRoom.transform.position == new Vector3(0, 0, 0)) // deletes rooms and refunds tokens that could go beyond the start room
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        }
                        else
                        {
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
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
                        if (genRoom.transform.position == new Vector3(0, 0, 0)) // deletes rooms and refunds tokens that could go beyond the start room
                        {
                            Debug.Log("destroyed room, out of bounds, refunded token cost " + genRoom.GetComponent<RoomData>().tokenCost);
                            tokens += genRoom.GetComponent<RoomData>().tokenCost;
                            Destroy(genRoom);
                        }
                        else
                        {
                            // checks node positions against eachother and then removes nodes with identical positions.
                            availableNodes = GameObject.FindGameObjectsWithTag("Node");
                            for (int i = 0; i < availableNodes.Length; i++)
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

        } else
        {
            Destroy(genRoom);
        }

    }

    // Update is called once per frame
    void Update()
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

        // if round end : deleteRooms();
    }

}
