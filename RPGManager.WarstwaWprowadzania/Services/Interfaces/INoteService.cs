using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface INoteService
    {
        Task<Note> GetNoteAsync(int id);
        Task<Result<Note>> AddNoteAsync(NoteDto noteDto);
        Task<Result<Note>> UpdateNoteAsync(int id, NoteDto noteDto);
        Task<Note> DeleteNoteAsync(int id);
    }
}
