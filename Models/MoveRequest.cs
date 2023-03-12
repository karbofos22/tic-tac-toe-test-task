using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tic_tac_toe_testTask.Models
{
    public class MoveRequest
    {
        [Description("Game id to play")]
        [Required]
        public int GameId { get; set; }

        [Description("Cell number to move(0 - 8)")]
        [Required]
        public int CellNumber { get; set; }

        [Description("Player symbol(X - O)")]
        [Required]
        public char PlayerSymbol { get; set; }
    }
}
