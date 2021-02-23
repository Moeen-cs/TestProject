using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform[] Targets;

    [SerializeField]
    float speed = 2f;

    [SerializeField]
    GameObject enamyMissile;

    [SerializeField]
    GameObject playerMissile;

    [SerializeField]
    public Transform targetPos;

    bool gameOver = false;

    public Camera Camera;

    [SerializeField]
    GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFiringMissile());
    }
    public void FireMissle()
    {
        float x = Random.Range(-10f, 10f);
        int rand = Random.Range(0, 4);
        GameObject Go = Instantiate(enamyMissile);
        Go.transform.position = new Vector3(x, 8.1f, 0);
        MissileMovement misileObject = Go.GetComponent<MissileMovement>();
        misileObject.MoveMissile(Targets[rand], speed);
    }
    IEnumerator StartFiringMissile()
    {
        while (gameOver == false)
        {
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
            FireMissle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            if (GameObject.FindGameObjectWithTag("usergun") == null)
            {
                gameOver = true;
                GameOverScreen.SetActive(true);
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                    if (hit.collider != null)
                    {
                          Transform go =  Instantiate(targetPos, hit.point, Quaternion.identity);
                        FireMissilePlayer(go);

                    }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            // Store current mouse position in pixel coordinates.
            Vector3 mousePixelPos = Input.mousePosition;

            // Add depth so it can actually be used to cast a ray.
            mousePixelPos.z = 20f;

            // Transform from pixel to world coordinates
            Vector3 mouseWorldPosition = Camera.ScreenToWorldPoint(mousePixelPos);

            // Remove depth
            mouseWorldPosition.z = 0f;

            // Spawn your prefab
          Transform go =  Instantiate(targetPos, mouseWorldPosition, Quaternion.identity);
            FireMissilePlayer(go);
        }
    }

    void FireMissilePlayer(Transform targetPos)
    {
        float[] distance = new float[4];
        Transform tempTarget = null;
        int ind = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Targets[i] != null)
            {
                distance[i] = Vector2.Distance(Targets[i].transform.position, targetPos.transform.position);
            }
        }
        if (Targets[0] != null)
        {
            tempTarget = Targets[0];
            ind = 0;
        }
        for (int i = 1; i < 4; i++)
        {
            if (Targets[i] != null)
            {
                if (tempTarget != null)
                {
                    if (distance[i] < distance[ind])
                    {
                        ind = i;
                        tempTarget = Targets[i];
                    }
                }
                else
                {
                    ind = i;
                    tempTarget = Targets[i];
                }
            }
        }
        if (tempTarget != null)
        {
            GameObject Go = Instantiate(playerMissile);
            Go.transform.position = Targets[ind].transform.position;
            PlayerMissileMovement misileObject = Go.GetComponent<PlayerMissileMovement>();
            misileObject.MoveMissile(targetPos, speed);
        }

    }
}
