
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpearController:MonoBehaviour
{
    public int everyDamage = 5;
    public float waitTime = 1f;
    public float moveTime = 0.3f;

    public float y = 1.5f;
    
    public List<GameObject> spears = new List<GameObject>();

    public void Init(GameObject target)
    {
        transform.position = target.transform.position;
        // transform.parent = target.transform;
        transform.position += new Vector3(0, y, 0);
        StartCoroutine(StartMagic(target));
    }

    private float timer = 0;
    IEnumerator StartMagic(GameObject target)
    {
        yield return new WaitForSeconds(waitTime);
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < spears.Count; i++)
        {
            positions.Add(spears[i].transform.position);
        }
        while (timer<moveTime)
        {
            float t = timer/moveTime;
            for (int i = 0; i < spears.Count; i++)
            {
                Vector3 pos = new Vector3();
                pos.x = Mathf.Lerp(positions[i].x, target.transform.position.x, t);
                pos.y = Mathf.Lerp(positions[i].y, target.transform.position.y, t);
                pos.z = Mathf.Lerp(positions[i].z, target.transform.position.z, t);
                spears[i].transform.position = pos;
            }    
            timer+= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
