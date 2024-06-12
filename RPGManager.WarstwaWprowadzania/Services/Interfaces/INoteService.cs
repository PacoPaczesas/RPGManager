using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface INoteService
    {
        Note GetNote(int id);
        Result<Note> AddNote(NoteDto noteDto);
        Result<Note> UpdateNote(int id, NoteDto noteDto);
        Note DeleteNote(int id);



    }
}

