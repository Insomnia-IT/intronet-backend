using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insomnia.Portal.Data.Dto
{
    internal class NotesCategoriesDto
    {
        public IList<NoteCategoryDto> Categories { get; set; } = new List<NoteCategoryDto>();

        public int Sum => Categories.Select(x => x.Count).Sum();
    }
}
