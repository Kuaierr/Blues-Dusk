using UnityEngine;
using System.Collections.Generic;
using GameKit;

[System.Serializable]
public struct DiceDirection
{
    public string name;
    public Transform direction;
}

public class DiceForRoll : MonoBehaviour
{
    public List<DiceDirection> diceDirections;
    private Rigidbody rigid;
    private BoxCollider coll;
    private Dice diceData;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        rigid = GetComponentInChildren<Rigidbody>();
        coll = GetComponentInChildren<BoxCollider>();
        Vector3 force = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        float strength = Random.Range(10, 30);
        rigid.velocity = new Vector3(0f, 0.1f, 0f);
        rigid.AddForce(force * strength, ForceMode.Impulse);
        // rigid.AddRelativeForce(force * strength, ForceMode.Impulse);

    }

    private void OnEnable()
    {
        // Vector3 force = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        // float strength = Random.Range(10, 30);
        // rigid.AddForce(force * strength, ForceMode.Impulse);
    }

    public void OnInit(Dice data)
    {
        this.diceData = data;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (rigid.velocity == Vector3.zero)
        {
            float minZ = 999;
            string directionName = "";
            foreach (var diceDirection in diceDirections)
            {
                if (minZ > diceDirection.direction.transform.position.z)
                {
                    directionName = diceDirection.name;
                    minZ = diceDirection.direction.position.z;
                }
            }
            Debug.Log($"The face on the top is " + directionName);
            Debug.Log($"The face data is " + diceData.GetFacet(directionName).value);
            Destroy(this.gameObject);
        }
    }
}