using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{   
    public int speed = 1, coinCount = 0, heartCount = 1;
    public float leftBorder = -2.5f, rightBorder = 2.5f;
    //float height = 1.25f;
    float transSpeed = 0.25f;
    public bool onLeft = false, mid = true, onRight = false;
    int timer = 0;
    public TextMeshProUGUI coinText, heartText;
    public GameObject failPanel;

    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Animator>().SetBool("Run", true);
            speed = 15;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("Jump");
            //transform.DOJump(new Vector3(transform.position.x,2,transform.position.z), 1, 1, 0.5f);
            transform.DOMoveY(4, 0.5f);
            transform.DOMoveY(-0.9f, 0.5f).SetDelay(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.A) && onLeft == false && mid == true)
        {
            onLeft = true;
            mid = false;
            transform.DOMoveX(leftBorder, transSpeed);
            GetComponent<Animator>().SetTrigger("GoLeft");
            //transform.position = new Vector3(-2, height, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.A) && mid == false && onRight == true)
        {
            onRight = false;
            mid = true;
            transform.DOMoveX(0, transSpeed);
            GetComponent<Animator>().SetTrigger("GoLeft");
            //transform.position = new Vector3(0, height, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D) && onRight == false && mid == true)
        {
            onRight = true;
            mid = false;
            transform.DOMoveX(rightBorder, transSpeed);
            GetComponent<Animator>().SetTrigger("GoRight");
            //transform.position = new Vector3(2, height, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D) && onLeft == true && mid == false)
        {
            onLeft = false;
            mid = true;
            transform.DOMoveX(0, transSpeed);
            GetComponent<Animator>().SetTrigger("GoRight");
            //transform.position = new Vector3(0, height, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "StopPoint")
        {
            GetComponent<Animator>().SetBool("Run", false);
            speed = 0;
        }
        if (other.tag == "Obstacle")
        {
            GetComponent<Animator>().SetBool("Die", true);
            speed = 0;
            failPanel.SetActive(true);
        }
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            coinCount++;
            coinText.text = coinCount.ToString();
        }
        if (other.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            if(heartCount < 5)
                heartCount++;
            heartText.text = "Heart: " + heartCount;
            transform.Find("skateboard").gameObject.SetActive(true);
            GetComponent<Animator>().SetBool("Idle", true);
            GetComponent<Animator>().SetBool("Run", false);
            transform.DOMoveY(0, 1);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(0);
    }
}
