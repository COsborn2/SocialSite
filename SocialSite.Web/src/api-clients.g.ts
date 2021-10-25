import * as $metadata from './metadata.g'
import * as $models from './models.g'
import * as qs from 'qs'
import { AxiosClient, ModelApiClient, ServiceApiClient, ItemResult, ListResult } from 'coalesce-vue/lib/api-client'
import { AxiosPromise, AxiosResponse, AxiosRequestConfig } from 'axios'

export class MessageApiClient extends ModelApiClient<$models.Message> {
  constructor() { super($metadata.Message) }
}


export class UserApiClient extends ModelApiClient<$models.User> {
  constructor() { super($metadata.User) }
  public getUsersOnPage(screenNames: string[] | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<$models.User[]>> {
    const $method = this.$metadata.methods.getUsersOnPage
    const $params =  {
      screenNames,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


