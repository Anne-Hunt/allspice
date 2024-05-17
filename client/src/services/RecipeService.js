import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipeService {
    async getRecipes() {
        const response = await api.get('api/recipes')
        const recipes = response.data.map(recipe => new Recipe(recipe))
        AppState.recipes = recipes
        logger.log("recipes", recipes)
    }

    async createRecipe(recipeData){
        const user = AppState.account
        recipeData.creatorId = user.id
        const recipe = await api.post('api/recipes', recipeData)
        const recipeDone = new Recipe(recipe)
        AppState.recipes.push(recipeDone)
    }
}

export const recipeService = new RecipeService()