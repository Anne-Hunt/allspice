import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Favorite } from '../models/Favorite.js'
import { Recipe } from '../models/Recipe.js'
import { RecipeFan } from '../models/RecipeFan.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
// import { recipeService } from './RecipeService.js'

class AccountService {
  
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
      this.getFavorites()
      // recipeService.getRecipesWithFavorites()
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  
  async updateAccount(accountData) {
    const response = await api.put('/account', accountData)
		logger.log('🧑‍🎨👍', response.data);
		AppState.account = new Account(response.data)
	}
  
  async getFavorites() {
    const response = await api.get("/account/favorites")
    const favorites = response.data.map(favoriteData => new RecipeFan(favoriteData))
    AppState.recipeFans = favorites
    logger.log("got favorites", favorites)
  }
  async getRecipes() {
    const response = await api.get('/account/recipes')
    const recipes = response.data.map(recipe => new Recipe(recipe))
    AppState.userRecipes = recipes
  }
}

export const accountService = new AccountService()
