﻿using RPGManager.Dtos;
using RPGManager.Models;

namespace WarstwaWprowadzania.Services.Interfaces
{
    public interface INoteService
    {
        Note GetNote(int id);
        ValidatorResult<Note> AddNote(NoteDto noteDto);
        Note UpdateNote (int id, NoteDto noteDto);
        Note DeleteNote (int id);


        
    }
}

