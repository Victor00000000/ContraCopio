using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMonster : MonoBehaviour
{

    int points;
    GameMaster gm;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        gm.ScorePoins(points);
    }
}
