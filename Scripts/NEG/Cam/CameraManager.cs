using System;
using UnityEngine;
using UnityEngine.UI;

namespace NEG.Cam
{
    public class CameraManager : MonoBehaviour
    {
        public Camera cameraObject;
        public bool forceEqualCameraBounds;
        public CameraUIProperties cameraUI;

        [System.Serializable]
        public class CameraUIProperties
        {
            public Canvas canvas;
            public GameObject cameraUIObject;

            public float lineWidth = 5;
            public Color lineColor = Color.white;
        }

        private Camera[] cameras;
        private Grid cameraGrid;

        class Grid
        {
            private int numElements, numCols, numRows, gridSize;
            private bool forceEqual;

            public Grid(int numElements, bool forceEqual = false)
            {
                this.numElements = numElements;
                this.forceEqual = forceEqual;

                numCols = (int)Mathf.Ceil(Mathf.Sqrt(numElements));
                numRows = (int)Mathf.Ceil((float)numElements / numCols);

                gridSize = numCols * numRows;
            }

            public Rect GetGridRect(int elementIndex)
            {
                if (elementIndex >= numElements)
                    throw new ArgumentOutOfRangeException("Element out of grid max size!");

                int c = elementIndex % numCols;
                int r = elementIndex / numCols;

                int numRowsInCol = numRows;
                if (!forceEqual && c >= (numCols - (gridSize - numElements)))
                    numRowsInCol--;

                float colSize = 1f / numCols;
                float rowSize = 1f / numRowsInCol;

                return new Rect(c * colSize, 1 - (r + 1) * rowSize, colSize, rowSize);
            }

            public bool HasRightNeighbor(int elementIndex)
            {
                if (elementIndex >= numElements)
                    throw new ArgumentOutOfRangeException("Element out of grid max size!");

                return elementIndex % numCols < numCols - 1;
            }

            public bool HasBelowNeighbor(int elementIndex)
            {
                if (elementIndex >= numElements)
                    throw new ArgumentOutOfRangeException("Element out of grid max size!");

                int numRowsInCol = numRows;
                if (!forceEqual && (elementIndex % numCols) >= (numCols - (gridSize - numElements)))
                    numRowsInCol--;

                return elementIndex / numCols < numRowsInCol - 1;
            }
        }

        public Camera[] Initalize(int numCameras)
        {
            cameras = new Camera[numCameras];
            cameraGrid = new Grid(numCameras, forceEqualCameraBounds);

            CreateCams();
            CreateCameraUI();

            return cameras;
        }

        void CreateCams()
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                Camera cam = Instantiate(cameraObject.gameObject, transform).GetComponent<Camera>();
                cameras[i] = cam;
                cam.GetComponent<Camera>().rect = cameraGrid.GetGridRect(i);

                if (i == 0)
                    cam.gameObject.AddComponent<AudioListener>();
            }
        }

        void CreateCameraUI()
        {
            Transform holder = null;
            if (cameraUI.canvas.gameObject.scene.name == null)
                holder = Instantiate(cameraUI.canvas.gameObject).transform;
            else
                holder = cameraUI.canvas.transform;

            for (int i = 0; i < cameras.Length; i++)
            {
                Rect rect = cameraGrid.GetGridRect(i);
                RectTransform rt = new GameObject("CameraUI_" + i).AddComponent<RectTransform>();
                rt.SetParent(holder);
                rt.localScale = Vector3.one;

                rt.anchorMin = rect.min;
                rt.anchorMax = rect.max;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;

                RectTransform rtObj = Instantiate(cameraUI.cameraUIObject, rt.transform).GetComponent<RectTransform>();
                rtObj.localScale = Vector3.one;
                rtObj.offsetMin = Vector2.zero;
                rtObj.offsetMax = Vector2.zero;

                if (cameraGrid.HasRightNeighbor(i))
                    CreateUILine(rt.transform, true);
                if (cameraGrid.HasBelowNeighbor(i))
                    CreateUILine(rt.transform, false);
            }
        }

        void CreateUILine(Transform camUIRect, bool isRight)
        {
            float lw = cameraUI.lineWidth / 2;

            RectTransform rt = new GameObject("GridLine_" + (isRight ? "R" : "D")).AddComponent<RectTransform>();
            rt.SetParent(camUIRect);
            rt.localScale = Vector3.one;

            rt.anchorMin = new Vector2(isRight ? 1 : 0, 0);
            rt.anchorMax = new Vector2(1, isRight ? 1 : 0);
            rt.offsetMin = new Vector2(isRight ? -lw : 0, isRight ? 0 : -lw);
            rt.offsetMax = new Vector2(isRight ? lw : 0, isRight ? 0 : lw);

            rt.gameObject.AddComponent<Image>().color = cameraUI.lineColor;
        }
    }
}