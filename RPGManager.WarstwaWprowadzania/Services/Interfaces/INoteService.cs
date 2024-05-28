using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface INoteService
    {
        Note GetNote(int id);
        ValidatorResult<Note> AddNote(NoteDto noteDto);
        ValidatorResult<Note> UpdateNote(int id, NoteDto noteDto);
        Note DeleteNote(int id);



    }
}

