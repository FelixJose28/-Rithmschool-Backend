using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rithmschool.Core.DTOs;
using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rithmschool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BuyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet(nameof(GetBuys))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBuys()
        {
            var buys = _unitOfWork.buyRepository.GetAll();
            if (buys == null)
            {
                return NotFound("Not teachers");
            }
            var buysDto = _mapper.Map<IEnumerable<BuyDTO>>(buys);
            return Ok(buysDto);
        }


        [HttpGet(nameof(GetBuy) + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBuy(int id)
        {
            var buy = await _unitOfWork.buyRepository.GetById(id);
            if (buy == null)
            {
                return NotFound("Not course");
            }
            var buyDTO = _mapper.Map<BuyDTO>(buy);
            return Ok(buyDTO);
        }

        [HttpPost(nameof(PostBuy))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostBuy(BuyDTO buyDTO)
        {
            var buy = _mapper.Map<Buy>(buyDTO);
            await _unitOfWork.buyRepository.Add(buy);
            await _unitOfWork.CommitAsync();
            return Ok(buyDTO);
        }

        [HttpDelete(nameof(RemoveBuy) + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveBuy(int id)
        {
            var buy = await GetBuy(id);
            if (buy == null)
            {
                return NotFound("Id not found");
            }
            await _unitOfWork.buyRepository.Remove(id);
            await _unitOfWork.CommitAsync();
            return Ok("Deteled");
        }

        [HttpPut(nameof(UpdateBuy))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBuy(BuyDTO buyDTO)
        {
            var buy = _mapper.Map<Course>(buyDTO);
            await _unitOfWork.courseRepository.Update(buy);
            await _unitOfWork.CommitAsync();
            return Ok(buyDTO);
        }
    }
}
