using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region CheatsCodes
    [SerializeField] private string _buffer;
    [SerializeField] private float _maxTimeDif = 1;
    private List<string> _validPatterns = new() { "UUDDLRLRBB" };
    private float _timeDif;
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        #region CheatsCode
        _timeDif = _maxTimeDif;
        #endregion
    }

    private void Update()
    {
        #region CheatsCode
        _timeDif -= Time.deltaTime;
        if( _timeDif <= 0)
        {
            _buffer = "";
        }

        if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            AddToBuffer("U");
        }
        
        if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            AddToBuffer("D");
        }
        
        if(Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            AddToBuffer("R");
        }
        
        if(Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            AddToBuffer("L");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AddToBuffer("B");
        }

        CheckPatterns();
        #endregion
    }

    #region CheatsCode
    private void AddToBuffer(string c)
    {
        _timeDif = _maxTimeDif;
        _buffer += c;
    }

    private void CheckPatterns()
    {
        if (_buffer.EndsWith(_validPatterns[0]))
        {
            Debug.Log("Cheat Activado");
            _buffer = "";
        }
    }
    #endregion
}