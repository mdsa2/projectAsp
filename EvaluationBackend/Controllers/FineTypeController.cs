﻿using EvaluationBackend.DATA.DTOs.Fine;
using EvaluationBackend.DATA;
using EvaluationBackend.Services;
using EvaluationBackend.Utils;
using Microsoft.AspNetCore.Mvc;
using EvaluationBackend.DATA.DTOs.FineType;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace EvaluationBackend.Controllers
{

     
     [Authorize]
      
    public class TypeFineController : BaseController
    {
        private readonly IFineTypeService _fineService;
        public TypeFineController(IFineTypeService fineService)
        {
            _fineService = fineService;
        }
        [HttpGet]
        public async Task<ActionResult<Respons<FineTypeDto>>> GetAll([FromQuery] FineTypeFilter filter) => Ok(await _fineService.GetAll(filter));
        [HttpPost]
        public async Task<ActionResult<FineTypeForm>> Add(FineTypeForm fineForm) => Ok(await _fineService.add(fineForm));
        [HttpPut("{id}")]
        public async Task<ActionResult<Respons<FineTypeUpdate>>> Update(int id, FineTypeUpdate fineForm) => Ok(await _fineService.update(fineForm, id));
        [HttpDelete("{id}")]
        public async Task<ActionResult<FineDto>> Delete(int id) => Ok(await _fineService.Delete(id));
    }
}
