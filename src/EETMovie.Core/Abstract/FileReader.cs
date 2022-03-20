using System.Text;
using System.Text.RegularExpressions;

namespace EETMovie.Core.Abstract;

public abstract class FileReader
{
    private const string SplitStringByCommaOutsideDoubleQuotes = "(?:^|,)(\"(?:[^\"])*\"|[^,]*)";
    private const int SkipTitleRow = 0;
    private const int BreakOutOfRemainingHandledColumnsByDelegate = 1;
    protected static async Task ReadFileToList<TDelegate, TEntity>(TDelegate @delegate, 
                                                                 string fullFilePath, 
                                                                 List<TEntity> entities, 
                                                                 CancellationToken cancellationToken, 
                                                                 int? id = null) where TDelegate : Delegate
    {
        string[] rows = await File.ReadAllLinesAsync(fullFilePath, Encoding.UTF8, cancellationToken);
        for (int row = 0; row < rows.Length; row++)
        {
            if (row == SkipTitleRow)
            {
                continue;
            }
            
            List<string> columns = Regex.Split(rows[row],SplitStringByCommaOutsideDoubleQuotes).ToList();
            columns.RemoveAll(column => column == "");
            
            for (int column = 0; column < columns.Count; column++)
            {
                if (column == BreakOutOfRemainingHandledColumnsByDelegate)
                {
                    break;
                }

                if (id is not null)
                {
                    @delegate.DynamicInvoke(columns, entities, id);
                }
                else
                {
                    @delegate.DynamicInvoke(columns, entities);
                }
            }
        }
    }
}