using UnityEngine;


public class GameManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   MovementFrequency
    //   Tetrominos
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public float        MovementFrequency = 0.8f;
    public GameObject[] Tetrominos;

    #endregion



    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _passedTime
    //   _currentTetromino
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private float      _passedTime;
    private GameObject _currentTetromino;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   CheckForLines()
    //   IsValidPosition()
    //   MoveTetromino()
    //   RotateTetromino()
    //   SpawnTetromino()
    //   Start()
    //   Update()
    //   UserInput()
    // -------------------------------------------------------------------------

    #region .  CheckForLines()  .
    // -------------------------------------------------------------------------
    //  Method.......:  CheckForLines()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void CheckForLines()
    {
        GetComponent<GridScript>().CheckForLines();

    }   // CheckForLines()
    #endregion


    #region .  IsValidPosition()  .
    // -------------------------------------------------------------------------
    //  Method.......:  IsValidPosition()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private bool IsValidPosition()
    {
        return GetComponent<GridScript>().IsValidPosition(this._currentTetromino.transform);

    }   // IsValidPosition()
    #endregion


    #region .  MoveTetromino()  .
    // -------------------------------------------------------------------------
    //  Method.......:  MoveTetromino()
    //  Description..:
    //  Parameters...:  Vector3 : direction to move.
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void MoveTetromino(Vector3 direction)
    {
        this._currentTetromino.transform.position += direction;

        if (!this.IsValidPosition())
        {
            this._currentTetromino.transform.position -= direction;

            if (direction == Vector3.down)
            {
                GetComponent<GridScript>().UpdateGrid(this._currentTetromino.transform);
                this.CheckForLines();
                this.SpawnTetromino();
            }   
        }

    }   // MoveTetromino()
    #endregion


    #region .  RotateTetromino()  .
    // -------------------------------------------------------------------------
    //  Method.......:  RotateTetromino()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void RotateTetromino()
    {
        this._currentTetromino.transform.Rotate(0f, 0f, 90f);

        if (!this.IsValidPosition())
        {
            this._currentTetromino.transform.Rotate(0f, 0f, -90f);
        }

    }   // RotateTetromino()
    #endregion


    #region .  SpawnTetromino()  .
    // -------------------------------------------------------------------------
    //  Method.......:  SpawnTetromino()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void SpawnTetromino()
    {
        int index = Random.Range(0, this.Tetrominos.Length);
        this._currentTetromino = Instantiate(this.Tetrominos[index], new Vector3(5f, 18f, 0f), Quaternion.identity);

    }   // SpawnTetromino()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //  Method.......:  Start()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void Start()
    {
        this.SpawnTetromino();

    }   // Start()
    #endregion


    #region .  Update()  .
    // -------------------------------------------------------------------------
    //  Method.......:  Update()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void Update()
    {
        this._passedTime += Time.deltaTime;

        if (this._passedTime >= this.MovementFrequency)
        {
            this._passedTime -= this.MovementFrequency;
            this.MoveTetromino(Vector3.down);
        }

        this.UserInput();

    }   // Update()
    #endregion


    #region .  UserInput()  .
    // -------------------------------------------------------------------------
    //  Method.......:  UserInput()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.MoveTetromino(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.MoveTetromino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.RotateTetromino();
        }

        this.MovementFrequency = (Input.GetKey(KeyCode.DownArrow)) ? 0.2f : 0.8f;

    }   // UserInput()
    #endregion


}   // class GameManager
