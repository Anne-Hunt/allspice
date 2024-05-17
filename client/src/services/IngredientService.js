import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { api } from "./AxiosService.js"

class IngredientsService{


    async createIngredients(recipeData){
        const ingredients = recipeData.ingredients
        ingredients.recipeId = recipeData.id
        const supplies = await api.post("api/ingredients")
        supplies.data.map(supplyData => new Ingredient(supplyData))
    }
}

export const ingredientService = new IngredientsService()