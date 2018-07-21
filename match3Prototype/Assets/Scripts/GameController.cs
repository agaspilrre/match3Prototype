using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Clase GameController
/// clase encargada de gestionar el grid y flujo del juego match3
/// </summary>
public class GameController : MonoBehaviour {

    //variables para guardar tiles instanciadas en un grid
    Tile[,] grid;
    Tile instance;

    //array que guarda todos los prefabs de las piezas ha utilizar 6 en total
    public Tile[] prefabsTile;

    //variables que definen el tamaño del grid
    public int sizeX;
    public int sizeY;

    //variables que guardan la fila y la columna de las dos tiles seleccionadas
    int firtsSelectionColumn;
    int firstSelectionRow;
    int secondSelectionRow;
    int secondSlectionColumn;

    //variables que guardan las tiles seleccionadas
    Tile firtsTile;
    Tile SecondTile;

    //variables encargadas de guardar puntuacion y mostrarla por pantalla
    public Text scoreView;
    public GameObject verticalText;
    public GameObject horizontalText;

    int score;

    //establece valores iniciales a las variables y crea el grid instanciando en sus posiciones tiles
    void Start() 
    {
        scoreView.text = "SCORE: " + score;
        verticalText.SetActive(false);
        horizontalText.SetActive(false);
        //ampliamos el tamaño del grid para poder colocar piezas arriba de las que se visualizan y hacer comprobaciones
        grid = new Tile[sizeX * 3, sizeY * 3];
        firstSelectionRow = -1;
        firtsSelectionColumn = -1;
        secondSelectionRow = -1;
        secondSlectionColumn = -1;

        for(int row=0; row < sizeX; row++) {

            for(int column=0; column < sizeY; column++) {

                instantiateTile(row,column);
            }
        }

        //checkNumbersGrid();
        checkMatch();
        checkVerticalMatch();
    }

    /// <summary>
    /// instancia una tile en el grid en la posicion dada por parametros
    /// </summary>
    /// <param name="_row"></param>
    /// <param name="_column"></param>
    public void instantiateTile(int _row, int _column) {

        instance = Instantiate(prefabsTile[Random.Range(0, prefabsTile.Length)], new Vector3(_column, _row), Quaternion.identity);
        grid[_row, _column] = instance;
        instance.setTile(this, _row, _column);

    }

    /// <summary>
    /// Metodo que comprueba las tiles seleccionadas 
    /// comprueba sus posiciones en el tablero y decide los movimientos posibles y si se puede realizar intercambio
    /// </summary>
    /// <param name="_row"></param>
    /// <param name="_column"></param>
    /// <param name="_tile"></param>
    public void setTileSelected(int _row, int _column, Tile _tile) {
        
        //si no hay ninguna tile seleccionada se guarda en la primera seleccion
        if(firtsSelectionColumn==-1 && firstSelectionRow == -1) {
            firstSelectionRow = _row;
            firtsSelectionColumn = _column;
            firtsTile = _tile;
            firtsTile.setColorSelection();
        }
        //si la primera tile contiene informacion guardamos segunda y hacemos comprobaciones
        //para determinar movimientos y si es posible el cambio de posicion de dichas tiles
        else {

            //compobar esquinas
            if(firstSelectionRow==0 && firtsSelectionColumn==0) {
                //mirar arriba y derecha
                if((_row==firstSelectionRow+1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow &&_column == firtsSelectionColumn + 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }
            }

            else if(firstSelectionRow==7 && firtsSelectionColumn==0) {
                //mirar abajo y derecha
                if ((_row == firstSelectionRow - 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn + 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }
            }

            else if (firstSelectionRow == 0 && firtsSelectionColumn == 7) {
                //mirar arriba y izquierda
                if ((_row == firstSelectionRow + 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn - 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }
            }

            else if (firstSelectionRow == 7 && firtsSelectionColumn == 7) {
                //mirar abajo e izquierda
                if ((_row == firstSelectionRow - 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn - 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }
            }

            //comprobar bordes del tablero
            //primera fila
            else if (firstSelectionRow == 0 && firtsSelectionColumn != 0 && firtsSelectionColumn != 7) {
                //comprobamos arriba izquierda y derecha
                if ((_row == firstSelectionRow + 1 && _column==firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn -1) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn + 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }

            }

            //ultima fila
            else if (firstSelectionRow == 7 && firtsSelectionColumn != 0 && firtsSelectionColumn != 7) {
                //comprobamos abajo izquierda y derecha
                if ((_row == firstSelectionRow - 1 && _column==firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn - 1) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn + 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }
            }

            //primera columna
            else if (firtsSelectionColumn == 0 && firstSelectionRow != 0 && firstSelectionRow != 7) {
                //comprobamos arriba abajo derecha
                if ((_row == firstSelectionRow + 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow - 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow &&_column == firtsSelectionColumn + 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }

            }

            else if (firtsSelectionColumn == 7 && firstSelectionRow != 0 && firstSelectionRow != 7) {
                //comprobamos arriba abajo izquierda
                if ((_row == firstSelectionRow + 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow - 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn - 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();
                }
            }

            //resto de casos comprobamos en las 4 direcciones
            else {

                if ((_row == firstSelectionRow + 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow - 1 && _column == firtsSelectionColumn) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn + 1) ||
                    (_row == firstSelectionRow && _column == firtsSelectionColumn - 1)) {
                    //mover ficha
                    secondSelectionRow = _row;
                    secondSlectionColumn = _column;
                    SecondTile = _tile;
                    SecondTile.setColorSelection();
                    moveTile();

                }

            }

            //la segunda seleccion no es valida
            if(secondSelectionRow==-1) {
                //volvemos a establecer menos uno para volver a poder seleccionar las variables de seleccion
                SecondTile = _tile;
                resetSelection();
            }
           
        }
    }

    //Metodo que setea las variables de seleccion para indicar que no hay ninguna tile seleccionada
    //quita los colores de seleccion
    //vacia las variables que guardaron las tiles antiguas seleccionadas
    void resetSelection() {

        
        firstSelectionRow = -1;
        firtsSelectionColumn = -1;
        firtsTile.returnColor();
        SecondTile.returnColor();
        firtsTile = null;
        SecondTile = null;
    }

    /// <summary>
    /// Funcion que hace intercambio entre dos tiles contiguas en posicion y en el grid
    /// </summary>
    public void moveTile() {

        //creamos variables auxiliares para guardar los datos de intercambio y que este se haga correctamente
        int auxRow1 = firtsTile.getX();
        int auxColumn1 = firtsTile.getY();
        int auxType = firtsTile.getType();

        int auxRow = SecondTile.getX();
        int auxColumn = SecondTile.getY();
        int auxType2 = SecondTile.getType();
        
        Vector3 auxPosition2 = SecondTile.transform.position;

       //hacemos el cambio de posicion y de datos de tile
        SecondTile.setTile(this,auxRow1, auxColumn1);
        SecondTile.transform.position = firtsTile.transform.position;
        SecondTile.setType(auxType);
        
        firtsTile.setTile(this,auxRow, auxColumn);
        firtsTile.transform.position = auxPosition2;
        firtsTile.setType(auxType2);
        
        Tile tileAux = grid[auxRow1, auxColumn1];
        grid[auxRow1, auxColumn1] = grid[auxRow, auxColumn].getTile();
        grid[auxRow, auxColumn] = tileAux;
        grid[auxRow1, auxColumn1].setType(auxType2);
        grid[auxRow, auxColumn].setType(auxType);

        //reseteamos variables de seleccion y comprobamos si hay match
        resetSelection();
        checkMatch();
        checkVerticalMatch();
        
        
    }


    //comprueba si hay match en filas y si es asi borra todas las piezas que hacen match
    void checkMatch() {

        int count;
        List<Tile> tileList=new List<Tile>();
        List<Tile> DeleteTiles = new List<Tile>();

        //comprueba si hay match en filas
        for (int row = 0; row < sizeX; row++) {

            tileList.Clear();
            count = 0;

            for (int column = 0; column < sizeY; column++) {

                //Cojo primera casilla y la guardo en la lista incremento contador
                if(column==0) {
                    tileList.Add(grid[row, column].getTile());
                    count = 1;
                }

                else {
                    //en la siguientes iteraciones comparo el tipo con el tipo de la lista
                    if(tileList[tileList.Count-1].getType()==grid[row,column].getType()) {
                        //si son iguales añado a la lista esta nueva tile
                        //incremento contador
                        count++;
                        tileList.Add(grid[row, column]);

                        if (count == 3) {

                            for(int i=0;i<tileList.Count;i++) {

                                DeleteTiles.Add(tileList[i]);
                            }
                        }

                        if (count > 3)
                            DeleteTiles.Add(grid[row, column]);
                    }
                    else {
                        //si no son iguales limpio lista y añado nuevo
                        //Contador 1
                        tileList.Clear();
                        tileList.Add(grid[row, column]);
                        count = 1;
                    }
                    
                    //si el contador llega a 3
                    //añado lista a otra lista de eliminar
                    //vuelvo a iterar hasta que sea distinto
                    //Destruyo las tiles de la lista eliminados
                }

            }
        }

        //Instanciamos nuevas tiles en parte superior y borramos las que hacen match
        int upX = sizeY;
        int auxX=0;
       

       
        for(int i = 0; i < DeleteTiles.Count; i++) {


            if (auxX != DeleteTiles[i].getX() && i!=0) {
                upX++;
            }

            auxX = DeleteTiles[i].getX();
            instantiateTile(upX, DeleteTiles[i].getY());

            
            grid[DeleteTiles[i].getX(), DeleteTiles[i].getY()].destroyTile();
            grid[DeleteTiles[i].getX(), DeleteTiles[i].getY()] = null;
        }

        //si hay tiles para borrar se actualiza la puntuacion
        if(DeleteTiles.Count!=0) {

            score += DeleteTiles.Count;
            horizontalText.SetActive(true);
            horizontalText.GetComponent<Text>().text = "GREAT!!!: " + DeleteTiles.Count;
            Invoke("setHorizontalText", 2);
            scoreView.text = "SCORE: " + score;

            //borramos los datos de la lista de tiles a borrar y llamamos al metodo que hace caer las tiles
            //y rellena los huecos que quedaron al borrar las antiguas
            DeleteTiles.Clear();
            fallTiles();
        }
       
        
        
        
    }

    //Comprueba si hay match por columnas y si es asi elimina las fichas que lo hacen
    void checkVerticalMatch() {

        int count;
        List<Tile> tileVerticalList = new List<Tile>();
        List<Tile> DeleteVerticalTiles = new List<Tile>();

        //comprueba si hay match en columnas
        for (int column = 0; column < sizeY; column++) {

            tileVerticalList.Clear();
            count = 0;

            for (int row=0;row<sizeX;row++) {

                //Cojo primera casilla y la guardo en la lista incremento contador
                if (row == 0) {
                    tileVerticalList.Add(grid[row, column].getTile());
                    count = 1;
                }

                else {
                    //en la siguientes iteraciones comparo el tipo con el tipo de la lista
                    if (tileVerticalList[tileVerticalList.Count - 1].getType() == grid[row, column].getType()) {
                        //si son iguales añado a la lista esta nueva tile
                        //incremento contador
                        count++;
                        tileVerticalList.Add(grid[row, column]);

                        if (count == 3) {

                            for (int i = 0; i < tileVerticalList.Count; i++) {

                                DeleteVerticalTiles.Add(tileVerticalList[i]);
                            }
                        }

                        if (count > 3)
                            DeleteVerticalTiles.Add(grid[row, column]);
                    }
                    else {
                        //si no son iguales limpio lista y añado nuevo
                        //Contador 1
                        tileVerticalList.Clear();
                        tileVerticalList.Add(grid[row, column]);
                        count = 1;
                    }

                    //si el contador llega a 3
                    //añado lista a otra lista de eliminar
                    //vuelvo a iterar hasta que sea distinto
                    //Destruyo las tiles de la lista eliminados
                }

            }
        }

        //Instanciamos nuevas tiles en parte superior y borramos las que hacen match
        int upX = sizeY;
        int auxX = 0;
        

       
        for (int i = 0; i < DeleteVerticalTiles.Count; i++) {


            if (auxX != DeleteVerticalTiles[i].getX() && i != 0) {
                upX++;
            }

            auxX = DeleteVerticalTiles[i].getX();
           
            instantiateTile(upX, DeleteVerticalTiles[i].getY());


            grid[DeleteVerticalTiles[i].getX(), DeleteVerticalTiles[i].getY()].destroyTile();
            grid[DeleteVerticalTiles[i].getX(), DeleteVerticalTiles[i].getY()] = null;
        }

        if(DeleteVerticalTiles.Count!=0) {

            score += DeleteVerticalTiles.Count;
            verticalText.SetActive(true);
            verticalText.GetComponent<Text>().text = "GREAT!!!: " + DeleteVerticalTiles.Count;
            Invoke("setVerticalText", 2);
            scoreView.text = "SCORE: " + score;
            DeleteVerticalTiles.Clear();
            fallTiles();
        }
       
       


    }

    //rellena los huecos de las fichas eliminadas con las fichas de la fila superior
    void fallTiles() {

        int index = 0;
        Tile auxTile;

        for (int row = 0; row < sizeX; row++) {

            for (int column = 0; column < sizeY; column++) {

                //cuando encontramos una casilla vacia en el grid
                if (grid[row, column] == null) {

                    //comprobamos si hay una pieza en la fila de arriba y si no hay incrementa contador hasta que encuentra una
                    do {

                        index++;

                    } while (grid[row + index, column]==null || grid[row + index, column].getTile().getX() != row + index);

                    //asigna las posiciones del hueco a la ficha de la fila superior y la deja vacia la fila superior
                    //asi sucesivamente hasta rellenar todos los huecos del grid
                    auxTile = grid[row + index, column].getTile();
                    auxTile.transform.position = new Vector3(column, row);
                    auxTile.setTile(this, row, column);
                    grid[row, column] = grid[row + index, column].getTile();
                    grid[row + 1, column] = null;
                    index = 0;
                }
            }
        }

        checkMatch();
        checkVerticalMatch();
    }

    //desactiva texto que indica que se ha obtenido puntuacion de un match horizontal
    void setHorizontalText() {

        horizontalText.SetActive(false);
    }
    //desactiva texto que indica que se ha obtenido puntuacion de un match vertical
    void setVerticalText() {

        verticalText.SetActive(false);
    }

    //metodo para hacer comprobaciones de las coordenadas de los tiles del grid
    //usado para debug
    void checkNumbersGrid() {

        for (int row = 0; row < sizeX; row++) {

            for (int column = 0; column < sizeY; column++) {

                Debug.Log("CoordenadaX: " + grid[row, column].getX() + "CoordenadaY:" + grid[row, column].getY());
            }
        }
    }



}

