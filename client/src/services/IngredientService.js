import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { Recipe } from "../models/Recipe.js"
import { api } from "./AxiosService.js"
import { recipeService } from "./RecipeService.js"

class IngredientsService{


    async createIngredients(ingredientData){
        const ingredients = ingredientData
        const supplies = await api.post("api/ingredients", ingredients)
        const pantry = new Ingredient(supplies.data)
        AppState.ingredients.push(pantry)
    }
    async updateIngredient(ingredientId, ingredientData){
        const ingredient = this.getIngredientById(ingredientId);
        AppState.activeIngredient = new Ingredient(ingredient)
        const recipeId = AppState.activeIngredient.recipeId
        const recipe = recipeService.getRecipeById(recipeId)
        AppState.activeRecipe = new Recipe(recipe)
        const creatorId = AppState.activeRecipe.creatorId
        const userId = AppState.account.id
        if(creatorId != userId){
            throw new Error("You don't own this ingredient!")
        }
        const updated = api.put(`api/ingredients${ingredientId}`, ingredientData)
        const updatedIngredient = new Ingredient(updated)
        const active = AppState.ingredients.findIndex(ingredientId)
        AppState.ingredients.splice(active, 1)
        AppState.ingredients.push(updatedIngredient)
    }

    async getIngredientById(ingredientId) {
        const ingredient = api.get(`api/ingredients/${ingredientId}`)
        const foundIngredient = new Ingredient(ingredient)
        AppState.activeIngredient = foundIngredient
    }

    async trashIngredient(ingredientId){
        await api.delete(`api/ingredients/${ingredientId}`)
        const ingredientToDelete = AppState.ingredients.findIndex(ingredientId)
        AppState.ingredients.splice(ingredientToDelete, 1)
    }
}

export const ingredientService = new IngredientsService()