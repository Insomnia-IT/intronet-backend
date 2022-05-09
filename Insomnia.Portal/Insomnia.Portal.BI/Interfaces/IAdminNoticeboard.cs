﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.ViewModels.Input;
using Insomnia.Portal.Data.Return;

namespace Insomnia.Portal.BI.Interfaces
{
    public interface IAdminNotesboard
    {
        Task<NoteReturn> Add(CreateNote note);

        Task<NoteReturn> Edit(EditNote note);

        Task<NoteReturn> Delete(int id);
    }
}
