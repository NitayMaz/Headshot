using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Gun : MonoBehaviour
{
    public bool spinning = false;
    public int revolutionsBeforeStop = 3;
    public float spinDuration = 2.0f;
    public UiHandler uiHandler;
    private Coroutine spinningCoroutine;


    public void clickedGun()
    {
        if(spinning)
        {
            return;
        }
        Debug.Log("Spin");
        spinningCoroutine =  StartCoroutine(spinSprite());
    }

    [ContextMenu("spin")]
    IEnumerator spinSprite()
    {
        int players = uiHandler.playerCount;
        spinning = true;
        int chosenPlayer = Random.Range(0,players);
        float endRotaton = 360 * revolutionsBeforeStop + chosenPlayer*(360/players);
        float startRotation = transform.eulerAngles.z;

        float timeSpinning = 0;
        while(timeSpinning < spinDuration)
        {
            float rotation = Mathf.Lerp(startRotation, endRotaton, timeSpinning / spinDuration);
            transform.eulerAngles = new Vector3(0, 0, rotation);
            timeSpinning += Time.deltaTime;
            yield return null;
        }
        spinning = false;
        transform.eulerAngles = new Vector3(0, 0, endRotaton);
    }

    public void StopSpinning()
    {
        StopCoroutine(spinningCoroutine);
        spinning = false;
    }
}
