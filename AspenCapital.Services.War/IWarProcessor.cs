using AspenCapital.Models.War;

namespace AspenCapital.Services.War
{
    public interface IWarProcessor
    {
        GameDetails CreateGame(string player1Name, string player2Name);

        GameDetails GetGameDetails(Guid gameId);

        Movement GetMovement(Guid gameId, int number);

        List<GameDetails> GetWinsByPlayer(string playerName);
    }
}
