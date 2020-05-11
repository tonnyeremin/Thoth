const apiConfig = {
    scopes: ["api://thothapi/user_impersonation"] ,
    webApi: "https://api.tipmeup.org/manage/quoteitem"
  };

const msalConfig = {
    auth: {
        clientId: "84b484d7-446f-471e-a300-96e4ec7dc9fe",
        authority: "https://login.microsoftonline.com/edd0fa85-fa74-4725-89be-78f496ea195b/",
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