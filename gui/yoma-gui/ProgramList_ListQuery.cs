using System.Collections.Generic;
using System.Threading.Tasks;

namespace yoma;

public class ProgramList_ListQuery
{
    public static async Task<List<string>> GetProgramsAsync()
    {
        await Task.Delay(10); // temp async placeholder

        return new List<string>
        {
            "Program 1",
            "Program 2",
            "Program 3",
            "Program 4"
        };
    }
}