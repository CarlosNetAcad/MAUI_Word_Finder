
namespace MAUI.Word.Finder.src.WordFinder.Infrastructure.Repository.MockRepository;

/// <summary>
/// Matrix wrapper.
/// </summary>
/// <param name="rows"></param>
public record MatrixRecord(List<string> rows);

/// <summary>
/// WordStream wrapper.
/// </summary>
/// <param name="wordStream"></param>
public record WordStreamRecord(List<string> wordStream);