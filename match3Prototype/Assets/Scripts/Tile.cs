using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CLASE TILE
/// clase que contiene los datos de una tile: tipo, coordenadas
/// detecta si se ha clicado con el raton encima de la misma
/// </summary>
public class Tile : MonoBehaviour {

    //variables que guardan el tipo coordenadas que tiene una tile
    public int type;
    public int x, y;

    //variable utilizada para llamar a los metodos del gameController
    public GameController controller;

    //variables que guardan el color inicial del sprite y el sprite renderer para modificar su color
    SpriteRenderer sr;
    Color initialColor;

    //obtenemos el sprite renderer y guardamos el color inicial de la tile
    private void Start() {

        sr = GetComponent<SpriteRenderer>();
        initialColor = sr.color;
    }

    /// <summary>
    /// asigna las propiedades que tiene esta tile
    /// </summary>
    /// <param name="_controller"></param>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    public void setTile(GameController _controller, int _x, int _y) 
    {
        controller = _controller;
        x = _x;
        y = _y;

    }

    /// <summary>
    /// asigna el tipo del que es la tile. por defecto viene definido en variable publica
    /// </summary>
    /// <param name="_type"></param>
    public void setType(int _type) {

        type = _type;
    }
  
    /// <summary>
    /// metodo que devuelve la coordenadaX
    /// </summary>
    /// <returns></returns>
    public int getX() {
        return x;
    }

    /// <summary>
    /// Metodo que devuelve la coordenada Y
    /// </summary>
    /// <returns></returns>
    public int getY() {
        return y;
    }

    /// <summary>
    /// Metodo que devuelve el tipo
    /// </summary>
    /// <returns></returns>
    public int getType() {

        return type;
    }

    /// <summary>
    /// Metodo que devuelve una referencia a esta misma tile
    /// </summary>
    /// <returns></returns>
    public Tile getTile() {

        return this;
    }

    //Detecta si se ha pulsado con el raton y si es asi llama a la funcion de establecer seleccion del controlador
    private void OnMouseDown() {

        //Debug.Log("Select tile: " + x+","+ y);
        controller.setTileSelected(x, y, this);
        
    }

    /// <summary>
    /// Devuelve el color inicial al sprite
    /// </summary>
    public void returnColor() {

        sr.color = initialColor;
    }

    /// <summary>
    /// Cambia el color del sprite a verde
    /// </summary>
    public void setColorSelection() {

        sr.color = Color.green;
    }

    /// <summary>
    /// Metodo para destruir este objeto tile
    /// </summary>
    public void destroyTile() {

        Destroy(this.gameObject);
    }


}
