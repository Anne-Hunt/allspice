import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { api } from "./AxiosService.js"


class FavoriteService{

    async favorite(recipeId) {
        // const userId = AppState.account.id
        const recipeData = {recipeId: recipeId}
        const response = await api.post("api/favorites", recipeData)
        const favorite = new Favorite(response.data)
        AppState.favorites.push(favorite)
    }

    async trashFavorite(favoriteId) {
        await api.delete(`api/favorites/${favoriteId}`)
        const trash = AppState.favorites.findIndex(favorite => favorite.id == favoriteId)
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