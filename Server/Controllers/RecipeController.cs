﻿using AiChef.Server.Data;
using AIChef.Server.Services;
using AIChef.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIChef.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        private readonly IOpenAIAPI _openAIService;

        public RecipeController(IOpenAIAPI openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost, Route("GetRecipeIdeas")]
        public async Task<ActionResult<List<Idea>>> GetRecipeIdeas(RecipeParms recipeParms)
        {
            string mealtime = recipeParms.MealTime;
            List<string> ingredients = recipeParms.Ingredients
                                                    .Where(x => !string.IsNullOrEmpty(x.Description))
                                                    .Select(x => x.Description!)
                                                    .ToList();
            if (string.IsNullOrEmpty(mealtime))
            {
                mealtime = "Breakfast";
            }

            var ideas = await _openAIService.CreateRecipeideas(mealtime, ingredients);

            return ideas;
        }


        [HttpPost, Route("GetRecipe")]
        public async Task<ActionResult<Recipe?>> GetRecipe(RecipeParms recipeParms)
        {
            return SampleData.Recipe;
        }

        [HttpGet, Route("GetRecipeImage")]
        public async Task<RecipeImage> GetRecipeImage(string title)
        {
            return SampleData.RecipeImage;
        }

    }
}
