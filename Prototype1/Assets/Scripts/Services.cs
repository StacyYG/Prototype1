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

    public struct TreesBound
    {
        private static float _maxY;

        public static float MaxY
        {
            get => _maxY;
            set
            {
                if (value > _maxY)
                {
                    _maxY = value;
                    Debug.Log("_maxY " + _maxY);
                }
            }
        }
    
        private static float _maxX;

        public static float MaxX
        {
            get => _maxX;
            set
            {
                if (value > _maxX)
                {
                    _maxX = value;
                    Debug.Log("_maxX " + _maxX);
                }
            }
        }
    
        private static float _minX;

        public static float MinX
        {
            get => _minX;
            set
            {
                if (value < _minX)
                {
                    _minX = value;
                    Debug.Log("_minX " + _minX);
                }
            }
        }
    }

    public static void CompareWithTreesBound(Vector3 position)
    {
        TreesBound.MaxY = position.y;
        TreesBound.MaxX = position.x;
        TreesBound.MinX = position.x;
    }
}
