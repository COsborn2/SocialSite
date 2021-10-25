import * as $metadata from './metadata.g'
import * as $models from './models.g'
import * as $apiClients from './api-clients.g'
import { ViewModel, ListViewModel, ServiceViewModel, DeepPartial, defineProps } from 'coalesce-vue/lib/viewmodel'

export interface MessageViewModel extends $models.Message {
  messageId: number | null;
  originalId: string | null;
  text: string | null;
  screenName: string | null;
  utc: Date | null;
  createdAt: Date | null;
  favorites: number | null;
  shares: number | null;
}
export class MessageViewModel extends ViewModel<$models.Message, $apiClients.MessageApiClient, number> implements $models.Message  {
  
  constructor(initialData?: DeepPartial<$models.Message> | null) {
    super($metadata.Message, new $apiClients.MessageApiClient(), initialData)
  }
}
defineProps(MessageViewModel, $metadata.Message)

export class MessageListViewModel extends ListViewModel<$models.Message, $apiClients.MessageApiClient, MessageViewModel> {
  
  constructor() {
    super($metadata.Message, new $apiClients.MessageApiClient())
  }
}


export interface UserViewModel extends $models.User {
  userId: number | null;
  screenName: string | null;
  profilePictureLink: string | null;
}
export class UserViewModel extends ViewModel<$models.User, $apiClients.UserApiClient, number> implements $models.User  {
  
  constructor(initialData?: DeepPartial<$models.User> | null) {
    super($metadata.User, new $apiClients.UserApiClient(), initialData)
  }
}
defineProps(UserViewModel, $metadata.User)

export class UserListViewModel extends ListViewModel<$models.User, $apiClients.UserApiClient, UserViewModel> {
  
  public get getUsersOnPage() {
    const getUsersOnPage = this.$apiClient.$makeCaller(
      this.$metadata.methods.getUsersOnPage,
      (c, screenNames: string[] | null) => c.getUsersOnPage(screenNames),
      () => ({screenNames: null as string[] | null, }),
      (c, args) => c.getUsersOnPage(args.screenNames))
    
    Object.defineProperty(this, 'getUsersOnPage', {value: getUsersOnPage});
    return getUsersOnPage
  }
  
  constructor() {
    super($metadata.User, new $apiClients.UserApiClient())
  }
}


const viewModelTypeLookup = ViewModel.typeLookup = {
  Message: MessageViewModel,
  User: UserViewModel,
}
const listViewModelTypeLookup = ListViewModel.typeLookup = {
  Message: MessageListViewModel,
  User: UserListViewModel,
}
const serviceViewModelTypeLookup = ServiceViewModel.typeLookup = {
}

