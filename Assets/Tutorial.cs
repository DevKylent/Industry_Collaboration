using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Level 2 Tutorial"))
        {
            StartCoroutine(WaitforLoadScreen());
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Level 3 Tutorial"))
        {
            Destroy(other.gameObject);
            changeposition();
            CharactersSwap.Instance.changeCharacter(1);
        }
    }
    private IEnumerator changeposition()
    {
        yield return new WaitForSeconds(0.2f);
    }
    private IEnumerator WaitforLoadScreen()
    {
        yield return new WaitForSeconds(1f);
        CharactersSwap.Instance.changeCharacter(1);
        changeposition();
        CharactersSwap.Instance.Swap();
    }
}
