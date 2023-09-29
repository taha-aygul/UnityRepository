
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject foodPrefab;

    float columnLenght = 3;
    [SerializeField] int columnInterval = 3;


    float rowLenght = 3;
    [SerializeField] Vector2Int obstacleValueRange;
    [SerializeField] Vector2Int foodValueRange;
    [SerializeField] Vector2Int foodCountRange;
    private int foodCount;

    private float columnCount;
    private float rowCount;
    private int possibilityOfRow = 30;

    public int roadCount; // bir nevi level gibi kullanýcam her road arttýðýnda sayýlar artcak

    public static RoadGenerator Instance;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        roadCount = 0;
        rowLenght = boxPrefab.transform.lossyScale.z;
        columnLenght = boxPrefab.transform.lossyScale.x;

        columnCount = roadPrefab.transform.lossyScale.x / columnLenght;
        rowCount = roadPrefab.transform.lossyScale.z / rowLenght;
    }




    public void GenerateRoad(Transform currentRoad)
    {
        roadCount++;
        obstacleValueRange += Vector2Int.up;
        foodValueRange += Vector2Int.up;

        List<Vector3> boxPos = new List<Vector3>();
        List<Vector3> foodPos = new List<Vector3>();

        Vector3 pos = currentRoad.transform.position - (currentRoad.transform.lossyScale.x) * Vector3.right;
        GameObject road = Instantiate(roadPrefab, pos, Quaternion.identity);

        Vector3 point = (road.transform.position) + new Vector3(road.transform.lossyScale.x / 2, 1.8f, 0);

        for (int i = 0; i < columnCount; i += columnInterval)
        {

            point.z = (roadPrefab.transform.lossyScale.z / 2) - boxPrefab.transform.lossyScale.z / 2; // Z pozisyonunu yolun kenarýna eþitliyor

            foodCount = (int)Random.Range(foodCountRange.x, foodCountRange.y);

            for (int j = 0; j < foodCount; j++)
            {
                Vector3 spawnPoint = point - new Vector3(Random.Range(1,columnInterval)*columnLenght  ,point.y, Random.Range(1, rowCount-1) * rowLenght);

                if (foodPos.Count !=0)
                {
                    for (int k = 0; k < foodPos.Count; k++)
                    {
                        while(spawnPoint == foodPos[k])
                        {
                            spawnPoint = point - new Vector3(Random.Range(1, columnInterval) * columnLenght, point.y, Random.Range(1, rowCount - 1) * rowLenght);
                        }
                    }
                }
                GameObject food = Instantiate(foodPrefab, spawnPoint, Quaternion.identity);
                food.GetComponent<Food>().foodNumber = (int)Random.Range(foodValueRange.x, foodValueRange.y);
                food.transform.SetParent(road.transform);     // Parent ayarlanýr ancak food un scale ý parente göre deðiþir bu yüzden unparent yapýp scale ayarlayýp sonra tekrar parent yapýlýr
                food.transform.parent = null;
                food.transform.localScale = foodPrefab.transform.localScale;
                food.transform.parent = road.transform;
            }

            //point = nextPoint;
            point -= new Vector3(columnInterval * columnLenght, 0, 0);

            //  if (randomPossibility(possibilityOfRow)) { continue; }  // Yüzde 70 ihtimalle satýrda en az 1 engel olucak

            for (int j = 0; j < rowCount; j++)
            {
                GameObject box = Instantiate(boxPrefab, point, Quaternion.identity);     // Box üretilir o pozisyonda
                boxPos.Add(point);
                box.GetComponent<Block>().breakNumber = (int)Random.Range(obstacleValueRange.x, obstacleValueRange.y);
                box.transform.SetParent(road.transform);     // Parent ayarlanýr ancak box un scale ý parente göre deðiþir bu yüzden unparent yapýp scale ayarlayýp sonra tekrar parent yapýlýr
                box.transform.parent = null;
                box.transform.localScale = boxPrefab.transform.localScale;
                box.transform.parent = road.transform;

                point += new Vector3(0, 0, -boxPrefab.transform.lossyScale.z);      // Satýr satýr pozisyon ilerletilir box un büyüklüðüne göre
            }
        }

        // TODO bonuslaer







    }


    private bool randomPossibility(int lessThan)
    {
        float a = Random.Range(0, 100);

        if (a < lessThan)
        {
            return true;
        }
        return false;

    }



}
