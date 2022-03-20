using EETMovie.Core.Abstract;
using EETMovie.Core.Configuration;
using EETMovie.Core.Repository.Entities;
using Microsoft.Extensions.Options;

namespace EETMovie.Core.Repository;

public class FileRepository : FileReader, IFileRepository
{
    private const string FileType = ".csv";
    private const string SavedToDatabase = "Saved to a database";
    public static readonly List<string> Database = new List<string>();
    private readonly FileConfiguration _fileConfiguration;

    public FileRepository(IOptions<FileConfiguration> fileConfiguration)
    {
        _fileConfiguration = fileConfiguration.Value;
    }
    
    public async Task<string> CreateMetadataAsync()
    {
        Database.Add(SavedToDatabase);
        return await Task.FromResult(SavedToDatabase);
    }

    public async Task<IEnumerable<Stats>> GetStats(CancellationToken cancellationToken)
    {
        List<Stats> statsList = new List<Stats>();
        await ReadFileToList<Action<List<string>, List<Stats>>, Stats>(AddStatsToList, FullFilePath(nameof(Stats)), statsList, cancellationToken);
        return statsList;
    }
    
    public async Task<IEnumerable<Metadata>> GetMetadatas(CancellationToken cancellationToken)
    {
        List<Metadata> metadatas = new List<Metadata>();
        await ReadFileToList<Action<List<string>, List<Metadata>>, Metadata>(AddMetadataToList, FullFilePath(nameof(Metadata)), metadatas, cancellationToken);
        return metadatas;
    }

    public async Task<Metadata> GetMetadataByMovieId(int movieId, CancellationToken cancellationToken)
    {
        List<Metadata> metadataList = new List<Metadata>();
        await ReadFileToList<Action<List<string>, List<Metadata>, int>, Metadata>(AddMetadataToListById, FullFilePath(nameof(Metadata)), metadataList, cancellationToken, movieId);
        return metadataList.OrderBy(metadata => metadata.ReleaseYear)
                           .ThenBy(metadata => metadata.Language)
                           .FirstOrDefault();
    }

    private string FullFilePath(string fileName)
    {
        return $"{_fileConfiguration.Path}\\{fileName.ToLower()}{FileType}";
    }

    private static void AddMetadataToList(List<string> columns, List<Metadata> metadataList)
    {
        metadataList.Add(new Metadata
                         {
                             MovieId = int.Parse(columns[1]),
                             Title = columns[2],
                             Language = columns[3],
                             Duration = columns[4],
                             ReleaseYear = int.Parse(columns[5])
                         });
    }

    private static void AddStatsToList(List<string> columns, List<Stats> statsList)
    {
        statsList.Add(new Stats { MovieId = int.Parse(columns[0]), WatchDurationMs = int.Parse(columns[1]) });
    }

    private static void AddMetadataToListById(List<string> columns, List<Metadata> metadataList, int movieId)
    {
        if (int.Parse(columns[1]) == movieId)
        {
            metadataList.Add(new Metadata
                             {
                                 MovieId = int.Parse(columns[1]),
                                 Title = columns[2],
                                 Language = columns[3],
                                 Duration = columns[4],
                                 ReleaseYear = int.Parse(columns[5])
                             });
        }
    }
}