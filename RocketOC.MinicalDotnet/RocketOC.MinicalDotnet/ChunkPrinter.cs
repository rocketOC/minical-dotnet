using System.Text;

namespace RocketOC.MinicalDotnet
{
    internal class ChunkPrinter
    {
        /// <summary>
        /// Calendar is a bool [,] indicating the positions that represent days rather than white space or padding.
        /// Activity is a bool [,]. We will put an x where activity is true. The activity array should use the same
        /// date logic as the calendar.
        /// </summary>
        internal static void PrintChunk(CalendarChunk chunk)
        {
            var lines = ToStrings(chunk);
            foreach(var l in lines)
            {
                Console.WriteLine(l);
            }
        }

        internal static IEnumerable<string> ToStrings(CalendarChunk chunk)
        {
            var ans = new List<string>();
            var sb = new StringBuilder();

            //print labels
            if (chunk.Labels != null)
            {
                var labelRow = new char[4 * chunk.Days.GetLength(1)];
                for (int i = 0; i < labelRow.Length; i++)
                {
                    labelRow[i] = ' ';
                }
                for (int i = 0; i < chunk.Labels.Length; i++)
                {
                    if (chunk.Labels[i] != null)
                    {
                        //4 here comes from the block width used below
                        int display = Math.Min(5 * 4, chunk.Labels[i].Length);
                        for (int j = 0; j < display; j++)
                        {
                            labelRow[i * 4 + j] = chunk.Labels[i][j];
                        }
                    }
                }
                sb.AppendLine(new string(labelRow));
                ans.Add(sb.ToString());
            }

            for (int r = 0; r < chunk.Days.GetLength(0); r++)
            {
                sb.Clear();
                for (int c = 0; c < chunk.Days.GetLength(1); c++)
                {
                    if (chunk.Days[r, c] || r > 0 && chunk.Days[r - 1, c])
                        sb.Append("+―――");
                    else if (c > 0 && chunk.Days[r, c - 1])
                        sb.Append("+   ");
                    else
                        sb.Append("    ");
                }
                ans.Add(sb.ToString());

                sb.Clear();
                for (int c = 0; c < chunk.Days.GetLength(1); c++)
                {
                    if (chunk.Days[r, c] && chunk.Activity[r, c])
                    {
                        sb.Append("| X ");
                    }
                    else if (chunk.Days[r, c])
                    {
                        sb.Append("|   ");
                    }
                    else if (c > 0 && chunk.Days[r, c - 1])
                    {
                        sb.Append("|   ");
                    }
                    else
                    {
                        sb.Append("    ");
                    }
                }
                ans.Add(sb.ToString());
                sb.Clear();
            }

            //one last row of +-+
            sb.Clear();
            var height = chunk.Days.GetLength(0);
            for (int c = 0; c < chunk.Days.GetLength(1); c++)
            {
                if (chunk.Days[height - 1, c])
                    sb.Append("+―――");
                else if (c > 0 && chunk.Days[height - 1, c - 1])
                    sb.Append("+   ");
                else
                    sb.Append("    ");
            }
            ans.Add(sb.ToString());
            sb.Clear();

            return ans;
        }
    }
}