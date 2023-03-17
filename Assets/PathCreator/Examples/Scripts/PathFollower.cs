using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed;
        [SerializeField] Transform zombieTf;
        float distanceTravelled;


        private void OnEnable()
        {
            distanceTravelled = 0;
            PathCreator[] pathList = LevelController.instance.CurrentLevel.Spawner.pathCreatorsList;
            pathCreator = pathList[Random.Range(0, pathList.Length)];
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }
        void Update()
        {
            if (pathCreator != null)
            {
                if (GameManager.instance.IsState(Enums.GameState.Playing))
                {
                    distanceTravelled += speed * Time.deltaTime;
                    zombieTf.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    zombieTf.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                    zombieTf.rotation = Quaternion.Euler(0, zombieTf.rotation.eulerAngles.y, 0);
                }
                else
                {
                    zombieTf.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    zombieTf.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                    zombieTf.rotation = Quaternion.Euler(0, zombieTf.rotation.eulerAngles.y, 0);
                }
                
            }
            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(zombieTf.position);
        }
    }
}