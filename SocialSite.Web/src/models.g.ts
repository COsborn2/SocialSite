import * as metadata from './metadata.g'
import { Model, DataSource, convertToModel, mapToModel } from 'coalesce-vue/lib/model'

export interface Message extends Model<typeof metadata.Message> {
  messageId: number | null
  originalId: string | null
  text: string | null
  screenName: string | null
  utc: Date | null
  createdAt: Date | null
  favorites: number | null
  shares: number | null
}
export class Message {
  
  /** Mutates the input object and its descendents into a valid Message implementation. */
  static convert(data?: Partial<Message>): Message {
    return convertToModel(data || {}, metadata.Message) 
  }
  
  /** Maps the input object and its descendents to a new, valid Message implementation. */
  static map(data?: Partial<Message>): Message {
    return mapToModel(data || {}, metadata.Message) 
  }
  
  /** Instantiate a new Message, optionally basing it on the given data. */
  constructor(data?: Partial<Message> | {[k: string]: any}) {
      Object.assign(this, Message.map(data || {}));
  }
}
export namespace Message {
  export namespace DataSources {
    
    export class MessageDefaultDataSource implements DataSource<typeof metadata.Message.dataSources.messageDefaultDataSource> {
      readonly $metadata = metadata.Message.dataSources.messageDefaultDataSource
    }
  }
}


export interface User extends Model<typeof metadata.User> {
  userId: number | null
  screenName: string | null
  profilePictureLink: string | null
}
export class User {
  
  /** Mutates the input object and its descendents into a valid User implementation. */
  static convert(data?: Partial<User>): User {
    return convertToModel(data || {}, metadata.User) 
  }
  
  /** Maps the input object and its descendents to a new, valid User implementation. */
  static map(data?: Partial<User>): User {
    return mapToModel(data || {}, metadata.User) 
  }
  
  /** Instantiate a new User, optionally basing it on the given data. */
  constructor(data?: Partial<User> | {[k: string]: any}) {
      Object.assign(this, User.map(data || {}));
  }
}


