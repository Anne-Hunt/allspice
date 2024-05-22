import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { Recipe } from "../models/Recipe.js"
import { RecipeFan } from "../models/RecipeFan.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipeService {
    resetRecipes() {
      AppState.activeRecipe = null
      AppState.newRecipe = null
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
        const userId = AppState.account.id
        if(recipeData.creatorId != userId){
            throw new Error("You cannot edit what is not yours!")
        }
        const recipeToUpdate = this.getRecipeById(recipeId)
        if(!recipeToUpdate){
            throw new Error("You cannot edit what doesn't exist!")
        }
        const updated = await api.put(`api/recipes/${recipeData.id}`)
        const revised = new Recipe(updated)
        const recipeUpdate = AppState.recipes.findIndex(revised.id)
        AppState.recipes.splice(recipeUpdate, 1)
        AppState.recipes.push(revised)
    }

    async trashRecipe(recipeId){
        const userId = AppState.account.id
        const recipe = this.getRecipeById(recipeId)
        if(!recipe){
            throw new Error ("Does not exist!")
        }
        const foundRecipe = new Recipe(recipe)
        if(foundRecipe.creatorId != userId){
            throw new Error("You can't delete what isn't yours!")
        }
        await api.delete(`api/recipes/${recipeId}`)
        const trash = AppState.recipes.findIndex(recipeId)
        AppState.recipes.splice(trash, 1)
    }
}

export const recipeService = new RecipeService()