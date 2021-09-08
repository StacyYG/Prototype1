using System.Collections.Generic;
using System.Linq;
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
    
    public static Player CurrentPlayer => Players.Last();

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

    public static int treeCount;

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

    public struct TreeRange
    {
        private static float _maxY, _minY, _maxX, _minX;

        private static float TopBorderY => _maxY + 10f; // Include some space above the trees to look nicer
        private static float LowBorderY => _minY;
        
        public static float Width
        {
            get => _maxX - _minX;
        }

        public static float Height
        {
            get => TopBorderY - LowBorderY;
        }

        public static Vector2 MidPoint
        {
            get => new Vector2((_maxX + _minX) / 2f, (TopBorderY + LowBorderY) / 2f);
        }
        
        public static void Reset()
        {
            _maxX = 0f;
            _minX = 0f;
            _maxY = 0f;
            _minY = 0f;
        }
        
        public static void Update(Vector2 position)
        {
            if (_maxX < position.x)
                _maxX = position.x;
            else if (_minX > position.x)
                _minX = position.x;

            if (_maxY < position.y)
                _maxY = position.y;
            else if (_minY > position.y)
                _minY = position.y;
        }
    }
}
