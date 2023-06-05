using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBarrelSpawner : MonoBehaviour
{
    public float minFrequency;
    public float maxFrequency;
    public float timer = 0.0f;
    [SerializeField]
    private GameObject _powerupBarrelPrefab;
    public Transform[] barrelSpawnPoints;
    private int i;
    private GameObject _newBarrel;
    [SerializeField]
    private float _nextSpawnTime;
    [SerializeField]
    private float _barrelTimeOut = 3.0f; //should always be < minFrequency to avoid more than one barrel at a time
    
    // Start is called before the first frame update
    void Start()
    {
        _nextSpawnTime = Random.Range(minFrequency, maxFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>_nextSpawnTime)
        {
            i = Random.Range(0, barrelSpawnPoints.Length);
            InstantiateBarrel(i);
            StartCoroutine(DestroyBarrel());
            _nextSpawnTime = Random.Range(minFrequency, maxFrequency);
            timer = 0;
        }
        
       
    }

    void InstantiateBarrel(int index)
    {
        Instantiate(_powerupBarrelPrefab, barrelSpawnPoints[index].position, transform.rotation);
    }

    IEnumerator DestroyBarrel()
    {
        yield return new WaitForSeconds(_barrelTimeOut);
        _newBarrel = GameObject.Find("PowerUp Barrel(Clone)");
        Destroy(_newBarrel);
    }

}
