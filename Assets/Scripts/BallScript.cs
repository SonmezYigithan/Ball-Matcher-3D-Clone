using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallScript : MonoBehaviour
{
    public BallColor ballColor;
    public float speed;
    
    private GameObject target;
    bool move = false;

    private BallMechanicsHandler ballMechanicsHandler;

    public enum BallColor
    {
        Red,
        Blue,
        Green,
        Pink
    }

    private void Start()
    {
        ballMechanicsHandler = gameObject.transform.parent.gameObject.GetComponent<BallMechanicsHandler>();
    }

    void OnMouseDown()
    {
        if (move == true)
            return;

        Debug.Log("target is " + gameObject.name);
        target = gameObject;
        ballMechanicsHandler.MoveBalls(ballColor, gameObject);

    }

    public void MoveTheBall(GameObject _target)
    {
        target = _target;
        move = true;
    }

    void Update()
    {
        MovementMethod();
    }

    void MovementMethod()
    {
        if (move)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.001f)
            {
                target.transform.position *= -1.0f;
                move = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    void HandleCollision(Collision collision)
    {
        if (gameObject == target)
            return;

        if (collision.gameObject.name.Contains("Floor"))
            return;

        if (collision.gameObject.GetComponent<BallScript>().ballColor != ballColor)
        {
            GameManager.instance.GameOver();
        }
        else if (collision.gameObject.GetComponent<BallScript>().ballColor == ballColor)
        {
            GameManager.instance.ScoreBall();
            ballMechanicsHandler.RemoveBall(gameObject);
        }
    }
}
