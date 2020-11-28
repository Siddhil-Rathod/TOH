using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Disc : MonoBehaviour
{
    public static Disc Instance;
    // public static int moves = 0;
    public int moves = 0;
    public int size = 0; // The size of the ring. Larger value = bigger ring
    public int poleNumber = 0; // The pole number this ring is on. Either 0,1, 2
    public Transform[] polePositions;
    public TextMeshProUGUI movesText;

    bool isBeingDragged = false;//To check if a disc is being dragged

    void Start()
    {
        DiscPositions.Instance.poles[poleNumber].Add(this as Disc);//Initially the pole number is 0
        Instance = this;
    }

    public void EnableText()//To display the invalid move text
    {
        DiscPositions.Instance.invalidMove.SetActive(true);
    }

    public void DisableText()//To hide the invalid move text after 1 second
    {
        DiscPositions.Instance.invalidMove.SetActive(false);
    }
        

    void OnMouseDown()//When a disc on a pole is clicked
    {
        // Find the minimum ring size on this pole
        int minSize = 99999;
        foreach (Disc r in DiscPositions.Instance.poles[poleNumber])
        {
            if (r.size < minSize)
            {
                minSize = r.size;//minSize is the smallest available disc on the pole
            }
        }

        // Can only move the ring if it is the minimum size ring on the pole
        if (size == minSize)//if the selected disc is the smallest disc on the pole
        {
            isBeingDragged = true;//The disc can be dragged
            this.GetComponent<BoxCollider2D>().isTrigger = true;//Object collision is set off
            this.GetComponent<SpriteRenderer>().sortingOrder = 2;//The selected disc can hover over the other discs
        }
        else
        {
           Invoke(nameof(EnableText),0);
            Invoke(nameof(DisableText), 1f);
        }
    }

    void OnMouseDrag()
    {
        if (isBeingDragged)//if a valid disc is selected and is being dragged
        {
            // Update position of this object to follow the mouse
            Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    void OnMouseUp()
    {
        if (isBeingDragged)
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;//Box collision is set on
            this.GetComponent<SpriteRenderer>().sortingOrder = 1;
            // Find the position of the nearest pole
            float closestDistance = 9999f;
            int nearestPoleNumber = poleNumber; // Either 1, 2 or 3
            for (int i = 0; i < polePositions.Length; ++i)
            {
                if (closestDistance > Vector3.Magnitude(transform.position - polePositions[i].position))
                {
                    closestDistance = Vector3.Magnitude(transform.position - polePositions[i].position);
                    nearestPoleNumber = i;
                }
            }
            // Make sure we have dragged it to different pole
            if (poleNumber != nearestPoleNumber)
            {
                // Find the smallest ring on the new pole
                int minSize = 99999;
                foreach (Disc r in DiscPositions.Instance.poles[nearestPoleNumber])
                {
                    if (r.size < minSize)
                    {
                        minSize = r.size;
                    }
                }

                // This ring must be smaller if we can put it on this pole
                if (size < minSize)
                {
                    DiscPositions.Instance.poles[poleNumber].Remove(this as Disc); 
                    DiscPositions.Instance.poles[nearestPoleNumber].Add(this as Disc);
                    // Change the position to the new pole
                    transform.position = new Vector3(polePositions[nearestPoleNumber].position.x, 1.05f, 0);
                    DiscPositions.Instance.lastDisc = size;//store the latest disc moved for the undo function
                    DiscPositions.Instance.lastPole = poleNumber;//store the pole from where the disc was moved 
                    poleNumber = nearestPoleNumber;
                    //For updating the number of moves
                    Instance.moves++;
                    movesText.text = "Moves : " + Instance.moves.ToString("00");
                }
                else
                {
                    // Change position back to the pole it was on
                        Invoke(nameof(EnableText), 0);
                        Invoke(nameof(DisableText), 1f);
                    transform.position = new Vector3(polePositions[poleNumber].position.x, 1.05f, 0);
                }
            }
            else
            {
                // Change position back to the pole it was on
                transform.position = new Vector3(polePositions[nearestPoleNumber].position.x, 1.05f, 0);
                    Invoke(nameof(EnableText), 0);
                    Invoke(nameof(DisableText), 1f);
            }
            isBeingDragged = false;
        }
    }
}
