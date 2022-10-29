using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float bounceForce = 6;
    private AudioManager audioManager;
    public static int passedLevels;

    private void Awake()
        {
        passedLevels = PlayerPrefs.GetInt("passedLevels", 0);
        }
    // Start is called before the first frame update
    void Start()
    {
       playerRb = GetComponent<Rigidbody>(); 
       audioManager = FindObjectOfType<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
        {
        playerRb.velocity = Vector3.up * bounceForce;
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        if(materialName == "Safe (Instance)")
            {
            
            }
        else if(materialName == "Unsafe (Instance)")
            {
            GameManager.gameOver = true;
            audioManager.Play("gameOver");
            }
        else if(materialName == "LastRing (Instance)")
            {
            Time.timeScale = 0;
            GameManager.levelComplete = true;
            audioManager.Play("winLevel");
            PlayerPrefs.SetInt("passedLevels", passedLevels+1);
            }

        audioManager.Play("bounce");
        }

    
    }
