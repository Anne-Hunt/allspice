import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { api } from "./AxiosService.js"
import { recipeService } from "./RecipeService.js"


class FavoriteService{

    async favorite(recipeId) {
        // const userId = AppState.account.id
        const response = await api.post("api/favorites", recipeId)
        const favorite = new Favorite(response.data)
        AppState.favorites.push(favorite)
    }

    async trashFavorite(recipeId) {
        // const recipe = await recipeService.getRecipeById(recipeId)
        const user = AppState.account.id
        const favorite = this.getFavoriteByRecipe(recipeId, user)
        const favoriteForm = new Favorite(favorite)
        const favoriteId = favoriteForm.id
        if(!favoriteId){
            throw new Error("Favorite not found!")
        }
        if(favoriteId.creatorId != user){
            throw new Error("You cannot remove favorites that are not yours.")
        }
        await api.delete(`api/favorites/${favoriteId}`)
        const trash = AppState.favorites.findIndex(favorite => favorite.id == favoriteId.id)
        AppState.favorites.splice(trash, 1)
    }

    async getFavoriteById(favoriteId){
        const response = await api.get(`api/favorites/${favoriteId}`)
        const favorite = new Favorite(response.data)
        AppState.activeFavorite = favorite
        return favorite
    }

    async getFavoriteByRecipe(recipeId, userId){
        const response = await api.get(`api/favorites/${recipeId}`, userId)
        if (response == null){
            return null
        }
        const favorite = new Favorite(response.data)
        return favorite
    }
}

export const favoriteService = new FavoriteService()