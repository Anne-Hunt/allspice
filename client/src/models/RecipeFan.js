
export class RecipeFan{
    constructor(data){
        this.id = data.id
        this.creatorId = data.creatorId
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
        this.title = data.title
        this.instructions = data.instructions
        this.img = data.img
        this.category = data.category
        this.creator = data.creator
        this.favorite = data.favorite
        this.fan = data.fan
    }
}