using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameDatabase;

namespace GameWebApi.Controllers
{
    public class StatementsController : ApiController
    {
        // GET api/statements
        public IEnumerable<Statement> Get()
        {
            using (GameApiEntities entities = new GameApiEntities())
            {
                return entities.Statements.ToList();
            }
        }

        // GET api/statements/<StatementId>
        public Statement Get(int id)
        {
            using (GameApiEntities entities = new GameApiEntities())
            {
                return entities.Statements.FirstOrDefault(e => e.StatementId == id);
            }
        }

        // POST api/statements
        [Authorize]
        [HttpPost]
        public IHttpActionResult CreateStatement(Statement stm)
        {
            GameApiEntities entities = new GameApiEntities();

            entities.Statements.Add(stm);
            entities.SaveChanges();

            return Ok(stm.StatementId);
        }
    }
}