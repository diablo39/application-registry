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
import {HttpClient} from "@/services/httpClient/HttpClient";
import AuthService from "@/services/AuthService";

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
  const config = await axios.create({}).get('/api/configuration/frontendconfiguration')
  return config.data;
};

getRuntimeConfig().then(function (config) {

  const mgr = new AuthService(config);

  const globalData = {
    isAuthenticated: false,
    user: null,
    mgr: mgr
  } as any;

  const globalMethods = {
    async authenticate(returnPath) {
      const user = await ((this as any).$root.mgr.getUser()); //see if the user details are in local storage
      if (user) {
        (this as any).isAuthenticated = true;
        (this as any).user = user;
      } else {
        await ((this as any).$root.mgr.login(returnPath));
      }
    },
  }

  HttpClient.HTTP.interceptors.request.use (
      async (config) => {

        const token = await mgr.getAccessToken(); // slightly longer running function than example above
        if (token) config.headers.Authorization = `Bearer ${token}`;
        return config;
      },
      (error) => {
        return Promise.reject (error);
      }
  );

  new Vue({
    vuetify,
    i18n,
    data: globalData,
    methods: globalMethods,
    render: h => h(App),
    router,
  }).$mount('#app')

});

