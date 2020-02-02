using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairGrowth : MonoBehaviour
{

    Tree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = gameObject.GetComponent<Tree>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
