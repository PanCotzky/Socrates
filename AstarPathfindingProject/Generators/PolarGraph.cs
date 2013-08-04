using UnityEngine;
using System.Collections;
//Include generics to be able to use Generics
using System.Collections.Generic;
//Include the Pathfinding namespace to gain access to a lot of useful classes
using Pathfinding;
using Pathfinding.Serialization.JsonFx;
//Inherit our new graph from a base graph type
[JsonOptIn]
public class PolarGraph : NavGraph
{

    [JsonMember]
    public int circles = 10;
    [JsonMember]
    public int steps = 20;

    [JsonMember]
    public Vector3 center = Vector3.zero;

    [JsonMember]
    public float scale = 2;

    public override void Scan()
    {

        //Create an array containing all nodes
        nodes = CreateNodes(circles * steps);

        Matrix4x4 matrix = Matrix4x4.TRS(center, Quaternion.identity, Vector3.one * scale);

        nodes[0].position = (Int3)matrix.MultiplyPoint(Vector3.zero);

        //The number of angles (in radians) each step will use
        float anglesPerStep = (2 * Mathf.PI) / steps;

        for (int i = 1; i < circles; i++)
        {

            for (int j = 0; j < steps; j++)
            {
                //Get the angle to the node relative to the center
                float angle = j * anglesPerStep;

                //Get the direction towards the node from the center
                Vector3 pos = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                //Multiply it with scale and the circle number to get the node position in graph space
                pos *= i * scale;

                //Multiply it with the matrix to get the node position in world space
                pos = matrix.MultiplyPoint(pos);

                //Get the node from an index
                //The middle node is at index 0
                //The first circle is from 1...steps-1
                //The second circle is from steps...2*steps-1
                //The third is from 2*steps...3*steps-1 and so on

                Node node = nodes[(i - 1) * steps + j + 1];

                //Assign the node position
                node.position = (Int3)pos;

            }
        }

        //Now all nodes are created, let's create some connections between them!

        //Loop through all circles except the first one which is only one point
        for (int i = 1; i < circles; i++)
        {

            for (int j = 0; j < steps; j++)
            {
                //Get the current node
                Node node = nodes[(i - 1) * steps + j + 1];

                //The nodes here will always have exactly four connections, like a grid, but polar.
                //Except for those in the last circle which will only have three connections
                int numConnections = (i < circles - 1) ? 4 : 3;
                Node[] connections = new Node[numConnections];
                int[] connectionCosts = new int[numConnections];

                //Get the next clockwise node in the current circle.
                //If j++ would overflow steps, it would create a connection to the first node in the next circle, so if it does
                //we have to prevent it by setting the steps index to 0 which will then create a connection to the correct node
                int connId = (i - 1) * steps + (j < steps - 1 ? j + 1 : 0) + 1;
                connections[0] = nodes[connId];

                //Counter clockwise node. Here we check for underflow instead
                connId = (i - 1) * steps + (j > 0 ? j - 1 : steps - 1) + 1;
                connections[1] = nodes[connId];

                //The node in the next circle (out from the center)
                if (i > 1)
                {
                    connId = (i - 2) * steps + j + 1;
                }
                else
                {
                    //Create a connection to the middle node, special case
                    connId = 0;
                }
                connections[2] = nodes[connId];

                //Are there any more circles outside this one?
                if (numConnections == 4)
                {
                    //The node in the next circle (out from the center)
                    connId = i * steps + j + 1;
                    connections[3] = nodes[connId];
                }

                for (int q = 0; q < connections.Length; q++)
                {
                    //Node.position is an Int3, here we get the cost of moving between the two positions
                    connectionCosts[q] = (node.position - connections[q].position).costMagnitude;
                }

                node.connections = connections;
                node.connectionCosts = connectionCosts;
            }
        }

        //The center node is a special case, so we have to deal with it separately
        Node centerNode = nodes[0];
        centerNode.connections = new Node[steps];
        centerNode.connectionCosts = new int[steps];
        //Assign all nodes in the first circle as connections to the center node
        for (int j = 0; j < steps; j++)
        {
            centerNode.connections[j] = nodes[1 + j];

            //centerNode.position is an Int3, here we get the cost of moving between the two positions
            centerNode.connectionCosts[j] = (centerNode.position - centerNode.connections[j].position).costMagnitude;
        }

        //Set all the nodes to be walkable
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].walkable = true;
        }
    }
}