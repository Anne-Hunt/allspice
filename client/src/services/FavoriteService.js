import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { api } from "./AxiosService.js"


class FavoriteService{

    async favorite(recipeId) {
        const userId = AppState.account.id
        const response = await api.post("api/favorites", recipeId, userId)
        const favorite = new Favorite(response.data)
        AppState.favorites.push(favorite)
    }

    async trashFavorite(favoriteId) {
        const response = await api.get(`api/favorites/${favoriteId}`)
        return response
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