﻿using EvaluationBackend.DATA.DTOs.ArticleDto;
using EvaluationBackend.DATA.DTOs;
using EvaluationBackend.Entities;
using EvaluationBackend.DATA.DTOs.Fine;
using EvaluationBackend.Repository;
using AutoMapper;
using EvaluationBackend.DATA;

namespace EvaluationBackend.Services
{
    public interface IFineService
    {

        Task<(Fine? fine, string? error)> add(FineForm fineForm);
        Task<(List<FineDto> fines, int? totalCount, string? error)> GetAll(FineFilter fineFilter);
        Task<(Fine? fine, string? error)> update(FineForm fineForm, int id);
        Task<(Fine? fine, string?)> Delete(int id);

    }
    public class FineService : IFineService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public FineService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<(Fine? fine, string? error)> add(FineForm fineForm)
        {
            var fine = _mapper.Map<Fine>(fineForm);

            var result = await _repositoryWrapper.fineRepositry.Add(fine);
     

            return result == null ? (null, "Fine could not add") : (fine, null);
        }


        public async Task<(Fine? fine, string?)> Delete(int id)
        {
           var fine = _repositoryWrapper.fineRepositry.GetById(id);
            if (fine == null) return (null, "fine not found");
            var result = await _repositoryWrapper.fineRepositry.Delete(id);
            return result == null ? (null, "result could not be deleted") : (result, null);
        }

        public async Task<(List<FineDto> fines, int? totalCount, string? error)> GetAll(FineFilter filter)
        {
            var (fines,  totalCount) = await _repositoryWrapper.fineRepositry.GetAll<FineDto>
                (
               
        f => (f.Number == filter.number || filter.number == null) &&
             (f.Status == filter.Status || filter.Status == null) &&
             (f.Vehicle.typeOfVechile.Name == filter.Name || filter.Name == null) &&
             (f.Vehicle.NumberOfVechile == filter.numbervehicle || filter.numbervehicle == null) ,
            
        

        filter.PageNumber, filter.PageSize
    );

            return (fines, totalCount, null);
        }

        public async Task<(Fine? fine, string? error)> update(FineForm fineForm, int id)
        {
            var fine = await _repositoryWrapper.fineRepositry.GetById(id);
            if (fine == null)
            {
                return (null, "fine not found");
            }
            fine = _mapper.Map(fineForm, fine);
            var response = await _repositoryWrapper.fineRepositry.Update(fine);
            return response == null ? (null, "fine could not be updated") : (fine, null);
        }
    }
}
