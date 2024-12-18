
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GroundCreater : MonoBehaviour
{


    public GameObject bos;
    public Image[] img;
    public GameObject Flag;
    public int howManyFlag;
    public GameObject clock;
    public GameObject[] bombNumber = new GameObject[8];
    public int howManyBomb = 15;
    public GameObject[,] obje = new GameObject[15, 10];
    public GameObject bomb;
    public int[,] arroundBomb = new int[15, 10];
    private bool[,] checking = new bool[15, 10];
    private int groundLine;
    private int groundColumn;
    private int downTrue;
    private GameObject click;
    private GameObject radarObject;
    private int bombLine;
    private int bombColumn;
    private bool gameEnd;
    int t = 0;
    int j = 0;
    private int finish;
    Vector2[] checkContunie = new Vector2[150];
    private void Start()
    {
        howManyFlag = howManyBomb;
        Debug.Log(howManyFlag);
    }
    void MapGenerator(int x, int y)
    {
        

        while (howManyBomb > 0)
        {
            groundLine = Random.Range(0, 15);
            groundColumn = Random.Range(0, 10);
            if (groundLine != x && groundColumn != y)
            {

                if (obje[groundLine, groundColumn] == null)
                {

                    GameObject BombGround = Instantiate(bomb, new Vector3(groundLine, groundColumn, 0), transform.rotation) as GameObject;
                    BombGround.transform.parent = this.transform;
                    BombGround.name = "bomb" + groundLine + "," + groundColumn + "," + 0 + "";

                    for (bombLine = groundLine - 1; bombLine < groundLine + 2; bombLine++)
                    {
                        for (bombColumn = groundColumn - 1; bombColumn < groundColumn + 2; bombColumn++)
                        {
                            if (bombLine > -1 && bombLine < 15 && bombColumn > -1 && bombColumn < 10)
                            {
                                arroundBomb[bombLine, bombColumn]++;
                            }
                        }
                    }
                    if(checking[groundLine, groundColumn] == false)
                    {
                        checking[groundLine, groundColumn] = true;
                        finish++;
                    }
                    
                    obje[groundLine, groundColumn] = BombGround;
                    howManyBomb--;
                }

            }

        }
        for (groundLine = 0; groundLine < 15; groundLine++)
        {
            for (groundColumn = 0; groundColumn < 10; groundColumn++)
            {
                if (obje[groundLine, groundColumn] == null)
                {

                    GameObject bombNumberG = Instantiate(bombNumber[arroundBomb[groundLine, groundColumn]], new Vector2(groundLine, groundColumn), transform.rotation) as GameObject;
                    bombNumberG.transform.parent = this.transform;
                    bombNumberG.name = "" + groundLine + "," + groundColumn + "," + 0 + "";
                    obje[groundLine, groundColumn] = bombNumberG;

                }
            }

        }


    }


    void Update()
    {
        
        Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

        if (gameEnd != true)
        {
            if (finish == 150)
                StartCoroutine(Hide(1));
            if (Input.GetMouseButtonDown(0))
            {
                int x = (int)(raycastPos.x + 0.5f);
                int y = (int)(raycastPos.y + 0.4f);
                if(downTrue>0)
                    click = hit.collider.gameObject;


              
                if (downTrue == 0)
                {
                    
                    MapGenerator(x, y);
                    Destroy(bos);

                    downTrue++;
                    click = obje[x, y];


                }
                if (click != null)
                {

                    if (click.tag == "flag"  )
                    {
                        Destroy(click);
                        howManyFlag++;
                    }
                    if(click.tag == "clock")
                    {
                        Destroy(click);
                    }


                    else if (click.tag == "bomb")
                    {
                        StartCoroutine(Hide(0));


                    }

                    else if (click.tag == "zeroBomb")
                    {

                        test(x, y);

                    }
                    else if(click.tag== "howManyBombNumber")
                    {
                        if (checking[x, y] == false)
                        {
                            checking[x, y] = true;
                            finish++;
                            click.GetComponent<Renderer>().enabled = false;
                        }
                        
                    }
                    click.GetComponent<Renderer>().enabled = false;
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                

                click = hit.collider.gameObject;

                if (click.tag != "flag" && click != null)
                {
                    if (click.tag == "clock")
                    {

                        Destroy(click);

                    }
                    Instantiate(Flag, hit.collider.gameObject.transform.position, transform.rotation);
                    howManyFlag--;
                   
                   
                }
            }
            if (Input.GetMouseButtonUp(2))
            {

                click = hit.collider.gameObject;


                if (click.tag != "clock" && click != null)
                {
                    if (click.tag == "flag")
                    {

                        Destroy(click);

                    }
                    Instantiate(clock, hit.collider.gameObject.transform.position, transform.rotation);

                }
            }
        }
        while (j < t)
        {
            test((int)checkContunie[j].x, (int)checkContunie[j].y);
            j++;
        }


    }
    private void test(int x, int y)
    {
        for (bombLine = x - 1; bombLine < x + 2; bombLine++)
        {
            if (bombLine > -1 && bombLine < 15)
            {
                for (bombColumn = y - 1; bombColumn < y + 2; bombColumn++)
                {

                    if (bombColumn > -1 && bombColumn < 10)
                    {
                        radarObject = obje[bombLine, bombColumn];
                        if (radarObject.tag == "howManyBombNumber")
                        {
                            if(checking[bombLine, bombColumn] == false)
                            {
                                checking[bombLine, bombColumn] = true;
                                finish++;
                            }
                            
                            radarObject.GetComponent<Renderer>().enabled = false;
                            
                        }
                        else if (radarObject.tag == "zeroBomb")
                        {
                            if (!checking[bombLine, bombColumn])
                            {
                                checkContunie[t] = new Vector2(bombLine, bombColumn);                                
                                t++;
                                checking[bombLine, bombColumn] = true;
                                finish++;   
                            }
                            
                            radarObject.GetComponent<Renderer>().enabled = false;
                            

                        }




                    }

                }
            }
            

        }
        
    }
    IEnumerator Hide(int x)
    {
        img[x].gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);

    }
}
