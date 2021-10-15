/* eslint-disable @typescript-eslint/camelcase */
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import i18n from './plugins/i18n';
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import '@mdi/font/css/materialdesignicons.css'
import './components/_globals'
import axios from 'axios'
import Oidc from "oidc-client";

declare module 'vue/types/vue' {
  interface Vue {
    $formatDateTime: typeof Vue.prototype.$formatDateTime;
  }
}

Vue.config.productionTip = false;

Vue.prototype.$formatDateTime = function (d: string) {
  return new Date(d).toLocaleString();
}

const getRuntimeConfig = async () => {
  const config = await axios.create({}).get('/runtimeConfig.json')
  return config.data;
};

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;
const config = window['runtimeConfig'];

const mgr = new Oidc.UserManager({
  authority: config.authentication.oidc.authority,
  client_id: config.authentication.oidc.client_id,
  redirect_uri: config.authentication.oidc.redirect_uri,
  response_type: config.authentication.oidc.response_type,
  scope: config.authentication.oidc.scope,
  post_logout_redirect_uri: config.authentication.oidc.post_logout_redirect_uri,
  userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
});

const globalData  = {
  isAuthenticated: false,
  user: '',
  mgr: mgr
} as any;

const globalMethods = {
  async authenticate(returnPath) {
    const user = await ((this as any).$root.getUser()); //see if the user details are in local storage
    if (user) {
      (this as any).isAuthenticated = true;
      (this as any).user = user;
    } else {
      await ((this as any).$root.signIn(returnPath));
    }
  },
  async getUser () {
    try {
      const user = await ((this as any).mgr.getUser());
      return user;
    } catch (err) {
      console.log(err);
    }
  },
  signIn (returnPath) {
    returnPath ? (this as any).mgr.signinRedirect({ state: returnPath })
        : (this as any).mgr.signinRedirect();
  }
}

new Vue({
  vuetify,
  i18n,
  data: globalData,
  methods: globalMethods,
  render: h => h(App),
  router,
}).$mount('#app');



