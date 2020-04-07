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
                }
            }
        }
        
        private static float _minY;

        public static float MinY
        {
            get => _minY;
            set
            {
                if (value < _minY)
                {
                    _minY = value;
                }
            }
        }
        
        public static float Width
        {
            get => _maxX - _minX;
        }

        public static float Height
        {
            get => _maxY - _minY;
        }

        public static Vector2 MidPoint
        {
            get => new Vector2((_maxX + _minX) / 2f, (_maxY + _minY) / 2f);
        }
        public static void ResetTreesBound()
        {
            _maxX = _minX = _maxY = _minY = 0f;
        }
    }

    public static void CompareWithTreesBound(Vector2 position)
    {
        TreesBound.MaxY = position.y;
        TreesBound.MinY = position.y;
        TreesBound.MaxX = position.x;
        TreesBound.MinX = position.x;
    }
    
}
