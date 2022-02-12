<template>
  <v-app id="inspire">
    <v-side-menu v-model="drawer"></v-side-menu>

    <v-app-bar app  dense hide-on-scroll >
<!--      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>-->
      <v-spacer></v-spacer>
      <v-menu
          :close-on-content-click="false"
          open-on-hover
          v-if="userCanViewMenu"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn
              small
              dense
              fab
              depressed
          >
            <v-icon v-bind="attrs" v-on="on">mdi-account-circle</v-icon>
          </v-btn>
        </template>
        <v-card>
          <v-list>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title>{{userName}}</v-list-item-title>
<!--                <v-list-item-subtitle>Founder of Vuetify</v-list-item-subtitle>-->
              </v-list-item-content>
            </v-list-item>
          </v-list>

          <v-divider></v-divider>

          <v-list
              nav
              dense
          >
            <v-list-item-group
                color="primary"
            >
<!--              <v-list-item>-->
<!--                <v-list-item-icon>-->
<!--                  <v-icon>mdi-cogs</v-icon>-->
<!--                </v-list-item-icon>-->
<!--                <v-list-item-content>-->
<!--                  <v-list-item-title>Settings</v-list-item-title>-->
<!--                </v-list-item-content>-->
<!--              </v-list-item>-->

<!--              <v-divider></v-divider>-->

              <v-list-item>
                <v-list-item-icon>
                  <v-icon color="red">mdi-power</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                  <v-list-item-title v-on:click="logoutEvent">Log out</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-card>
      </v-menu>
    </v-app-bar>
    <v-main>
      <router-view :key="$route.fullPath"></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import AuthService from "@/services/AuthService";

export default Vue.extend({
  name: "app",

  data: () => ({
    drawer: null,

  }),
  computed: {
    userCanViewMenu: function () {
      const app = (this.$root as any).$data || {isAuthenticated: false};
      if (app.isAuthenticated) {
        //already signed in, we can navigate anywhere
        return true;
      }
      return false;
    },
    userName: function(){
      const result = (this.$root as any).user.profile.name;
      return result;
    }
  },

  methods:{
     logoutEvent: async function() {
      await (this.$root as any).mgr.logout();
    }
  }
});
</script>


