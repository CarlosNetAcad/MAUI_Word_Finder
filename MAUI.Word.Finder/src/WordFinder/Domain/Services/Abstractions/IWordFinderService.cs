namespace MAUI.Word.Finder.src.WordFinder.Domain.Services;

public interface IWordFinderService
{
    IEnumerable<string> Find(IEnumerable<string> wordStream);
    Task<IEnumerable<string>> FindAsync(IEnumerable<string> wordStream, CancellationToken cancellationToken = default);
}
