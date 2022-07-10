using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelDataReader;
using Insomnia.Portal.BI.Interfaces;
using Insomnia.Portal.Data.Dto;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.General.Expansions;
using Insomnia.Portal.EF;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Insomnia.Portal.BI.Services
{
    public class AnimationParser : IAnimationImport
    {
        public AnimationParser()
        {
        }

        private DataSet GetFileContent(Stream file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(ms))
                {
                    return reader.AsDataSet();
                }
            }
        }

        public async Task<List<AnimationTimetable>> GetAnimations(Stream stream)
        {
            var dataset = GetFileContent(stream);
            var result = new[]
            {
                dataset.Tables["ЦУЭ 1"],
                dataset.Tables["ЦУЭ 2"],
            }.SelectMany(ParseSheet).Select(Convert).ToList();

            return result;
        }

        private AnimationTimetable Convert(DayInfo dayInfo)
        {
            return new AnimationTimetable
            {
                Screen = dayInfo.Screen,
                Day = DateTime.Parse(dayInfo.Date).DayOfWeek.MappingDay(),
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

                        var movie = new MovieInfo(arr[0]?.Trim(), arr[1]?.Trim(), arr[2]?.Trim(), arr[3]?.Trim(), arr[4]?.Trim());
                        blockInfo.Movies.Add(movie);
                    }

                    row++;
                }
            }

            return dayColums.Values;
        }
    }
}