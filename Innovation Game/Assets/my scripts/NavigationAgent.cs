using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NavigationAgent : MonoBehaviour {

    //Navigation Variables
    public WaypointGraph graphNodes;
    public List<int> openList = new List<int>();
    public List<int> closedList = new List<int>();
    public List<int> currentPath = new List<int>();
    public List<int> greedyPaintList = new List<int>();
    public int currentPathIndex = 0;
    public int currentNodeIndex = 0;

    //dirctionary to store parent and child nodes
    public Dictionary<int, int> cameFrom = new Dictionary<int, int>();

    // Use this for initialization
    void Start () {
        //Find waypoint graph
        graphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>();

        //Initial node index to move to
        currentPath.Add(currentNodeIndex);
        print(currentPath.Count);
    }

    //A-Star Search
    public List<int> AStarSearch(int start, int goal) {

        //clear everything at start
        openList.Clear();
        closedList.Clear();
        cameFrom.Clear();

        //Begin
        openList.Add(start);

        float gScore = 0;
        float fScore = gScore + Heuristic(start, goal);

        while (openList.Count > 0) { 
            //Find the Node in openList that has the lowest fScore value
            int currentNode = bestOpenListFScore(start, goal);
            
            //Found the end, reconstruct entire path and return
            if (currentNode == goal) { 
                return ReconstructPath(cameFrom, currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            
            for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++) { 
                int thisNeighbourNode = graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i];
                
                //Ignore if neighbour node is attached
                if(!closedList.Contains(thisNeighbourNode)) { 
                    //Distance from current to the nextNode
                    float tentativeGScore = Heuristic(start, currentNode) + Heuristic(currentNode, thisNeighbourNode);

                    //Check to see if in openList or if GScore is more sensible
                    if (!openList.Contains(thisNeighbourNode) || tentativeGScore < gScore) { 
                        openList.Add(thisNeighbourNode);
                    }

                    //Add to dictionary - this neighbour came from this parent
                    if (!cameFrom.ContainsKey(thisNeighbourNode)){ 
                        cameFrom.Add(thisNeighbourNode, currentNode);
                    }

                    gScore = tentativeGScore;
                    fScore = Heuristic(start, thisNeighbourNode) + Heuristic(thisNeighbourNode, goal);
                }
            }
        }    

        return null;
    }

    //ReconstructPath
    public List<int> ReconstructPath(Dictionary<int, int> CF, int current) { 
        List<int> finalPath = new List<int>();
        finalPath.Add(current);
        while (CF.ContainsKey(current)) { 
            current = CF[current];
            finalPath.Add(current);
        }

        finalPath.Reverse();

        return finalPath;
    }

    //Heuristic function
    public float Heuristic(int a, int b) { 
        return Vector3.Distance(graphNodes.graphNodes[a].transform.position, graphNodes.graphNodes[b].transform.position);
    }

    //for loop for A-Star Search
    public int bestOpenListFScore(int start, int goal) { 
        int bestIndex = 0;
        
        for (int i = 0; i < openList.Count; i++){ 
            if ((Heuristic(openList[i], start) + Heuristic(openList[i], goal)) < (Heuristic(openList[bestIndex], start) + Heuristic(openList[bestIndex], goal))) { 
                bestIndex = i;
            }
        }
        int bestNode = openList[bestIndex];
        return bestNode;
    }
    public class GreedyChildren :  IComparable<GreedyChildren> { 
        public int childID { get; set; }
        public float childHScore { get; set; }

        public GreedyChildren(int childrenID, float childrenHScore) { 
            this.childID = childrenID;
            this.childHScore = childrenHScore;
        }

        public int CompareTo(GreedyChildren other) { 
            return this.childHScore.CompareTo(other.childHScore);
        }
    }

    //Greedy Search
    public List<int> GreedySearch(int currentNode, int goal, List<int> path) {
        
        //Make a custom list that stores the current node's children nodes and HScores. Sort them by ascending order of Heuristic
        List<GreedyChildren>thisNodesChildren = new List<GreedyChildren>();
        for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++) { 
            thisNodesChildren.Add(new GreedyChildren(graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i],Heuristic(graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i], goal)));
        }
        //Sort them by ascending order of Heuristic
        thisNodesChildren.Sort();

        //Loop through children
        for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++){ 
            //if the child has not been painted
            if (!greedyPaintList.Contains(currentNode)) { 
                //paint the child
                greedyPaintList.Add(currentNode);
                if (i == goal) { 
                    path.Add(thisNodesChildren[i].childID);
                    return path;
                }
                //Recurse the search on the child node
                path = GreedySearch(thisNodesChildren[i].childID, goal, path);
                //We have a path now unwind the stack
                if (path.Count != 0){ 
                    path.Add(thisNodesChildren[i].childID);
                    return path;
                }
            } 
        }
        return path;
    }

    public List<int> ReconstructPath(List<int> path, int current, int start) {

        List<int> finalPath = new List<int>();
        path = path.Distinct<int>().ToList();
        path.Reverse();
        int end = current;

        bool linked = false;
        int a = 0;
        //Look at nodes attached to current one
        while (a < 15) {

            for (int i = 0; i < graphNodes.graphNodes[current].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++) {

                //Reset linked breaker
                linked = false;

                //Go through path until you find a matched index
                for (int j = 0; j < path.Count; j++) {

                    //If found, add it to the final path, make it the current node index and remove from the path list
                    if (path[j] == graphNodes.graphNodes[current].GetComponent<LinkedNodes>().linkedNodesIndex[i]) {
                        finalPath.Add(path[j]);
                        current = path[j];
                        path.Remove(path[j]);
                        linked = true;
                    }
                    if (linked) //Stop iterating if solution found - efficiency
                        break;
                }
                if (linked) //Stop iterating if solution found - efficiency
                    break;
            }
            a++;
        }

        finalPath.Reverse();
        finalPath.Add(end);

        return finalPath;
    }
}
