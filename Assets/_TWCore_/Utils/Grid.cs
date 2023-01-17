using System;
using UnityEngine;

namespace TWCore.Utils
{
    public class Grid<TGridObject>
    {
        public event EventHandler<OnChangedGridObjectEventArgs> OnChangedGridObject;

        private int width;
        private int height;
        private float cellSize;
        private Vector3 originPosition;

        private TGridObject[,] gridArray;
        private TextMesh[,] debugTextArray;

        private GridPlane plane;

        public bool IsPositionValid(Vector3 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return x >= 0 && y >= 0 && x < width && y < height;
        }
        public bool IsPositionValid(int x, int y) =>
            x >= 0 && y >= 0 && x < width && y < height;

        #region CONSTRUCTOR
        public Grid(int width, int height, float cellSize, Vector3 originPosition, 
            Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug = false, GridPlane plane = GridPlane.XY)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.plane = plane;

            gridArray = new TGridObject[width, height];
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            if (!showDebug) return;
            Vector3 offset = GetGridPlaneVector() * cellSize / 2; ;

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = CodeUtils.CreateWorldText(gridArray[x, y]?.ToString(), null,
                                          GetWorldPosition(x, y) + offset, 
                                          Quaternion.Euler(plane == GridPlane.XY ? Vector3.zero : 90 * GetGridXVector()),
                                          16, UnityEngine.Color.white, TextAnchor.MiddleCenter) ;
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), UnityEngine.Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), UnityEngine.Color.white, 100f);
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), UnityEngine.Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), UnityEngine.Color.white, 100f);

                OnChangedGridObject += (object sender, OnChangedGridObjectEventArgs eventArgs) =>
                {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
                };
            }
        }
        #endregion

        #region GETTERS
        public Vector3 GetWorldPosition(int x, int y)
        {
            return (GetGridXVector() * x + GetGridYVector() * y) * cellSize + originPosition;
        }
        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = -1; y = -1;
            switch (plane)
            {
                case GridPlane.XY:
                    x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
                    y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
                    break;
                case GridPlane.XZ:
                    x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
                    y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
                    break;
                case GridPlane.YZ:
                    x = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
                    y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
                    break;
            }
            if (!IsPositionValid(x, y))
            {
                x = -1; y = -1;
            }
        }
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
        public TGridObject GetGridObject(int x, int y)
        {
            if (IsPositionValid(x, y) is false) return default;
            return gridArray[x, y];
        }
        public int GetWidth() => width;
        public int GetHeight() => height;
        public Vector3 GetOriginPosition() => originPosition;
        public float GetCellSize() => cellSize;
        #endregion

        #region SETTERS
        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        public void SetGridObject(int x, int y, TGridObject value)
        {
            if (IsPositionValid(x, y) is false) return;
            gridArray[x, y] = value;
            if (OnChangedGridObject != null)
                OnChangedGridObject(this, new OnChangedGridObjectEventArgs { x = x, y = y });
        }
        #endregion

        #region AUXILIARY METHODS
        private Vector3 GetGridPlaneVector()
        {
            Vector3 result = Vector3.zero;
            switch (plane)
            {
                case GridPlane.XY: result = new Vector3(1, 1, 0); break;
                case GridPlane.XZ: result = new Vector3(1, 0, 1); break;
                case GridPlane.YZ: result = new Vector3(0, 1, 1); break;
                default: break;
            }
            return result;
        }

        private Vector3 GetGridXVector()
        {
            Vector3 result = Vector3.zero;
            switch (plane)
            {
                case GridPlane.XY:
                case GridPlane.XZ: result = new Vector3(1, 0, 0); break;
                case GridPlane.YZ: result = new Vector3(0, 1, 0); break;
                default: break;
            }
            return result;
        }

        private Vector3 GetGridYVector()
        {
            Vector3 result = Vector3.zero;
            switch (plane)
            {
                case GridPlane.XY: result = new Vector3(0, 1, 0); break;
                case GridPlane.XZ:
                case GridPlane.YZ: result = new Vector3(0, 0, 1); break;
                default: break;
            }
            return result;
        }
        #endregion

        public void TriggerGridObjectChanged(int x, int y)
        {
            if (OnChangedGridObject != null)
                OnChangedGridObject(this, new OnChangedGridObjectEventArgs { x = x, y = y });
        }
    }
    public class OnChangedGridObjectEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public enum GridPlane
    {
        XY,
        XZ,
        YZ
    }
}