using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tic_tac_toe_testTask.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
        public string? CurrentPlayer { get; set; }
        [StringLength(9)]
        public string? BoardState { get; set; }
        public bool IsGameOver { get; set; }
        public string? Winner { get; set; }
        [Description("Current turn (X or O)")]
        public char CurrentPlayerSymbol { get { return CurrentPlayer == Player1 ? 'X' : 'O'; } }
    }
}
