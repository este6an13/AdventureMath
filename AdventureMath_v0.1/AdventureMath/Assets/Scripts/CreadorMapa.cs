using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;

// COMPONENTE usado por el Creador del Mapa para crear el mapa como tal
public class CreadorMapa : MonoBehaviour
{
    [SerializeField]  // Permite la visualización de atributos privados en el inspector
    private Transform map;
    [SerializeField]
    private Texture2D[] mapData; // Un arreglo de texturas: Capas del mapa, dibujos paint
    [SerializeField]
    private MapElement[] mapElements; // Un arreglo de elementos del mapa: Hierba, Arena, Agua, ver clase definida más abajo
    [SerializeField] 
    private Sprite defaultTile;

    private Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();

    [SerializeField]
    private SpriteAtlas waterAtlas;

    public AdminJuego aj;
    

    private Vector3 worldStartPos // Vector 3D Inicio del Mundo
    {
        get { return new Vector3(-11.40396f, -6.44f, 0); }
    }

    // Start is called before the first frame update -0.54123
    void Start()
    {
        GenerateMap(0, aj.tilesAldeaPacaembu); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Instancia prefabs (tiles) en una matriz 2D para crear el mapa durante el RunTime
    public void GenerateMap(int i, List<GameObject> listaTiles)
    {
        for (int x = 0; x < mapData[i].width; x++)
        {
            for (int y = 0; y < mapData[i].height; y++)
            {
                Color c = mapData[i].GetPixel(x, y); // Retorna el color del pixel en dicha coordinada

                MapElement newElement = Array.Find(mapElements, e => e.MyColor == c); // Busca el elemento del mapa que tenga el color c

                if (newElement != null) // Si se encuentra ...
                {
                    float xPos = worldStartPos.x + (defaultTile.bounds.size.x * x);
                    float yPos = worldStartPos.y + (defaultTile.bounds.size.y * y);
                        
                    GameObject go = Instantiate(newElement.MyElementPrefab);
                    listaTiles.Add(go);
                    go.transform.position = new Vector2(xPos, yPos); // Ubica los prefabs Hierba, Arena o Agua que instanciados, en la posición adecuada

                    if (newElement.MyTileTag == "Water")
                    {
                        waterTiles.Add(new Point(x, y), go);
                    }

                    go.transform.parent = map; // Asigna el transform map al padre del prefab, es decir el Mapa
                }
            }
        }

        CheckWater(i, listaTiles);
    }

    private void CheckWater(int i, List<GameObject> listaTiles)
    {
        foreach (KeyValuePair<Point, GameObject> tile in waterTiles)
        {
            string composition = TileCheck(tile.Key);

            if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("0");
            }
            if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("1");
            }
            if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("2");
            }
            if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("3");
            }
            if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("4");
            }
            if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("5");
            }
            if (composition[1] == 'W' && composition[4] == 'W' && composition[3] == 'E' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("6");
            }
            if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("7");
            }
            if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("8");
            }
            if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("9");
            }
            if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'W')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("10");
            }
            if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("11");
            }
            if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("12");
            }
            if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
            {
                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("13");
            }
            if (composition[3] == 'W' && composition[5] == 'E' && composition[6] == 'W')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity, map);
                listaTiles.Add(go);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("14");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (composition[1] == 'W' && composition[2] == 'E' && composition[4] == 'W')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity, map);
                listaTiles.Add(go);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("15");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'E')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity, map);
                listaTiles.Add(go);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("16");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (composition[0] == 'E' && composition[1] == 'W' && composition[3] == 'W')
            {
                GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity, map);
                listaTiles.Add(go);
                go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("17");
                go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'E')
            {

                tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("18");

            }
            if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
            {
                int randomTile = UnityEngine.Random.Range(0, 100);
                if (randomTile < 15)
                {
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("19");
                }
            }
            if (composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'W' && composition[6] == 'W')
            {
                int randomTile = UnityEngine.Random.Range(0, 100);
                if (randomTile < 10)
                {
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("20");
                }

            }
        }   
    }

    public string TileCheck(Point currentPoint)
    {
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (waterTiles.ContainsKey(new Point(currentPoint.MyX + x, currentPoint.MyY + y)))
                    {
                        composition += "W";
                    }
                    else
                    {
                        composition += "E";
                    }
                }
            }
        }
        
        return composition;
    }
}

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color color;
    
    [SerializeField]
    private GameObject elementPrefab;

    // Métodos de acceso para cada atributo
    public GameObject MyElementPrefab 
    {
        get { return elementPrefab; }
    }
    
    public Color MyColor
    {
        get { return color; }
    }
    
    public string MyTileTag
    {
        get { return tileTag; }
    }
}

public struct Point
{
    public int MyX { get; set; }
    public int MyY { get; set; }

    public Point(int x, int y)
    {
        this.MyX = x;
        this.MyY = y;
    }
}
