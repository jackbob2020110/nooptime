﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nooptime.Domain.Models;
using Nooptime.Domain.Services;
using Nooptime.Web.Models;

namespace Nooptime.Web.Controllers
{
	[Produces("application/json")]
	[Route("api/UptimeCheck")]
	public class UptimeCheckController : Controller
	{
		private readonly IUptimeCheckService _uptimeCheckService;

		public UptimeCheckController(IUptimeCheckService uptimeCheckService)
		{
			_uptimeCheckService = uptimeCheckService;
		}

		[HttpPost]
		[Route("Post")]
		public Guid Post([FromBody] UptimeCheckDataModel model)
		{
			Guid id = _uptimeCheckService.Create(new UptimeCheckData()
			{
				Name = model.Name,
				Description = model.Description,
				Interval = model.Interval,
				Properties = model.Properties
			});

			return id;
		}

		[HttpPatch]
		[Route("Patch")]
		public void Patch([FromBody] UptimeCheckDataModel model)
		{
			_uptimeCheckService.Update(new UptimeCheckData()
			{
				Name = model.Name,
				Description = model.Description,
				Interval = model.Interval,
				Properties = model.Properties
			});
		}

		[HttpDelete]
		[Route("Delete")]
		public void Delete(Guid id)
		{
			_uptimeCheckService.Delete(id);
		}

		[HttpGet]
		[Route("Get")]
		public async Task<UptimeCheckData> Get(Guid id)
		{
			UptimeCheckData uptimeCheckData = await _uptimeCheckService.Load(id);
			return uptimeCheckData;
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IEnumerable<UptimeCheckData>> GetAll()
		{
			IEnumerable<UptimeCheckData> allData = await _uptimeCheckService.LoadAll();
			return allData;
		}
	}
}