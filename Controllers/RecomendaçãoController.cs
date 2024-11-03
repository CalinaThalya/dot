using Microsoft.AspNetCore.Mvc;
using PlusoftRecommender.Models;
using PlusoftRecommender.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlusoftRecommender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationsController(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        // GET: api/recommendations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recommendation>>> GetRecommendations()
        {
            var recommendations = await _recommendationRepository.GetAllRecommendationsAsync();
            return Ok(recommendations);
        }

        // GET: api/recommendations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recommendation>> GetRecommendation(int id)
        {
            var recommendation = await _recommendationRepository.GetRecommendationByIdAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }
            return Ok(recommendation);
        }

        // POST: api/recommendations
        [HttpPost]
        public async Task<ActionResult<Recommendation>> CreateRecommendation(Recommendation recommendation)
        {
            if (recommendation == null)
            {
                return BadRequest();
            }

            await _recommendationRepository.AddRecommendationAsync(recommendation);
            return CreatedAtAction(nameof(GetRecommendation), new { id = recommendation.Id }, recommendation);
        }

        // PUT: api/recommendations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecommendation(int id, Recommendation recommendation)
        {
            if (id != recommendation.Id)
            {
                return BadRequest();
            }

            var existingRecommendation = await _recommendationRepository.GetRecommendationByIdAsync(id);
            if (existingRecommendation == null)
            {
                return NotFound();
            }

            await _recommendationRepository.UpdateRecommendationAsync(recommendation);
            return NoContent();
        }

        // DELETE: api/recommendations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecommendation(int id)
        {
            var recommendation = await _recommendationRepository.GetRecommendationByIdAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }

            await _recommendationRepository.DeleteRecommendationAsync(id);
            return NoContent();
        }
    }
}
