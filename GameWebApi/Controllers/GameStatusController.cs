using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameDatabase;

namespace GameWebApi.Controllers
{
    public class GameStatusController : ApiController
    {
        // GET api/GameStatus
        public IEnumerable<GameStatu> Get()
        {
            using (GameApiEntities entities = new GameApiEntities())
            {
                return entities.GameStatus.ToList();
            }
        }

        [HttpPost]
        [Authorize]
        public object InsertGameStatus(GameStatu gamestatus)
        {
            GameApiEntities entities = new GameApiEntities();

            entities.GameStatus.Add(gamestatus);
            entities.SaveChanges();

            return Ok(gamestatus.GameStatusId);
        }

        // GET api/GameStatus
        /*[HttpGet]
        public IQueryable<Object> CheckGameStatusByGameID(int gid)
        {
            GameApiEntities entities = new GameApiEntities();

            var query = from gm in entities.Games               
                        join gamestat in entities.GameStatus on gm.GameID equals gamestat.GameID
                        join statement in entities.Statements on gm.GameID equals statement.GameID
                        join ply in entities.Players on gm.GameID equals ply.GameID
                        join plyscore in entities.PlayerInGameStatus on ply.PlayerId equals plyscore.PlayerId
                        where gm.GameID == gid
                        select new
                        {
                            player_turn = gamestat.PlayersTurn,
                            game_state = gamestat.GameState,
                            is_submitted = gamestat.IsSubmitted,
                            timeRemaining = gamestat.TimeRemaining,
                            next_players_turn = gamestat.NextPlayersTurn,
                            statement_id = statement.StatementId,
                            msg = statement.Msg,
                            is_true = statement.IsTrue,
                            player_id = ply.PlayerId,
                            player_name = ply.PlayerName,
                            player_score = plyscore.PlayerScore
                        };
            return query.AsQueryable();
        }*/
    }
}