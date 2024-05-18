/* eslint-disable no-unused-vars */
import { reactive } from 'vue'
import { Recipe } from './models/Recipe.js'
import { Ingredient } from './models/Ingredient.js'
import { Favorite } from './models/Favorite.js'
import { Profile } from './models/Profile.js'

export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /**@type {Recipe[]} */
  recipes: [],

  /**@type {Ingredient[]} */
  ingredients: [],

  /**@type {Favorite[]} */
  favorites: [],

  /**@type {Recipe} */
  activeRecipe: null,

/**@type {Profile} */
activeProfile: null,

/**@type {Ingredient} */
activeIngredient: null,

/**@type {Favorite} */
activeFavorite: null
})