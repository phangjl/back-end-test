using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameDatabase;

namespace GameWebApi.Controllers
{
    public class PlayerInGameStatusController : ApiController
    {
        // GET api/GameStatu
        public IEnumerable<PlayerInGameStatu> Get()
        {
            using (GameApiEntities entities = new GameApiEntities())
            {
                return entities.PlayerInGameStatus.ToList();
            }
        }

        // POST api/games
        [HttpPost]
        [Authorize]
        public object InsertPlayerStatus(PlayerInGameStatu PlayerInGameStatus)
        {
            GameApiEntities entities = new GameApiEntities();

            entities.PlayerInGameStatus.Add(PlayerInGameStatus);
            entities.SaveChanges();

            return Ok(PlayerInGameStatus.PlayerInGameStatusId);
        }
    }
}