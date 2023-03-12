using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using tic_tac_toe_testTask.Models;

namespace tic_tac_toe_testTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameContext _context;

        public GameController(GameContext context)
        {
            _context = context;
        }

        [HttpPost("start-game")]
        public ActionResult<Game> NewGameStart()
        {
            Game board = new()
            {
                Player1 = "Player1",
                Player2 = "Player2",
                CurrentPlayer = "Player1",
                BoardState = "---------",
                IsGameOver = false,
                Winner = ""
            };

            _context.Games.Add(board);
            _context.SaveChanges();

            return board;
        }
        [HttpPost("Make-move")]
        public ActionResult<Game> MakeMove([FromQuery] MoveRequest request)
        {
            var game = _context.Games.Find(request.GameId);
            if (game == null)
            {
                return BadRequest("Game not found with the provided ID");
            }
            if (!game.IsGameOver)
            {
                var boardState = game.BoardState?.ToCharArray();
                if (boardState == null)
                {
                    return BadRequest("Error while retrieving game status");
                }

                if (request.PlayerSymbol != game.CurrentPlayerSymbol)
                {
                    return BadRequest("It is not your turn");
                }
                if (request.PlayerSymbol != 'X' && request.PlayerSymbol != 'O')
                {
                    return BadRequest("Players can use only X or O symbols");
                }

                if (request.CellNumber > boardState.Length)
                {
                    return BadRequest("There is no cell with this number, enter a cell from 0 to 8.");
                }
                if (boardState[request.CellNumber] != '-')
                {
                    return BadRequest("The selected cell is already taken. Please choose another cell.");
                }
                else
                {
                    boardState[request.CellNumber] = request.PlayerSymbol;

                    if (CheckWin(boardState, request.PlayerSymbol))
                    {
                        game.IsGameOver = true;
                        game.Winner = request.PlayerSymbol.ToString();
                    }
                }

                game.BoardState = new string(boardState);

                game.CurrentPlayer = game.CurrentPlayer == "Player1" ? "Player2" : "Player1";

                _context.SaveChanges();

                return game;
            }
            else
            {
                return BadRequest("Game is over. Start new one");
            }
        }
        [HttpGet("Get-all-games")]
        public ActionResult<List<Game>> GetGames()
        {
            var games = _context.Games.ToList();
            return games;
        }
        [HttpGet("Get-game-by-id")]
        public ActionResult<Game> GetGameById([FromQuery][Required] int gameId)
        {
            var game = _context.Games.Find(gameId);
            if (game == null)
            {
                return NotFound("Game not found with the provided ID");
            }
            return game;
        }
        [HttpDelete("Delete-game-by-id")]
        public IActionResult DeleteGameById([FromQuery][Required] int gameId)
        {
            var game = _context.Games.Find(gameId);
            if (game == null)
            {
                return NotFound("Game not found with the provided ID");
            }
            else
            {
                _context.Remove(game);
                _context.SaveChanges();
                return Ok($"Game with id {gameId} was deleted");
            }
        }
        private bool CheckWin(char[] boardState, char playerSymbol)
        {
            int[][] winPositions = new int[][]
            {
                new int[] {0,1,2},
                new int[] {3,4,5},
                new int[] {6,7,8},
                new int[] {0,3,6},
                new int[] {1,4,7},
                new int[] {2,5,8},
                new int[] {0,4,8},
                new int[] {2,4,6}
             };

            foreach (var position in winPositions)
            {
                if (boardState[position[0]] == playerSymbol && boardState[position[1]] == playerSymbol && boardState[position[2]] == playerSymbol)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
