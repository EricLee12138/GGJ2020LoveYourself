using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardImage : MonoBehaviour
{
    public Camera camera;
    public bool trigger;
    public ObjectName objectName;
    public float animTime = 2f, animSpeed = 10f, endTime = 2f;
    private Vector3 initRot, toRot, toSize;
    private bool shrink, grow;
    
    public enum ObjectName 
    {
        Hammer,
        LightBulb,
        Apple,
        Cup
    }
    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation.eulerAngles;
        shrink = false;
        grow = false;
        toSize = transform.localScale * 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger)
            transform.LookAt(camera.transform.position, Vector3.up);
        else
        {
            switch(objectName)
            {
                case ObjectName.Hammer :
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(toRot), Time.deltaTime * animSpeed);
                    break;
                }
                case ObjectName.LightBulb :
                {
                    break;
                }
                case ObjectName.Cup : 
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(toRot), Time.deltaTime * animSpeed);
                    break;
                }
                case ObjectName.Apple :
                {
                    break;
                }
            }
        }

        if (grow)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, toSize, animSpeed / 20f);
        }

        if (shrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, animSpeed / 20f);
        }
    }

    public void Trigger()
    {
        initRot = transform.rotation.eulerAngles;
        trigger = true;
        if (objectName == ObjectName.Hammer)
        {
            toRot = new Vector3(initRot.x, initRot.y, 40f);
            StartCoroutine(ChangeRotation());
        }
        else if (objectName == ObjectName.Cup)
        {
            toRot = new Vector3(initRot.x, initRot.y, -60f);
            StartCoroutine(InitParticles());
        }
        StartCoroutine(DestroyInTime());
    }

    IEnumerator ChangeRotation()
    {
        yield return new WaitForSeconds(0.5f);
        toRot = new Vector3(initRot.x, initRot.y, -60f);
        yield return new WaitForSeconds(0.5f);
        toRot = new Vector3(initRot.x, initRot.y, 20f);
        yield return new WaitForSeconds(0.5f);
        toRot = new Vector3(initRot.x, initRot.y, -60f);
        yield return new WaitForSeconds(0.5f);
        toRot = new Vector3(initRot.x, initRot.y, 0f);
    }

    IEnumerator InitParticles()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 offset = new Vector3(-0.2f, -0.3f, 0f);
        GameObject particleObject = (GameObject) Instantiate(Resources.Load("FX_WaterSplatter"), transform.position + offset, Quaternion.identity);
        StartCoroutine(DestroyParticles(particleObject));
    }

    IEnumerator DestroyInTime()
    {
        yield return new WaitForSeconds(animTime);
        grow = true;
        yield return new WaitForSeconds(endTime/3f);
        shrink = true;
        grow = false;
        yield return new WaitForSeconds(endTime*2f/3f);
        StopAllCoroutines();
        GameObject.Destroy(this.gameObject);
    }

    IEnumerator DestroyParticles(GameObject particleOject)
    {
        yield return new WaitForSeconds(animTime + endTime - 1f);
        GameObject.Destroy(particleOject);
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }
}
