
using MAUI.Word.Finder.src.WordFinder.Domain.Entities;

namespace MAUI.Word.Finder.src.WordFinder.Domain.Services
{
    public class WordFinderService : IWordFinderService
    {
        #region Const
        /// <summary>
        /// Setting of the top words required as a response.
        /// </summary>
        /// <remarks>
        /// This could be set in an external file such as json, resx, config, etc.
        /// </remarks>
        private const int TOP_WORD_REPEATED = 10;
        #endregion

        #region Flds
        /// <summary>
        /// Set of words found
        /// </summary>
        /// <remarks>
        /// Acceptance criteria 6.
        /// If any word is found more that once, the search result should count it only once.
        /// A Set is a collection of uniques elements.
        /// </remarks>
        private HashSet<string> _wordResponse = new HashSet<string>();
        #endregion

        #region Ctor
        /// <summary>
        /// Primary Ctor.
        /// </summary>
        public WordFinderService() { }
        /// <summary>
        /// Second ctor.
        /// </summary>
        /// <remarks>
        /// Acceptance criteria 1:
        /// The matrix receives a set of strings whis represent a char matrix.
        /// </remarks>
        public WordFinderService(IEnumerable<string> matrix)
        {
            WordFinderEntity = new WordFinderEntity(matrix.ToList());

            if (!WordFinderEntity.IsValidMatrix)
                throw new Exception("Word.Finder.Service, Cannot process the matrix");

        }
        #endregion

        #region Properties
        WordFinderEntity? WordFinderEntity { get; set; } = null;
        #endregion

        #region public functions
        /// <summary>
        /// Find method
        /// </summary>
        /// <param name="wordStream">Words to find.</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordRequest = new HashSet<string>(wordStream);

            //->Saving time of processing if we have invalid matrix.
            //    returning empty Set of strings
            if (!WordFinderEntity.IsValidMatrix)
            {
                _wordResponse.Add(String.Empty);
                return _wordResponse;
            }



            foreach (var word in wordRequest)
            {
                //-> Accetance criteria 4
                //   Must return the top 10 must repeated words.
                //   To save performace time, we break the matrix iterator if we get the result
                //     that we are expecting.
                if (_wordResponse.Count > TOP_WORD_REPEATED)
                {
                    break;
                }

                IsWordInMatrix(word);
            }

            // Acceptance criteria 5
            // If no words are found, the find return an empty set of strings.
            if (_wordResponse.Count == 0)
            {
                _wordResponse.Add(String.Empty);
                return _wordResponse;
            }

            return _wordResponse;
        }
        #endregion

        #region private functions
        private bool IsWordInMatrix(string word)
        {
            for (int i = 0; i < WordFinderEntity.Rows; i++)
            {
                for (int j = 0; j < WordFinderEntity.Columns; j++)
                {
                    if (SearchWord(word, i, j, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<IEnumerable<string>> FindAsync(IEnumerable<string> wordStream, CancellationToken cancellationToken)
        {
            var wordRequest = new HashSet<string>(wordStream);

            //->Saving time of processing if we have invalid matrix.
            //    returning empty Set of strings
            if (!WordFinderEntity.IsValidMatrix)
            {
                _wordResponse.Add(String.Empty);
                return _wordResponse;
            }

            var tasks = wordRequest.Select(async word =>
            {
                await IsWordInMatrixAsync(word, cancellationToken);
                if (_wordResponse.Count >= TOP_WORD_REPEATED)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
            });

            await Task.WhenAll(tasks);

            return _wordResponse;
        }
        private async Task<bool> IsWordInMatrixAsync(string word, CancellationToken cancellationToken)
        {
            for (int i = 0; i < WordFinderEntity.Rows; i++)
            {
                for (int j = 0; j < WordFinderEntity.Columns; j++)
                {
                    if (await SearchWordAsync(word, i, j, 0, cancellationToken))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool SearchWord(string word, int row, int col, int index)
        {
            if (index == word.Length)
            {
                return true;
            }

            if (row < 0 ||
                row >= WordFinderEntity.Rows ||
                col < 0 ||
                col >= WordFinderEntity.Columns ||
                WordFinderEntity.Matrix[row, col] != word[index])
            {
                return false;
            }

            char temp = WordFinderEntity.Matrix[row, col];
            WordFinderEntity.Matrix[row, col] = '#'; // Mark as visited

            bool found = SearchWord(word, row + 1, col, index + 1) || // Down
                         SearchWord(word, row - 1, col, index + 1) || // Up
                         SearchWord(word, row, col + 1, index + 1) || // Right
                         SearchWord(word, row, col - 1, index + 1);   // Left

            WordFinderEntity.Matrix[row, col] = temp; // Unmark

            _wordResponse.Add(word);

            return found;
        }

        private async Task<bool> SearchWordAsync(string word, int row, int col, int index, CancellationToken cancellationToken)
        {
            if (index == word.Length)
            {
                return true;
            }

            if (row < 0 ||
                row >= WordFinderEntity.Rows ||
                col < 0 ||
                col >= WordFinderEntity.Columns ||
                WordFinderEntity.Matrix[row, col] != word[index])
            {
                return false;
            }

            cancellationToken.ThrowIfCancellationRequested();

            char temp = WordFinderEntity.Matrix[row, col];
            WordFinderEntity.Matrix[row, col] = '#'; // Mark as visited

            bool found = await SearchWordAsync(word, row + 1, col, index + 1, cancellationToken) || // Down
                         await SearchWordAsync(word, row - 1, col, index + 1, cancellationToken) || // Up
                         await SearchWordAsync(word, row, col + 1, index + 1, cancellationToken) || // Right
                         await SearchWordAsync(word, row, col - 1, index + 1, cancellationToken);    // Left

            WordFinderEntity.Matrix[row, col] = temp; // Unmark

            _wordResponse.Add(word);

            return found;
        }
        #endregion

    }
}
