using System.ComponentModel.DataAnnotations;

namespace RPGManager.Dtos
{
    public record class NPCDto(
        [Required(ErrorMessage = "Wymagane Imie NPC")]
        string Name,
        string Description,
        int CountryId,
        [Range(1, int.MaxValue, ErrorMessage = "Siła musi być wyższa niż 0")]
        int Strength,
        [Range(1, int.MaxValue, ErrorMessage = "Moc musi być wyższa niż 0")]
        int Might,
        [Range(0, int.MaxValue, ErrorMessage = "Epx jest wymagany i musi wynosić co najmniej 0")]
        int Exp);
}
