using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTester : MonoBehaviour
{
    public bool test = false;
    public BillBoardImage billBoardImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            test = false;
            billBoardImage.Trigger();
        }
    }
}
