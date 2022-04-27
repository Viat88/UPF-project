using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject hayBalePrefab;
    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;


    public Vector3 rotationSpeed;
    public float movementSpeed;
    public float horizontalBoundary = 22;

    public Transform modelParent; // 1

// 2
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;


    // Start is called before the first frame update
    void Start()
    {
        LoadModel();

    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); // 1

        switch (GameSettings.hayMachineColor) // 2
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
            break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
            break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
            break;
        }
}


    // Update is called once per frame
    void Update()
    {
       transform.Rotate(rotationSpeed*Time.deltaTime);
       UpdateMovement();
       UpdateShooting();
    }

    private void UpdateMovement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //1

        if (horizontalInput<0 && transform.position.x > - horizontalBoundary)//2
        {
            transform.Translate(transform.right*-movementSpeed*Time.deltaTime);
        }
        else if (horizontalInput >0 && transform.position.x < horizontalBoundary)//3
        {
            transform.Translate(transform.right*movementSpeed*Time.deltaTime);
        }
    }

    private void UpdateShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) {
            shootTimer = shootInterval;
            ShootHay();
        }
    }



    private void ShootHay(){
        Instantiate (hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }
}
