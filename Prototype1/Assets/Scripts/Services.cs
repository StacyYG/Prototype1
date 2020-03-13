using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    private static EventManager _eventManager;

    public static EventManager EventManager
    {
        get
        {
            Debug.Assert(_eventManager != null);
            return _eventManager;
        }
        set => _eventManager = value;
    }

    private static List<Player> _players;

    public static List<Player> Players
    {
        get
        {
            Debug.Assert(_players != null);
            return _players;
        }
        set => _players = value; 
    }

    private static CameraController _cameraController;

    public static CameraController CameraController
    {
        get
        {
            Debug.Assert(_cameraController != null);
            return _cameraController;
        }
        set => _cameraController = value;
    }

    private static Control _control;

    public static Control Control
    {
        get
        { 
            Debug.Assert(_control !=null);
            return _control;
        }
        set => _control = value;
    }

    private static List<TreeGrowControl> _trees;

    public static List<TreeGrowControl> Trees
    {
        get
        {
            Debug.Assert(_trees != null);
            return _trees;
        }
        set => _trees = value;
    }

    private static ScoreBoard _scoreBoard;

    public static ScoreBoard ScoreBoard
    {
        get
        {
            Debug.Assert(_scoreBoard != null);
            return _scoreBoard;
        }
        set => _scoreBoard = value;
    }
    
    
}
