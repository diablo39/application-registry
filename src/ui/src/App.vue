<template>
  <v-app id="inspire">
    <v-side-menu v-model="drawer"></v-side-menu>

    <v-app-bar app  dense hide-on-scroll >
<!--      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>-->
      <v-spacer></v-spacer>
      <v-menu
          :close-on-content-click="false"
          open-on-hover
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
                <v-list-item-title>John Leider</v-list-item-title>
                <v-list-item-subtitle>Founder of Vuetify</v-list-item-subtitle>
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
                  <v-list-item-title>Log out</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-card>
      </v-menu>
    </v-app-bar>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "app",

  data: () => ({
    drawer: null,
  }),
});
</script>


