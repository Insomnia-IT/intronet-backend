using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelDataReader;

namespace Insomnia.Portal.BI.Services
{
    public class AnimationParser
    {
        private readonly string _filePath;

        public AnimationParser(string filePath)
        {
            _filePath = filePath;
        }

        private DataSet GetFileContent()
        {
            using var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read);
            // Auto-detect format, supports:
            //  - Binary Excel files (2.0-2003 format; *.xls)
            //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            using var reader = ExcelReaderFactory.CreateReader(stream);
            // Choose one of either 1 or 2:
            // 2. Use the AsDataSet extension method
            return reader.AsDataSet();
        }

        public IReadOnlyList<AnimationTimetable> GetAnimations()
        {
            var dataset = GetFileContent();
            return new[]
            {
                dataset.Tables["ЦУЭ 1"],
                dataset.Tables["ЦУЭ 2"],
            }.SelectMany(ParseSheet).Select(Convert).ToList();
        }

        private AnimationTimetable Convert(DayInfo dayInfo)
        {
            return new AnimationTimetable
            {
                Screen = dayInfo.Screen,
                Day = DateTime.Parse(dayInfo.Date),
                Blocks = dayInfo.Blocks.Select(Convert).ToList() 
            };
        } 
        private AnimationBlock Convert(BlockInfo block)
        {
            var regex = new Regex(@"(?<name1>[\w\s\&]+)\s*-\s*(?<name2>[\w\s\&]+)\s*часть\s*(?<part>\d+)\s*" +
                                  @"\((?<nameEn1>[\w\s\&]+)\s*-\s*(?<nameEn2>[\w\s\&]+)\s*part\s*(?<partEn>\d+)\s*\)" +
                                  @"\s*(?<age>\d+)+"
            );
            var res = new AnimationBlock
            {
                Start = block.Start,
                End = block.End,
                Movies = block.Movies,
            };
            var match = regex.Match(block.Title);
            if (match.Success)
            {
                res.Part = int.Parse(match.Groups["part"].Value);
                res.MinAge = int.Parse(match.Groups["age"].Value);
                res.Title = match.Groups["name1"].Value;
                res.SubTitle = match.Groups["name2"].Value;
                res.TitleEn = match.Groups["nameEn1"].Value;
                res.SubTitleEn = match.Groups["nameEn2"].Value;
            }
            else
            {
                res.Title = block.Title;
            }
            return res;
        }

        private IEnumerable<DayInfo> ParseSheet(DataTable data)
        {
            var dayColums = data.Columns.Cast<DataColumn>()
                .Select(x =>
                {
                    var text = data.Rows[0][x].ToString();
                    if (string.IsNullOrEmpty(text))
                        return null;
                    return new
                    {
                        Info = new DayInfo(text, data.TableName, new List<BlockInfo>()),
                        Column = x
                    };
                }).Where(x => x != null).ToDictionary(x => x.Column, x => x.Info);
            foreach (var (column, info) in dayColums)
            {
                var row = 1;
                var blockInfo = new BlockInfo();
                while (row < data.Rows.Count)
                {
                    var text = data.Rows[row][column].ToString();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        if (string.IsNullOrEmpty(blockInfo.Title))
                            break;
                        info.Blocks.Add(blockInfo);
                        blockInfo = new BlockInfo();
                    }
                    else if (string.IsNullOrEmpty(blockInfo.Title))
                    {
                        blockInfo.Title = text;
                    }
                    else if (string.IsNullOrEmpty(blockInfo.Start))
                    {
                        var startEnd = text.Split("-");
                        blockInfo.Start = startEnd[0].Trim();
                        blockInfo.End = startEnd[0].Trim();
                    }
                    else
                    {
                        var arr = text.Split("/").ToList();
                        while (arr.Count < 5)
                        {
                            arr.Add(null);
                        }

                        var movie = new MovieInfo(arr[0], arr[1], arr[2], arr[3], arr[4]);
                        blockInfo.Movies.Add(movie);
                    }

                    row++;
                }
            }

            return dayColums.Values;
        }

        private record DayInfo(string Date, string Screen, List<BlockInfo> Blocks);

        private record BlockInfo
        {
            public string Title { get; set; }
            public string Start { get; set; }
            public string End { get; set; }

            public List<MovieInfo> Movies = new List<MovieInfo>();
        }
    }

    public class AnimationTimetable
    {
        public DateTime Day { get; set; }
        public string Screen { get; set; }
        public IReadOnlyList<AnimationBlock> Blocks { get; set; }
    }

    public class AnimationBlock
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string TitleEn { get; set; }
        public string SubTitleEn { get; set; }
        public int MinAge { get; set; }
        public int Part { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public IReadOnlyList<MovieInfo> Movies = new List<MovieInfo>();
    }

    public record MovieInfo(string Name = null, string Author = null, string Country = null, string Year = null,
        string Duration = null);
}