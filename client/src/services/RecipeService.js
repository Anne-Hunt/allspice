import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { Recipe } from "../models/Recipe.js"
import { RecipeFan } from "../models/RecipeFan.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipeService {
    prepareEdit() {
      AppState.newRecipe = null
      AppState.newRecipe = AppState.activeRecipe
      AppState.activeRecipe = null
    }
    resetRecipes() {
      AppState.activeRecipe = null
      AppState.newRecipe = null
      AppState.ingredients = []
    }
    async setActiveRecipe(recipeId) {
        AppState.activeRecipe = null
        const active = AppState.recipes.find(recipe => recipeId == recipe.id)
        AppState.activeRecipe = active
        await this.getIngredientsByRecipe(recipeId)
    }
    async getRecipesWithFavorites() {
        if (AppState.account == null){
            this.getRecipes()
        }
        const accountId = AppState.account.id
      const response = await api.get(`api/recipes/${accountId}/favorites`)
      const recipes = response.data.map(recipe => new RecipeFan(recipe))
      AppState.recipeFans = recipes
      logger.log(recipes)
    //   AppState.favorites = AppState.recipes.filter(recipe.favorite.CreatorId == accountId)
    }
    async getIngredientsByRecipe(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}/ingredients`)
        const ingredients = response.data.map(ingredientData => new Ingredient(ingredientData))
        logger.log(ingredients, response)
        AppState.ingredients = ingredients
    }
    async getRecipeById(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}`)
        const recipe = new Recipe(response.data)
        AppState.activeRecipe = recipe
    }
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
        const recipeDone = new Recipe(recipe.data)
        AppState.recipes.push(recipeDone)
        AppState.newRecipe = recipeDone
        logger.log(AppState.newRecipe)
    }

    async updateRecipe(recipeData, recipeId){
        const recipeToUpdate = AppState.recipes.find(recipe => recipe.id == recipeId)
        logger.log(recipeToUpdate)
        recipeData.title = recipeToUpdate.title ?? recipeData.title
        recipeData.instructions = recipeToUpdate.instructions ?? recipeData.instructions
        recipeData.category = recipeToUpdate.category ?? recipeData.category
        recipeData.img = recipeToUpdate.img ?? recipeData.img
        const updated = await api.put(`api/recipes/${recipeId}`, recipeData)
        logger.log(updated)
        this.getRecipes()
    }

    async trashRecipe(recipeId){
        await api.delete(`api/recipes/${recipeId}`)
        const trash = AppState.recipes.findIndex(recipe => recipe.id == recipeId)
        AppState.recipes.splice(trash, 1)
    }

    async searchRecipes(searchingQuery) {
        AppState.recipes = []
        const response = await api.get(`api/recipes/search/query=${searchingQuery}`)
        const recipes = response.data.map(recipe => new Recipe(recipe))
        AppState.recipes = recipes
        logger.log(recipes)
      }
}

export const recipeService = new RecipeService()