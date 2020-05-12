const apiConfig = {
    scopes: ["api://thothapi/user_impersonation"] ,
    webApi: "https://api.tipmeup.org/manage/quoteitem"
  };

const msalConfig = {
    auth: {
        clientId: "84b484d7-446f-471e-a300-96e4ec7dc9fe",
        authority: "https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad/v2.0",
        validateAuthority: false
    },
    cache: {
        cacheLocation: "localStorage",
        storeAuthStateInCookie: true
      }
}

const loginRequest = {
    scopes: ["openid", "profile"],
  };

const tokenRequest = {
    scopes: apiConfig.scopes
  };