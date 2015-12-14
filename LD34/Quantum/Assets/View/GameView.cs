using UnityEngine;

using Controller;
using Model;

namespace View
{
    public class GameView : MonoBehaviour
    {

        public static float deltaTime
        {
            get
            {
                return Time.deltaTime;
            }
        }

        public static Vector mouse
        {
            get
            {
                Vector3 v = Input.mousePosition;
                v.z = 10.0f;
                v = Camera.main.ScreenToWorldPoint(v);
                return FromWorldVector(v);
            }
        }


        public static GameView instance;
        private static GameObject statsBar;
        private static GameObject scoreCounter;
        private static GameObject livesCounter;
        private static GameObject instructions;
        private static GameObject tacoSystem;

        public static float unitWidth = 400;

        private static Camera mainCamera;
        public static float ViewHeight;
        public static float ViewWidth;

        public static float unitHeight;


        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
            ViewHeight = 2f * mainCamera.orthographicSize;
            ViewWidth = ViewHeight * mainCamera.aspect;

            unitHeight = unitWidth * ViewHeight / ViewWidth;
            instance = this;

            GameController.Start();

            transform.localScale = new Vector3(ViewWidth, ViewHeight);
            statsBar = GameObject.Find("Stats Bar");
            statsBar.transform.position = new Vector3(-ViewWidth / 3, ViewWidth / 5);
            scoreCounter = GameObject.Find("Score");
            livesCounter = GameObject.Find("Lives");
            instructions = GameObject.Find("Instructions");
            tacoSystem = GameObject.Find("Taco System");
            tacoSystem.GetComponent<Renderer>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            GameController.Update();
            instructions.transform.position -= new Vector3(0.5f * Time.deltaTime * GameController.GameDifficulty, 0);
            if (Input.GetMouseButtonDown(1))
            {
                tacoSystem.GetComponent<Renderer>().enabled = !tacoSystem.GetComponent<Renderer>().enabled;
            }
        }

        public static bool MouseButtonDown(int btn)
        {
            return Input.GetMouseButtonDown(btn);
        }

        public static Vector3 ToWorldVector(float x, float y)
        {
            return new Vector3(x / unitWidth,
                y / unitHeight);
        }

        public static Vector FromWorldVector(Vector3 v)
        {
            return new Vector((v.x + ViewWidth / 2) * unitWidth / ViewWidth,
                -(v.y - ViewHeight / 2) * unitHeight / ViewHeight);
        }

        public static void Render()
        {
            for (int i = GameController.NumEntities - 1; i >= 0; i--)
            {
                Sprites.Render(GameController.GetEntity(i));
            }
            Sprites.HideSpareSprites();

            scoreCounter.GetComponent<TextMesh>().text =
                 "\u00D7" + GameController.Player.Score.ToString();
            livesCounter.GetComponent<TextMesh>().text =
                 "\u00D7" + GameController.Player.Lives.ToString();
        }

        public static void Reset()
        {
            // Reset the view to its original state
            Sprites.Reset();
        }

        public static void Log(string msg)
        {
            Debug.Log(msg);
        }
    }
}