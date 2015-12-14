using Model;
using View;

namespace Controller {
    public class GameController {

        public static float Width
        {
            get
            {
                return GameView.unitWidth;
            }
        }

        public static float GameDifficulty
        {
            get
            {
                return GameModel.difficulty;
            }
        }

        public static float Height
        {
            get
            {
                return GameView.unitHeight;
            }
        }

        public static float deltaTime
        {
            get
            {
                return GameView.deltaTime;
            }
        }

        public static Vector mouse
        {
            get
            {
                return GameView.mouse;
            }
        }
        
        public static Player Player
        {
            get
            {
                return GameModel.player;
            }
        }

        public static Entity GetEntity(int index)
        {
            return GameModel.Entities[index];
        }

        public static int NumEntities {
            get {
                return GameModel.Entities.Count;
            }
        }

        public static bool MouseButtonDown(int btn)
        {
            return GameView.MouseButtonDown(btn);
        }

        // Use this for initialization
        public static void Start()
        {
            GameModel.Start();
        }

        // Update is called once per frame
        public static void Update()
        {
            GameModel.Update();
            GameView.Render();
        }

        public static void Reset()
        {
            GameModel.Reset();
            GameView.Reset();
            Start();
        }
    }
}