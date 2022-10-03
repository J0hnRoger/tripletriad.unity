using System.Collections;
using System.Linq;
using TripleTriad.Core.EventArchitecture;
using TripleTriad.WorldScene;
using UnityEngine;

public class WorldSceneView : MonoBehaviour
{
    [SerializeField] public  LevelsRuntimeSet Levels;
    [SerializeField] private GameObject _levelNodeTemplate;
    [SerializeField] public MapLevelData _runtimeCurrentLevel;
    [SerializeField] private SceneEventDispatcher _sceneDispatcher; 
    [SerializeField] private int _distanceBetweenNodes;

    private int sliderIndex = 0;
    
    private void Awake()
    {
       CreateWorldMap(); 
    }

    public void SlidePreviousWorld()
    {
        if (sliderIndex != 0)
        {
            StartCoroutine(MoveSlider(4));
            sliderIndex--;
        }
    }
    
    public void SlideNextWorld()
    {
        if (sliderIndex  < Levels.Items.Count - 1)
        {
            StartCoroutine(MoveSlider(-4));
            sliderIndex++;
        }
    }

    private IEnumerator MoveSlider(float movement)
    {
        float lerpDuration = 0.3f;
        float timeElapsed = 0;
        float startValue = transform.position.x; 
        float endValue = transform.position.x + movement;
        
        while (timeElapsed < lerpDuration)
        {
            float newX = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);  
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(endValue, transform.position.y, transform.position.z);  
    }
    
    public void CreateWorldMap()
    {
        float x = transform.position.x;
        foreach (MapLevelData level in Levels.Items)
        {
            var newNode = Instantiate(_levelNodeTemplate,
                new Vector3(x,0,0),
                Quaternion.identity, gameObject.transform);

            var nodeView = newNode.GetComponent<WorldNodeView>();
            nodeView.IsAvailable = CanVisitLevel(level); 
            nodeView.Node = level;
            nodeView.Render(); 
            nodeView.OnVisitedNode += VisitNodeIfPossible;

            x += _distanceBetweenNodes;
        }
    }

    private bool CanVisitLevel(MapLevelData level)
    {
        int currentLevel = Levels.Items.IndexOf(level);
        return Levels.Items.Take(currentLevel).All(c => c.IsVisited);
    }

    private void VisitNodeIfPossible(WorldNodeView targetLevel)
    {
        if (CanVisitLevel(targetLevel.Node) 
            && !targetLevel.Node.IsVisited)
        {
            _runtimeCurrentLevel.Set(targetLevel.Node);
            _sceneDispatcher.LoadScene();            
        } else 
            Debug.Log("Niveau non-accessible ou déjà visité");
    }
}
