using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMechanicsHandler : MonoBehaviour
{
    // keyi renk listesi tutabilir
    Dictionary<GameObject, BallScript.BallColor> Balls = new Dictionary<GameObject, BallScript.BallColor>();

    [Tooltip("3 is default speed")]
    public float ballSpeed;

    private GamePanelHandle gamePanelHandler;

    private void Awake()
    {
        gamePanelHandler = GameObject.FindGameObjectWithTag("PanelHandler").GetComponent<GamePanelHandle>();
        gamePanelHandler.UpdateGameSpeedPanel(ballSpeed-2);

        foreach (Transform ball in gameObject.transform)
        {
            ball.GetComponent<BallScript>().speed = ballSpeed;
            Balls.Add(ball.gameObject, ball.GetComponent<BallScript>().ballColor);
        }
    }

    public void MoveBalls(BallScript.BallColor targetcolor, GameObject targetObj)
    {
        foreach(KeyValuePair<GameObject, BallScript.BallColor> entry in Balls)
        {
            if (entry.Key != targetObj && entry.Value == targetcolor)
            {
                // burada renk countu tutup sýfýr olursa targeti destroy edebilirim
                entry.Key.GetComponent<BallScript>().MoveTheBall(targetObj);
            }
        }
    }

    public void RemoveBall(GameObject obj)
    {
        var colorToBeChecked = Balls[obj];
        Balls.Remove(obj);
        Destroy(obj);

        CheckBallCountForColor(colorToBeChecked);
    }

    void CheckBallCountForColor(BallScript.BallColor color)
    {
        int count = 0;
        KeyValuePair<GameObject, BallScript.BallColor> objectToRemove;

        if(Balls.Count <= 1)
        {
            GameManager.instance.LevelCleared();
        }

        foreach (KeyValuePair<GameObject, BallScript.BallColor> entry in Balls)
        {
            if (entry.Value == color)
            {
                count++;
                objectToRemove = entry;
            }
        }

        if(count == 1)
        {
            var lastBall = objectToRemove.Key;
            Balls.Remove(objectToRemove.Key);
            Destroy(lastBall);
        }
    }
}
