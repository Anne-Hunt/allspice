import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { api } from "./AxiosService.js"


class FavoriteService{
    async favorite(recipeId) {
        const userId = AppState.account.id
        const favStatus = this.getFavoriteByRecipe(recipeId, userId)
        if (favStatus){
            const favoriteExist = new Favorite(favStatus)
            const favoriteId = favoriteExist.id
            this.trashFavorite(favoriteId)
        }
        const response = await api.post("api/favorites", recipeId, userId)
        const favorite = new Favorite(response.data)
        AppState.favorites.push(favorite)
    }

    async trashFavorite(favoriteId) {
        const response = await api.get(`api/favorites/${favoriteId}`)
        return response
    }

    async getFavorites() {
        const response = await api.get("api/favorites")
        const favorites = response.data.map(favoriteData => new Favorite(favoriteData))
        AppState.favorites = favorites        
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