using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Insomnia.Portal.Data.Dto;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAnimationImport
    {
        Task<List<AnimationTimetable>> GetAnimations(Stream stream);
    }
}
