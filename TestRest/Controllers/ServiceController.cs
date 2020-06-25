using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NHibernate;
using Server.Entity;
using Server.Models;

namespace Server.Controllers
{
    public class ServiceController : ControllerBase
    {
		private readonly ISession _session;

		public ServiceController(ISession session)
		{
			_session = session;
		}
		public ActionResult Index()
		{
			return Ok();
		}
		public ActionResult MySplit(string text = "")
		{
			return Ok(text.Split(new char[] { ' ' }));
		}

		public ActionResult Get()
		{
			var model = _session.Query<Person>().Select(x => new PersonModel(x)).ToList(); 

			return Ok(model);
		}

		[HttpPost]
		public ActionResult Create([FromBody]object data)
		{
			var model = JsonConvert.DeserializeObject<PersonCreateModel>(data.ToString());

			if (TryValidateModel(model))
			{
				var errors = model.Validate(_session);
				if (errors.Any())
					return BadRequest(errors);

				model.Save(_session);

				return Ok("Данные успешно сохранились !{сохранено}");
			}

			return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.Exception)));
		}
	}
}
