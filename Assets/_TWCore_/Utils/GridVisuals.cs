using System.Collections.Generic;
using UnityEngine;

namespace TWCore.Utils
{
    public abstract class GridVisuals<TGridObject> : MonoBehaviour
    {
        protected Grid<TGridObject> grid;
        protected List<Transform> visualNodeList;
        protected bool visualsToUpdate;

        private void Update()
        {
            if (visualsToUpdate)
            {
                visualsToUpdate = false;
                UpdateVisuals(grid);
            }
        }
        public void Setup(Grid<TGridObject> grid)
        {
            this.grid = grid;
            visualNodeList = new List<Transform>();

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    Vector3 worldPosition = grid.GetWorldPosition(x, y);
                    Transform visualNode = CreateVisualNode(worldPosition, grid.GetGridObject(x, y));
                    if (visualNode != null) visualNodeList.Add(visualNode);
                }
            }

            UpdateVisuals(grid);

            grid.OnChangedGridObject += OnChangedGridObject;
        }
        public void ClearVisuals()
        {
            foreach (var visual in visualNodeList)
            {
                Destroy(visual.gameObject);
            }
            visualNodeList.Clear();
        }

        #region ABSTRACT METHODS
        public abstract Transform CreateVisualNode(Vector3 worldPosition, TGridObject gridObject);
        public abstract void UpdateVisuals(Grid<TGridObject> grid);
        protected abstract void OnChangedGridObject(object sender, OnChangedGridObjectEventArgs e);
        #endregion
    }
}