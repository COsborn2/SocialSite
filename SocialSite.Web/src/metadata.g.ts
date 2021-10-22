import {
  Domain, getEnumMeta, solidify, ModelType, ObjectType,
  PrimitiveProperty, ForeignKeyProperty, PrimaryKeyProperty,
  ModelCollectionNavigationProperty, ModelReferenceNavigationProperty
} from 'coalesce-vue/lib/metadata'


const domain: Domain = { enums: {}, types: {}, services: {} }
export const Message = domain.types.Message = {
  name: "Message",
  displayName: "Message",
  get displayProp() { return this.props.messageId }, 
  type: "model",
  controllerRoute: "Message",
  get keyProp() { return this.props.messageId }, 
  behaviorFlags: 7,
  props: {
    messageId: {
      name: "messageId",
      displayName: "Message Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    originalId: {
      name: "originalId",
      displayName: "Original Id",
      type: "string",
      role: "value",
    },
    text: {
      name: "text",
      displayName: "Text",
      type: "string",
      role: "value",
    },
    screenName: {
      name: "screenName",
      displayName: "Screen Name",
      type: "string",
      role: "value",
    },
    utc: {
      name: "utc",
      displayName: "Utc",
      dateKind: "datetime",
      type: "date",
      role: "value",
    },
    createdAt: {
      name: "createdAt",
      displayName: "Created At",
      dateKind: "datetime",
      type: "date",
      role: "value",
    },
    favorites: {
      name: "favorites",
      displayName: "Favorites",
      type: "number",
      role: "value",
    },
    shares: {
      name: "shares",
      displayName: "Shares",
      type: "number",
      role: "value",
    },
  },
  methods: {
  },
  dataSources: {
    messageDefaultDataSource: {
      type: "dataSource",
      name: "MessageDefaultDataSource",
      displayName: "Message Default Data Source",
      isDefault: true,
      props: {
      },
    },
  },
}
export const User = domain.types.User = {
  name: "User",
  displayName: "User",
  get displayProp() { return this.props.userId }, 
  type: "model",
  controllerRoute: "User",
  get keyProp() { return this.props.userId }, 
  behaviorFlags: 7,
  props: {
    userId: {
      name: "userId",
      displayName: "User Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    screenName: {
      name: "screenName",
      displayName: "Screen Name",
      type: "string",
      role: "value",
    },
    profilePictureLink: {
      name: "profilePictureLink",
      displayName: "Profile Picture Link",
      type: "string",
      role: "value",
    },
  },
  methods: {
    getUsersOnPage: {
      name: "getUsersOnPage",
      displayName: "Get Users On Page",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
        screenNames: {
          name: "screenNames",
          displayName: "Screen Names",
          type: "collection",
          itemType: {
            name: "$collectionItem",
            displayName: "",
            role: "value",
            type: "string",
          },
          role: "value",
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "collection",
        itemType: {
          name: "$collectionItem",
          displayName: "",
          role: "value",
          type: "model",
          get typeDef() { return (domain.types.User as ModelType) },
        },
        role: "value",
      },
    },
  },
  dataSources: {
  },
}

interface AppDomain extends Domain {
  enums: {
  }
  types: {
    Message: typeof Message
    User: typeof User
  }
  services: {
  }
}

solidify(domain)

export default domain as AppDomain
