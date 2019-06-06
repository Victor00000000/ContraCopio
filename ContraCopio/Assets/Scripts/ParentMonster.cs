using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMonster : MonoBehaviour
{

    public int points;
    GameMaster gm;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        gm.ScorePoins(points);
    }
}
