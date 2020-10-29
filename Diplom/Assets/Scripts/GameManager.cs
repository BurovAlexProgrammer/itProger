using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    bool lockCursorOnStart;

    bool _lockCursor;

    bool isPropChanged;
    public bool lockCursor
    {
        get { return _lockCursor; }
        set { isPropChanged = _lockCursor != value; _lockCursor = value; if (isPropChanged) MouseLockChanged?.Invoke(); }
    }

    UnityEvent MouseLockChanged = new UnityEvent();

    private void Awake()
    {
        MouseLockChanged.AddListener(OnLockMouse);
    }

    private void Start()
    {
        MouseLockChanged?.Invoke();
        if (lockCursorOnStart)
            lockCursor = true;
    }

    void OnLockMouse()
    {
        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

    }
}
