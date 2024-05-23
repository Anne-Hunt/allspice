import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"


class SearchService{
	searchTerms(searchingQuery) {
		AppState.searchTerms = searchingQuery
        logger.log(AppState.searchTerms)
	}
}

export const searchService = new SearchService()