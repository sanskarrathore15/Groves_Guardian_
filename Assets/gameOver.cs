using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    public GameObject GameOver_;
    public GameObject Game_Complete_;
    public static GameObject GameOver;
    public static GameObject Game_Complete;

    // Start is called before the first frame update
    void Start()
    {
        GameOver = GameOver_;
        Game_Complete = Game_Complete_;

        GameOver.gameObject.SetActive(false);
        Game_Complete.gameObject.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
