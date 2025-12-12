using UnityEngine;


public class GridScript : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Grid
    //   Height
    //   Width
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Transform[,] Grid;
    public int          Height;
    public int          Width;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   CheckForLines()
    //   IsValidPosition()
    //   UpdateGrid()
    // -------------------------------------------------------------------------

    #region .  CheckForLines()  .
    // -------------------------------------------------------------------------
    //  Method.......:  CheckForLines()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void CheckForLines()
    {
        for (int y = 0; y < this.Height; y++)
        {
            if (this.IsLineFull(y))
            {
                this.DeleteLine(y);
                this.DecreaseRowsAbove(y + 1);
                y--;
            }
        }

    }   // CheckForLines()
    #endregion


    #region .  IsValidPosition()  .
    // -------------------------------------------------------------------------
    //  Method.......:  IsValidPosition()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public bool IsValidPosition(Transform tetromino)
    {
        foreach (Transform mino in tetromino)
        {
            Vector2 position = Round(mino.position);

            if (!this.IsInsideBorder(position))
            {
                return false;
            }
            if (GetTransformAtGridPosition(position)        != null &&
                GetTransformAtGridPosition(position).parent != tetromino)
            {
                return false;
            }
        }

        return true;

    }   // IsValidPosition()
    #endregion


    #region .  UpdateGrid()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateGrid()
    //   Description..:  
    //   Parameters...:  Transform : the tetromino to update.
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void UpdateGrid(Transform tetromino)
    {
        for (int y = 0; y < this.Height; y++)
        {
            for (int x = 0; x < this.Width; x++)
            {
                if (this.Grid[x, y] != null)
                {
                    if (this.Grid[x, y].parent == tetromino)
                    {
                        this.Grid[x, y] = null;
                    }
                }
            }

            foreach (Transform mino in tetromino)
            {
                Vector2 position = Round(mino.position);

                if (position.y < this.Height)
                {
                    this.Grid[(int)position.x, (int)position.y] = mino;
                }
            }
        }

    }   // UpdateGrid()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   DecreaseRowsAbove()
    //   DeleteLine()
    //   GetTransformAtGridPosition()
    //   IsInsideBorder()
    //   IsLineFull()
    //   Round()    
    //   Start()
    // -------------------------------------------------------------------------

    #region .  DecreaseRowsAbove()  .
    // -------------------------------------------------------------------------
    //  Method.......:  DecreaseRowsAbove()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void DecreaseRowsAbove(int startRow)
    {
        for (int y = startRow; y < this.Height; y++)
        {
            for (int x = 0; x < this.Width; x++)
            {
                if (this.Grid[x, y] != null)
                {
                    this.Grid[x, y - 1] = this.Grid[x, y];
                    this.Grid[x, y - 1].position += Vector3.down;
                    this.Grid[x, y] = null;
                }
            }
        }

    }   // DecreaseRowsAbove()
    #endregion


    #region .  DeleteLine()  .
    // -------------------------------------------------------------------------
    //  Method.......:  DeleteLine()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void DeleteLine(int y)
    {
        for (int x = 0; x < this.Width; x++)
        {
            Destroy(this.Grid[x, y].gameObject);
            this.Grid[x, y] = null;
        }

    }   // DeleteLine()
    #endregion


    #region .  GetTransformAtGridPosition()  .
    // -------------------------------------------------------------------------
    //  Method.......:  GetTransformAtGridPosition()
    //  Description..:
    //  Parameters...:  Vector2 : the position to check.
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private Transform GetTransformAtGridPosition(Vector2 position)
    {
        if (position.y > this.Height - 1)
        {
            return null;
        }

        return this.Grid[(int)position.x, (int)position.y];

    }   // GetTransformAtGridPosition()
    #endregion


    #region .  IsInsideBorder()  .
    // -------------------------------------------------------------------------
    //  Method.......:  IsInsideBorder()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private bool IsInsideBorder(Vector2 position)
    {
        return (int)position.x >= 0          &&
               (int)position.x <  this.Width && 
               (int)position.y >= 0          && 
               (int)position.y <  this.Height;

    }   // IsInsideBorder()
    #endregion


    #region .  IsLineFull()  .
    // -------------------------------------------------------------------------
    //  Method.......:  IsLineFull()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private bool IsLineFull(int y)
    {
        for (int x = 0; x < this.Width; x++)
        {
            if (this.Grid[x, y] == null)
            {
                return false;
            }
        }

        return true;

    }   // IsLineFull()
    #endregion


    #region .  Round()  .
    // -------------------------------------------------------------------------
    //  Method.......:  Round()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private static Vector2 Round(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));

    }   // Round()
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
        this.Grid = new Transform[this.Width, this.Height];

    }   // Start()
    #endregion


}   // GridScript
