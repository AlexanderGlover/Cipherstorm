using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMgr : MonoBehaviour {

    public float spikeBeatCount;
    public float fadeInDuration;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public GameObject[] heartObjs;
    public GameObject[] audioObjects;
    
    private float beatTime = 0.48f; //base plays 4 times per beat
    private float timer = 0.0f;
    private int fadingTrack = 0;
    private float gameTimer = 0.0f;

    private int mHealth = 3;
    private int score = 0;

    private int xRowState = 0;
    private int yRowState = 0;
    private int zRowState = 0;

    private bool isActive = true;
    private int mState = 2 ;
    // Default      - 0
    // Spikes       - 1
    //   - 2
    //   - 3
    //   - 4
    //   - 5
    //   - 6
    //   - 7
	
	void Update ()
    {
        timer += Time.deltaTime;
        gameTimer += Time.deltaTime;

        mState = Mathf.Min((int)Mathf.Round(gameTimer) / 10, 7);

        switch(mState)
        {
            case 0:
                break;
            case 7:
                if (audioObjects[7].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 7;
                }
                goto case 6;
            case 6:
                if (audioObjects[6].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 6;
                }
                goto case 5;
            case 5:
                if (audioObjects[5].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 5;
                }
                goto case 4;
            case 4:
                if (audioObjects[4].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 4;
                }
                if (timer > beatTime * spikeBeatCount)
                {
                    GameObject[] spikeArray = GameObject.FindGameObjectsWithTag("TopSpike");
                    isActive = !isActive;
                    float heightAdjustment = isActive ? 0.5f : -0.5f;
                    for (int i = 0; i < spikeArray.Length; i++)
                    {
                        if (spikeArray[i].GetComponent(typeof(SpikeMgr)) as SpikeMgr)
                        {
                            spikeArray[i].transform.position = spikeArray[i].transform.position + new Vector3(0.0f, heightAdjustment, 0.0f);
                            (spikeArray[i].GetComponent(typeof(SpikeMgr)) as SpikeMgr).SetEnabled(isActive);

                        }
                    }
                }
                goto case 3;
            case 3:
                if (audioObjects[3].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 3;
                }
                goto case 2;
            case 2:
                if (audioObjects[2].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 2;
                }
                if (timer > beatTime)
                {
                    //Next z-Row State
                    if (yRowState == 8)
                    {
                        yRowState = 0;
                    }
                    else
                    {
                        yRowState += 1;
                    }

                    GameObject[] rowThreeArray = GameObject.FindGameObjectsWithTag("yRowOne");

                    switch (yRowState)
                    {
                        case 0:
                            for (int m = 0; m < rowThreeArray.Length; m++)
                            {
                                rowThreeArray[m].GetComponent<ShiftMgr>().MoveUp();
                            }
                            break;
                        case 1:
                        case 2:
                        case 3:
                            break;
                        case 4:
                            for (int m = 0; m < rowThreeArray.Length; m++)
                            {
                                rowThreeArray[m].GetComponent<ShiftMgr>().MoveDown();
                            }
                            break;
                        case 5:
                        case 6:
                        case 7:
                            break;
                    }
                }

                goto case 1;
            case 1:
                if(audioObjects[1].GetComponent<AudioSource>().volume == 0.0f)
                {
                    fadingTrack = 1;
                }

                if (timer > beatTime)
                {
                    timer -= beatTime * spikeBeatCount;
                    
                    //Next z-Row State
                    if(zRowState == 8)
                    {
                        zRowState = 0;
                    }
                    else
                    {
                        zRowState += 1;
                    }

                    GameObject[] rowOneArray = GameObject.FindGameObjectsWithTag("zRowOne");
                    GameObject[] rowTwoArray = GameObject.FindGameObjectsWithTag("zRowTwo");

                    switch (zRowState)
                    {
                        case 0:
                            for(int m =0; m<rowOneArray.Length; m++)
                            {
                                rowOneArray[m].GetComponent<ShiftMgr>().MoveBack();
                            }
                            for(int n = 0; n < rowTwoArray.Length; n++)
                            {
                                rowTwoArray[n].GetComponent<ShiftMgr>().MoveForward();
                            }
                            break;
                        case 1:
                        case 2:
                        case 3:
                            break;
                        case 4:
                            for (int m = 0; m < rowOneArray.Length; m++)
                            {
                                rowOneArray[m].GetComponent<ShiftMgr>().MoveForward();
                            }
                            for (int n = 0; n < rowTwoArray.Length; n++)
                            {
                                rowTwoArray[n].GetComponent<ShiftMgr>().MoveBack();
                            }
                            break;
                        case 5:
                        case 6:
                        case 7:
                            break;
                    }
                }
                break;
            default:
                //State invalid, return to default
                mState = 0;
                break;
        }

        score = Mathf.Max(Mathf.RoundToInt(GameObject.FindGameObjectWithTag("PlayerAvatar").transform.position.y / 3.47f), score);
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = score.ToString();


        if (fadingTrack != 0 && audioObjects[fadingTrack].GetComponent<AudioSource>().volume != 1.0f)
        {
            FadeInMusic(audioObjects[fadingTrack].GetComponent<AudioSource>());
        }		
	}

    void FadeInMusic(AudioSource trackSource)
    {
        if(trackSource.volume != 1.0f)
        {
            trackSource.volume += Mathf.Min(Time.deltaTime / fadeInDuration, 1.0f);
        }
        return;
    }

    public void SetState(int newState)
    {
        if(mState != newState)
        {
            timer = 1 - timer % beatTime;
            mState = newState;
        }
    }

    public void DamagePlayer()
    {
        mHealth -= 1;
        switch(mHealth)
        {
            case 2:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                break;
            case 1:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                break;
            case 0:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                break;
            case -1:
                SceneManager.LoadScene("TryAgain");
                break;
        }
        
    }

    public void HealPlayer(int newHealth)
    {
        mHealth = newHealth;
        switch (mHealth)
        {
            case 3:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = fullHeart;
                break;
            case 2:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                break;
            case 1:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = fullHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                break;
            case 0:
                heartObjs[0].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                heartObjs[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                heartObjs[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                break;
            case -1:
                HealPlayer(2);
                break;
        }
    }

    public int GetHealth()
    {
        return(mHealth);
    }
}
