using RPGManager.Dtos;
using RPGManager.Models;

namespace RPGManager.Services.Interfaces
{
    public interface INoteService
    {
        Note GetNote(int id);
        (Note, Validator) AddNote(NoteDto noteDto);
        Note UpdateNote (int id, NoteDto noteDto);
        Note DeleteNote (int id);


        
    }
}

