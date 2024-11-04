
namespace MAUI.Word.Finder.src.WordFinder.Domain.Entities;

public class WordFinderEntity
{
    #region Constants
    /// <summary>
    /// Maximum chars acceptep for the row size.
    /// </summary>
    /// <remarks>
    /// Acceptance criteria 2:
    /// The matrix size does not exceed 64x64.
    /// </remarks>
    const int MAX_SIZE_ROWS = 64;

    /// <summary>
    /// Maximum chars acceptep for the column size.
    /// </summary>
    /// <remarks>
    /// Acceptance criteria 2:
    /// The matrix size does not exceed 64x64.
    /// </remarks>
    const int MAX_SIZE_COLUMNS = MAX_SIZE_ROWS;
    #endregion

    #region Flds
    private readonly int _rows;

    private readonly int _columns;

    private char[,] _matrix;
    #endregion

    #region Ctors
    /// <summary>
    /// </summary>
    /// <remarks>Empty constructor in case we want to use Dependency injection.</remarks>
    public WordFinderEntity()
    {
    }
    /// <summary>
    /// Word Finder Entity.
    /// </summary>
    /// <param name="matrix"></param>
    public WordFinderEntity(List<string>? matrix)
    {
        if (matrix != null)
        {
            _rows = matrix.Count;
            _columns = matrix[0].Length;

            //-> Only build the matrix if is a valid size criteria.
            //-> Will use IsValidMatriz property as a flag to continue procesing the happy path or avoid
            //    process unnecesary steps on the flow.
            //-> Acceptance criteria 3:
            //   All strings contain the same number of chars, so,
            //      the bidimentional array shall be O(nxn).
            if ((_rows <= MAX_SIZE_ROWS &&
                _columns <= MAX_SIZE_COLUMNS) &&
                _rows == _columns)
            {
                BuildMatrix(matrix);
                IsValidMatrix = true;
            }
        }
    }
    #endregion

    #region Props
    public bool IsValidMatrix { get; set; } = false;

    public char[,] Matrix => _matrix;

    public int Rows => _rows;

    public int Columns => _columns;
    #endregion

    /// <summary>
    /// </summary>
    /// <remarks>
    /// We use first solid principle (Single reponsability) to decouple the matrix creation from the
    /// constructor.
    /// </remarks>
    /// <param name="matrixList"></param>
    private void BuildMatrix(List<string> matrixList)
    {
        _matrix = new char[_rows, _columns];

        //-> We use for loop instead "foreach" or "high order function foreach"
        //  because is faster and we are filling another array, so, the "big O" notation for our 
        //  bidimentional array is O(nxm), then, the maximum iteration would be O(64x64) = 4096 iterations.
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _matrix[i, j] = matrixList[i][j];
            }
        }
    }
}
