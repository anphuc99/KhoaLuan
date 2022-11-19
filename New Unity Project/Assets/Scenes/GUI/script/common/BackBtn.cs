using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        Event.emit(Events.goBack, null);
    }
}
